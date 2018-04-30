// Retrieves both current and session metrics or datasets.

using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

public class DataHandler : MonoBehaviour
{

    // Adapted Gamma's Singleton (Gamma, 1995)
    private static DataHandler local = null;
    public static DataHandler Instance
    {
        get
        {
            if (local == null)
            {
                local = (DataHandler)FindObjectOfType(typeof(DataHandler));
            }

            return local;
        }
    }
    void Awake()
    {
        local = this;
    }

    // Static because don't want path changed
    public static string path;

    VZController controller;
    TextMesh FHIRHUD;
    double distance, speed, speedTotal, resistance, resistanceTotal, heartrate,
        heartrateTotal, rotation, rotationTotal, lean, leanTotal, incline, inclineTotal;

    // Initialization
    void Start()
    {
        controller = VZPlayer.Controller;
        path = Application.persistentDataPath + "/";
        FHIRHUD = GetComponentInChildren<TextMesh>();
        FHIRHUD.text = "";
    }

    // Called once every frame - updates metrics
    void Update()
    {
        // Distance is already cumulative
        distance = controller.Distance;
        speed = controller.InputSpeed;
        speedTotal += speed;
        resistance = controller.UncalibratedResistance();
        resistanceTotal += resistance;
        heartrate = controller.HeartRate();
        heartrateTotal += heartrate;
        rotation = controller.HeadRot;
        rotationTotal += rotation;
        lean = controller.HeadLean;
        leanTotal += lean;
        incline = controller.HeadBend;
        inclineTotal += incline;
    }

    // Get all data, current/session specified by parameter
    public double[] GetAllData(string type)
    {
        // We do the check here instead of re-using that of GetMetric to avoid 7 error messages
        if (type == "current" || type == "session")
        {
            double[] metrics = { GetMetric("distance", type), GetMetric("speed", type),
                GetMetric("resistance", type), GetMetric("heartrate", type),
                GetMetric("rotation", type), GetMetric("lean", type), GetMetric("incline", type) };

            return metrics;
        }
        else
        {
            Debug.LogError("That type does not exist. Types are 'current' and 'session.'");

            // Default to 0 for all metrics
            return new double[7];
        }
    }

    // Get a metric, current/session specified by parameter
    public double GetMetric(string metric, string type)
    {
        if (type == "current")
        {
            switch (metric)
            {
                case "distance":
                    return distance;

                case "speed":
                    return speed;

                case "resistance":
                    return resistance;

                case "heartrate":
                    return heartrate;

                case "rotation":
                    return rotation;

                case "lean":
                    return lean;

                case "incline":
                    return incline;

                default:
                    Debug.LogError("That metric does not exist. Metrics are 'distance,' 'speed,' " +
                        "'resistance,' 'heartrate,' 'rotation,' 'lean' and 'incline.'");
                    return 0;
            }
        }
        if (type == "session")
        {
            double frames = Time.frameCount;

            switch (metric)
            {
                case "distance":
                    return distance;

                case "speed":
                    return speedTotal / frames;

                case "resistance":
                    return resistanceTotal / frames;

                case "heartrate":
                    return heartrateTotal / frames;

                case "rotation":
                    return rotationTotal / frames;

                case "lean":
                    return leanTotal / frames;

                case "incline":
                    return inclineTotal / frames;

                default:
                    Debug.LogError("That metric does not exist. Metrics are 'distance,' 'speed,' " +
                        "'resistance,' 'heartrate,' 'rotation,' 'lean' and 'incline.'");
                    return 0;
            }
        }
        else
        {
            Debug.LogError("That type does not exist. Types are 'current' and 'session.'");
            return 0;
        }
    }


    // Optional parameter for hiding displayed data after a certain period of time
    public void DisplayAllData(string type, double duration = 0, string additionalText = "")
    {
        FHIRHUD.text = type.ToUpper() + " READINGS\n" + WriteMetric("distance", type) + 
            WriteMetric("speed", type) + WriteMetric("resistance", type) +
            WriteMetric("heartrate", type) + WriteMetric("rotation", type) +
            WriteMetric("lean", type) + WriteMetric("incline", type) + additionalText;

        if (duration != 0)
        {
            // Use a coroutine so we can yield in the waiting period
            StartCoroutine(HideAfterDuration(duration));
        }
    }

    // Optional parameter for hiding displayed data after a certain period of time
    // and for adding additional text to the HUD element
    public void DisplayMetric(string metric, string type, double duration = 0,
        string additionalText = "")
    {
        FHIRHUD.text = type.ToUpper() + " READING\n" + WriteMetric(metric, type) + additionalText;

        if (duration != 0)
        {
            StartCoroutine(HideAfterDuration(duration));
        }
    }

    public void Hide()
    {
        FHIRHUD.text = "";
    }

    public void StartSession()
    {
        // Creates device profile.
        // If device profile already exists, changes FHIR state to active and updates dateTime.
        GenFHIR.Device();
        Debug.Log("FHIRWEAVR session started.");
    }

    public void EndSession()
    {
        // Changes FHIR active state to inactive and updates FHIR dateTime.
        GenFHIR.Device(0);
        Debug.Log("FHIRWEAVR session ended.");
    }

    string WriteMetric(string metric, string type)
    {
        // Type handling done in GetMetric
        double metricData = GetMetric(metric, type);

        string unit = "";

        switch (metric)
        {
            case "distance":
                unit = " km";
                break;

            case "speed":
                unit = " m/s";
                break;

            case "heartrate":
                unit = " bpm";
                break;

            case "rotation":
                unit = " rad";
                break;

            case "lean":
            case "incline":
                unit = " m";
                break;

            // Keep unit == "" (default) if metric is resistance or nonexistent metric entered
        }

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(metric) + ": " +
            String.Format("{0:0.0}", metricData) + unit + "\n";
    }

    IEnumerator HideAfterDuration(double duration)
    {
        yield return new WaitForSeconds((float)duration);
        FHIRHUD.text = "";
    }

}

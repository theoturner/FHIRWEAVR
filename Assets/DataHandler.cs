// Scripts for getting both current and session metrics and datasets.
// N.B. while it may seem sensible to keep current and session arrays class-wide, GenFHIR cannot create documents in time.

using System.Collections;
using System.Globalization;
using UnityEngine;

// LATER: go directly to bike without SDK using mBikeState

public class DataHandler : MonoBehaviour
{

    // Create singleton
    private static DataHandler local = null;
    public static DataHandler Instance
    {
        get
        {
            if (local == null)
                local = (DataHandler)FindObjectOfType(typeof(DataHandler));
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
    string spatialUIText;
    double distance, speed, speedTotal, resistance, resistanceTotal, heartrate, heartrateTotal, rotation, rotationTotal, lean, leanTotal, incline, inclineTotal;

    // Initialization
    void Start()
    {
        controller = VZPlayer.Controller;
        path = Application.persistentDataPath + "/";
        FHIRHUD = GetComponentInChildren<TextMesh>();
        FHIRHUD.text = "";
    }

    // Called once every frame
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
        double frames = Time.frameCount;
        double[] current = { distance, speed, resistance, heartrate, rotation, lean, incline };
        double[] session = { distance, speedTotal / frames, resistanceTotal / frames, heartrateTotal / frames, rotationTotal / frames, leanTotal / frames, inclineTotal / frames };
        if (type == "current")
        {
            return current;
        }
        else if (type == "session")
        {
            return session;
        }
        else
        {
            Debug.Log("That type does not exist. Types are 'current' and 'session.'");
            return new double[7]; // Defaults to 0 for all metrics
        }
    }

    // Get a metric, current/session specified by parameter
    public double GetMetric(string metric, string type)
    {
        if (type == "current")
        {
            if (metric == "distance")
            {
                return distance;
            }
            else if (metric == "speed")
            {
                return speed;
            }
            else if (metric == "resistance")
            {
                return resistance;
            }
            else if (metric == "heartrate")
            {
                return heartrate;
            }
            else if (metric == "rotation")
            {
                return rotation;
            }
            else if (metric == "lean")
            {
                return lean;
            }
            else if (metric == "incline")
            {
                return incline;
            }
            else
            {
                Debug.Log("That metric does not exist. Metrics are 'distance,' 'speed,' 'resistance,' 'heartrate,' 'rotation,' 'lean' and 'incline.'");
                return 0;
            }
        }
        if (type == "session")
        {
            double frames = Time.frameCount;
            if (metric == "distance")
            {
                return distance;
            }
            else if (metric == "speed")
            {
                return speedTotal / frames;
            }
            else if (metric == "resistance")
            {
                return resistanceTotal / frames;
            }
            else if (metric == "heartrate")
            {
                return heartrateTotal / frames;
            }
            else if (metric == "rotation")
            {
                return rotationTotal / frames;
            }
            else if (metric == "lean")
            {
                return leanTotal / frames;
            }
            else if (metric == "incline")
            {
                return inclineTotal / frames;
            }
            else
            {
                Debug.Log("That metric does not exist. Metrics are 'distance,' 'speed,' 'resistance,' 'heartrate,' 'rotation,' 'lean' and 'incline.'");
                return 0;
            }
        }
        else
        {
            Debug.Log("That type does not exist. Types are 'current' and 'session.'");
            return 0;
        }
    }



    public void DisplayAllData(string type, double duration)
    {
        string[] descriptors = { "Distance: ", "Speed: ", "Resistance: ", "Heartrate: ", "Rotation: ", "Lean: ", "Incline: " };
        // Type handling done in GetAllData
        double[] allData = GetAllData(type);
        int dataCount;
        spatialUIText = type.ToUpper() + " READINGS\n";

        //var controller = VZPlayer.Controller;

        for (dataCount = 0; dataCount < 7; dataCount++)
        {
            spatialUIText = spatialUIText + descriptors[dataCount] + allData[dataCount] + "\n";
        }
        FHIRHUD.text = spatialUIText;
        StartCoroutine(HideAfterDuration(duration));
        /*
        "LeftGrip: " + GripText(controller.LeftButton.Down, controller.DpadUp.Down, controller.DpadDown.Down, controller.DpadLeft.Down, controller.DpadRight.Down) + "\n" +
        "RightGrip: " + GripText(controller.RightButton.Down, controller.RightUp.Down, controller.RightDown.Down, controller.RightLeft.Down, controller.RightRight.Down);
        */
    }

    public void DisplayMetric(string metric, string type, double duration)
    {
        // Type handling done in GetMetric
        double metricData = GetMetric(metric, type);
        spatialUIText = type.ToUpper() + " READING\n";

        //var controller = VZPlayer.Controller;

        FHIRHUD.text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(metric) + ": " + metricData;
        StartCoroutine(HideAfterDuration(duration));

        /*
        "LeftGrip: " + GripText(controller.LeftButton.Down, controller.DpadUp.Down, controller.DpadDown.Down, controller.DpadLeft.Down, controller.DpadRight.Down) + "\n" +
        "RightGrip: " + GripText(controller.RightButton.Down, controller.RightUp.Down, controller.RightDown.Down, controller.RightLeft.Down, controller.RightRight.Down);
        */
    }

    public void Hide()
    {
        FHIRHUD.text = "";
    }

    IEnumerator HideAfterDuration(double duration)
    {
        yield return new WaitForSeconds((float)duration);
        FHIRHUD.text = "";
    }


    /*
    string TypeText(int type, int version)
    {
        if (type < 0)
            return "none";
        else if (type == 0)
            return "unsupported bike";
        else if (type == 1)
            return "alpha bike";
        else if (type == 2)
        {
            if (version == 2)
                return "bike sensor";
            else
                return "beta bike";
        }
        else
            return "unknown";
    }

    string GripText(bool trigger, bool up, bool down, bool left, bool right)
    {
        string text = "";

        if (trigger)
            text += "trigger ";
        if (up)
            text += "up ";
        if (down)
            text += "down ";
        if (left)
            text += "left ";
        if (right)
            text += "right ";

        return text;
    }
    */




}
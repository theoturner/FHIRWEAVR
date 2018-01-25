// Scripts for getting both current and session metrics and datasets.

using UnityEngine;

// LATER: go directly to bike without SDK using mBikeState

public class GetData : MonoBehaviour
{
    private VZController controller;
    private static double distance, speed, resistance, heartrate, rotation, lean, incline;
    private static double[] current, session;

    // Initialization
    void Start()
    {
        controller = VZPlayer.Controller;
        current = new double[7];
        session = new double[7];
    }

    // Called once every frame
    void Update()
    {
        int i;
        distance = controller.Distance;
        current[0] = distance;
        speed = controller.InputSpeed;
        current[1] = speed;
        resistance = controller.UncalibratedResistance();
        current[2] = resistance;
        heartrate = controller.HeartRate();
        current[3] = heartrate;
        rotation = controller.HeadRot;
        current[4] = rotation;
        lean = controller.HeadLean; // ENSURE THESE TWO ARE THE RIGHT WAY ROUND
        current[5] = rotation;
        incline = controller.HeadBend; // ENSURE THESE TWO ARE THE RIGHT WAY ROUND
        current[6] = incline;
        session[0] = distance; // Distance is already cumulative
        for (i = 1; i < 7; i++) // Consequently, start at index 1
        {
            session[i] += current[i];
        }
    }

    // Get all data, current/session specified by parameter - after Update()
    public static double[] GetAllData(string type)
    {
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

    // Get a metric, current/session specified by parameter - after Update()
    // Test on resistance and heart rate - they may not be doubles ===============================================
    public static double GetMetric(string metric, string type)
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
            double i = Time.frameCount;
            if (metric == "distance")
            {
                return distance;
            }
            else if (metric == "speed")
            {
                return session[1] / i;
            }
            else if (metric == "resistance")
            {
                return session[2] / i;
            }
            else if (metric == "heartrate")
            {
                return session[3] / i;
            }
            else if (metric == "rotation")
            {
                return session[4] / i;
            }
            else if (metric == "lean")
            {
                return session[5] / i;
            }
            else if (metric == "incline")
            {
                return session[6] / i;
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
}
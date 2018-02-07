// TODO ERROR HANDLING IN THIS FILE IN CASE OF FAILURE TO GET DATA

using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using System;
using System.IO;

public class DisplayData : MonoBehaviour
{
    TextMesh FHIRHUD;
    private string[] descriptors = { "Distance: ", "Speed: ", "Resistance: ", "Heartrate: ", "Rotation: ", "Lean: ", "Incline: " };
    private double[] allData = new double[7];
    private double metricData;
    private string spatialUIText;
    private int dataCount;

    void Start()
    {
        FHIRHUD = GetComponent<TextMesh>();
        FHIRHUD.text = "";
    }

    void All(string type, double duration)
    {
        // Type handling done in GetData.cs
        allData = GetData.GetAllData(type);
        spatialUIText = type.ToUpper() + " READINGS\n";

        //var controller = VZPlayer.Controller;

        for (dataCount = 0; dataCount < 7; dataCount++)
        {
            spatialUIText = spatialUIText + descriptors[dataCount] + allData[dataCount] + "\n";
        }
        FHIRHUD.text = spatialUIText;
        if (duration != 0 & Time.time > duration)
        {
            FHIRHUD.text = "";
        }
            /*
            "LeftGrip: " + GripText(controller.LeftButton.Down, controller.DpadUp.Down, controller.DpadDown.Down, controller.DpadLeft.Down, controller.DpadRight.Down) + "\n" +
            "RightGrip: " + GripText(controller.RightButton.Down, controller.RightUp.Down, controller.RightDown.Down, controller.RightLeft.Down, controller.RightRight.Down);
            */
    }

    public void Metric(string type, string metric, double duration)
    {
        // Type handling done in GetData.cs
        metricData = GetData.GetMetric(type, metric);
        spatialUIText = type.ToUpper() + " READING\n";

        //var controller = VZPlayer.Controller;

        FHIRHUD.text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(metric) + ": " + metricData;
        if (duration != 0 & Time.time > duration)
        {
            FHIRHUD.text = "";
        }
        /*
        "LeftGrip: " + GripText(controller.LeftButton.Down, controller.DpadUp.Down, controller.DpadDown.Down, controller.DpadLeft.Down, controller.DpadRight.Down) + "\n" +
        "RightGrip: " + GripText(controller.RightButton.Down, controller.RightUp.Down, controller.RightDown.Down, controller.RightLeft.Down, controller.RightRight.Down);
        */
    }

    public void Hide()
    {
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

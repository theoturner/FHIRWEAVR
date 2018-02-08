using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO WHY DOES GETMETRIC NOT MATCH GENFHIR RESULT?
// TODO RENAME THIS FILE TO Example.cs

public class TestFunctionality : MonoBehaviour {

    // ||||| User must add this initialisation |||||
    DataHandler myDataInstance;

    // Use this for initialization
    void Start ()
    {
        // ||||| User must add this in start |||||
        myDataInstance = DataHandler.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(myDataInstance.GetMetric("rotation", "current"));
        if (Time.frameCount == 300)
        {
            myDataInstance.DisplayMetric("resistance", "current", 5);
            GenFHIR.Document("session");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunctionality : MonoBehaviour {

    // ||||| User must add this initialisation |||||
    GetData myDataInstance;

    // Use this for initialization
    void Start ()
    {
        // ||||| User must add this in start |||||
        myDataInstance = GetData.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(myDataInstance.GetMetric("rotation", "current"));
        Debug.Log(GetData.path);
        if (Time.frameCount == 300)
        {
            GenFHIR.Document("session");
        }
    }
}

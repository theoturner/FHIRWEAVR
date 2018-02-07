using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunctionality : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //DisplayData.All();
        Debug.Log(GetData.GetMetric("rotation", "current"));
        if (Time.frameCount == 300)
        {
            GenFHIR.Document("session");
        }
    }
}

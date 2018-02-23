using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Vector3 location;
    DataHandler myDataInstance;

    void Start ()
    {

        myDataInstance = DataHandler.Instance;
        location = transform.position;

    }
	
	void Update ()
    {

        location.x = -100 * (float)myDataInstance.GetMetric("lean", "current");
        location.y = 100 * (float)myDataInstance.GetMetric("incline", "current") + 15;
        transform.position = location;
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Top" || col.gameObject.name == "Bottom")
        {
            Debug.Log("Collision!");
        }
    }

}

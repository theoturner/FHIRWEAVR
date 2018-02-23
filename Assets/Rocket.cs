using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Vector3 location;
    DataHandler myDataInstance;
    // Use this for initialization
    void Start () {
        myDataInstance = DataHandler.Instance;
        location = transform.position;
        //transform.position = new Vector3(0, 2, 4);
    }
	
	// Update is called once per frame
	void Update () {
        location.x = -10 * (float)myDataInstance.GetMetric("lean", "current");
        location.y = 10 * (float)myDataInstance.GetMetric("incline", "current") + (float)1.5;
        transform.position = location;
        /*
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.name == "Top" || col.gameObject.name == "Left" || col.gameObject.name == "Right" || col.gameObject.name == "Bottom")
            {
                Debug.Log("Bitconeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeect!");
            }
        }
        */
    }
    //Transform.Position
}
}

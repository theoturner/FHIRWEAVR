using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Vector3 location;
    DataHandler myDataInstance;
    System.Random random = new System.Random();

    void Start()
    {

        myDataInstance = DataHandler.Instance;
        location = transform.position;

    }

    void Update()
    {
        if (location.z <= -5)
        {
            if (random.Next(0, 1) == 0)
            {
                location.x *= -1;
            }
            location.z += 30;
        }
        location.z += (float)-0.01;
        //location.x = -100 * (float)myDataInstance.GetMetric("lean", "current");
        //location.y = 100 * (float)myDataInstance.GetMetric("incline", "current") + 15;
        transform.position = location;

    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "VZPlayer")
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (col.gameObject.name == "Camera")
        {
            Debug.Log("Collision!");
        }

    }
}
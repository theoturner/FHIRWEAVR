﻿using UnityEngine;

public class Ocean : MonoBehaviour
{

    DataHandler data;
    Vector3 location;
    double speedMultiplier;

    void Start()
    {

        data = DataHandler.Instance;
        location = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        //speedMultiplier = data.GetMetric("speed", "current") / 2;
        speedMultiplier = 2; // REMOVE THIS TEST STATEMENT ****************************************************

        if (location.z <= -187)
        {
            location.z += 748;
        }
        location.z += (float)(-0.01 * speedMultiplier);
        transform.position = location;

    }
}

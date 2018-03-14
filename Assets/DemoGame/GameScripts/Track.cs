// TODO Consolidate all moving object scripts into this file

using UnityEngine;

public class Track : MonoBehaviour {

    DataHandler data;
    Vector3 location;
    double speedMultiplier;

    void Start () {

        data = DataHandler.Instance;
        location = transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        //speedMultiplier = data.GetMetric("speed", "current") / 2;
        speedMultiplier = 2; // REMOVE THIS TEST STATEMENT ****************************************************

        if (location.z <= -68.58)
        {
            location.z += (float)152.4;
        }
        location.z += (float)(-0.01 * speedMultiplier);
        transform.position = location;

    }
}

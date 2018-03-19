using UnityEngine;

public class Track : MonoBehaviour
{

    DataHandler data;
    Vector3 location;
    double speedMultiplier;

    void Start () {

        data = DataHandler.Instance;
        location = transform.position;

    }
	
	void Update () {

        //speedMultiplier = data.GetMetric("speed", "current") / 2;
        speedMultiplier = 5; // REMOVE THIS TEST STATEMENT ****************************************************

        if (location.z <= -68.58)
        {
            location.z += 152.4f;
        }
        location.z += (float)(-0.01 * speedMultiplier);
        transform.position = location;

    }
}

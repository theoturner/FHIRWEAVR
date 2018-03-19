using UnityEngine;

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

    void Update()
    {

        // Move ocean at twice the speed of everything else, creates 'fish-eye' sense of speed
        //speedMultiplier = data.GetMetric("speed", "current");
        speedMultiplier = 10; // REMOVE THIS TEST STATEMENT ****************************************************

        if (location.z <= -187)
        {
            location.z += 748;
        }
        location.z += (float)(-0.01 * speedMultiplier);
        transform.position = location;

    }
}

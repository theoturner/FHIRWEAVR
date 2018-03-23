// Ocean animation for 'fish-eye' sense of speed.

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
        // Uncomment this for use with the bike
        //speedMultiplier = data.GetMetric("speed", "current");
        // Uncomment this for Unity Editor emulation
        speedMultiplier = 10;

        if (location.z <= -187)
        {
            location.z += 748;
        }
        location.z += (float)(-0.01 * speedMultiplier);

        transform.position = location;
    }

}

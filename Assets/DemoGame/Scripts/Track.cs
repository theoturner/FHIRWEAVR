// Generates the track.

using UnityEngine;

public class Track : MonoBehaviour
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
        // Uncomment this for use with the bike
        //speedMultiplier = data.GetMetric("speed", "current") / 2;
        // Uncomment this for Unity Editor emulation
        speedMultiplier = 5;

        if (location.z <= -68.58)
        {
            location.z += 152.4f;
        }

        location.z += (float)(-0.01 * speedMultiplier);

        transform.position = location;
    }

}

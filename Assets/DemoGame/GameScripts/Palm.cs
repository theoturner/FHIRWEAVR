// TODO Consolidate all moving object scripts into this file

using UnityEngine;

public class Palm : MonoBehaviour
{

    DataHandler data;
    Vector3 location;
    double speedMultiplier;
    float randomScale;

    void Start()
    {

        data = DataHandler.Instance;
        location = transform.position;

    }

    void Update()
    {

        //speedMultiplier = data.GetMetric("speed", "current") / 2;
        speedMultiplier = 5; // REMOVE THIS TEST STATEMENT ****************************************************

        if (location.z <= -72.39)
        {

            // Need a new random seed any time we want to respawn the tree
            // Note the need to use ranom.Next(i, j + 1) for a random integer between i and j
            System.Random random = new System.Random();

            // Random scale in acceptable range
            randomScale = (float)random.Next(8, 16) / 10;
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Random right-angle rotation
            transform.rotation = Quaternion.Euler(0, random.Next(0, 4) * 90, 0);

            // Random distance from track in acceptable range 
            location.x = (float)random.Next(31, 33) / 10;

            // Random side of track
            if (random.Next(0, 2) == 0)
            {

                location.x *= -1;

            }
            // Random height in acceptable range
            location.y = (float)random.Next(-18, -10) / 10;

            // Random z-positon in acceptable range
            location.z += (float)random.Next(-32, 33) / 10;

            // Move trees forward at end of conveyor belt
            location.z += (float)152.4;

        }

        location.z += (float)(-0.01 * speedMultiplier);

        // Update location after all calculations done
        transform.position = location;

    }
}

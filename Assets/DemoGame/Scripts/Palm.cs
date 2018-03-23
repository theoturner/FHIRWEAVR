// Random generation of palm trees.

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
        // Uncomment this for use with the bike
        //speedMultiplier = data.GetMetric("speed", "current") / 2;
        // Uncomment this for Unity Editor emulation
        speedMultiplier = 5;

        if (location.z <= -72.39)
        {
            // Need a new random seed any time we want to respawn the tree
            // Note the need to use ranom.Next(i, j + 1) for a random integer between i and j
            System.Random random = new System.Random();

            // Random scale in acceptable range
            randomScale = random.Next(8, 16) / 10f;
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Random right-angle rotation
            transform.rotation = Quaternion.Euler(0, random.Next(0, 4) * 90, 0);

            // Random distance from track in acceptable range 
            location.x = random.Next(31, 33) / 10f;

            // Random side of track
            if (random.Next(0, 2) == 0)
            {
                location.x *= -1;
            }
            // Random height in acceptable range
            location.y = random.Next(-18, -6) / 10f;

            // Random z-positon in acceptable range
            location.z += random.Next(-32, 33) / 10f;

            // Move trees forward at end of conveyor belt
            location.z += 152.4f;
        }

        location.z += (float)(-0.01 * speedMultiplier);

        // Update location after all calculations done
        transform.position = location;
    }

}

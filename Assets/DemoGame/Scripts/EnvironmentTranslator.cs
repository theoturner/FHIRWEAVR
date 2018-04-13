// Common script which transforms objects for the game's random generation, added programatically as a component for each game object type.

using UnityEngine;

public class EnvironmentTranslator : MonoBehaviour
{

    // Variables set by scripts belonging to specific game objects
    double limit;
    bool doesFishEye, doesScale, isScenery;
    int scaleMin, scaleMax, xMin, xMax, zSpread;
    public double Limit { set { limit = value; } }
    public bool DoesFishEye { set { doesFishEye = value; } }
    public bool DoesScale { set { doesScale = value; } }
    public bool IsScenery { set { isScenery = value; } }
    public int ScaleMin { set { scaleMin = value; } }
    public int ScaleMax { set { scaleMax = value; } }
    public int XMin { set { xMin = value; } }
    public int XMax { set { xMax = value; } }
    public int ZSpread { set { zSpread = value; } }

    DataHandler data;
    Vector3 location;
    double speedMultiplier, proximityMultiplier;
    float randomScale;

    void Start()
    {
        data = DataHandler.Instance;
        location = transform.position;

        // Uncomment this for Unity Editor emulation, comment speedMultiplier in Update()
        // Average speed in testing was found to be 3.3m/s, assuming a constant 60fps (not demanding) this means a speedMultiplier of 5.5
        speedMultiplier = 5.5;

        // Set base movement of objects depending on whether appearance is fish-eye
        proximityMultiplier = 0.01;
        if (doesFishEye)
        {
            proximityMultiplier *= 2;
        }
    }

    void Update()
    {
        // Uncomment this for use with the bike, comment speedMultiplier in Start()
        //speedMultiplier = data.GetMetric("speed", "current");

        if (location.z <= -limit)
        {
            // Need a new random seed any time we want to respawn an object
            // Note the need to use ranom.Next(i, j + 1) for a random integer between i and j
            System.Random random = new System.Random();

            // Random distance from track in specified range 
            location.x = random.Next(xMin, xMax) / 10f;

            // Random z-positon in specified range
            location.z += random.Next(-zSpread, zSpread + 1) / 10f;

            if (doesScale)
            {
                // Random scale in specified range
                randomScale = random.Next(scaleMin, scaleMax + 1) / 10f;

                transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            }

            if (isScenery)
            {
                // Random side of track
                if (random.Next(0, 2) == 0)
                {
                    location.x *= -1;
                }

                // Random right-angle rotation
                transform.rotation = Quaternion.Euler(0, random.Next(0, 4) * 90, 0);

                // Random height in acceptable range
                location.y = random.Next(-18, -6) / 10f;
            }

            // Respawn object by moving it to the front
            if (doesFishEye)
            {
                location.z += 748f;
            }
            else
            {
                location.z += 152.4f;
            }

            transform.position = location;
        }

        location.z -= (float)(speedMultiplier * proximityMultiplier);

        transform.position = location;
    }

}

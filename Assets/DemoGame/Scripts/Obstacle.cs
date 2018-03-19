using UnityEngine;

public class Obstacle : MonoBehaviour
{

    DataHandler data;
    AudioSource crashSound;
    Vector3 location;
    double speedMultiplier;
    float randomScale;
    bool collisionTrigger;

    void Start()
    {

        data = DataHandler.Instance;
        crashSound = GetComponentInParent<AudioSource>();
        location = transform.position;

    }

    void Update()
    {

        //speedMultiplier = data.GetMetric("speed", "current") / 2;
        speedMultiplier = 5; // REMOVE THIS TEST STATEMENT ****************************************************

        if (location.z <= -72.39)
        {

            // Need a new random seed any time we want to respawn the obstacle
            // Note the need to use ranom.Next(i, j + 1) for a random integer between i and j
            System.Random random = new System.Random();

            // Random scale in acceptable range
            randomScale = random.Next(8, 11) / 10f;
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Random left/right position on track in acceptable range 
            location.x = random.Next(-13, 14) / 10f;

            // Random z-positon in acceptable range (define relative to the center of each track piece)
            location.z += random.Next(-1, 2) / 10f;

            // Move obstacles forward at end of conveyor belt
            location.z += 152.4f;

            collisionTrigger = false;

        }

        location.z += (float)(-0.01 * speedMultiplier);

        // Update location after all calculations done
        transform.position = location;

    }


    void OnCollisionEnter(Collision collision)
    {

        // We don't use the bicycle hitbox as testing shows users perceive the bike is always directly under their view
        // regardless of leaning. Instead we use the camera and extend the hitbox to the ground and the width of the bike.

        if (collision.gameObject.name == "Camera" && collisionTrigger == false)
        {

            collisionTrigger = true;
            crashSound.Play();
            if (Player.score != 0)
            {

                Player.score--;

            }

        }

    }


}


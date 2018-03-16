// TODO Consolidate all moving object scripts into this file

using UnityEngine;

public class Obstacle : MonoBehaviour
{

    DataHandler data;
    Vector3 location;
    double speedMultiplier;
    float randomScale;
    int collisionTrigger, incrementTrigger;

    void Start()
    {

        data = DataHandler.Instance;
        location = transform.position;

    }

    void Update()
    {

        //speedMultiplier = data.GetMetric("speed", "current") / 2;
        speedMultiplier = 5; // REMOVE THIS TEST STATEMENT ****************************************************

        if (location.z <= -0.6)
        {
            if (collisionTrigger == 0 && incrementTrigger == 0)
            {
                Player.score++;
                incrementTrigger = 1;
            }
        }

        if (location.z <= -72.39)
        {

            // Need a new random seed any time we want to respawn the tree
            // Note the need to use ranom.Next(i, j + 1) for a random integer between i and j
            System.Random random = new System.Random();

            // Random scale in acceptable range
            randomScale = (float)random.Next(8, 11) / 10;
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Random left/right position on track in acceptable range 
            location.x = (float)random.Next(-13, 14) / 10;

            // Random z-positon in acceptable range (define relative to the center of each track piece)
            location.z += (float)random.Next(-1, 2) / 10;

            // Move trees forward at end of conveyor belt
            location.z += (float)152.4;

            incrementTrigger = 0;
            collisionTrigger = 0;

        }

        location.z += (float)(-0.01 * speedMultiplier);

        // Update location after all calculations done
        transform.position = location;

    }


    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "VZPlayer")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (collision.gameObject.name == "Camera")
        {
            collisionTrigger = 1;
            if (Player.score != 0)
            {
                Player.score--;
            }
            // TODO FLASH SCREEN RED
            Debug.Log("Collision!");
        }

    }


}


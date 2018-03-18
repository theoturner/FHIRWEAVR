//TODO Add the following to notes
// Ignore bike hitbox - testing shows users perceive the boke is always directly under their view, regardless of leaning
// As frames are not infinite per second, we don't have strictly continuous data, to check for an object passing a point,
//      we have to check for a certain range of positions. If a frame is rendered when the world model says the object is
//      in that range, the result is true.
// Bounce user off edge of track to clearly demonstrate the barrier. Continued leaning against the edge 'shakes' the
//      camera, like rumble strips
// Start moving left/right above a certain degree of leaning to account for minor head movements

using UnityEngine;

public class Obstacle : MonoBehaviour
{

    DataHandler data;
    Vector3 location;
    double speedMultiplier;
    float randomScale;
    int collisionTrigger;

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

            // Need a new random seed any time we want to respawn the obstacle
            // Note the need to use ranom.Next(i, j + 1) for a random integer between i and j
            System.Random random = new System.Random();

            // Random scale in acceptable range
            randomScale = (float)random.Next(8, 11) / 10;
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Random left/right position on track in acceptable range 
            location.x = (float)random.Next(-13, 14) / 10;

            // Random z-positon in acceptable range (define relative to the center of each track piece)
            location.z += (float)random.Next(-1, 2) / 10;

            // Move obstacles forward at end of conveyor belt
            location.z += 152.4f;

            collisionTrigger = 0;

        }

        location.z += (float)(-0.01 * speedMultiplier);

        // Update location after all calculations done
        transform.position = location;

    }


    void OnCollisionEnter(Collision collision)
    {

        // Ignore bike hitbox, testing shows users perceive the bike is always directly under their view, regardless of leaning
        if (collision.gameObject.tag == "VZPlayer")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (collision.gameObject.name == "Camera" && collisionTrigger == 0)
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


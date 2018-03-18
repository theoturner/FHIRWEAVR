//TODO Add the following to notes
// Ignore bike hitbox - testing shows users perceive the boke is always directly under their view, regardless of leaning
// As frames are not infinite per second, we don't have strictly continuous data, to check for an object passing a point,
//      we have to check for a certain range of positions. If a frame is rendered when the world model says the object is
//      in that range, the result is true.

using UnityEngine;

public class Coin : MonoBehaviour
{

    DataHandler data;
    Vector3 location;
    double speedMultiplier;
    float randomScale;
    int rotation;
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

        if (location.z <= -76.2)
        {

            // Need a new random seed any time we want to respawn the obstacle
            // Note the need to use ranom.Next(i, j + 1) for a random integer between i and j
            System.Random random = new System.Random();

            // Random left/right position on track in acceptable range 
            location.x = (float)random.Next(-16, 17) / 10;

            // Random z-positon in acceptable range (define relative to the center of each track piece)
            location.z += (float)random.Next(-1, 2) / 10;

            // Move coins forward at end of conveyor belt
            location.z += (float)152.4;

            collisionTrigger = 0;

        }

        location.z += (float)(-0.01 * speedMultiplier);

        // Rotate continually
        transform.Rotate(0, 360 * Time.deltaTime, 0, Space.World);

        // Update location after all calculations done
        transform.position = location;

    }


    void OnCollisionEnter(Collision collision)
    {

        // Ignore bike hitbox, testing shows users perceive the bike is always directly under their view, regardless of leaning
        if (collision.gameObject.name == "Camera" && collisionTrigger == 0)
        {
            collisionTrigger = 1;
            if (Player.score != 0)
            {
                Player.score++;
            }
            // TODO FLASH SCREEN RED
            Debug.Log("Coin!");
        }

    }


}


//TODO Add the following to notes
// Ignore bike hitbox - testing shows users perceive the boke is always directly under their view, regardless of leaning
// As frames are not infinite per second, we don't have strictly continuous data, to check for an object passing a point,
//      we have to check for a certain range of positions. If a frame is rendered when the world model says the object is
//      in that range, the result is true.

using UnityEngine;

public class Coin : MonoBehaviour
{

    DataHandler data;
    AudioSource coinSound;
    Vector3 location;
    double speedMultiplier;
    float randomScale;
    int rotation;
    bool collisionTrigger;

    void Start()
    {

        data = DataHandler.Instance;
        coinSound = GetComponentInParent<AudioSource>();
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
            location.x = random.Next(-16, 17) / 10f;

            // Random z-positon in acceptable range (define relative to the center of each track piece)
            location.z += random.Next(-1, 2) / 10f;

            // Move coins forward at end of conveyor belt
            location.z += 152.4f;

            collisionTrigger = false;

        }

        location.z += (float)(-0.01 * speedMultiplier);

        // Rotate continually
        transform.Rotate(0, 360 * Time.deltaTime, 0, Space.World);

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
            coinSound.Play();
            Player.score++;

        }

    }


}


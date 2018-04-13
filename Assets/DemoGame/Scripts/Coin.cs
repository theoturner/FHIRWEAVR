// Random generation of coins and player pick-up results.

using UnityEngine;

public class Coin : MonoBehaviour
{

    EnvironmentTranslator objectMover;
    AudioSource coinSound;
    bool coinTrigger;

    void Start()
    {
        objectMover = gameObject.AddComponent<EnvironmentTranslator>();
        objectMover.Limit = 76.2;
        objectMover.XMin = -16;
        objectMover.XMax = 16;
        objectMover.ZSpread = 1;

        coinSound = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        // Let user clear coin before making it able to be picked up again
        if (transform.position.z <= -3)
        {
            coinTrigger = false;
        }

        // Rotate continually
        transform.Rotate(0, 360 * Time.deltaTime, 0, Space.World);
    }


    void OnCollisionEnter(Collision collision)
    {
        // We don't use the bicycle hitbox as testing shows users perceive the bike is always directly under their view
        // regardless of leaning. Instead we use the camera and extend the hitbox to the ground and the width of the bike.

        if (collision.gameObject.name == "Camera" && coinTrigger == false)
        {
            coinTrigger = true;
            coinSound.Play();

            Player.score++;
        }
    }

}

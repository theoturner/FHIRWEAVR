// Random generation of obstacles and player collision results.

using UnityEngine;

public class Obstacle : MonoBehaviour
{

    EnvironmentTranslator objectMover;
    AudioSource crashSound;
    bool collisionTrigger;

    void Start()
    {
        objectMover = gameObject.AddComponent<EnvironmentTranslator>();
        objectMover.Limit = 72.39;
        objectMover.DoesScale = true;
        objectMover.XMin = -13;
        objectMover.XMax = 13;
        objectMover.ScaleMin = 8;
        objectMover.ScaleMax = 10;
        objectMover.ZSpread = 1;

        crashSound = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        // Let user clear obstacle before making it collidable again - prevents multiple collision detections in a single 'real' collision
        if (transform.position.z <= -5)
        {
            collisionTrigger = false;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // We don't use the bicycle hitbox as testing shows users perceive the bike is always directly under their view
        // regardless of leaning. Instead we use the camera and extend the hitbox to the ground and the width of the bike.

        if (collision.gameObject.name == "Camera" && collisionTrigger == false)
        {
            collisionTrigger = true;
            crashSound.Play();

            // Prevent negative score
            if (Player.score != 0)
            {
                Player.score--;
            }
        }
    }

}

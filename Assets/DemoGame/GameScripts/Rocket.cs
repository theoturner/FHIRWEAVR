using UnityEngine;

public class Rocket : MonoBehaviour
{

    Vector3 location;
    DataHandler data;
    System.Random random = new System.Random();
    int collisionTrigger, incrementTrigger;
    double speedMultiplier;

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
        if (location.z <= -3)
        {
            if (random.Next(0, 1) == 0)
            {
                location.x *= -1;
            }
            location.z += 28;
            incrementTrigger = 0;
            collisionTrigger = 0;
        }
        location.z += (float)(-0.01 * speedMultiplier);
        transform.position = location;

    }


    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "VZPlayer")
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (col.gameObject.name == "Camera")
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
using System.Collections;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{

    Vector3 originalPosition, newPosition;
    int collisionTrigger;

    void OnCollisionEnter(Collision collision)
    {

        // Ignore bike hitbox, testing shows users perceive the bike is always directly under their view, regardless of leaning
        if (collision.gameObject.tag == "Obstacle" && collisionTrigger == 0)
        {

            collisionTrigger = 1;
            Debug.Log("hit that one");
            StartCoroutine(ShakeCamera());

        }

    }

    IEnumerator ShakeCamera()
    {

        originalPosition = transform.position;

        for (int i = 0; i < 7; i++)
        {
            newPosition = transform.position + Random.insideUnitSphere * 0.1f;
            transform.position = newPosition;
            yield return new WaitForSeconds(0.05f);
        }

        transform.position = originalPosition;
        collisionTrigger = 0;

    }

}
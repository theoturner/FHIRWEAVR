// Camera effects for in-game events.

using System.Collections;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{

    CanvasGroup effects;
    Vector3 originalPosition, newPosition;
    int effectGrade;
    bool collisionTrigger;

    private void Start()
    {
        effects = GetComponentInChildren<CanvasGroup>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // See Obstacle.cs for hitbox information.

        if (collision.gameObject.tag == "Obstacle" && collisionTrigger == false)
        {
            collisionTrigger = true;

            // We use couroutines so we can yield when waiting between effect increments
            StartCoroutine(ShakeCamera());
            StartCoroutine(FlashCamera());

            collisionTrigger = false;
        }
    }

    IEnumerator ShakeCamera()
    {
        originalPosition = transform.position;

        for (effectGrade = 0; effectGrade < 30; effectGrade++)
        {
            newPosition = transform.position + Random.insideUnitSphere * 0.1f;
            transform.position = newPosition;
            yield return new WaitForSeconds(0.001f);
        }

        transform.position = originalPosition;
    }

    IEnumerator FlashCamera()
    {
        for (effectGrade = 0; effectGrade < 5; effectGrade++)
        {
            effects.alpha = effects.alpha + 0.04f;
            yield return new WaitForSeconds(0.001f);
        }

        for (effectGrade = 0; effectGrade < 28; effectGrade++)
        {
            effects.alpha = effects.alpha - 0.004f;
            yield return new WaitForSeconds(0.001f);
        }

        // Frames are not infinite per second, so sometimes we don't return exactly to 0
        effects.alpha = 0;
    }

}

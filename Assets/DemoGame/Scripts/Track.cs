// Generates the track.

using UnityEngine;

public class Track : MonoBehaviour
{

    EnvironmentTranslator objectMover;

    void Start()
    {
        objectMover = gameObject.AddComponent<EnvironmentTranslator>();
        objectMover.Limit = 68.58;
    }

}

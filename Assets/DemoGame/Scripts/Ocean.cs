// Ocean animation for 'fish-eye' sense of speed.

using UnityEngine;

public class Ocean : MonoBehaviour
{

    EnvironmentTranslator objectMover;

    void Start()
    {
        objectMover = gameObject.AddComponent<EnvironmentTranslator>();
        objectMover.Limit = 187;
        objectMover.DoesFishEye = true;
    }

}

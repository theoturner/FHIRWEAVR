// Ocean animation for realistic motion parallax - mitigates virtual reality sickness.

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

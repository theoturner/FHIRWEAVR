// Random generation of palm trees.

using UnityEngine;

public class Palm : MonoBehaviour
{

    EnvironmentTranslator objectMover;

    void Start()
    {
        objectMover = gameObject.AddComponent<EnvironmentTranslator>();
        objectMover.Limit = 72.39;
        objectMover.DoesScale = true;
        objectMover.IsScenery = true;
        objectMover.XMin = 31;
        objectMover.XMax = 32;
        objectMover.ScaleMin = 8;
        objectMover.ScaleMax = 15;
        objectMover.ZSpread = 32;
    }

}

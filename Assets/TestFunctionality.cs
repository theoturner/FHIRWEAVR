using UnityEngine;

// TODO WHY DOES GETMETRIC NOT MATCH GENFHIR RESULT?
// TODO RENAME THIS FILE TO Example.cs

public class TestFunctionality : MonoBehaviour {

    // You must add this at the start of your MonoBehaviour class. Name myDataInstance whatever you'd like.
    DataHandler myDataInstance;

    // This function is used for initialisation.
    void Start ()
    {
        // You must must add this in Start().
        myDataInstance = DataHandler.Instance;
    }

    // This function is called once per frame.
    // Example uses of FHIRWEAVR are included.
    void Update () {
        Debug.Log(myDataInstance.GetMetric("rotation", "current"));
        if (Time.frameCount == 300)
        {
            myDataInstance.DisplayMetric("resistance", "current", 5);
            GenFHIR.Document("session");
        }
    }
}

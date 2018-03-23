// Example uses of FHIRWEAVR.

using UnityEngine;

public class ExampleUses : MonoBehaviour
{

    /*

    // You must add this at the start of your MonoBehaviour class. Name the instances whatever you'd like.
    DataHandler myDataInstance;
    PushData myUploaderInstance = new PushData();

    // This function is used for initialisation.
    void Start()
    {
        // You must must add this in Start().
        myDataInstance = DataHandler.Instance;
        myDataInstance.StartSession();
    }

    // This function is called once per frame.
    // Example uses of FHIRWEAVR are included.
    void Update()
    {
        myDataInstance.DisplayAllData("current");
        Debug.Log(myDataInstance.GetMetric("rotation", "current"));

        if (Time.frameCount == 600)
        {
            myDataInstance.DisplayAllData("session", 10);
            GenFHIR.Document("session");
            GenFHIR.Document("current", "VirZOOM-updatable-output");
            myUploaderInstance.Upload("last", "http://ptsv2.com/t/VirZOOM/post");
        }

        if (Time.frameCount == 1000)
        {
            myDataInstance.EndSession();
        }
    }

    */

}

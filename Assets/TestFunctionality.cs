using System.Threading;
using UnityEngine;

// TODO WHY DOES GETMETRIC NOT MATCH GENFHIR RESULT?
// TODO DECIDE HOW TO DO MULTITHREADING, FORCED IN PUSHDATA.CS OR OPTIONALLY BY THE DEVELOPER.
// TODO RENAME THIS FILE TO Example.cs

public class TestFunctionality : MonoBehaviour {

    // You must add this at the start of your MonoBehaviour class. Name the instances whatever you'd like.
    DataHandler myDataInstance;
    PushData myUploaderInstance = new PushData();

    // This function is used for initialisation.
    void Start ()
    {
        // You must must add this in Start().
        myDataInstance = DataHandler.Instance;
        
    }

    // This function is called once per frame.
    // Example uses of FHIRWEAVR are included.
    void Update () {
        //Debug.Log(myDataInstance.GetMetric("rotation", "current"));
        if (Time.frameCount == 300)
        {
            myDataInstance.DisplayMetric("resistance", "current", 5);
            GenFHIR.Document("session");
            Debug.Log(DataHandler.path);
            //myUploaderInstance.Upload("last", "http://ptsv2.com/t/VirZOOM/post");
            myUploaderInstance.ManualUpload("VirZOOM-output-02-08-16h18m02s.xml", DataHandler.path, "http://ptsv2.com/t/VirZOOM/post");
            //myUploaderInstance.FileExistsAtURL("http://ptsv2.com/t/VirZOOM/post/VirZOOM-device-profile.xml");
        }
    }
}

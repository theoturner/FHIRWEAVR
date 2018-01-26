// Sends data not yet on server to server via HTTP POST.

//TODO MAKE THIS WORK. MAKE SURE YOU PUSH DEVICE PROFILE ALSO!!!! CHECK IF DEVICE PROFILE EXISTS ON SERVER BEFORE SENDING.

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PostData : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(HTTPPOST());
    }

    IEnumerator HTTPPOST()
    {
        List<IMultipartFormSection> dataToSend = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("field1=foo&field2=bar"),
            new MultipartFormFileSection("my file data", "myfile.txt")
        };

        UnityWebRequest request = UnityWebRequest.Post("http://www.my-server.com/myform", dataToSend);

        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log("Sending data to server failed." + request.error);
        }
    }
}
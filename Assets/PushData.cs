// Sends data not yet on server to server via FTP.

//TODO MAKE THIS WORK. MAKE SURE YOU PUSH DEVICE PROFILE ALSO!!!! CHECK IF DEVICE PROFILE EXISTS ON SERVER BEFORE SENDING.

using UnityEngine;
using System;
using System.Net;
using System.IO;

public class PushData
{

    public void FTPUpload(string fileToUpload, string host, string username, string password)
    {
        string path = DataHandler.path + fileToUpload;

        WebClient client = new System.Net.WebClient();
        Uri uri = new Uri(host + new FileInfo(path).Name);

        client.UploadProgressChanged += new UploadProgressChangedEventHandler(Progress);
        client.UploadFileCompleted += new UploadFileCompletedEventHandler(Complete);
        client.Credentials = new System.Net.NetworkCredential(username, password);
        client.UploadFileAsync(uri, "STOR", path);
    }

    void Progress(object sender, UploadProgressChangedEventArgs e)
    {
        Debug.Log("Uploading: " + e.ProgressPercentage);
    }

    void Complete(object sender, UploadFileCompletedEventArgs e)
    {
        Debug.Log("Uploaded");
    }

}
// Sends data not yet on server to server via FTP.

//TODO MAYBE Detect if server exists/allows uploads and inform the dev using Debug.Log if not

using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using UnityEngine;

public class PushData
{

    // <CODE REVIEW> Is there a better way to force threading of all methods in this class than to use Upload as abtraction for UploadCore?

    public void Upload(string fileToUpload, string host)
    {

        Thread uploadWorker = new Thread(() => UploadCore(fileToUpload, host));
        uploadWorker.Start();

    }

    public void ManualUpload(string fileToUpload, string filePath, string host)
    {

        Thread manualUploadWorker = new Thread(() => ManualUploadCore(fileToUpload, filePath, host));
        manualUploadWorker.Start();

    }

    public bool FileExistsAtURL(string url)
    {

        bool returnValue = false;
        // <CODE REVIEW> Does this work? Probably: https://stackoverflow.com/questions/1314155/returning-a-value-from-thread
        Thread fileWorker = new Thread(() => returnValue = FileExistsAtURLCore(url));
        fileWorker.Start();
        return returnValue;

    }

    void UploadCore(string fileToUpload, string host)
    {

        // Note that HTTP allows additional / characters so don't need to check final character of host
        if (!FileExistsAtURLCore(host + "/VirZOOM-device-profile.xml"))
        {
            UploadViaClient(host, DataHandler.path + "VirZOOM-device-profile.xml");
        }

        // For 'last' keyword as first parameter to push last saved file
        if (fileToUpload == "last")
        {
            var folder = new DirectoryInfo(DataHandler.path);
            var lastFile = folder.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            fileToUpload = lastFile.Name;
        }
        
        string fullFilePath = DataHandler.path + fileToUpload;
        if (!(File.Exists(fullFilePath)))
        {
            Debug.Log("File not found. Please check the filename or use ManualUpload(string fileToUpload, string filePath, string host) for files in non-default save locations.");
        }

        UploadViaClient(host, fullFilePath);

    }

    void ManualUploadCore(string fileToUpload, string filePath, string host)
    {

        char lastPathChar = filePath[filePath.Length - 1];
        // Note that DOS uses / and \ interchangeably, even allowing both in a single path. Note \\ is an escape on \.
        if (lastPathChar != '/' || lastPathChar != '\\')
        {
            filePath = filePath + '/';
        }
        string fullFilePath = filePath + fileToUpload;
        if (!(File.Exists(fullFilePath)))
        {
            Debug.Log("File not found. Please check that the filename and path.");
        } 
        UploadViaClient(host, fullFilePath);

    }

    void UploadViaClient(string host, string fileToUpload)
    {

        WebClient FHIRClient = new WebClient();
        using(FHIRClient)
        {
            FHIRClient.UploadFile(host, fileToUpload);
        }

    }

    bool FileExistsAtURLCore(string url)
    {

        try
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "HEAD";
            // If HTTP status is 200, file exists
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Close();
            return (response.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            Debug.Log("Could not detect whether certain files exist on the server. Ensure that you push the device profile.");
            return false;
        }

    }

}
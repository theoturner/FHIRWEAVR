// Sends data not yet on server to server via FTP.

//TODO TEST THIS. ESPECIALLY TEST SENDING TO NONEXISTENT HOST TO SEE IF WEBCLIENT HANDLES ERROR AUTOMATICALLY, IF NOT ADD ERROR HANDLING.

using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;

public class PushData
{

    public void Upload(string fileToUpload, string host)
    {
        // Note that HTTP allows additional / characters so don't need to check final character of host
        if (!FileExistsAtURL(host + "/VirZOOM-device-profile.xml"))
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

        UploadViaClient(host, fileToUpload);
    }

    public void ManualUpload(string fileToUpload, string filePath, string host)
    {
        char lastPathChar = filePath[filePath.Length - 1];
        // Note that DOS uses / and \ interchangeably, even allowing both in a single path
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

    public bool FileExistsAtURL(string url)
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
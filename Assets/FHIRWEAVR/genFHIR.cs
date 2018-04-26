// Generates FHIR Documents and Device Profiles.

using System;
using System.IO;
using System.Xml;

class GenFHIR
{

    // Optional parameter used for closing connections after sessions to ensure VirZOOM is registered as inactive
    public static void Device(int connected = 1)
    {
        string dateTime = DateTimeFormats.GetDT("full");
        // dateTime guarantees URI and is human-readable
        string virZoomReference = "Device/VirZOOM";

        XmlWriter xw = XmlWriter.Create(DataHandler.path + "VirZOOM-device-profile.xml");

        xw.WriteStartDocument();

        xw.WriteStartElement("Bundle", "http://hl7.org/fhir");

        // Identifying data
        xw.WriteStartElement("meta");
        xw.WriteStartElement("lastUpdated");
        xw.WriteAttributeString("value", dateTime);
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("identifier");
        xw.WriteAttributeString("value", "FHIRWEAVR-device-" + dateTime);
        xw.WriteFullEndElement();

        xw.WriteStartElement("type");
        xw.WriteAttributeString("value", "document");
        xw.WriteFullEndElement();

        // The FHIR composition
        xw.WriteStartElement("entry");
        xw.WriteStartElement("resource");
        xw.WriteStartElement("Composition");

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "final");
        xw.WriteFullEndElement();

        xw.WriteStartElement("type");
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "VirZOOM device profile");
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("subject");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", virZoomReference);
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("date");
        xw.WriteAttributeString("value", dateTime);
        xw.WriteFullEndElement();

        xw.WriteStartElement("author");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", virZoomReference);
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("title");
        xw.WriteAttributeString("value", "VirZOOM device profile");
        xw.WriteFullEndElement();

        xw.WriteStartElement("section");
        xw.WriteStartElement("entry");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", virZoomReference);
        xw.WriteFullEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        // End composition, resource, entry
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        // The actual device information - note Device has no required resources in FHIR
        xw.WriteStartElement("entry");
        xw.WriteStartElement("resource");
        xw.WriteStartElement("Device");

        xw.WriteStartElement("id");
        xw.WriteAttributeString("value", "VirZOOM");
        xw.WriteFullEndElement();

        xw.WriteStartElement("status");
        if (connected == 1)
        {
            xw.WriteAttributeString("value", "active");
        }
        else
        {
            xw.WriteAttributeString("value", "inactive");
        }
        xw.WriteFullEndElement();

        xw.WriteStartElement("manufacturer");
        xw.WriteAttributeString("value", "VirZOOM Inc.");
        xw.WriteFullEndElement();

        xw.WriteStartElement("model");
        xw.WriteAttributeString("value", "beta bicycle");
        xw.WriteFullEndElement();

        xw.WriteStartElement("contact");
        xw.WriteStartElement("ContactPoint");
        xw.WriteStartElement("system");
        xw.WriteAttributeString("value", "email");
        xw.WriteFullEndElement();
        // Yes, the FHIR spec has a value attribute with a value attribute
        xw.WriteStartElement("value");
        xw.WriteAttributeString("value", "computerscience.comms@ucl.ac.uk");
        xw.WriteFullEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("safety");
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "Consult a medical professional before use. Use only on a level surface " +
            "in a clear space and ensure that the bicycle is properly assembled. Do not exceed weight limit.");
        xw.WriteEndElement();
        xw.WriteFullEndElement();

        // End device, resource, entry
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        // End bundle
        xw.WriteEndElement();

        xw.WriteEndDocument();
        xw.Close();
    }

    public static void Document(string type, string overwriteName = "")
    {
        // In case of forgetting to use StartSession(), a forced creation of the device profile.
        // Forgetting to use the function means device active status can be registered incorrectly.
        // Checked every time a Document is created for the sake of Android 6's handling of the persistent data path.
        if (!(File.Exists(DataHandler.path + "VirZOOM-device-profile.xml")))
        {
            Device();
        }

        // Type handling done in DataHandler
        double[] output = DataHandler.Instance.GetAllData(type);
        int dataCount;

        string[] identifiers = { "distance", "speed", "resistance", "heartrate", "rotation", "lean", "incline" };
        string[] unit = { " km", " m/s", "", " bpm", " rad", " m", " m" };

        // Get date/time immediately after getting data
        string dateTime = DateTimeFormats.GetDT("full");
        string fileDateTime = DateTimeFormats.GetDT("filename");
        string documentReference = "DocumentReference/VirZOOM-device-profile";

        string fullFilePath = DataHandler.path;

        if (overwriteName == "")
        {
            fullFilePath += "VirZOOM-output-" + fileDateTime;
        }
        else
        {
            fullFilePath += overwriteName;
        }

        fullFilePath += ".xml";
        XmlWriter xw = XmlWriter.Create(fullFilePath);

        xw.WriteStartDocument();

        xw.WriteStartElement("Bundle", "http://hl7.org/fhir");

        xw.WriteStartElement("meta");
        xw.WriteStartElement("lastUpdated");
        xw.WriteAttributeString("value", dateTime);
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("identifier");
        xw.WriteAttributeString("value", "FHIRWEAVR-output-" + dateTime);
        xw.WriteFullEndElement();

        xw.WriteStartElement("type");
        xw.WriteAttributeString("value", "document");
        xw.WriteFullEndElement();

        xw.WriteStartElement("entry");
        xw.WriteStartElement("resource");
        xw.WriteStartElement("Composition");

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "final");
        xw.WriteFullEndElement();

        xw.WriteStartElement("type");
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "VirZOOM output");
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("subject");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", documentReference);
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("date");
        xw.WriteAttributeString("value", dateTime);
        xw.WriteFullEndElement();

        xw.WriteStartElement("author");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", documentReference);
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("title");
        xw.WriteAttributeString("value", "VirZOOM output");
        xw.WriteFullEndElement();

        xw.WriteStartElement("section");
        xw.WriteStartElement("entry");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", "Basic/metrics");
        xw.WriteFullEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        // Metric list - Basic is used for custom entities not yet supported by FHIR
        xw.WriteStartElement("entry");
        xw.WriteStartElement("resource");
        xw.WriteStartElement("Basic");

        xw.WriteStartElement("id");
        xw.WriteAttributeString("value", "metrics");
        xw.WriteFullEndElement();

        xw.WriteStartElement("code");
        xw.WriteStartElement("text");
        xw.WriteStartElement("AVRPhysioData");
        for (dataCount = 0; dataCount < 7; dataCount++)
        {
            xw.WriteStartElement(identifiers[dataCount]);
            xw.WriteAttributeString("value", String.Format("{0:0.0}", output[dataCount]) + unit[dataCount]);
            xw.WriteFullEndElement();
        }
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        // Reference to the VirZOOM device profile
        xw.WriteStartElement("entry");
        xw.WriteStartElement("resource");
        xw.WriteStartElement("DocumentReference");

        xw.WriteStartElement("id");
        xw.WriteAttributeString("value", "VirZOOM-device-profile");
        xw.WriteFullEndElement();

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "current");
        xw.WriteFullEndElement();

        xw.WriteStartElement("type");
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "device profile");
        xw.WriteFullEndElement();
        xw.WriteFullEndElement();

        xw.WriteStartElement("indexed");
        xw.WriteAttributeString("value", dateTime);
        xw.WriteFullEndElement();

        xw.WriteStartElement("content");
        xw.WriteStartElement("attachment");
        xw.WriteStartElement("url");
        // We always push the device profile with physio data files
        xw.WriteAttributeString("value", "VirZOOM-device-profile.xml");
        xw.WriteFullEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteEndElement();

        xw.WriteEndDocument();
        xw.Close();
    }

}

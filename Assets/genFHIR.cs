// Scripts for generating FHIR Documents and Device Profiles.

using System;
using System.IO;
using System.Xml;

//TODO Verify with a FHIR team member that you have all required elements
//TODO for documentation: document manual initial creation of device profile, will be created on document creation if does not exist.
//TODO Device(): read and change to inactive if device disconnected
//TODO MAYBE Find out how to overwrite old XML entries for continuously updating (current) metrics

class GenFHIR
{

    public static void Device()
    {

        XmlWriter xw = XmlWriter.Create(DataHandler.path + "VirZOOM-device-profile.xml");

        xw.WriteStartDocument();

        xw.WriteStartElement("Device", "http://hl7.org/fhir");

        xw.WriteStartElement("identifier");
        xw.WriteString("FHIRWEAVR-device");
        xw.WriteFullEndElement();

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "active"); //TODO read and change to inactive if device disconnected
        xw.WriteFullEndElement();

        xw.WriteStartElement("type");
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "Networked virtual-reality-enabled stationary exercise bicycle");
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("manufacturer");
        xw.WriteAttributeString("value", "VirZOOM Inc.");
        xw.WriteFullEndElement();

        xw.WriteStartElement("model");
        xw.WriteAttributeString("value", "Beta bicycle");
        xw.WriteFullEndElement();

        xw.WriteStartElement("contact");
        xw.WriteString("VirZOOM: info@virzoom.com - FHIRWEAVR: zcabttu@ucl.ac.uk");
        xw.WriteFullEndElement();

        xw.WriteStartElement("safety");
        xw.WriteString("Consult a medical professional before use. Use only on a level surface in a clear space and ensure that the bicycle is properly assembled. Do not exceed weight limit.");
        xw.WriteFullEndElement();

        xw.WriteEndElement();

        xw.WriteEndDocument();
        xw.Close();

    }

    public static void Document(string type)
    {

        if (!(File.Exists(DataHandler.path + "VirZOOM-device-profile.xml")))
        {
            Device();
        }

        // Type handling done in DataHandler
        double[] output = DataHandler.Instance.GetAllData(type);

        // Get date/time immediately after getting data
        string dateTime = DateTimeFormats.GetDT("full");
        string fileDateTime = DateTimeFormats.GetDT("filename");

        XmlWriter xw = XmlWriter.Create(DataHandler.path + "VirZOOM-output-" + fileDateTime + ".xml");
        int dataCount;
        string[] identifiers = { "distance", "speed", "resistance", "heartrate", "rotation", "lean", "incline" };
        string[] unit = { " km", " m/s", "", " bpm", " rad", " m", " m" };

        xw.WriteStartDocument();

        xw.WriteStartElement("Bundle", "http://hl7.org/fhir");

        xw.WriteStartElement("meta");
        xw.WriteStartElement("lastUpdated");
        xw.WriteAttributeString("value", dateTime);
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("identifier");
        xw.WriteString("FHIRWEAVR-output");
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
        xw.WriteStartElement("coding");
        xw.WriteStartElement("system");
        xw.WriteAttributeString("value", "http://loinc.org");
        xw.WriteFullEndElement();
        xw.WriteEndElement();
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "VirZOOM output");
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "final");
        xw.WriteFullEndElement();

        xw.WriteStartElement("subject");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", "VirZOOM-device-profile.xml");
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("date");
        xw.WriteAttributeString("value", dateTime);
        xw.WriteFullEndElement();

        xw.WriteStartElement("author");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", "VirZOOM-device-profile.xml");
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("title");
        xw.WriteString("VirZOOM output");
        xw.WriteFullEndElement();


        // FHIR List definitions
        xw.WriteStartElement("List");

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "current");
        xw.WriteFullEndElement();

        xw.WriteStartElement("mode");
        xw.WriteAttributeString("value", "snapshot");
        xw.WriteFullEndElement();


        // FHIR List entries
        for (dataCount = 0; dataCount < 7; dataCount++)
        {
            xw.WriteStartElement("entry");
            xw.WriteStartElement("item");
            xw.WriteAttributeString(identifiers[dataCount], String.Format("{0:0.0}", output[dataCount]) + unit[dataCount]);
            xw.WriteFullEndElement();
            xw.WriteEndElement();
        }

        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteEndDocument();
        xw.Close();

    }

}
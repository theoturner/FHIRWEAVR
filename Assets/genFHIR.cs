// Scripts for generating FHIR Documents and Dveice Profiles

//TODO 1-2
//TODO Verify with a FHIR team member that you have all required elements
//TODO Is using static this much bad? Try converting to singleton.
//TODO for documentation: document manual initial creation of device profile, will be created on document creation if does not exist.

using System.IO;
using System.Xml;

//TODO1 push with HTML post. MUST PUSH DEVICE PROFILE ALSO!
//TODO2 Find out how to overwrite old XML entries for continuously updating (current) metrics ==========

class GenFHIR
{

    public static void Device() // Put parameters here if you need them.
    {
        XmlWriter xw = XmlWriter.Create("VirZOOM-device-profile.xml");

        xw.WriteStartDocument();

        xw.WriteStartElement("Device", "http://hl7.org/fhir");

        xw.WriteStartElement("identifier");
        xw.WriteString("FHIRWEAVR-device");
        xw.WriteFullEndElement();

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "active"); //TODO2 read and change to inactive if device disconnected
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

        if (!(File.Exists(@"VirZOOM-device.xml")))
        {
            Device();
        }

        double[] output = new double[7];
        output = GetData.GetAllData(type); // type handling done in getData.cs

        // Get date/time immediately after getting data
        string dateTime = DateTimeFormats.GetDT("full");
        string fileDateTime = DateTimeFormats.GetDT("filename");

        XmlWriter xw = XmlWriter.Create("VirZOOM-output-" + fileDateTime + ".xml");
        int i;
        string[] identifiers = { "distance", "speed", "resistance", "heartrate", "rotation", "lean", "incline" };

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
        xw.WriteAttributeString("value", "final"); //TODO2 change to 'amended' if changed
        xw.WriteFullEndElement();

        xw.WriteStartElement("type");
        xw.WriteStartElement("coding");
        xw.WriteStartElement("system");
        xw.WriteAttributeString("value", "http://loinc.org");
        // Do we need a 'code' sub-element of 'coding'? See FHIR document example.
        xw.WriteFullEndElement();
        xw.WriteEndElement();
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "VirZOOM output");
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "final"); //TODO2 change to 'amended' if changed
        xw.WriteFullEndElement();

        xw.WriteStartElement("subject");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", "VirZOOM-device.xml"); //change if you change directory structure
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("date");
        xw.WriteAttributeString("value", dateTime);
        xw.WriteFullEndElement();

        xw.WriteStartElement("author");
        xw.WriteStartElement("reference");
        xw.WriteAttributeString("value", "VirZOOM-device-profile.xml"); //change if you change directory structure
        xw.WriteFullEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("title");
        xw.WriteString("VirZOOM output");
        xw.WriteFullEndElement();


        // FHIR List definitions
        xw.WriteStartElement("List");

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "current"); //TODO2 overwrite to 'retired' if new document made ==========
        xw.WriteFullEndElement();

        xw.WriteStartElement("mode");
        xw.WriteAttributeString("value", "snapshot");
        xw.WriteFullEndElement();


        // FHIR List entries
        for (i = 0; i < 7; i++)
        {
            xw.WriteStartElement("entry");
            xw.WriteStartElement("item");
            xw.WriteAttributeString(identifiers[i], output[i].ToString());
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
// Scripts for generating FHIR Documents and Dveice Profiles

using System.IO;
using System.Xml;
//TODO1-3 !!!!!!!!!!!!!!!!!!!!!!!!!

// VERIFY WITH SOMEONE THAT YOU HAVE ALL REQUIRED ITEMS

//TODO3 Find out how to overwrite old XML entries for continuously updating (current) metrics ==========

class genFHIR
{

    public static void Device() // Put parameters here if you need them. Document manual initial creation of device profile, will be created on document creation if does not exist.
    {
        XmlWriter xw = XmlWriter.Create("VirZOOM-device-profile.xml");

        xw.WriteStartDocument();

        xw.WriteStartElement("Device", "http://hl7.org/fhir");

        xw.WriteStartElement("identifier");
        xw.WriteString("FHIRWEAVR-device");
        xw.WriteEndElement();

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "active"); // read and change to inactive if device disconnected?
        xw.WriteEndElement();

        xw.WriteStartElement("type");
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "Networked virtual-reality-enabled stationary exercise bicycle");
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("manufacturer");
        xw.WriteAttributeString("value", "VirZOOM Inc.");
        xw.WriteEndElement();

        xw.WriteStartElement("model");
        xw.WriteAttributeString("value", "Beta bicycle"); // If FHIRWEAVR is made to work with future versions, ammend to read and set model accordingly
        xw.WriteEndElement();

        xw.WriteStartElement("contact");
        xw.WriteString("VirZOOM: info@virzoom.com - FHIRWEAVR: zcabttu@ucl.ac.uk");
        xw.WriteEndElement();

        xw.WriteStartElement("safety");
        xw.WriteString("Consult a medical professional before use. Use only on a level surface in a clear space and ensure that the bicycle is properly assembled. Do not exceed weight limit.");
        xw.WriteEndElement();

        xw.WriteEndElement();

        xw.WriteEndDocument();
        xw.Close();

    }

    public static void Document(string type) // put arguments in if you want them
    {

        if (!(File.Exists(@"VirZOOM-device.xml")))
        {
            Device();
        }

        double[] output = new double[7];
        output = getData.GetAllData(type); // type handling done in getData.cs

        //TODO2 Read in some timestamp info and change file name based on it ==========
        XmlWriter xw = XmlWriter.Create("VirZOOM-output.xml");
        int i;
        string[] identifiers = { "distance", "speed", "resistance", "heartrate", "rotation", "lean", "incline" };

        xw.WriteStartDocument();

        xw.WriteStartElement("Bundle", "http://hl7.org/fhir");

        xw.WriteStartElement("meta");
        xw.WriteStartElement("lastUpdated");
        xw.WriteAttributeString("value", "[DATETIME]"); //TODO2 read in ==========
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("identifier");
        xw.WriteString("FHIRWEAVR-output");
        xw.WriteEndElement();

        xw.WriteStartElement("type");
        xw.WriteAttributeString("value", "document");
        xw.WriteEndElement(); // Can we make this a single-line end tag? i.e. /> THIS NEEDS TO BE APPLIED TO ALL

        xw.WriteStartElement("entry");
        xw.WriteStartElement("resource");
        xw.WriteStartElement("Composition");

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "final"); //TODO2 change to 'amended' if changed
        xw.WriteEndElement();

        xw.WriteStartElement("type");
        xw.WriteStartElement("coding");
        xw.WriteStartElement("system");
        xw.WriteAttributeString("value", "http://loinc.org");
        // Do we need a 'code' sub-element of 'coding'? See FHIR document example.
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteStartElement("text");
        xw.WriteAttributeString("value", "VirZOOM output");
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "final"); //TODO2 change to 'amended' if changed
        xw.WriteEndElement();

        xw.WriteStartElement("subject");
        xw.WriteString("Reference(Device)"); //TODO1 read how to create reference and create a Device record for the VirZOOM ==========
        xw.WriteEndElement();

        xw.WriteStartElement("date");
        xw.WriteAttributeString("value", "[dateTime]"); //TODO1 read in date + time and give in exact FHIR format ==========
        xw.WriteEndElement();

        xw.WriteStartElement("author");
        xw.WriteString("Reference(Device)"); //TODO1 read how to create reference and create a Device record for the VirZOOM ==========
        xw.WriteEndElement();

        xw.WriteStartElement("title");
        xw.WriteString("VirZOOM output");
        xw.WriteEndElement();


        // FHIR List definitions
        xw.WriteStartElement("List");

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "current"); //TODO2 overwrite to 'retired' if new document made ==========
        xw.WriteEndElement();

        xw.WriteStartElement("mode");
        xw.WriteAttributeString("value", "working"); //TODO2 change to 'snapshot' when exporting halfway through session or new document made ==========
        xw.WriteEndElement();


        // FHIR List entries
        for (i = 0; i < 7; i++)
        {
            xw.WriteStartElement("entry");
            xw.WriteStartElement("item");
            xw.WriteAttributeString(identifiers[i], output[i].ToString());
            xw.WriteEndElement();
            xw.WriteEndElement();
        }

        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteEndDocument();
        xw.Close();
    }
}
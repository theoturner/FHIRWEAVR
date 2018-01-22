using System.Xml;
using UnityEngine;
//TODO0-4 !!!!!!!!!!!!!!!!!!!!!!!!!

// ================================================= RUN BEFORE LINKING getData to see document generates correctly
// VERIFY WITH SOMEONE THAT YOU HAVE ALL REQUIRED ITEMS

//TODO4 Find out how to overwrite old XML entries for continuously updating (current) metrics ==========

class createFHIRDoc
{

    //TODO1 create method to read in from getData and put in a format able to be written by createFHIRDoc. Maybe class instead?


    public static void Main() // put arguments in if you want them
    {
        //TODO3 Read in some timestamp info and change file name based on it ==========
        XmlWriter xw = XmlWriter.Create("results.xml");
        int i;
        string[] identifiers = { "distance", "speed", "resistance", "heartrate", "rotation", "lean", "incline" };
        
        //TODO0 create document start information (before List start)

        xw.WriteStartDocument();

        xw.WriteStartElement("Bundle", "http://hl7.org/fhir");
        //xw.WriteAttributeString("xmlns", "http://hl7.org/fhir");

        xw.WriteStartElement("meta");
        xw.WriteStartElement("lastUpdated");
        xw.WriteAttributeString("value", "[DATETIME]"); //TODO3 read in ==========
        xw.WriteEndElement();
        xw.WriteEndElement();

        xw.WriteStartElement("identifier");
        xw.WriteString("FHIRWEAVR");
        xw.WriteEndElement();

        xw.WriteStartElement("type");
        xw.WriteAttributeString("value", "document");
        xw.WriteEndElement(); // Can we make this a single-line end tag? i.e. />

        xw.WriteStartElement("entry");
        xw.WriteStartElement("resource");
        xw.WriteStartElement("Composition");

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "final"); //TODO3 change to 'amended' if changed
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
        xw.WriteAttributeString("value", "final"); //TODO3 change to 'amended' if changed
        xw.WriteEndElement();

        xw.WriteStartElement("subject");
        xw.WriteString("Reference(Device)"); //TODO2 read how to create reference and create a Device record for the VirZOOM ==========
        xw.WriteEndElement();

        xw.WriteStartElement("date");
        xw.WriteAttributeString("value", "[dateTime]"); //TODO2 read in date + time and give in exact FHIR format ==========
        xw.WriteEndElement();

        xw.WriteStartElement("author");
        xw.WriteString("Reference(Device)"); //TODO2 read how to create reference and create a Device record for the VirZOOM ==========
        xw.WriteEndElement();

        xw.WriteStartElement("title");
        xw.WriteString("VirZOOM output");
        xw.WriteEndElement();


        // FHIR List definitions
        xw.WriteStartElement("List");
        //xw.WriteAttributeString("xmlns", "http://hl7.org/fhir"); Don't think we need this if it's defined for the bundle

        xw.WriteStartElement("status");
        xw.WriteAttributeString("value", "current"); //TODO3 overwrite to 'retired' if new document made ==========
        xw.WriteEndElement();

        xw.WriteStartElement("mode");
        xw.WriteAttributeString("value", "working"); //TODO3 change to 'snapshot' when exporting halfway through session or new document made ==========
        xw.WriteEndElement();

        ///////////////////////////
        //TODO1 read in the below//
        ///////////////////////////
        // FHIR List entries
        for (i = 0; i < 7; i++)
        {
            xw.WriteStartElement("entry");
            xw.WriteStartElement("item");
            xw.WriteAttributeString(identifiers[i], "[VALUE]"); //TODO1 Read in value
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
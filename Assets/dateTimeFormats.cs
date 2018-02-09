// Outputs current date/time in multiple FHIR-compliant formats.

using System;
using UnityEngine;

//TODO CHECK ALL OF THESE WORK
//TODO Anything wrong with making this static? User would have to instantiate DateTimeFormats otherwise.

public class DateTimeFormats
{
    //TODO Anything wrong with making this static? User would have to instantiate DateTimeFormats otherwise.
	public static string GetDT(string type)
	{
        string dtFormatted;
        DateTime dt = DateTime.Now;
        // Format for use in file names
        if (type == "filename")
        {
            dtFormatted = dt.ToString("MM-dd-HH\"h\"mm\"m\"ss\"s\"");
        }
        // Date only
        else if (type == "date")
        {
            dtFormatted = dt.ToString("yyyy-MM-dd");
        }
        // Time only
        else if (type == "time")
        {
            dtFormatted = dt.ToString("HH-mm-ssZzzz");
        }
        // Fully-formed FHIR datatype dateTime, required in elements such as <date>
        // If other prarameter inputted, defualts to this - slightly more efficient in a single else
        else
        {
            if (type != "full")
            {
                Debug.Log("That type does not exist. Defaulting to FHIR fatatype dateTime. Types are 'filename,' 'date,' 'time,' and 'full.'");
            }
            dtFormatted = dt.ToString("yyyy-MM-ddTHH:mm:ssZzzz");
        }
        return dtFormatted;
    }
}

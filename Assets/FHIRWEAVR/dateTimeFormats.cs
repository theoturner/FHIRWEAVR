// Outputs current date and time in multiple FHIR-compliant formats.

using System;
using UnityEngine;

public class DateTimeFormats
{

    public static string GetDT(string type)
    {
        string dtFormatted;
        DateTime dt = DateTime.Now;

        switch (type)
        {
            // Format for use in file names
            case "filename":
                dtFormatted = dt.ToString("MM-dd-HH\"h\"mm\"m\"ss\"s\"");
                break;

            // Date only
            case "date":
                dtFormatted = dt.ToString("yyyy-MM-dd");
                break;

            // Time only
            case "time":
                dtFormatted = dt.ToString("HH-mm-ssZzzz");
                break;

            // Fully-formed FHIR datatype dateTime, required in elements such as <date>
            case "full":
                dtFormatted = dt.ToString("yyyy-MM-ddTHH:mm:ssZzzz");
                break;

            default:
                Debug.LogError("That type does not exist. Defaulting to FHIR datatype dateTime. Types are 'filename,' 'date,' 'time,' and 'full.'");
                dtFormatted = dt.ToString("yyyy-MM-ddTHH:mm:ssZzzz");
                break;
        }

        return dtFormatted;
    }

}

using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://manushri1415.github.io/CSE445-XML-Project/Hotels.xml";
        public static string xmlErrorURL = "https://manushri1415.github.io/CSE445-XML-Project/HotelsErrors.xml";
        public static string xsdURL = "https://manushri1415.github.io/CSE445-XML-Project/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdUrl); // Add the schema to the settings object
            // Set the validation type to Schema
            settings.ValidationType = ValidationType.Schema;
            string error = "No Error";
            //Add a ValidationEventHandler to capture errors if there is a validation error, set the 'error' variable to the error message.
            settings.ValidationEventHandler += (sender, e) => 
            {
                error = e.Message;
            };
            using (XmlReader reader = XmlReader.Create(xmlUrl, settings)) // Create an XmlReader with the settings
            {
                while (reader.Read()) ;
            }
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
            return error;
        }

        public static string Xml2Json(string xmlUrl)
        {
            XmlDocument xd = new XmlDocument(); //using xmlDocument to load the XML from the given URL
            xd.Load(xmlUrl);
            string jsonText = JsonConvert.SerializeXmlNode(xd); // Convert the XML document to JSON string using Newtonsoft.Json package
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }
    }

}

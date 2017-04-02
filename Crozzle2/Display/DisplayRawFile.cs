using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle2
{
    static class DisplayRawFile
    {
        public static HTML File(string filepath, string title)
        {
            Log.New("Raw file displayed in WebBrowser: " + filepath +".");

            // Initilise html page
            HTML html = CrozzleHTML.Initialize();
            html.AppendStyle(".file {margin:10px 0; padding:10px 22px; background-color:#EEE; font-family:'Courier New', sans-serif;}");
            html.AppendStyle(".file ol {word-break:break-all;}");

            html.Append("<H1>"+title+"</h1>");
            html.Append("<H2>" + filepath + "</h2>");
            html.Append("<div class='file'><ol>");

            // Attempt to open file and add each line to the html           
            StreamReader sr = new StreamReader(filepath);
            try
            {
                while (!sr.EndOfStream)
                {
                    html.Append("<li>" + sr.ReadLine() + "</li>");
                }
            }
            catch (Exception)
            {
                Log.New("Log File could not open properly.");
            }           
            sr.Close();

            html.Append("</ol></div>");

            // return the html page
            return html;
        }

    }
}

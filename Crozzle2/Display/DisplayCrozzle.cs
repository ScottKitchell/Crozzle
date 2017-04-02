using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crozzle2.CrozzleElements;

namespace Crozzle2
{
    static class DisplayCrozzle
    {
        public static HTML Using(Crozzle crozzle, CrozzleFile crozzleFile, ConfigFile configFile)
        {
            Log.New("Crozzle Display in WebBrowser.");
            HTML page = CrozzleHTML.Initialize();

            // Construct HTML page to display in a WebBrowser.
            page.Append("<h1>Crozzle</h1>");
            if (crozzleFile.ValidationResult == true)
            {
                ConfigRef Config = new ConfigRef();
                page.Append("<p>Difficulty: " + crozzle.Difficulty +"</p>");

                // Display Avalible Words as a table
                page.Append("<h2>Words</h2>");
                page.Append("<table>");
                int wordIndex = 0;
                while (wordIndex < crozzle.Wordlist.Count)
                {
                    page.Append("<tr>");
                    for (int cell = 0; cell < 5 && wordIndex < crozzle.Wordlist.Count; cell++)
                    {
                        page.Append("<td>" + crozzle.Wordlist[wordIndex] + "</td>");
                        wordIndex++;
                    }
                    page.Append("</tr>");
                }
                page.Append("</table>");

                // Display Score
                page.Append(string.Format("<h2>Score: " + crozzle.Score + "</h2>"));

                // Display Crozzle grid
                page.Append("<table class=\"Grid\">");
                page.Append("<tr><td class='header'></td>");
                for (int col = 1; col <= crozzle.Cols; col++)
                {
                    page.Append("<td class='header'>" + col + "</td>");
                }
                page.Append("</tr>");
                for (int row = 1; row <= crozzle.Rows; row++)
                {
                    page.Append("<tr><td class='header'>" + row + "</td>");
                    for (int col = 1; col <= crozzle.Cols; col++)
                    {
                        if (crozzle[row, col] != null)
                            page.Append("<td>" + crozzle[row, col] + "</td>");
                        else
                            page.Append("<td class='null'></td>");
                    }
                    page.Append("</tr>");
                }
                page.Append("</table>");
                page.Append("<p>" + crozzle.ActiveWordList.Count + " words out of " + crozzle.Wordlist.Count + " were used in " + crozzle.GroupCount + " group(s).</p>");

                // Display the rules validation results 
                if (crozzle.ValidationResult == false)
                {
                    page.Append("<h2>Crozzle Rules Broken</h2>");
                    page.Append("<ol>");
                    foreach (string error in crozzle.ValidationErrorList)
                    {
                        page.Append("<li>" + error + "</li>");
                    }
                    page.Append("</ol>");
                }
                else
                {
                    page.Append("<p>No rules broken!</p>");
                }
            }
            else
            {
                page.Append("<p>Crozzle cannot display due to file errors.</p>");
            }

            // Display the validation results 
            page.Append("<h1>Crozzle File Validation</h1>");
            if (crozzleFile.ValidationResult == false)
            {
                page.Append("<h2>Validation Errors</h2>");
                page.Append("<ol>");
                foreach (string error in crozzleFile.ValidationErrorList)
                {
                    page.Append("<li>" + error + "</li>");
                }
                page.Append("</ol>");
            }
            else
            {
                page.Append("<p>No validation errors!</p>");
            }
            
            // Display the config validation results 
            if (configFile.ValidationResult == false)
            {
                page.Append("<h2>Configuration File Validation Errors</h2>");
                page.Append("<ol>");
                foreach (string error in configFile.ValidationErrorList)
                {
                    page.Append("<li>" + error + "</li>");
                }
                page.Append("</ol>");
            }
            else
            {
                page.Append("<p>No configuration validation errors!</p>");
            }

            // Return the html page to display in the WebBrowser
            return page;
        }




    }
}

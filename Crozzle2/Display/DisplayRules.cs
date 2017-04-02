using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle2
{
    class DisplayRules
    {
        public static HTML GetPage()
        {
            Log.New("Crozzle rules displayed in WebBroswer.");

            // Initilise html page
            HTML html = CrozzleHTML.Initialize();

            html.Append("<H1>Crozzle Rules</h1>");
            html.Append("<H2>General</h2>");
            html.Append("<ol>");
            html.Append("<li>Each sequence of two or more horizontal(vertical) characters delimited by spaces or the crozzle edge must form a word that can be found in the wordlist.</li>");
            html.Append("<li>A word cannot be inserted in the crozzle more that once.</li>");
            html.Append("<li>A horizontal word can only run from left to right.</li>");
            html.Append("<li>A vertical word can only run from high to low.</li>");
            html.Append("<li>Diagonal sequences of characters, which can be formed by horizontal and vertical words, are not pertinent to scoring or validity.</li>");
            html.Append("</ol>");
            html.Append("<H2>Easy Difficulty</h2>");
            html.Append("<ol>");
            html.Append("<li>A horizontal word is limited to intersecting at least 1 and at most 2 other vertical words.</li>");
            html.Append("<li>A vertical word is limited to intersecting at least 1 and at most 2 other horizontal words.</li>");
            html.Append("<li>A horizontal word cannot touch any other horizontal word. That is, there must be at least one grid space between a horizontal word and any other horizontal word.</li>");
            html.Append("<li>A vertical word cannot touch any other vertical word. That is, there must be at least one grid space between a vertical word and any other vertical word.</li>");
            html.Append("</ol>");
            html.Append("<H2>Medium Difficulty</h2>");
            html.Append("<ol>");
            html.Append("<li>A horizontal word is limited to intersecting at least 1 and at most 3 other vertical words.</li>");
            html.Append("<li>A vertical word is limited to intersecting at least 1 and at most 3 other horizontal words.</li>");
            html.Append("<li>A horizontal word can touch another horizontal word, and a vertical word can touch another vertical word.However, you must adhere to the 1st common constraint.</li>");
            html.Append("</ol>");
            html.Append("<H2>Hard Difficulty</h2>");
            html.Append("<ol>");
            html.Append("<li>A horizontal word must intersect 1 or more vertical words.</li>");
            html.Append("<li>A vertical word must intersect 1 or more horizontal words.</li>");
            html.Append("</ol>");
            return html;
        }
    }
}

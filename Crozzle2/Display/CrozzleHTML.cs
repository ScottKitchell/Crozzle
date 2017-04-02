using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle2
{
    static class CrozzleHTML
    {
        public static HTML Initialize()
        {
            HTML html = new HTML();

            // Default CSS 
            html.AppendStyle("body {padding: 22px; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size:14px; color:#333333;}");
            html.AppendStyle("h1 {font-size:1.2em; color:#00BFFF;}");
            html.AppendStyle("h2 {font-size:1em; color:#222222;}");
            html.AppendStyle("table {width:100%; border-collapse:collapse; margin-bottom:14px; table-layout:fixed;}");
            html.AppendStyle("table.Grid td {border:solid 2px #000; background-color:#FFF; text-align:center; height:30px; font-weight:bold;}");
            html.AppendStyle("table.Grid td.null {background-color:#00BFFF;}");
            html.AppendStyle("table.Grid td.header {background-color:#00BFFF; color:#FFF;}");

            return html;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle2
{
    /// <summary>
    /// A HTML object to assist in creating HTML page strings
    /// </summary>
    class HTML
    {
        #region Properties

        private string _HTML;

        private string _CSS;
        /// <summary>
        /// A string containing all the CSS elements.
        /// </summary>
        public string CSS { get { return _CSS; } set { _CSS = value; } }

        private string _Body;
        /// <summary>
        /// A string containing all the HTML body elements.
        /// </summary>
        public string Body { get { return _Body; } set { _Body = value; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a HTML page.
        /// </summary>
        public HTML()
        {
            _CSS = BuildCSS();
            _Body = "";
            _HTML = BuildHTML();
        }

        #endregion

        #region Methods: BuildCSS(), BuildHTML(), AppendStyle(), Style()

        /// <summary>
        /// Builds the CSS for Crozzle display.
        /// </summary>
        /// <returns></returns>
        private string BuildCSS()
        {
            StringBuilder css = new StringBuilder();
            return css.ToString();
        }


        /// <summary>
        /// Builds the HTML page framework.
        /// </summary>
        /// <returns></returns>
        private string BuildHTML()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang=\"en\">");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset=\"utf-8\" />");
            html.AppendLine("<title>Crozzle Display</title>");
            html.AppendLine("<style>" + _CSS + "</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine(_Body);
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }


        /// <summary>
        /// Appends a new CSS style to the page.
        /// </summary>
        /// <param name="style"></param>
        public void AppendStyle(string style)
        {
            _CSS += style + "\r\n";
        }


        /// <summary>
        /// Appends a line to the body of the page.
        /// </summary>
        /// <param name="line"></param>
        public void Append(string line)
        {
            _Body += line + "\r\n";
        }


        /// <summary>
        /// Returns the page as a single string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            _HTML = BuildHTML();
            return _HTML;
        }

        #endregion
    }
}

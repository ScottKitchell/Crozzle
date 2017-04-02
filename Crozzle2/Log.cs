using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Crozzle2.CrozzleElements;

namespace Crozzle2
{
    /// <summary>
    /// Adds a log entry to the applications log file.
    /// </summary>
    static class Log
    {
        private static ConfigRef Config = new ConfigRef();

        #region Properties
        private static string _FileName = "\\log.txt";
        public static string FileName { get { return Environment.CurrentDirectory + _FileName; } set { _FileName = value; } }
        #endregion

        #region Methods: New Log
        /// <summary>
        /// Logs a line to the log file.
        /// </summary>
        /// <param name="line"></param>
        public static void New(String line)
        {
            // Append a line to the log file.
            try
            {
                DateTime lineMeta = DateTime.Now;
                var filepath = Environment.CurrentDirectory + _FileName;
                using (StreamWriter file = new StreamWriter(filepath, true))
                {
                    file.WriteLine(lineMeta + " : " + line);
                }
            }
            catch (Exception)
            {
                // Error logging.
            }
        }


        #endregion
    }
}

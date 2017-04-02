using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crozzle2.CrozzleElements
{
    /// <summary>
    /// Crozzle Configuration file.
    /// </summary>
    public class ConfigFile
    {
        // Constants
        private const int LineRef_MaxGroups = 0;
        private const int LineRef_PointsPerWord = 1;
        private const int LineRef_IntersectingWords = 2;
        private const int LineRef_NonIntersectingWords = 28;
        private char[] Letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        

        #region Properties

        private string _FileName;
        /// <summary>
        /// File path to the configuration file.
        /// </summary>
        public string FileName { get { return _FileName; } }

        private int _MaxGroups;
        public int MaxGroups { get { return _MaxGroups; } }

        private int _PointsPerWord;
        public int PointsPerWord { get { return _PointsPerWord; } }

        private int[] _NonIntersectingLetterPoints;
        public int[] NonIntersectingLetterPoints { get { return _NonIntersectingLetterPoints; } }

        private int[] _IntersectingLetterPoints;
        public int[] IntersectingLettePoints { get { return _IntersectingLetterPoints; } }

        private List<string> _Content;
        /// <summary>
        /// A line by line file reference.
        /// </summary>
        public List<string> Content { get { return _Content; } }

        private bool _ValidationResult;
        /// <summary>
        /// A boolean representing if validation succeeded. 
        /// </summary>
        public bool ValidationResult { get { return _ValidationResult; } }

        private List<string> _ValidationErrorList;
        /// <summary>
        /// A list of all errors generated during validation.
        /// </summary>
        public List<string> ValidationErrorList { get { return _ValidationErrorList; } }

        #endregion

        #region Constructors
        /// <summary>
        /// Will open and validate a Crozzle txt file.
        /// </summary>
        /// <param name="fileName"></param>
        public ConfigFile(string filePath)
        {
            _FileName = filePath;
            _NonIntersectingLetterPoints = new int[26];
            _IntersectingLetterPoints = new int[26];
            _Content = new List<string>();
            _ValidationErrorList = new List<string>();
            _ValidationResult = false;
            if (OpenFile())
                try
                {
                    Log.New("Configuration file opened. File Validation Started.");
                    _ValidationResult = ValidateFile();
                    Log.New("Configuration file validation complete. Status: " + _ValidationResult + ".");
                }
                catch (Exception)
                {
                    Log.New("There was an error opening the Configuration file.");
                    _ValidationResult = false;
                }
        }
        #endregion

        #region Methods: OpenFile
        /// <summary>
        /// Opens the Crozzle txt file and stores its contents in Content.
        /// </summary>
        /// <returns>Returns true if file opens.</returns>
        public bool OpenFile()
        {
            bool result = true;
            try
            {
                // For each line ensure all field data is alphanumeric. Add the line to Content.
                using (StreamReader sr = new StreamReader(_FileName))
                {
                    int lineIndex = 1;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        _Content.Add(line);
                        Regex regex = new Regex(@"^[A-Za-z0-9:=]*$");
                        Match match = regex.Match(line);
                        if (!match.Success)
                        {
                            ValidationErrorList.Add("File contains invalid characters on line " + lineIndex + ".");
                            Log.New("File contains invalid characters on line " + lineIndex + ".");
                        }
                        lineIndex++;
                    }
                }
            }
            catch (Exception)
            {
                _ValidationErrorList.Add("Configuration file could not open.");
                Log.New("Configuration file could not open.");
                result = false;
            }

            // Return True if the file opens correctly.
            return result;
        }
        #endregion

        #region Validate File
        /// <summary>
        /// Validates the Configuration file given. Class properties are set once validated.
        /// </summary>
        /// <returns>Returns True if no error are present.</returns>
        public bool ValidateFile()
        {
            bool result = true;

            // Validate Groups per crozzle
            if (Validate_MaxGroups() != true)
                result = false;

            // Validate Points per word
            if (Validate_PointsPerWord() != true)
                result = false;

            // Validate Intersecting Letters
            if (Validate_IntersectingWords() != true)
                result = false;

            // Validate NonIntersecting Letters
            if (Validate_NonIntersectingWords() != true)
                result = false;

            // Return True if validation was successfull;
            return result;
        }

        // Validate the Maximum groups.
        private bool Validate_MaxGroups()
        {
            bool result = true;
            try
            {
                string line = _Content[LineRef_MaxGroups];
                Regex regex = new Regex(@"GROUPSPERCROZZLELIMIT\=(\d+)");
                Match match = regex.Match(line);
                if (match.Success)
                    _MaxGroups = Convert.ToInt32(match.Groups[1].Value);        
            }
            catch (Exception e)
            {
                _ValidationErrorList.Add("There was an error retrieving the number of groups allowed per Crozzle indicated in the configuration file. " + e.Message);
                Log.New("There was an error retrieving the number of groups allowed per Crozzle indicated in the configuration file. " + e.Message);
                result = false;
            }
            return result;
        }

        // Validate the point per words.
        private bool Validate_PointsPerWord()
        {
            bool result = true;
            try
            {
                string line = _Content[LineRef_PointsPerWord];
                Regex regex = new Regex(@"POINTSPERWORD\=(\d+)");
                Match match = regex.Match(line);
                if (match.Success)
                    _PointsPerWord = Convert.ToInt32(match.Groups[1].Value);
            }
            catch (Exception e)
            {
                _ValidationErrorList.Add("There was an error retrieving the number of points per indicated in the configuration file. " + e.Message);
                Log.New("There was an error retrieving the number of points per indicated in the configuration file. " + e.Message);
                result = false;
            }
            return result;
        }

        // Validite intersecting words
        private bool Validate_IntersectingWords()
        {
            bool result = true;
            int letterIndex = 0;

            // Validate each letter
            for (int lineIndex = LineRef_IntersectingWords; lineIndex < (LineRef_IntersectingWords + 26); lineIndex++)
            {
                try
                {
                    string line = _Content[lineIndex];
                    Regex regex = new Regex(@"INTERSECTING:" + Letters[letterIndex] + @"\=(\d+)");
                    Match match = regex.Match(line);
                    if (match.Success)
                        _IntersectingLetterPoints[letterIndex] = Convert.ToInt32(match.Groups[1].Value);
                    else
                    {
                        _ValidationErrorList.Add("There was a configuration error retrieving the intersecting points for the letter \'" + Letters[letterIndex] + "\'.");
                        Log.New("There was a configuration error retrieving the intersecting points for the letter \'" + Letters[letterIndex] + "\'.");
                        result = false;
                    }
                }
                catch (Exception e)
                {
                    _ValidationErrorList.Add("There was a configuration error retrieving the intersecting points for the letter \'" + Letters[letterIndex] + "\'. " + e.Message);
                    Log.New("There was a configuration error retrieving the intersecting points for the letter \'" + Letters[letterIndex] + "\'. " + e.Message);
                    result = false;
                }
                finally
                {
                    letterIndex++;
                }
            }
            return result;
        }

        // Validate non-intersecting words
        private bool Validate_NonIntersectingWords()
        {
            bool result = true;
            int letterIndex = 0;

            // Validate each letter
            for (int lineIndex = LineRef_NonIntersectingWords; lineIndex < (LineRef_NonIntersectingWords + 26); lineIndex++)
            {
                try
                {
                    string line = _Content[lineIndex];
                    Regex regex = new Regex(@"NONINTERSECTING:" + Letters[letterIndex] + @"\=(\d+)");
                    Match match = regex.Match(line);
                    if (match.Success)
                        _NonIntersectingLetterPoints[letterIndex] = Convert.ToInt32(match.Groups[1].Value);
                    else
                    {
                        _ValidationErrorList.Add("There was a configuration error retrieving the nonintersecting points for the letter \"" + Letters[letterIndex] + "\".");
                        Log.New("There was a configuration error retrieving the nonintersecting points for the letter \"" + Letters[letterIndex] + "\".");
                        result = false;
                    }
                }
                catch (Exception e)
                {
                    _ValidationErrorList.Add("There was a configuration error retrieving the nonintersecting points for the letter \"" + Letters[letterIndex] + "\". " + e.Message);
                    Log.New("There was a configuration error retrieving the nonintersecting points for the letter \"" + Letters[letterIndex] + "\". " + e.Message);
                    result = false;
                }
                finally
                {
                    letterIndex++;
                }
            }
            return result;
        }
        #endregion
    }
}

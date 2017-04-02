using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Crozzle2.CrozzleElements
{
    /// <summary>
    /// Will open and validate a Crozzle txt file.
    /// </summary>
    public class CrozzleFile
    {
        ConfigRef Config = new ConfigRef();

        #region Class Constants

        // File Configuration
        private enum SectionIndex { Header, Words, WordsUsed }
        private const int LineRef_Header = 0;
        private const int LineRef_Words = 1;
        private const int LineRef_WordsUsed = 2;
        private const char separator = ',';
        // Header Configuration
        private const int HeaderNumbeOfFields = 6;

        // WordsUsed Configuration
        private const int WordsUsedNumbeOfFields = 4;
        private enum WordsUsedIndex { Orientation, Row, Column, Word }

        #endregion

        #region Properties

        private string _FileName;
        /// <summary>
        /// The file path to the Crozzle data file.
        /// </summary>
        public string FileName { get { return _FileName; } }

        private string _Difficulty;
        /// <summary>
        /// The Crozzle difficulty specified in the header.
        /// </summary>
        public string Difficulty { get { return _Difficulty; } }

        private int _WordlistCount;
        /// <summary>
        /// The number of listed words specified in the header.
        /// </summary>
        public int WordlistCount { get { return _WordlistCount; } }

        private int _Rows;
        /// <summary>
        /// The number of rows specified in the header.
        /// </summary>
        public int Rows { get { return _Rows; } }

        private int _Cols;
        /// <summary>
        /// The number of columns specified in the header.
        /// </summary>
        public int Cols { get { return _Cols; } }

        private int _HorizontalWordCount;
        /// <summary>
        /// The number of horizontally placed words specified in the header.
        /// </summary>
        public int HorizontalWordCount { get { return _HorizontalWordCount; } }

        private int _VerticalWordCount;
        /// <summary>
        /// The number of vertically placed words specified in the header.
        /// </summary>
        public int VerticalWordCount { get { return _VerticalWordCount; } }

        private List<string> _Content;
        /// <summary>
        /// A line by line file reference.
        /// </summary>
        public List<string> Content { get { return _Content; } }

        private List<Word> _Words;
        /// <summary>
        /// A list of avalible words.
        /// </summary>
        public List<Word> Words { get { return _Words; } }

        private List<ActiveWord> _ActiveWords;
        /// <summary>
        /// A list of words used in the Crozzle.
        /// </summary>
        public List<ActiveWord> ActiveWords { get { return _ActiveWords; } }

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
        public CrozzleFile(string fileName)
        {
            _FileName = fileName;
            _Content = new List<string>();
            _Words = new List<Word>();
            _ActiveWords = new List<ActiveWord>();
            _ValidationErrorList = new List<string>();

            if (OpenFile(_FileName))
                try
                {
                    Log.New("Crozzle file opened. File validation started.");
                    _ValidationResult = ValidateFile();
                    Log.New("Crozzle file validation complete. Status: " + _ValidationResult +".");
                }
                catch (Exception)
                {
                    Log.New("There was an error opening the Configuration file.");
                    _ValidationResult = false;
                }

        }

        /// <summary>
        /// Will construct a Crozzle file form a Crozzle.
        /// </summary>
        /// <param name="crozzle"></param>
        public CrozzleFile(Crozzle crozzle)
        {
            _Difficulty = crozzle.Difficulty;
            _WordlistCount = crozzle.Wordlist.Count;
            _Rows = crozzle.Rows;
            _Cols = crozzle.Cols;
            _HorizontalWordCount = 0;
            _VerticalWordCount = 0;
            _Words = crozzle.Wordlist;
            _ActiveWords = crozzle.ActiveWordList;
            _Content = new List<string>();
            foreach(ActiveWord word in _ActiveWords)
            {
                if (word.Orientation == Config.HorizontalKeyWord)
                    _HorizontalWordCount++;
                else if (word.Orientation == Config.VerticalKeyWord)
                    _VerticalWordCount++;
            }
            ConstructContent();
        }

        #endregion

        #region Methods: OpenFile(), SaveFile(), ConstructContent()
        /// <summary>
        /// Opens the Crozzle txt file and stores its contents in Content.
        /// </summary>
        /// <returns>Returns true if file opens.</returns>
        public bool OpenFile(string fileName)
        {
            bool result = true;
            StreamReader sr = new StreamReader(fileName);
            try
            {
                // For each line ensure all field data is alphanumeric. Add the line to Content.
                int lineIndex = 1;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    _Content.Add(line);
                    Regex regex = new Regex(@"^[A-Za-z0-9\,]*$");
                    Match match = regex.Match(line);
                    if (!match.Success)
                    {
                        ValidationErrorList.Add("File contains invalid characters on line " + lineIndex + ".");
                        Log.New("File contains invalid characters on line " + lineIndex + ".");
                    }
                    lineIndex++;
                }
            }
            catch (Exception)
            {
                _ValidationErrorList.Add("File could not open.");
                Log.New("File could not open.");
                result = false;
            }
            sr.Close();

            // Return OpenFile() result.
            return result;
        }

        /// <summary>
        /// Saves the Crozzle to a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool SaveFile(string fileName)
        {
            bool result = true;
            _FileName = fileName;
            // Try and write each line of the content to the file 
            try
            {
                File.Create(fileName).Close();
                using (StreamWriter file = new StreamWriter(fileName, true))
                {
                    foreach(string line in _Content)
                        file.WriteLine(line);
                }
            }
            catch (Exception)
            {
                result = false;
            }

            // Return SaveFile() result.
            return result;
        }

        private void ConstructContent()
        {
            // Add header
            string headerLine = _Difficulty + separator + _WordlistCount + separator + _Rows + separator + _Cols + separator + _HorizontalWordCount + separator + _VerticalWordCount ;
            _Content.Add(headerLine);

            // Add WordList
            string wordsLine = "";
            foreach(Word word in Words)
            {
                wordsLine += word.String + separator;
            }
            wordsLine.TrimEnd(separator);
            _Content.Add(wordsLine);

            // Add each Active Word 
            foreach(ActiveWord word in _ActiveWords)
            {
                string activeWordLine = word.Orientation + separator + word.RowStart + separator + word.ColStart + separator + word.String;
                _Content.Add(activeWordLine);
            }
        }

        #endregion

        #region Validate file
        /// <summary>
        /// Validates the Crozzle txt file. Any errors are stored in the ErrorList property.
        /// </summary>
        /// <returns>Returns true if the file is a valid Crozzle file.</returns>
        public bool ValidateFile()
        {
            // Every field within the file is seperated and temperarily stored as a part of it's section.
            bool result = true;

            // Validate Header section of file
            if (ValidateHeader() != true)
                result = false;

            // Validate Words section of file
            if (ValidateWords() != true)
                result = false;

            // Validate Data section of file
            if (ValidateWordsUsed() != true)
                result = false;

            // Return Validate() result.
            return result;
        }


        /// <summary>
        /// Validates the header of the Crozzle file.  Any errors are stored in the ErrorList property.
        /// </summary>
        /// <param name="tempHeader"></param>
        /// <returns>Returns true if the Header is valid.</returns>
        private bool ValidateHeader()
        {
            bool result = true;
            // Validate Header - Must contain a number fields equal to HeaderNumbeOfFields.
            string line = "";
            if (_Content.Count >= LineRef_Header)
                 line = _Content[LineRef_Header];
            string[] header = line.Split(separator); 

            if (header.Length != HeaderNumbeOfFields)
            {
                _ValidationErrorList.Add("Header has missing informaion.");
                Log.New("Header has missing informaion.");
                result = false;
            }
            // Validate Difficulty - Must be either EASY, MEDIUM or HARD.
            string difficulty = header[0];
            if (difficulty != Config.EasyKeyWord && difficulty != Config.MediumKeyWord && difficulty != Config.HardKeyWord)
            {
                _ValidationErrorList.Add("Difficulty is not recognised.");
                Log.New("Difficulty is not recognised.");
                result = false;
            }
            else
            {
                _Difficulty = difficulty;
            }

            // Validate Number of Words - Must be a positive number in the range of MinNumberOfWords to MaxNumberOfWords.
            try
            {
                int numberOfWords = Convert.ToInt32(header[1]);
                if (numberOfWords <= Config.MinNumberOfWords && numberOfWords >= Config.MaxNumberOfWords)
                {
                    _ValidationErrorList.Add("Number of Words indicated in the header is out of range.");
                    Log.New("Number of Words indicated in the header is out of range.");
                    result = false;
                }
                else
                {
                    _WordlistCount = numberOfWords;
                }
            }
            catch (Exception)
            {
                _ValidationErrorList.Add("Number of Words indicated in the header is not an a valid number.");
                Log.New("Number of Words indicated in the header is not an a valid number.");
                result = false;
            }

            // Validate Number of Rows - Must be a positive number in the range of MinNumberOfRows to MaxNumberOfRows.
            try
            {
                int numberOfRows = Convert.ToInt32(header[2]);
                if (numberOfRows < Config.MinNumberOfRows || numberOfRows > Config.MaxNumberOfRows)
                {
                    _ValidationErrorList.Add("Number of Rows indicated in the header is out of range.");
                    Log.New("Number of Rows indicated in the header is out of range.");
                    result = false;
                }
                else
                {
                    _Rows = numberOfRows;
                }
            }
            catch (Exception)
            {
                _ValidationErrorList.Add("Number of Rows indicated in the header is not an a valid number.");
                Log.New("Number of Rows indicated in the header is not an a valid number.");
                result = false;
            }

            // Validate Number of Columns - Must be a positive number in the range of MinNumberOfCols to MaxNumberOfCols.
            try
            {
                int numberOfCols = Convert.ToInt32(header[3]);
                if (numberOfCols < Config.MinNumberOfCols || numberOfCols > Config.MaxNumberOfCols)
                {
                    _ValidationErrorList.Add("Number of Columns indicated in the header is out of range.");
                    Log.New("Number of Columns indicated in the header is out of range.");
                    result = false;
                }
                else
                {
                    _Cols = numberOfCols;
                }
            }
            catch (Exception)
            {
                _ValidationErrorList.Add("Number of Columns indicated in the header is not an a valid number.");
                Log.New("Number of Columns indicated in the header is not an a valid number.");
                result = false;
            }

            // Validate Horizontal words - Must be a positive number.
            try
            {
                int numberOfHorizontalWords = Convert.ToInt32(header[4]);
                if (numberOfHorizontalWords < Config.MinNumberOfHorizontalWords)
                {
                    _ValidationErrorList.Add("Number of Horizontal Words indicated in the header is out of range.");
                    Log.New("Number of Horizontal Words indicated in the header is out of range.");
                    result = false;
                }
                else
                {
                    _HorizontalWordCount = numberOfHorizontalWords;
                }
            }
            catch (Exception)
            {
                _ValidationErrorList.Add("Number of Horizontal Words indicated in the header is not an a valid number.");
                Log.New("Number of Horizontal Words indicated in the header is not an a valid number.");
                result = false;
            }

            // Validate vertical words - Must be a positive number.
            try
            {
                int numberOfVerticalWords = Convert.ToInt32(header[5]);
                if (numberOfVerticalWords < Config.MinNumberOfVerticalWords)
                {
                    _ValidationErrorList.Add("Number of Vertical Words indicated in the header is out of range.");
                    Log.New("Number of Vertical Words indicated in the header is out of range.");
                    result = false;
                }
                else
                {
                    _VerticalWordCount = numberOfVerticalWords;
                }
            }
            catch (Exception)
            {
                _ValidationErrorList.Add("Number of Vertical Words indicated in the header is not an a valid number.");
                Log.New("Number of Vertical Words indicated in the header is not an a valid number.");
                result = false;
            }

            // Return ValidateHeader() result.
            return result;
        }


        /// <summary>
        /// Validates the avalible word list section of the Crozzle file. Any errors are stored in the ErrorList property.
        /// </summary>
        /// <param name="tempWords"></param>
        /// <returns>Returns true is the avalible words section of the file is valid.</returns>
        private bool ValidateWords()
        {
            bool result = true;

            string line = _Content[LineRef_Words];
            string[] words = _Content[LineRef_Words].Split(separator);

            // Validate each Word - Must not already be contained in the Word List
            foreach (string word in words)
            {
                foreach (Word listedWord in _Words)
                {
                    // Check word is not contained in list
                    if (word == listedWord.String)
                    {
                        _ValidationErrorList.Add("The Words List contains a duplicate of the word \"" + word + "\".");
                        Log.New("The Words List contains a duplicate of the word \"" + word + "\".");
                        result = false;
                    }
                }
                _Words.Add(new Word(word));
            }

            // Return ValidateWords() result.
            return result;
        }


        /// <summary>
        /// Validates the list of words used in the data section of the Crozzle file. Any errors are stored in the ErrorList property.
        /// </summary>
        /// <param name="tempWordsUsed"></param>
        /// <returns>Returns true if the used words section of the file is valid.</returns>
        private bool ValidateWordsUsed()
        {
            bool result = true;
            int horizontalWordsFound = 0;
            int verticalWordsFound = 0;
            if(_Content.Count >= LineRef_WordsUsed)
            {
                // for each data line
                for (int lineIndex = LineRef_WordsUsed; lineIndex < _Content.Count; lineIndex++)
                {
                    string line = _Content[lineIndex];
                    string[] wordUsedData = line.Split(separator);
                    ActiveWord word;

                    // Validate Word Line - Must have 4 fields.
                    if (wordUsedData.Length != WordsUsedNumbeOfFields)
                    {
                        _ValidationErrorList.Add("A word in the words used section has missing data on line " + lineIndex + ".");
                        Log.New("A word in the words used section has missing data on line " + lineIndex + ".");
                        result = false;
                    }

                    // Validate Word - Must contain a value of letters only.
                    word = new ActiveWord(wordUsedData[3]);
                    Regex regex = new Regex(@"^[A-Z]+$");
                    Match match = regex.Match(word.String);
                    if (!match.Success)
                    {
                        ValidationErrorList.Add("The word " + word + " in the Words Used List is not a valid word.");
                        Log.New("A word " + word + "in the Words Used List is not a valid word.");
                    }

                    // Validate Orientation - Must be either HORIZONTAL or VERTICAL.
                    string orientation = wordUsedData[0];
                    if (orientation != "HORIZONTAL" && orientation != "VERTICAL")
                    {
                        _ValidationErrorList.Add("The word " + word + " does not contain a valid orientation value.");
                        Log.New("The word " + word + " does not contain a valid orientation value.");
                        result = false;
                    }
                    else
                    {
                        word.Orientation = orientation;
                        if (word.Orientation == Config.HorizontalKeyWord)
                            horizontalWordsFound++;
                        else if (word.Orientation == Config.VerticalKeyWord)
                            verticalWordsFound++;
                    }

                    // Validate Starting Row - Must be a number greater than 0 and must allow full word to fit onto Crozzle grid.
                    int startRow = 0;
                    try
                    {
                        startRow = Convert.ToInt32(wordUsedData[(int)WordsUsedIndex.Row]);
                        word.RowStart = startRow;
                    }
                    catch (Exception)
                    {
                        _ValidationErrorList.Add("The word " + word + " starting row in not a valid number.");
                        Log.New("The word " + word + " starting row in not a valid number.");
                        result = false;
                    }
                    if (startRow < 1 || startRow > _Rows)
                    {
                        _ValidationErrorList.Add("The word " + word + " starting row is outside of the Crozzle grid.");
                        Log.New("The word " + word + " starting row is outside of the Crozzle grid.");
                        result = false;
                    }
                    if (word.Orientation == Config.VerticalKeyWord)
                    {
                        if ((startRow - 1) + word.Length > _Rows)
                        {
                            _ValidationErrorList.Add("The word " + word + " (" + orientation + " starting at [" + startRow + ", " + startRow + "]) extends below the Crozzle grid.");
                            Log.New("The word " + word + " (" + orientation + " starting at [" + startRow + ", " + startRow + "]) extends below the Crozzle grid.");
                            result = false;
                        }
                    }

                    // Validate Starting Column - Must be a number greater than 0 and must allow full word to fit onto Crozzle grid.
                    int startCol = 0;
                    try
                    {
                        startCol = Convert.ToInt32(wordUsedData[2]);
                        word.ColStart = startCol;
                    }
                    catch (Exception)
                    {
                        _ValidationErrorList.Add("The word " + word + " starting column in not a valid number.");
                        Log.New("The word " + word + " starting column in not a valid number.");
                        result = false;
                    }

                    if (startCol < 1 || startCol > _Cols)
                    {
                        _ValidationErrorList.Add("The word " + word + " starting column is outside of the Crozzle grid.");
                        Log.New("The word " + word + " starting column is outside of the Crozzle grid.");
                        result = false;
                    }
                    if (word.Orientation == Config.HorizontalKeyWord)
                    {
                        if ((startCol - 1) + word.Length > _Cols)
                        {
                            _ValidationErrorList.Add("The word " + word + " (" + orientation + " starting at [" + startRow + ", " + startRow + "]) extends outside of the Crozzle grid.");
                            Log.New("The word " + word + " (" + orientation + " starting at [" + startRow + ", " + startRow + "]) extends below the Crozzle grid.");
                            result = false;
                        }
                    }

                    // Add Word to WordsUsed list. 
                    _ActiveWords.Add(word);
                }
            }
            

            // Consistancy Check - The number of Horizontal and Vertical words must match the Header data.
            if (horizontalWordsFound != _HorizontalWordCount)
            {
                _ValidationErrorList.Add("The number of horizontal words found (" + horizontalWordsFound + ") doesn't match the number identified in the file header (" + _HorizontalWordCount + ").");
                Log.New("The number of horizontal words found (" + horizontalWordsFound + ") doesn't match the number identified in the file header (" + _HorizontalWordCount + ").");
                result = false;
            }
            if (verticalWordsFound != _VerticalWordCount)
            {
                _ValidationErrorList.Add("The number of vertical words found (" + verticalWordsFound + ") doesn't match the number identified in the file header (" + _VerticalWordCount + ").");
                Log.New("The number of vertical words found (" + verticalWordsFound + ") doesn't match the number identified in the file header (" + _VerticalWordCount + ").");
                result = false;
            }

            // Return ValidateWordsUsed() result.
            return result;
        }

        #endregion
    }
}
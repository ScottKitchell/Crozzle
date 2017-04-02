using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace Crozzle2.CrozzleElements
{
    public class Crozzle
    {
        ConfigRef Config = new ConfigRef();

        #region Constants

        private const int MaxBuildRunTime = 30000; // 5 minutes

        #endregion

        #region Properties

        private Board _Grid;
        /// <summary>
        /// The Crozzle board.
        /// </summary>
        public Board Grid { get { return _Grid; } set { _Grid = value; } }

        /// <summary>
        /// The number of rows in the Crozzle grid.
        /// </summary>
        public int Rows { get { return _Grid.Rows; } }

        /// <summary>
        /// The number of columns in the crozzle grid.
        /// </summary>
        public int Cols { get { return _Grid.Cols; } }

        private List<Word> _WordList;
        /// <summary>
        /// The list of avalible Crozzle words.
        /// </summary>
        public List<Word> Wordlist { get { return _WordList; } set { _WordList = value; } }

        private List<ActiveWord> _ActiveWordList;
        /// <summary>
        /// The list of words possitioned on the crozzle board.
        /// </summary>
        public List<ActiveWord> ActiveWordList { get { return _ActiveWordList; } }

        /// <summary>
        /// The overall Crozzle score.
        /// </summary>
        public int Score { get { return CalcScore(); } }

        private string _Difficulty;
        /// <summary>
        /// The Cozzle difficulty.
        /// </summary>
        public string Difficulty { get { return _Difficulty; } }        

        /// <summary>
        /// The number of word groups on the Crozzle grid.
        /// </summary>
        public int GroupCount { get { return _Grid.GroupCount; } }

        protected bool _ValidationResult;
        /// <summary>
        /// A boolean representing if validation succeeded. 
        /// </summary>
        public bool ValidationResult { get { return _ValidationResult; } }

        protected List<string> _ValidationErrorList;
        /// <summary>
        /// A list of all errors generated during validation.
        /// </summary>
        public List<string> ValidationErrorList { get { return _ValidationErrorList; } }

        private Timer timer;

        #endregion

        #region Constructors

        public Crozzle()
        {
            _Grid = new Board(Config.MinNumberOfRows, Config.MinNumberOfRows);
            _WordList = new List<Word>();
            _ActiveWordList = new List<ActiveWord>();
            _Difficulty = Config.Difficulty;

            _ValidationErrorList = new List<string>();
            _ValidationResult = true;
        }

        public Crozzle(int rows, int cols)
        {
            _Grid = new Board(rows, cols);
            _WordList = new List<Word>();
            _ActiveWordList = new List<ActiveWord>();
            _Difficulty = Config.Difficulty;

            _ValidationErrorList = new List<string>();
            _ValidationResult = true;
        }

        public void ConstructFromCrozzleFile(CrozzleFile crozzleFile)
        {
            _WordList = crozzleFile.Words;
            _ActiveWordList = crozzleFile.ActiveWords;
            _Difficulty = crozzleFile.Difficulty;
            
            Board board = new Board(crozzleFile.Rows, crozzleFile.Cols);
            board.AddWordList(_ActiveWordList);
            _Grid = board;
            _ValidationResult = Validate();
        }

        public void Construct(Board grid)
        {
            foreach (ActiveWord word in grid.ActiveWordList)
                _WordList.Add(word);
            _ActiveWordList = grid.ActiveWordList;
            _Difficulty = Config.Difficulty;
            
            _Grid = grid;
            _ValidationErrorList = new List<string>();
            _ValidationResult = Validate();
        }

        public Element this[int row, int col]
        {
            get { return _Grid[row, col]; }
            set { _Grid[row, col] = value; }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validate the crozzle.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            bool result = true;

            // Intiilise class validation properties
            _ValidationErrorList = new List<string>();
            _ValidationResult = true;

            // Check for duplicates.
            if (Validate_NoDuplicates() != true)
                result = false;

            // Check positioned words are in avalible word list.
            if (Validate_WordsAreListed() != true)
                result = false;

            // Check Groups is less than max allowed.
            if (Validate_MaxGroups() != true)
                result = false;

            // Check spacing around words is above the min allowed.
            if (Validate_Spacing() != true)
                result = false;

            // Check the number of Intersections is in allowable range.
            if (Validate_Intersections() != true)
                result = false;

            // Return the validate result;
            _ValidationResult = result;
            return result;
        }

        // Validate that no word duplicates exist on the crozzle grid.
        private bool Validate_NoDuplicates()
        {
            bool result = true;
            foreach (Word word in _WordList)
            {
                int wordCount = 0;
                foreach (ActiveWord listword in _ActiveWordList)
                {
                    if (word.String == listword.String)
                        wordCount++;                      
                }
                if(wordCount > 1)
                {
                    _ValidationErrorList.Add("The word " + word + " was used more than once.");
                    Log.New("The word " + word + " was used more than once.");
                    result = false;
                }
            }
            return result;
        }

        // Validate that words positioned on the grid are in the avalible words list.
        private bool Validate_WordsAreListed()
        {
            bool result = true;
            foreach(ActiveWord word in _Grid.ActiveWordList)
            {
                int wordCount = 0;
                foreach (Word listword in _WordList)
                {
                    if (word.String == listword.String)
                        wordCount++;
                }
                if (wordCount > 1)
                {
                    _ValidationErrorList.Add("The word " + word + " is not one of the listed Crozzle words.");
                    Log.New("The word " + word + " is not one of the listed Crozzle words.");
                    result = false;
                }
            }
            return result;
        }

        // Validate that the number of word groups is less than the max allowed.
        private bool Validate_MaxGroups()
        {
            bool result = true;
            if(_Grid.GroupCount > Config.GroupsPerCrozzleLimit)
            {
                _ValidationErrorList.Add("The number of word groups ("+_Grid.GroupCount+") exceeds the groups per Crozzle limit of "+Config.GroupsPerCrozzleLimit+".");
                Log.New("The number of word groups (" + _Grid.GroupCount + ") exceeds the groups per Crozzle limit of " + Config.GroupsPerCrozzleLimit + ".");
                result = false;
            }
            return result;
        }

        // Validate the spacing around words is above the min allowed.
        private bool Validate_Spacing()
        {
            bool result = true;
            for(int rowIndex = 0; rowIndex <= _Grid.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex <= _Grid.Cols; colIndex++)
                {
                    if (_Grid[rowIndex, colIndex] != null)
                    {
                        Element element = _Grid[rowIndex, colIndex];
                        // Check horizontal spacing
                        if (colIndex+1 <= _Grid.Cols)
                        {
                            Element elementToRight = _Grid[rowIndex, colIndex + 1];
                            if(elementToRight != null)
                            {
                                if (element.HorizontalWord != null && element.HorizontalWord != elementToRight.HorizontalWord)
                                {
                                    _ValidationErrorList.Add("The word in the grid square [" + rowIndex + "," + (colIndex + 1) + "] is too close to the horizontal word " + element.HorizontalWord + ".");
                                    Log.New("The word in the grid square [" + rowIndex + "," + (colIndex + 1) + "] is too close to the horizontal word " + element.HorizontalWord + ".");
                                    result = false;
                                }
                                else if (Config.MinWordSpacing > 0 && element.VerticalWord != null && elementToRight.VerticalWord != null)
                                {
                                    _ValidationErrorList.Add("The word in the grid square [" + rowIndex + "," + (colIndex + 1) + "] is too close to the horizontal word " + element.HorizontalWord + ".");
                                    Log.New("The word in the grid square [" + rowIndex + "," + (colIndex + 1) + "] is too close to the horizontal word " + element.HorizontalWord + ".");
                                    result = false;
                                }
                            }                            
                        }
                        // Check vertical spacing
                        if (rowIndex + 1 <= _Grid.Rows)
                        {
                            Element elementBelow = _Grid[rowIndex + 1, colIndex];
                            if(elementBelow != null)
                            {
                                if (element.VerticalWord != null && element.VerticalWord != elementBelow.VerticalWord)
                                {
                                    _ValidationErrorList.Add("The word in the grid square [" + (rowIndex + 1) + "," + colIndex + "] is too close to the vertical word " + element.VerticalWord + ".");
                                    Log.New("The word in the grid square [" + (rowIndex + 1) + "," + colIndex + "] is too close to the vertical word " + element.VerticalWord + ".");
                                    result = false;
                                }
                                else if (Config.MinWordSpacing > 0 && element.HorizontalWord != null && elementBelow.HorizontalWord != null)
                                {
                                    _ValidationErrorList.Add("The word in the grid square [" + (rowIndex + 1) + "," + colIndex + "] is too close to the vertical word " + element.VerticalWord + ".");
                                    Log.New("The word in the grid square [" + (rowIndex + 1) + "," + colIndex + "] is too close to the vertical word " + element.VerticalWord + ".");
                                    result = false;
                                }
                            }                          
                        }

                    }
                        
                }
            }
            return result;
        }

        // Validate the number of intersections is in range.
        private bool Validate_Intersections()
        {
            bool result = true;
            foreach (ActiveWord word in _Grid.ActiveWordList)
            {
                int intersectionCount = 0;
                for(int letterIndex = 0; letterIndex < word.Length; letterIndex++)
                {
                    if (_Grid.ElementIn(word, letterIndex).HorizontalWord != null && _Grid.ElementIn(word, letterIndex).VerticalWord != null)
                        intersectionCount++;
                }
                if(intersectionCount < Config.MinIntersectingWords || intersectionCount > Config.MaxIntersectingWords)
                {
                    _ValidationErrorList.Add("The word " + word + " does not have the required "+ Config.MinIntersectingWords +"-"+Config.MaxIntersectingWords+" intersecting letters.");
                    Log.New("The word " + word + " does not have the required " + Config.MinIntersectingWords + "-" + Config.MaxIntersectingWords + " intersecting letters.");
                    result = false;
                }
            }
            return result;
        }

        #endregion

        #region Generate Optimal

        /// <summary>
        /// Generates the optimal Crozzle from the list of avalible words.
        /// </summary>
        public void GenerateOptimal()
        {
            // Completion Status
            bool status = false;

            // Setup Timer - Max run time is 5 min
            timer = new Timer(60000);

            // Start the timer
            timer.Start();
            Log.New("Start generating optimal Crozzle.");
            DateTime timeStart = DateTime.Now;

            // Set temperary Crozzle properties for processing
            List<Word> Words = new List<Word>();
            foreach (Word word in _WordList)
                Words.Add(new Word(word.String));
            int totalWords = Words.Count;
            int wordsAdded = 0;
            MagicBoard Grid = new MagicBoard(_Grid.Rows, _Grid.Cols); // Dynamic Crozzle board          

            while (timer.Enabled && status == false)
            {

                List<ActiveWord> BestWords;
                while ((BestWords = Grid.GetBestWord(Words)).Count > 0)
                {
                    // Add the words to the grid
                    foreach (ActiveWord BestWord in BestWords)
                    {
                        // Add the Bestword
                        Grid.AddMagicWord(BestWord);

                        // Remove the word from the avalible words list
                        for (int index = 0; index < Words.Count; index++)
                            if (Words[index].String == BestWord.String)
                                Words.RemoveAt(index);
                        
                        // Increment the words added property - For progress reporting
                        wordsAdded++;
                    }
                }

                // Processing has complete - Set the status to true
                status = true;

            }
            DateTime timeStop = DateTime.Now;
            TimeSpan timeTaken = timeStop - timeStart;
            Log.New("Generating optimal Crozzle complete. Tame taken: " + timeTaken.Milliseconds+"ms");


            // Update the Crozzle to that of the optimised crozzle generated.
            _Grid = Grid.LoseTheMagic();
            _ActiveWordList = _Grid.ActiveWordList;
        }

        #endregion

        #region Methods: CalcScore()

        // Calculates the total score for the Crozzle.
        private int CalcScore()
        {
            int score = 0;
            
            // Add all individual letter scores
            foreach (Element letter in _Grid.BoardGrid)
                if (letter != null)
                    score += letter.Score;
            
            // Add points per word
            score += _Grid.ActiveWordList.Count * Config.PointsPerWord;
            
            // Return score;
            return score;
        }

        #endregion
    }
}

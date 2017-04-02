using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Crozzle2.CrozzleElements;

namespace TestCrozzle
{
    [TestClass]
    public class Test_GetBestWord
    {
        ConfigRef Config = new ConfigRef();

        /// <summary>
        /// This is a test for whether the best word selected for an empty Crozzle
        /// grid is the top scorring word.
        /// </summary>
        [TestMethod]
        public void GetBestWord_EmptyGridBoard_Test()
        {
            // Arrange
            Config.Difficulty = Config.MediumKeyWord;
            Config.NonIntersectingLetterPoints = new int[] { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; // A=4 Everything else=1
            Config.IntersectingLetterPoints = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1 }; // S=6 Everything else=1
            MagicBoard CrozzleBoard = new MagicBoard(10,15); // Dynamic Crozzle board - 10rows 15cols
            List<Word> Wordlist = new List<Word>() { new Word("SCOTT"), new Word("AARON"), new Word("ELIZIBETH") }; // SCOTT=5, AARON=11, ELIZIBETH=9
            string Expected = "AARON";

            // Act
            List<ActiveWord> BestWord = CrozzleBoard.GetBestWord(Wordlist);
            string Actual = BestWord[0].String;
            
            // Assert
            Assert.AreEqual(Expected, Actual);

        }

        /// <summary>
        /// This is a test for whether the best word selected for a Crozzle
        /// grid containing other words is the top scorring word.
        /// </summary>
        [TestMethod]
        public void GetBestWord_NotEmptyGridBoard_Test()
        {
            // Arrange
            Config.Difficulty = Config.MediumKeyWord;
            Config.NonIntersectingLetterPoints = new int[] { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; // A=4 Everything else=1
            Config.IntersectingLetterPoints = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1 }; // S=6 Everything else=1
            MagicBoard CrozzleBoard = new MagicBoard(10, 15); // Dynamic Crozzle board - 10rows 15cols
            CrozzleBoard.AddMagicWord(new ActiveWord("SIMON", Config.HorizontalKeyWord,11,16)); // Insert SIMON horizontally at row 1 col 1
            List<Word> Wordlist = new List<Word>() { new Word("SCOTT"), new Word("AARON"), new Word("ELIZIBETH") }; // SCOTT=10, AARON=5, ELIZIBETH=9
            string Expected = "SCOTT";

            // Act
            List<ActiveWord> BestWord = CrozzleBoard.GetBestWord(Wordlist);
            string Actual = BestWord[0].String;

            // Assert
            Assert.AreEqual(Expected, Actual);

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Crozzle2.CrozzleElements;

namespace TestCrozzle
{
    [TestClass]
    public class Test_AddWord
    {
        ConfigRef Config = new ConfigRef();

        /// <summary>
        /// This is a test for whether a word can be added to a Crozzle grid without thowing
        /// an exception.
        /// </summary>
        [TestMethod]
        public void AddWord_NoOverlap()
        {
            // Arrange
            Config.Difficulty = Config.EasyKeyWord;
            Board CrozzleBoard = new Board(10, 15); // Crozzle board - 10rows 15cols
            bool Expected = true;

            // Act
            bool Actual = true;
            try
            {
                CrozzleBoard.AddWord(new ActiveWord("SCOTT", Config.HorizontalKeyWord, 1, 1)); // Insert SCOTT horizontally at row 1 col 1
            }
            catch
            {
                Actual = false;
            }

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        /// <summary>
        /// This is a test for whether an exception is correctly thrown when a word is added over
        /// the top of another word (of non-mathching letters).
        /// </summary>
        [TestMethod]
        public void AddWord_OverlapNonMatching()
        {
            // Arrange
            Config.Difficulty = Config.EasyKeyWord;
            Board CrozzleBoard = new Board(10, 15); // Crozzle board - 10rows 15cols
            CrozzleBoard.AddWord(new ActiveWord("SCOTT", Config.HorizontalKeyWord, 3, 1)); // Insert SCOTT horizontally starting at row 3 col 1
            bool Expected = false;

            // Act
            bool Actual = true;
            try
            {
                CrozzleBoard.AddWord(new ActiveWord("LAURA", Config.VerticalKeyWord, 1, 1)); // Insert LAURA vertically starting at row 1 col 1 - U from LAURA will overlap S from SCOTT.
            }
            catch
            {
                Actual = false;
            }

            // Assert
            Assert.AreEqual(Expected, Actual);
        }

        /// <summary>
        /// This is a test for whether an exception is thrown if a word is added over the top
        /// of another word (of mathching letters).
        /// </summary>
        [TestMethod]
        public void AddWord_OverlapMatching()
        {
            // Arrange
            Config.Difficulty = Config.EasyKeyWord;
            Board CrozzleBoard = new Board(10, 15); // Crozzle board - 10rows 15cols
            CrozzleBoard.AddWord(new ActiveWord("OSCAR", Config.HorizontalKeyWord, 3, 1)); // Insert OSCAR horizontally starting at row 3 col 1.
            bool Expected = true;

            // Act
            bool Actual = true;
            try
            {
                CrozzleBoard.AddWord(new ActiveWord("SCOTT", Config.VerticalKeyWord, 1, 1)); // Insert SCOTT vertically starting at row 1 col 1. O from SCOTT will overlap O from OSCAR.
            }
            catch
            {
                Actual = false;
            }

            // Assert
            Assert.AreEqual(Expected, Actual);
        }
    }
}

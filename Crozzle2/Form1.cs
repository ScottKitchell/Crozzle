using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using Crozzle2.CrozzleElements;

namespace Crozzle2
{
    public partial class MainCrozzleForm : Form
    {
        private ConfigRef Config = new ConfigRef();

        #region Properties

        private ConfigFile _CurrentConfigFile;
        private CrozzleFile _CurrentCrozzleFile;
        private Crozzle _CurrentCrozzle;

        #endregion

        #region Constructers

        public MainCrozzleForm()
        {
            InitializeComponent();
            HTML html = CrozzleHTML.Initialize();
            html.Append("<H1>Welcome to Crozzle</H1>");
            html.Append("<p>Select 'New' from the menu to start!</p>");
            CrozzleMainDisplay.DocumentText = html.ToString();
        }

        #endregion

        #region Menu event handlers: GenerateCrozzle, ValidateCrozzle, SaveAs, Close

        private void GenerateCrozzleMenuItem_Click(object sender, EventArgs e)
        {
            // Acknoledge click
            CrozzleLabel.Text = "Generate Crozzle";

            // Open Crozzle files
            if (!OpenFiles("Generate Crozzle"))
            {
                // Update Display with error
                UpdateForm_Error("No files selected.");
                UpdateForm_Generate();
            }
            else
            {
                // Update Display
                UpdateForm_Generate();

                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;

                // what to do in the background thread
                bw.DoWork += new DoWorkEventHandler(
                delegate (object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;

                // Create optimal Crozzle 
                if (_CurrentConfigFile.ValidationResult == true && _CurrentCrozzleFile.ValidationResult == true)
                    {
                        _CurrentCrozzle.GenerateOptimal();
                    }

                });

                // what to do when worker completes its task (notify the user)
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    _CurrentCrozzle.Validate();
                    CrozzleLabel.Text = "Generate Crozzle";
                    HTML page = DisplayCrozzle.Using(_CurrentCrozzle, _CurrentCrozzleFile, _CurrentConfigFile);
                    CrozzleMainDisplay.DocumentText = page.ToString();
                });

                bw.RunWorkerAsync();
            }
        }   

        private void NewValidateCrozzleMenuItem_Click(object sender, EventArgs e)
        {
            // Acknoledge click
            CrozzleLabel.Text = "Validate a Crozzle";

            // Open Crozzle files
            if (!OpenFiles("Validate Crozzle"))
            {
                // Update Display with error
                UpdateForm_Error("No files selected.");
            }
            else
            {
                // Display the crozzle
                HTML page = DisplayCrozzle.Using(_CurrentCrozzle, _CurrentCrozzleFile, _CurrentConfigFile);
                CrozzleMainDisplay.DocumentText = page.ToString();
            }
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            // Ensure Crozzle is stored
            if (_CurrentCrozzle != null)
            {
                // Update UI
                CrozzleLabel.Text = "Save Crozzle";

                // Show save dialog 
                if (saveCrozzleFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // If success save file
                    string filepath = saveCrozzleFileDialog.FileName;
                    Log.New("Save Crozzle as: " + filepath);
                    _CurrentCrozzleFile = new CrozzleFile(_CurrentCrozzle);
                    try
                    {
                        _CurrentCrozzleFile.SaveFile(filepath);
                        CrozzleLabel.Text = "Crozzle saved!";

                    }
                    catch (Exception ex)
                    {
                        Log.New("There was an error svaing the Crozzle. " + ex.Message);
                        UpdateForm_Error("There was an error saving the file.");
                    }
                }
            }
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Meneu event handlers: ViewRules ViewLog, ViewRawCrozzleFile, ViewRawCofigFile 

        private void rulesMenuItem_Click(object sender, EventArgs e)
        {
            CrozzleLabel.Text = "Crozzle Rules";
            HTML page = DisplayRules.GetPage();
            CrozzleMainDisplay.DocumentText = page.ToString();
        }

        private void viewLogMenuItem_Click(object sender, EventArgs e)
        {
            HTML page = DisplayRawFile.File(Log.FileName, "Log File");
            CrozzleMainDisplay.DocumentText = page.ToString();
        }

        private void viewRawCrozzleFileMenuItem_Click(object sender, EventArgs e)
        {
            HTML page = DisplayRawFile.File(_CurrentCrozzleFile.FileName, "Raw Crozzle File");
            CrozzleMainDisplay.DocumentText = page.ToString();
        }

        private void viewRawConfigFileMenuItem_Click(object sender, EventArgs e)
        {
            HTML page = DisplayRawFile.File(_CurrentConfigFile.FileName, "Raw Configuration File");
            CrozzleMainDisplay.DocumentText = page.ToString();
        }

        #endregion

        #region Methods: OpenFiles(), updateForm_Validate(), updateForm_Generate(), updateForm_Error()

        private bool OpenFiles(string CTA)
        {
            bool result = false;
            // Open Crozzle files
            OpenCrozzleFiles openFilesDialog = new OpenCrozzleFiles(CTA);
            if (openFilesDialog.ShowDialog() == DialogResult.OK)
            {
                // Open the Configuration files (validation included)
                _CurrentConfigFile = new ConfigFile(openFilesDialog.ConfigFilePath);
                Config.SetFromConfigFile(_CurrentConfigFile);

                // Open the Crozzle files (validation incuded)
                _CurrentCrozzleFile = new CrozzleFile(openFilesDialog.CrozzleFilePath);
                Config.Difficulty = _CurrentCrozzleFile.Difficulty;

                // Construct the Crozzle (Validation of rules incuded)
                _CurrentCrozzle = new Crozzle();
                _CurrentCrozzle.ConstructFromCrozzleFile(_CurrentCrozzleFile);

                // result to true
                result = true;
            }
            return result;
        }

        private void UpdateForm_Validate()
        {
            // Update html
            HTML html = CrozzleHTML.Initialize();
            CrozzleMainDisplay.DocumentText = html.ToString();
            // Update menu
            viewRawCrozzleFileMenuItem.Enabled = true;
            viewRawConfigFileMenuItem.Enabled = true;
            NewDesignCrozzleMenuItem.Checked = false;
            NewValidateCrozzleMenuItem.Checked = true;
            saveAsMenuItem.Enabled = true;
        }

        private void UpdateForm_Generate()
        {
            //Update main label
            CrozzleLabel.Text = "Generate Crozzle";
            //Update html
            HTML html = CrozzleHTML.Initialize();
            html.Append("<H1>Generating the optimal Crozzle solution...</H1>");
            html.Append("<p>Processing will stop after 5 miniutes if it hasn't already completed.</p>");
            CrozzleMainDisplay.DocumentText = html.ToString();
            // Update menu 
            viewRawCrozzleFileMenuItem.Enabled = true;
            viewRawConfigFileMenuItem.Enabled = true;
            NewDesignCrozzleMenuItem.Checked = true;
            NewValidateCrozzleMenuItem.Checked = false;
            saveAsMenuItem.Enabled = true;
        }

        private void UpdateForm_Error(string message)
        {
            // Update file values
            _CurrentCrozzleFile = null;
            _CurrentConfigFile = null;
            // update html
            HTML html = CrozzleHTML.Initialize();
            html.Append("<H1>Umm.. sorry</H1>");
            html.Append("<H2>"+message+"</H2>");
            CrozzleMainDisplay.DocumentText = html.ToString();
            // update menu
            NewDesignCrozzleMenuItem.Checked = false;
            NewValidateCrozzleMenuItem.Checked = false;
            viewRawCrozzleFileMenuItem.Enabled = false;
            viewRawConfigFileMenuItem.Enabled = false;
            saveAsMenuItem.Enabled = false;
        }

        #endregion

    }
}

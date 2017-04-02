using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crozzle2
{
    public partial class OpenCrozzleFiles : Form
    {
        #region Properties

        private static string _CrozzleFilePath;
        /// <summary>
        /// Crozzle file path.
        /// </summary>
        public string CrozzleFilePath { get { return _CrozzleFilePath; } }

        private static string _ConfigFilePath;
        /// <summary>
        /// Configuration file path.
        /// </summary>
        public string ConfigFilePath { get { return _ConfigFilePath; } }

        #endregion

        #region Contructors

        /// <summary>
        /// Open Crozzle file window.
        /// </summary>
        public OpenCrozzleFiles()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open Crozzle file window with non-default CTA.
        /// </summary>
        /// <param name="callToAction"></param>
        public OpenCrozzleFiles(string callToAction)
        {
            InitializeComponent();
            CTABtn.Text = callToAction;
        }
        #endregion

        #region Event handlers: openCrozzleFileBtn_Click(), openConfigFileBtn_Click(), CTABtn_Click()

        private void openCrozzleFileBtn_Click(object sender, EventArgs e)
        {
            if (openCrozzleFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Store file path
                _CrozzleFilePath = openCrozzleFileDialog.FileName;

                // Display the File Address.
                CrozzleFileLabel.Text = openCrozzleFileDialog.SafeFileName;

                // Enable generate button
                if (_CrozzleFilePath != null && _ConfigFilePath != null)
                    CTABtn.Enabled = true;
            }
        }

        private void openConfigFileBtn_Click(object sender, EventArgs e)
        {
            if (this.openConfigFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Store file path
                _ConfigFilePath = openConfigFileDialog.FileName;

                // Display the File Address.
                ConfigFileLabel.Text = openConfigFileDialog.SafeFileName;

                // Enable generate button
                if (_CrozzleFilePath != null && _ConfigFilePath != null)
                    CTABtn.Enabled = true;
            }
        }

        private void CTABtn_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}

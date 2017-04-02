namespace Crozzle2
{
    partial class MainCrozzleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainCrozzleForm));
            this.CrozzleMainMenu = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.validateCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewDesignCrozzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewValidateCrozzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rulesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.viewRawCrozzleFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewRawConfigFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CrozzleMainDisplay = new System.Windows.Forms.WebBrowser();
            this.CrozzleLabel = new System.Windows.Forms.Label();
            this.saveCrozzleFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.CrozzleMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // CrozzleMainMenu
            // 
            this.CrozzleMainMenu.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.CrozzleMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.coolToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.CrozzleMainMenu.Location = new System.Drawing.Point(0, 0);
            this.CrozzleMainMenu.Name = "CrozzleMainMenu";
            this.CrozzleMainMenu.Size = new System.Drawing.Size(784, 24);
            this.CrozzleMainMenu.TabIndex = 0;
            this.CrozzleMainMenu.Text = "CrozzleMainMenu";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validateCrozzleToolStripMenuItem,
            this.saveAsMenuItem,
            this.closeToolStripMenuItem});
            this.File.ForeColor = System.Drawing.SystemColors.ControlText;
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(37, 20);
            this.File.Text = "File";
            // 
            // validateCrozzleToolStripMenuItem
            // 
            this.validateCrozzleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewDesignCrozzleMenuItem,
            this.NewValidateCrozzleMenuItem});
            this.validateCrozzleToolStripMenuItem.Name = "validateCrozzleToolStripMenuItem";
            this.validateCrozzleToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.validateCrozzleToolStripMenuItem.Text = "New";
            // 
            // NewDesignCrozzleMenuItem
            // 
            this.NewDesignCrozzleMenuItem.Name = "NewDesignCrozzleMenuItem";
            this.NewDesignCrozzleMenuItem.Size = new System.Drawing.Size(162, 22);
            this.NewDesignCrozzleMenuItem.Text = "Generate Crozzle";
            this.NewDesignCrozzleMenuItem.Click += new System.EventHandler(this.GenerateCrozzleMenuItem_Click);
            // 
            // NewValidateCrozzleMenuItem
            // 
            this.NewValidateCrozzleMenuItem.Name = "NewValidateCrozzleMenuItem";
            this.NewValidateCrozzleMenuItem.Size = new System.Drawing.Size(162, 22);
            this.NewValidateCrozzleMenuItem.Text = "Validate Crozzle";
            this.NewValidateCrozzleMenuItem.Click += new System.EventHandler(this.NewValidateCrozzleMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Enabled = false;
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveAsMenuItem.Text = "Save as";
            this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // coolToolStripMenuItem
            // 
            this.coolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rulesMenuItem});
            this.coolToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.coolToolStripMenuItem.Name = "coolToolStripMenuItem";
            this.coolToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.coolToolStripMenuItem.Text = "Crozzle Rules";
            // 
            // rulesMenuItem
            // 
            this.rulesMenuItem.Name = "rulesMenuItem";
            this.rulesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rulesMenuItem.Text = "Rules";
            this.rulesMenuItem.Click += new System.EventHandler(this.rulesMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewLogToolStripMenuItem,
            this.toolStripSeparator1,
            this.viewRawCrozzleFileMenuItem,
            this.viewRawConfigFileMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewLogToolStripMenuItem
            // 
            this.viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
            this.viewLogToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.viewLogToolStripMenuItem.Text = "View Log";
            this.viewLogToolStripMenuItem.Click += new System.EventHandler(this.viewLogMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // viewRawCrozzleFileMenuItem
            // 
            this.viewRawCrozzleFileMenuItem.Enabled = false;
            this.viewRawCrozzleFileMenuItem.Name = "viewRawCrozzleFileMenuItem";
            this.viewRawCrozzleFileMenuItem.Size = new System.Drawing.Size(215, 22);
            this.viewRawCrozzleFileMenuItem.Text = "View raw crozzle file";
            this.viewRawCrozzleFileMenuItem.Click += new System.EventHandler(this.viewRawCrozzleFileMenuItem_Click);
            // 
            // viewRawConfigFileMenuItem
            // 
            this.viewRawConfigFileMenuItem.Enabled = false;
            this.viewRawConfigFileMenuItem.Name = "viewRawConfigFileMenuItem";
            this.viewRawConfigFileMenuItem.Size = new System.Drawing.Size(215, 22);
            this.viewRawConfigFileMenuItem.Text = "View raw configuration file";
            this.viewRawConfigFileMenuItem.Click += new System.EventHandler(this.viewRawConfigFileMenuItem_Click);
            // 
            // CrozzleMainDisplay
            // 
            this.CrozzleMainDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrozzleMainDisplay.Location = new System.Drawing.Point(0, 73);
            this.CrozzleMainDisplay.MinimumSize = new System.Drawing.Size(20, 20);
            this.CrozzleMainDisplay.Name = "CrozzleMainDisplay";
            this.CrozzleMainDisplay.Size = new System.Drawing.Size(784, 488);
            this.CrozzleMainDisplay.TabIndex = 1;
            // 
            // CrozzleLabel
            // 
            this.CrozzleLabel.AutoSize = true;
            this.CrozzleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CrozzleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CrozzleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CrozzleLabel.Location = new System.Drawing.Point(12, 36);
            this.CrozzleLabel.Name = "CrozzleLabel";
            this.CrozzleLabel.Size = new System.Drawing.Size(61, 21);
            this.CrozzleLabel.TabIndex = 2;
            this.CrozzleLabel.Text = "Crozzle";
            // 
            // saveCrozzleFileDialog
            // 
            this.saveCrozzleFileDialog.DefaultExt = "txt";
            this.saveCrozzleFileDialog.FileName = "MyCrozzle";
            this.saveCrozzleFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.saveCrozzleFileDialog.Title = "Save the Crozzle";
            // 
            // MainCrozzleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.CrozzleLabel);
            this.Controls.Add(this.CrozzleMainDisplay);
            this.Controls.Add(this.CrozzleMainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.CrozzleMainMenu;
            this.Name = "MainCrozzleForm";
            this.Text = "Crozzle";
            this.CrozzleMainMenu.ResumeLayout(false);
            this.CrozzleMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem validateCrozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewDesignCrozzleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewValidateCrozzleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.WebBrowser CrozzleMainDisplay;
        private System.Windows.Forms.ToolStripMenuItem coolToolStripMenuItem;
        private System.Windows.Forms.MenuStrip CrozzleMainMenu;
        private System.Windows.Forms.ToolStripMenuItem rulesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.Label CrozzleLabel;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewRawCrozzleFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewRawConfigFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SaveFileDialog saveCrozzleFileDialog;
    }
}


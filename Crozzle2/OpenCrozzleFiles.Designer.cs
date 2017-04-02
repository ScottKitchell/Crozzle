namespace Crozzle2
{
    partial class OpenCrozzleFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenCrozzleFiles));
            this.CTABtn = new System.Windows.Forms.Button();
            this.ConfigFileLabel = new System.Windows.Forms.Label();
            this.CrozzleFileLabel = new System.Windows.Forms.Label();
            this.openConfigFileBtn = new System.Windows.Forms.Button();
            this.FormHeading = new System.Windows.Forms.Label();
            this.openCrozzleFileBtn = new System.Windows.Forms.Button();
            this.openCrozzleFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openConfigFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // CTABtn
            // 
            this.CTABtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CTABtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CTABtn.Enabled = false;
            this.CTABtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CTABtn.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.CTABtn.Location = new System.Drawing.Point(12, 207);
            this.CTABtn.Name = "CTABtn";
            this.CTABtn.Size = new System.Drawing.Size(220, 42);
            this.CTABtn.TabIndex = 11;
            this.CTABtn.Text = "Done";
            this.CTABtn.UseVisualStyleBackColor = false;
            this.CTABtn.Click += new System.EventHandler(this.CTABtn_Click);
            // 
            // ConfigFileLabel
            // 
            this.ConfigFileLabel.AutoSize = true;
            this.ConfigFileLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ConfigFileLabel.Location = new System.Drawing.Point(12, 175);
            this.ConfigFileLabel.Name = "ConfigFileLabel";
            this.ConfigFileLabel.Size = new System.Drawing.Size(10, 13);
            this.ConfigFileLabel.TabIndex = 10;
            this.ConfigFileLabel.Text = " ";
            // 
            // CrozzleFileLabel
            // 
            this.CrozzleFileLabel.AutoSize = true;
            this.CrozzleFileLabel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.CrozzleFileLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CrozzleFileLabel.Location = new System.Drawing.Point(12, 101);
            this.CrozzleFileLabel.Name = "CrozzleFileLabel";
            this.CrozzleFileLabel.Size = new System.Drawing.Size(10, 13);
            this.CrozzleFileLabel.TabIndex = 9;
            this.CrozzleFileLabel.Text = " ";
            // 
            // openConfigFileBtn
            // 
            this.openConfigFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openConfigFileBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.openConfigFileBtn.Location = new System.Drawing.Point(12, 130);
            this.openConfigFileBtn.Name = "openConfigFileBtn";
            this.openConfigFileBtn.Size = new System.Drawing.Size(220, 42);
            this.openConfigFileBtn.TabIndex = 8;
            this.openConfigFileBtn.Text = "Open configuration file";
            this.openConfigFileBtn.UseVisualStyleBackColor = true;
            this.openConfigFileBtn.Click += new System.EventHandler(this.openConfigFileBtn_Click);
            // 
            // FormHeading
            // 
            this.FormHeading.AutoSize = true;
            this.FormHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormHeading.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormHeading.Location = new System.Drawing.Point(12, 18);
            this.FormHeading.Name = "FormHeading";
            this.FormHeading.Size = new System.Drawing.Size(142, 20);
            this.FormHeading.TabIndex = 7;
            this.FormHeading.Text = "Open Crozzle Files";
            this.FormHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // openCrozzleFileBtn
            // 
            this.openCrozzleFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openCrozzleFileBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.openCrozzleFileBtn.Location = new System.Drawing.Point(12, 56);
            this.openCrozzleFileBtn.Name = "openCrozzleFileBtn";
            this.openCrozzleFileBtn.Size = new System.Drawing.Size(220, 42);
            this.openCrozzleFileBtn.TabIndex = 6;
            this.openCrozzleFileBtn.Text = "Open crozzle file";
            this.openCrozzleFileBtn.UseVisualStyleBackColor = true;
            this.openCrozzleFileBtn.Click += new System.EventHandler(this.openCrozzleFileBtn_Click);
            // 
            // openCrozzleFileDialog
            // 
            this.openCrozzleFileDialog.DefaultExt = "txt";
            this.openCrozzleFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openCrozzleFileDialog.Title = "Open Crozzle txt file";
            // 
            // openConfigFileDialog
            // 
            this.openConfigFileDialog.DefaultExt = "txt";
            this.openConfigFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openConfigFileDialog.Title = "Open Configuration txt file";
            // 
            // OpenCrozzleFiles
            // 
            this.AcceptButton = this.CTABtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(244, 261);
            this.Controls.Add(this.CTABtn);
            this.Controls.Add(this.ConfigFileLabel);
            this.Controls.Add(this.CrozzleFileLabel);
            this.Controls.Add(this.openConfigFileBtn);
            this.Controls.Add(this.FormHeading);
            this.Controls.Add(this.openCrozzleFileBtn);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenCrozzleFiles";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Open Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CTABtn;
        private System.Windows.Forms.Label ConfigFileLabel;
        private System.Windows.Forms.Label CrozzleFileLabel;
        private System.Windows.Forms.Button openConfigFileBtn;
        private System.Windows.Forms.Label FormHeading;
        private System.Windows.Forms.Button openCrozzleFileBtn;
        private System.Windows.Forms.OpenFileDialog openCrozzleFileDialog;
        private System.Windows.Forms.OpenFileDialog openConfigFileDialog;
    }
}
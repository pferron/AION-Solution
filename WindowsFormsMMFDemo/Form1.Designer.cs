namespace DemoInterface
{
    partial class Form1
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
            this.buttonExit = new System.Windows.Forms.Button();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tcDemo = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtCSVAccelaProjectModel = new System.Windows.Forms.TextBox();
            this.btnLoadTest = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRecID = new System.Windows.Forms.TextBox();
            this.btnLoadAccelaRecordToAION = new System.Windows.Forms.Button();
            this.btnStartMMFTest = new System.Windows.Forms.Button();
            this.btnRunEPMSamples = new System.Windows.Forms.Button();
            this.rtbDemoInfo = new System.Windows.Forms.RichTextBox();
            this.btnParseProfessionals = new System.Windows.Forms.Button();
            this.tcDemo.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(829, 485);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(164, 34);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // tcDemo
            // 
            this.tcDemo.Controls.Add(this.tabPage1);
            this.tcDemo.Location = new System.Drawing.Point(3, 12);
            this.tcDemo.Name = "tcDemo";
            this.tcDemo.SelectedIndex = 0;
            this.tcDemo.Size = new System.Drawing.Size(1008, 467);
            this.tcDemo.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tabPage1.Controls.Add(this.btnParseProfessionals);
            this.tabPage1.Controls.Add(this.txtCSVAccelaProjectModel);
            this.tabPage1.Controls.Add(this.btnLoadTest);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtRecID);
            this.tabPage1.Controls.Add(this.btnLoadAccelaRecordToAION);
            this.tabPage1.Controls.Add(this.btnStartMMFTest);
            this.tabPage1.Controls.Add(this.btnRunEPMSamples);
            this.tabPage1.Controls.Add(this.rtbDemoInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1000, 441);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Record processing";
            // 
            // txtCSVAccelaProjectModel
            // 
            this.txtCSVAccelaProjectModel.Location = new System.Drawing.Point(24, 19);
            this.txtCSVAccelaProjectModel.Multiline = true;
            this.txtCSVAccelaProjectModel.Name = "txtCSVAccelaProjectModel";
            this.txtCSVAccelaProjectModel.Size = new System.Drawing.Size(706, 243);
            this.txtCSVAccelaProjectModel.TabIndex = 28;
            // 
            // btnLoadTest
            // 
            this.btnLoadTest.Location = new System.Drawing.Point(646, 372);
            this.btnLoadTest.Name = "btnLoadTest";
            this.btnLoadTest.Size = new System.Drawing.Size(201, 41);
            this.btnLoadTest.TabIndex = 27;
            this.btnLoadTest.Text = "Select CSV file to be Tested.";
            this.btnLoadTest.UseVisualStyleBackColor = true;
            this.btnLoadTest.Click += new System.EventHandler(this.btnLoadTest_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 394);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(543, 20);
            this.textBox1.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 367);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Select the CSV maping file to be loaded for MApping Test";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(757, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = " Enter Accela Record ID   ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtRecID
            // 
            this.txtRecID.Location = new System.Drawing.Point(756, 71);
            this.txtRecID.Name = "txtRecID";
            this.txtRecID.Size = new System.Drawing.Size(194, 20);
            this.txtRecID.TabIndex = 23;
            this.txtRecID.TextChanged += new System.EventHandler(this.txtRecID_TextChanged);
            // 
            // btnLoadAccelaRecordToAION
            // 
            this.btnLoadAccelaRecordToAION.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnLoadAccelaRecordToAION.ForeColor = System.Drawing.Color.Gold;
            this.btnLoadAccelaRecordToAION.Location = new System.Drawing.Point(756, 122);
            this.btnLoadAccelaRecordToAION.Name = "btnLoadAccelaRecordToAION";
            this.btnLoadAccelaRecordToAION.Size = new System.Drawing.Size(167, 27);
            this.btnLoadAccelaRecordToAION.TabIndex = 22;
            this.btnLoadAccelaRecordToAION.Text = "Load AccelaRecord to AION";
            this.btnLoadAccelaRecordToAION.UseVisualStyleBackColor = false;
            this.btnLoadAccelaRecordToAION.Click += new System.EventHandler(this.btnLoadAccelaRecordToAION_Click);
            // 
            // btnStartMMFTest
            // 
            this.btnStartMMFTest.Location = new System.Drawing.Point(55, 291);
            this.btnStartMMFTest.Name = "btnStartMMFTest";
            this.btnStartMMFTest.Size = new System.Drawing.Size(129, 48);
            this.btnStartMMFTest.TabIndex = 12;
            this.btnStartMMFTest.Text = "Start  MMF test";
            this.btnStartMMFTest.UseVisualStyleBackColor = true;
            // 
            // btnRunEPMSamples
            // 
            this.btnRunEPMSamples.Location = new System.Drawing.Point(280, 291);
            this.btnRunEPMSamples.Name = "btnRunEPMSamples";
            this.btnRunEPMSamples.Size = new System.Drawing.Size(130, 48);
            this.btnRunEPMSamples.TabIndex = 11;
            this.btnRunEPMSamples.Text = "Run EPM Samples";
            this.btnRunEPMSamples.UseVisualStyleBackColor = true;
            // 
            // rtbDemoInfo
            // 
            this.rtbDemoInfo.Location = new System.Drawing.Point(6, 19);
            this.rtbDemoInfo.Name = "rtbDemoInfo";
            this.rtbDemoInfo.Size = new System.Drawing.Size(724, 254);
            this.rtbDemoInfo.TabIndex = 2;
            this.rtbDemoInfo.Text = "";
            // 
            // btnParseProfessionals
            // 
            this.btnParseProfessionals.Location = new System.Drawing.Point(655, 315);
            this.btnParseProfessionals.Name = "btnParseProfessionals";
            this.btnParseProfessionals.Size = new System.Drawing.Size(233, 36);
            this.btnParseProfessionals.TabIndex = 29;
            this.btnParseProfessionals.Text = "GetParsed Professionals ";
            this.btnParseProfessionals.UseVisualStyleBackColor = true;
            this.btnParseProfessionals.Click += new System.EventHandler(this.btnParseProfessionals_CLick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 531);
            this.Controls.Add(this.tcDemo);
            this.Controls.Add(this.buttonExit);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tcDemo.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonExit;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabControl tcDemo;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rtbDemoInfo;
        private System.Windows.Forms.Button btnStartMMFTest;
        private System.Windows.Forms.Button btnRunEPMSamples;
        private System.Windows.Forms.TextBox txtRecID;
        private System.Windows.Forms.Button btnLoadAccelaRecordToAION;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnLoadTest;
        private System.Windows.Forms.TextBox txtCSVAccelaProjectModel;
        private System.Windows.Forms.Button btnParseProfessionals;
    }
}


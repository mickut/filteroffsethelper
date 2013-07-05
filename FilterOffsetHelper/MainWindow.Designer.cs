namespace FilterOffsetHelper
{
    partial class MainWindow
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
            this.referenceComboBox = new System.Windows.Forms.ComboBox();
            this.ReferenceLabel = new System.Windows.Forms.Label();
            this.focusmaxConnectButton = new System.Windows.Forms.Button();
            this.filterwheelConnectButton = new System.Windows.Forms.Button();
            this.measureButton = new System.Windows.Forms.Button();
            this.filterListBox = new System.Windows.Forms.CheckedListBox();
            this.measureBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // referenceComboBox
            // 
            this.referenceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.referenceComboBox.Enabled = false;
            this.referenceComboBox.FormattingEnabled = true;
            this.referenceComboBox.Location = new System.Drawing.Point(101, 12);
            this.referenceComboBox.MaxDropDownItems = 20;
            this.referenceComboBox.Name = "referenceComboBox";
            this.referenceComboBox.Size = new System.Drawing.Size(166, 21);
            this.referenceComboBox.TabIndex = 0;
            this.referenceComboBox.SelectedIndexChanged += new System.EventHandler(this.referenceComboBox_SelectedIndexChanged);
            // 
            // ReferenceLabel
            // 
            this.ReferenceLabel.AutoSize = true;
            this.ReferenceLabel.Location = new System.Drawing.Point(12, 15);
            this.ReferenceLabel.Name = "ReferenceLabel";
            this.ReferenceLabel.Size = new System.Drawing.Size(82, 13);
            this.ReferenceLabel.TabIndex = 1;
            this.ReferenceLabel.Text = "Reference Filter";
            // 
            // focusmaxConnectButton
            // 
            this.focusmaxConnectButton.Location = new System.Drawing.Point(277, 12);
            this.focusmaxConnectButton.Name = "focusmaxConnectButton";
            this.focusmaxConnectButton.Size = new System.Drawing.Size(180, 47);
            this.focusmaxConnectButton.TabIndex = 3;
            this.focusmaxConnectButton.Text = "Connect to FocusMax";
            this.focusmaxConnectButton.UseVisualStyleBackColor = true;
            this.focusmaxConnectButton.Click += new System.EventHandler(this.focusmaxConnectButton_Click);
            // 
            // filterwheelConnectButton
            // 
            this.filterwheelConnectButton.Location = new System.Drawing.Point(277, 65);
            this.filterwheelConnectButton.Name = "filterwheelConnectButton";
            this.filterwheelConnectButton.Size = new System.Drawing.Size(180, 47);
            this.filterwheelConnectButton.TabIndex = 4;
            this.filterwheelConnectButton.Text = "Connect to filterwheel";
            this.filterwheelConnectButton.UseVisualStyleBackColor = true;
            this.filterwheelConnectButton.Click += new System.EventHandler(this.filterwheelConnectButton_Click);
            // 
            // measureButton
            // 
            this.measureButton.Enabled = false;
            this.measureButton.Location = new System.Drawing.Point(277, 118);
            this.measureButton.Name = "measureButton";
            this.measureButton.Size = new System.Drawing.Size(180, 75);
            this.measureButton.TabIndex = 5;
            this.measureButton.Text = "Measure!";
            this.measureButton.UseVisualStyleBackColor = true;
            this.measureButton.Click += new System.EventHandler(this.measureButton_Click);
            // 
            // filterListBox
            // 
            this.filterListBox.CheckOnClick = true;
            this.filterListBox.Enabled = false;
            this.filterListBox.FormattingEnabled = true;
            this.filterListBox.Location = new System.Drawing.Point(15, 39);
            this.filterListBox.Name = "filterListBox";
            this.filterListBox.Size = new System.Drawing.Size(252, 154);
            this.filterListBox.TabIndex = 6;
            // 
            // measureBackgroundWorker
            // 
            this.measureBackgroundWorker.WorkerSupportsCancellation = true;
            this.measureBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.measureBackgroundWorker_DoWork);
            this.measureBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.measureBackgroundWorker_RunWorkerCompleted);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 209);
            this.Controls.Add(this.filterListBox);
            this.Controls.Add(this.measureButton);
            this.Controls.Add(this.filterwheelConnectButton);
            this.Controls.Add(this.focusmaxConnectButton);
            this.Controls.Add(this.ReferenceLabel);
            this.Controls.Add(this.referenceComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Filter Offset Helper";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox referenceComboBox;
        private System.Windows.Forms.Label ReferenceLabel;
        private System.Windows.Forms.Button focusmaxConnectButton;
        private System.Windows.Forms.Button filterwheelConnectButton;
        private System.Windows.Forms.Button measureButton;
        private System.Windows.Forms.CheckedListBox filterListBox;
        private System.ComponentModel.BackgroundWorker measureBackgroundWorker;
    }
}
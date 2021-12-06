namespace Advent_of_Code
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DaySelection = new System.Windows.Forms.NumericUpDown();
            this.DayLabel = new System.Windows.Forms.Label();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.InputLabel = new System.Windows.Forms.Label();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.GoButton = new System.Windows.Forms.Button();
            this.YearLabel = new System.Windows.Forms.Label();
            this.YearSelection = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.DaySelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // DaySelection
            // 
            this.DaySelection.Location = new System.Drawing.Point(169, 12);
            this.DaySelection.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.DaySelection.Name = "DaySelection";
            this.DaySelection.Size = new System.Drawing.Size(57, 27);
            this.DaySelection.TabIndex = 6;
            this.DaySelection.ValueChanged += new System.EventHandler(this.DaySelection_ValueChanged);
            // 
            // DayLabel
            // 
            this.DayLabel.AutoSize = true;
            this.DayLabel.Location = new System.Drawing.Point(128, 14);
            this.DayLabel.Name = "DayLabel";
            this.DayLabel.Size = new System.Drawing.Size(35, 20);
            this.DayLabel.TabIndex = 5;
            this.DayLabel.Text = "&Day";
            // 
            // InputTextBox
            // 
            this.InputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputTextBox.Location = new System.Drawing.Point(12, 73);
            this.InputTextBox.Multiline = true;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InputTextBox.Size = new System.Drawing.Size(776, 199);
            this.InputTextBox.TabIndex = 8;
            // 
            // InputLabel
            // 
            this.InputLabel.AutoSize = true;
            this.InputLabel.Location = new System.Drawing.Point(12, 50);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(43, 20);
            this.InputLabel.TabIndex = 7;
            this.InputLabel.Text = "&Input";
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.Location = new System.Drawing.Point(12, 322);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(776, 199);
            this.OutputTextBox.TabIndex = 2;
            // 
            // OutputLabel
            // 
            this.OutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(16, 299);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(55, 20);
            this.OutputLabel.TabIndex = 1;
            this.OutputLabel.Text = "&Output";
            // 
            // GoButton
            // 
            this.GoButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.GoButton.Location = new System.Drawing.Point(330, 278);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(137, 33);
            this.GoButton.TabIndex = 0;
            this.GoButton.Text = "&Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // YearLabel
            // 
            this.YearLabel.AutoSize = true;
            this.YearLabel.Location = new System.Drawing.Point(15, 14);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(37, 20);
            this.YearLabel.TabIndex = 3;
            this.YearLabel.Text = "&Year";
            // 
            // YearSelection
            // 
            this.YearSelection.Location = new System.Drawing.Point(56, 12);
            this.YearSelection.Maximum = new decimal(new int[] {
            2021,
            0,
            0,
            0});
            this.YearSelection.Minimum = new decimal(new int[] {
            2021,
            0,
            0,
            0});
            this.YearSelection.Name = "YearSelection";
            this.YearSelection.Size = new System.Drawing.Size(57, 27);
            this.YearSelection.TabIndex = 4;
            this.YearSelection.Value = new decimal(new int[] {
            2021,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 533);
            this.Controls.Add(this.YearLabel);
            this.Controls.Add(this.YearSelection);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.InputLabel);
            this.Controls.Add(this.InputTextBox);
            this.Controls.Add(this.DayLabel);
            this.Controls.Add(this.DaySelection);
            this.Name = "MainForm";
            this.Text = "Advent of Code - Ross";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DaySelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearSelection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown DaySelection;
        private Label DayLabel;
        private TextBox InputTextBox;
        private Label InputLabel;
        private TextBox OutputTextBox;
        private Label OutputLabel;
        private Button GoButton;
        private Label YearLabel;
        private NumericUpDown YearSelection;
    }
}
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
            DaySelection = new NumericUpDown();
            DayLabel = new Label();
            InputTextBox = new TextBox();
            InputLabel = new Label();
            OutputTextBox = new TextBox();
            OutputLabel = new Label();
            GoButton = new Button();
            YearLabel = new Label();
            YearSelection = new NumericUpDown();
            UseTestInput = new CheckBox();
            UseInputPart1 = new RadioButton();
            UseInputPart2 = new RadioButton();
            ExpectedAnswerTextBox = new TextBox();
            ExpectedAnswerLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)DaySelection).BeginInit();
            ((System.ComponentModel.ISupportInitialize)YearSelection).BeginInit();
            SuspendLayout();
            // 
            // DaySelection
            // 
            DaySelection.Location = new Point(169, 12);
            DaySelection.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
            DaySelection.Name = "DaySelection";
            DaySelection.Size = new Size(57, 27);
            DaySelection.TabIndex = 8;
            DaySelection.ValueChanged += DaySelection_ValueChanged;
            // 
            // DayLabel
            // 
            DayLabel.AutoSize = true;
            DayLabel.Location = new Point(128, 14);
            DayLabel.Name = "DayLabel";
            DayLabel.Size = new Size(35, 20);
            DayLabel.TabIndex = 7;
            DayLabel.Text = "&Day";
            // 
            // InputTextBox
            // 
            InputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InputTextBox.Font = new Font("Courier New", 9F);
            InputTextBox.Location = new Point(12, 73);
            InputTextBox.Multiline = true;
            InputTextBox.Name = "InputTextBox";
            InputTextBox.ScrollBars = ScrollBars.Both;
            InputTextBox.Size = new Size(898, 203);
            InputTextBox.TabIndex = 13;
            // 
            // InputLabel
            // 
            InputLabel.AutoSize = true;
            InputLabel.Location = new Point(9, 50);
            InputLabel.Name = "InputLabel";
            InputLabel.Size = new Size(43, 20);
            InputLabel.TabIndex = 12;
            InputLabel.Text = "&Input";
            // 
            // OutputTextBox
            // 
            OutputTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OutputTextBox.Font = new Font("Courier New", 9F);
            OutputTextBox.Location = new Point(15, 321);
            OutputTextBox.Multiline = true;
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.ScrollBars = ScrollBars.Both;
            OutputTextBox.Size = new Size(898, 199);
            OutputTextBox.TabIndex = 2;
            // 
            // OutputLabel
            // 
            OutputLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            OutputLabel.AutoSize = true;
            OutputLabel.Location = new Point(12, 295);
            OutputLabel.Name = "OutputLabel";
            OutputLabel.Size = new Size(55, 20);
            OutputLabel.TabIndex = 1;
            OutputLabel.Text = "&Output";
            // 
            // GoButton
            // 
            GoButton.Anchor = AnchorStyles.Bottom;
            GoButton.Location = new Point(378, 282);
            GoButton.Name = "GoButton";
            GoButton.Size = new Size(137, 33);
            GoButton.TabIndex = 0;
            GoButton.Text = "&Go";
            GoButton.UseVisualStyleBackColor = true;
            GoButton.Click += GoButton_Click;
            // 
            // YearLabel
            // 
            YearLabel.AutoSize = true;
            YearLabel.Location = new Point(15, 14);
            YearLabel.Name = "YearLabel";
            YearLabel.Size = new Size(37, 20);
            YearLabel.TabIndex = 5;
            YearLabel.Text = "&Year";
            // 
            // YearSelection
            // 
            YearSelection.Location = new Point(56, 12);
            YearSelection.Maximum = new decimal(new int[] { 2021, 0, 0, 0 });
            YearSelection.Minimum = new decimal(new int[] { 2021, 0, 0, 0 });
            YearSelection.Name = "YearSelection";
            YearSelection.Size = new Size(57, 27);
            YearSelection.TabIndex = 6;
            YearSelection.Value = new decimal(new int[] { 2021, 0, 0, 0 });
            YearSelection.ValueChanged += YearSelection_ValueChanged;
            // 
            // UseTestInput
            // 
            UseTestInput.AutoSize = true;
            UseTestInput.Checked = true;
            UseTestInput.CheckState = CheckState.Checked;
            UseTestInput.Location = new Point(255, 14);
            UseTestInput.Name = "UseTestInput";
            UseTestInput.Size = new Size(123, 24);
            UseTestInput.TabIndex = 9;
            UseTestInput.Text = "Use &Test Input";
            UseTestInput.UseVisualStyleBackColor = true;
            UseTestInput.CheckedChanged += UseTestInput_CheckedChanged;
            // 
            // UseInputPart1
            // 
            UseInputPart1.AutoSize = true;
            UseInputPart1.Checked = true;
            UseInputPart1.Location = new Point(401, 14);
            UseInputPart1.Name = "UseInputPart1";
            UseInputPart1.Size = new Size(67, 24);
            UseInputPart1.TabIndex = 10;
            UseInputPart1.TabStop = true;
            UseInputPart1.Text = "Part &1";
            UseInputPart1.UseVisualStyleBackColor = true;
            UseInputPart1.CheckedChanged += UseInputPart2_CheckedChanged;
            // 
            // UseInputPart2
            // 
            UseInputPart2.AutoSize = true;
            UseInputPart2.Location = new Point(474, 14);
            UseInputPart2.Name = "UseInputPart2";
            UseInputPart2.Size = new Size(67, 24);
            UseInputPart2.TabIndex = 11;
            UseInputPart2.Text = "Part &2";
            UseInputPart2.UseVisualStyleBackColor = true;
            UseInputPart2.CheckedChanged += UseInputPart2_CheckedChanged;
            // 
            // ExpectedAnswerTextBox
            // 
            ExpectedAnswerTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ExpectedAnswerTextBox.Font = new Font("Courier New", 9F);
            ExpectedAnswerTextBox.Location = new Point(9, 571);
            ExpectedAnswerTextBox.Multiline = true;
            ExpectedAnswerTextBox.Name = "ExpectedAnswerTextBox";
            ExpectedAnswerTextBox.ReadOnly = true;
            ExpectedAnswerTextBox.Size = new Size(898, 59);
            ExpectedAnswerTextBox.TabIndex = 4;
            // 
            // ExpectedAnswerLabel
            // 
            ExpectedAnswerLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ExpectedAnswerLabel.AutoSize = true;
            ExpectedAnswerLabel.Location = new Point(9, 548);
            ExpectedAnswerLabel.Name = "ExpectedAnswerLabel";
            ExpectedAnswerLabel.Size = new Size(122, 20);
            ExpectedAnswerLabel.TabIndex = 3;
            ExpectedAnswerLabel.Text = "Expected &Answer";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 642);
            Controls.Add(ExpectedAnswerLabel);
            Controls.Add(ExpectedAnswerTextBox);
            Controls.Add(UseInputPart2);
            Controls.Add(UseInputPart1);
            Controls.Add(UseTestInput);
            Controls.Add(YearLabel);
            Controls.Add(YearSelection);
            Controls.Add(GoButton);
            Controls.Add(OutputLabel);
            Controls.Add(OutputTextBox);
            Controls.Add(InputLabel);
            Controls.Add(InputTextBox);
            Controls.Add(DayLabel);
            Controls.Add(DaySelection);
            Name = "MainForm";
            Text = "Advent of Code - Ross";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)DaySelection).EndInit();
            ((System.ComponentModel.ISupportInitialize)YearSelection).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private CheckBox UseTestInput;
        private RadioButton UseInputPart1;
        private RadioButton UseInputPart2;
        private TextBox ExpectedAnswerTextBox;
        private Label ExpectedAnswerLabel;
    }
}
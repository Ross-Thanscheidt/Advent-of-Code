namespace Advent_of_Code
{
    public partial class MainForm : Form
    {
        /*
         * Advent of Code (https://adventofcode.com)
         * Ross W. Thanscheidt
         */

        const string INPUT_YEAR_FOLDER = @"..\..\..\..\..\..\Private\Advent-of-Code-Input\Advent of Code\Year {0}"; 
        const string INPUT_YEAR_FOLDER_TEST = @"..\..\..\Year {0}";
        const string INPUT_FILE_FORMAT = @"\Input{0}{1}\Day_{2:00}{3}{4}.txt";

        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowInputPartSelection(bool showSelection = true)
        {
            UseInputPart1.Visible = showSelection;
            UseInputPart2.Visible = showSelection;

            if (showSelection)
            {
                UseInputPart1.Checked = true;
            }
        }

        private string Input_Year_Folder(decimal Year)
        {
            return String.Format(Environment.ExpandEnvironmentVariables(UseTestInput.Checked ? INPUT_YEAR_FOLDER_TEST : INPUT_YEAR_FOLDER), Year);
        }

        private string Input_Filename(decimal year, decimal day, int part = 0, bool answer = false)
        {
            return Input_Year_Folder(year) +
                String.Format(
                    INPUT_FILE_FORMAT,
                    UseTestInput.Checked ? ".Test" : "",
                    answer ? @"\Answers" : "",
                    day,
                    part > 0 ? "_Part" + part : "",
                    answer ? "_Answer" : "");
        }

        private void UpdateInputTextBoxText()
        {
            decimal year = YearSelection.Value;
            decimal day = DaySelection.Value;
            string inputFilename = Input_Filename(year, day);

            if (!File.Exists(inputFilename))
            {
                if (File.Exists(Input_Filename(year, day, 1)) &&
                    File.Exists(Input_Filename(year, day, 2)))
                {
                    if (!UseInputPart1.Visible)
                    {
                        ShowInputPartSelection();
                        UseInputPart1.Checked = true;
                    }

                    inputFilename = Input_Filename(year, day, UseInputPart1.Checked ? 1 : 2);
                }
            }

            InputTextBox.Text = File.Exists(inputFilename) ? File.ReadAllText(inputFilename) : "";

            ExpectedAnswerTextBox.Text = "";

            string answerFilename = Input_Filename(year, day, 1, answer: true);

            if ((!UseInputPart1.Visible || (UseInputPart1.Visible && UseInputPart1.Checked)) && File.Exists(answerFilename))
            {
                ExpectedAnswerTextBox.Text += string.Join("\r\n", File.ReadAllText(answerFilename).ReplaceLineEndings("\n").Split("\n").Select(line => $"Part One: {line}"));
            }

            answerFilename = Input_Filename(year, day, 2, answer: true);

            if ((!UseInputPart1.Visible || (UseInputPart1.Visible && UseInputPart2.Checked)) && File.Exists(answerFilename))
            {
                if (ExpectedAnswerTextBox.Text.Length > 0)
                {
                    ExpectedAnswerTextBox.Text += "\r\n";
                }

                ExpectedAnswerTextBox.Text += string.Join("\r\n", File.ReadAllText(answerFilename).ReplaceLineEndings("\n").Split("\n").Select(line => $"Part Two: {line}"));
            }
        }

        private void YearSelection_ValueChanged(object sender, EventArgs e)
        {
            ShowInputPartSelection(false);
            UpdateInputTextBoxText();
            OutputTextBox.Text = "";
        }

        private void DaySelection_ValueChanged(object sender, EventArgs e)
        {
            ShowInputPartSelection(false);
            UpdateInputTextBoxText();
            OutputTextBox.Text = "";
        }

        private void UseTestInput_CheckedChanged(object sender, EventArgs e)
        {
            ShowInputPartSelection(false);
            UpdateInputTextBoxText();
            OutputTextBox.Text = "";
        }


        private void UseInputPart1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInputTextBoxText();
            OutputTextBox.Text = "";
        }

        private void UseInputPart2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInputTextBoxText();
            OutputTextBox.Text = "";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (var year = YearSelection.Minimum + 1; year <= DateTime.Now.Year; year++)
            {
                if (Directory.Exists(Input_Year_Folder(year)))
                {
                    YearSelection.Maximum = year;
                }
            }
            YearSelection.Value = YearSelection.Maximum;

            if (DateTime.Today < new DateTime((int)YearSelection.Value, 12, 1))
            {
                DaySelection.Value = 1;
            }
            else if (DateTime.Today > new DateTime((int)YearSelection.Value, 12, 25))
            {
                DaySelection.Value = 25;
            }
            else
            {
                DaySelection.Value = DateTime.Today.Day;
            }

            while (DaySelection.Value > 1 && string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                DaySelection.Value--;
            }
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            IYear? year = YearSelection.Value switch
            {
                2021 => new Year_2021(),
                2022 => new Year_2022(),
                2023 => new Year_2023(),
                2024 => new Year_2024(),
                _ => null
            };

            using (var input = new StringReader(InputTextBox.Text))
                OutputTextBox.Text = DaySelection.Value switch
                {
                    1 => year?.Day_01(input),
                    2 => year?.Day_02(input),
                    3 => year?.Day_03(input),
                    4 => year?.Day_04(input),
                    5 => year?.Day_05(input),
                    6 => year?.Day_06(input),
                    7 => year?.Day_07(input),
                    8 => year?.Day_08(input),
                    9 => year?.Day_09(input),
                    10 => year?.Day_10(input),
                    11 => year?.Day_11(input),
                    12 => year?.Day_12(input),
                    13 => year?.Day_13(input),
                    14 => year?.Day_14(input),
                    15 => year?.Day_15(input),
                    16 => year?.Day_16(input),
                    17 => year?.Day_17(input),
                    18 => year?.Day_18(input),
                    19 => year?.Day_19(input),
                    20 => year?.Day_20(input),
                    21 => year?.Day_21(input),
                    22 => year?.Day_22(input),
                    23 => year?.Day_23(input),
                    24 => year?.Day_24(input),
                    25 => year?.Day_25(input),
                    _ => string.Empty
                };
        }
    }
}
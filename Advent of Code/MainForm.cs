namespace Advent_of_Code
{
    public partial class MainForm : Form
    {
        /*
         * Advent of Code (https://adventofcode.com)
         * Ross W. Thanscheidt
         */

        string INPUT_YEAR_FOLDER = @"%UserProfile%\Code\Advent of Code\Advent of Code\Year {0}";
        string INPUT_FILE_FORMAT = @"\Input\Day {0:00}.txt";

        public MainForm()
        {
            InitializeComponent();
        }

        private string Input_Year_Folder(decimal Year)
        {
            return String.Format(Environment.ExpandEnvironmentVariables(INPUT_YEAR_FOLDER), Year);
        }

        private string Input_Filename(decimal Year, decimal Day)
        {
            return Input_Year_Folder(Year) + String.Format(INPUT_FILE_FORMAT, DaySelection.Value);
        }

        private void DaySelection_ValueChanged(object sender, EventArgs e)
        {
            var inputFilename = Input_Filename(YearSelection.Value, DaySelection.Value);
            InputTextBox.Text = File.Exists(inputFilename) ? File.ReadAllText(inputFilename) : "";
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

            DaySelection.Value = DateTime.Today < new DateTime((int)YearSelection.Value, 12, 1)
                                 ? 1
                                 : DateTime.Today > new DateTime((int)YearSelection.Value, 12, 25)
                                 ? 25
                                 : DateTime.Today.Day;
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            IYear? year = null;
            switch (YearSelection.Value)
            {
                case 2021:
                    year = new Year_2021();
                    break;
            }

            using (var input = new StringReader(InputTextBox.Text))
            {
                switch (DaySelection.Value)
                {
                    case 1:
                        OutputTextBox.Text = year?.Day_01(input);
                        break;

                    case 2:
                        OutputTextBox.Text = year?.Day_02(input);
                        break;

                    case 3:
                        OutputTextBox.Text = year?.Day_03(input);
                        break;

                    case 4:
                        OutputTextBox.Text = year?.Day_04(input);
                        break;

                    case 5:
                        OutputTextBox.Text = year?.Day_05(input);
                        break;

                    case 6:
                        OutputTextBox.Text = year?.Day_06(input);
                        break;

                    case 7:
                        OutputTextBox.Text = year?.Day_07(input);
                        break;

                    case 8:
                        OutputTextBox.Text = year?.Day_08(input);
                        break;

                    case 9:
                        OutputTextBox.Text = year?.Day_09(input);
                        break;

                    case 10:
                        OutputTextBox.Text = year?.Day_10(input);
                        break;

                    case 11:
                        OutputTextBox.Text = year?.Day_11(input);
                        break;

                    case 12:
                        OutputTextBox.Text = year?.Day_12(input);
                        break;

                    case 13:
                        OutputTextBox.Text = year?.Day_13(input);
                        break;

                    case 14:
                        OutputTextBox.Text = year?.Day_14(input);
                        break;

                    case 15:
                        OutputTextBox.Text = year?.Day_15(input);
                        break;

                    case 16:
                        OutputTextBox.Text = year?.Day_16(input);
                        break;

                    case 17:
                        OutputTextBox.Text = year?.Day_17(input);
                        break;

                    case 18:
                        OutputTextBox.Text = year?.Day_18(input);
                        break;

                    case 19:
                        OutputTextBox.Text = year?.Day_19(input);
                        break;

                    case 20:
                        OutputTextBox.Text = year?.Day_20(input);
                        break;

                    case 21:
                        OutputTextBox.Text = year?.Day_21(input);
                        break;

                    case 22:
                        OutputTextBox.Text = year?.Day_22(input);
                        break;

                    case 23:
                        OutputTextBox.Text = year?.Day_23(input);
                        break;

                    case 24:
                        OutputTextBox.Text = year?.Day_24(input);
                        break;

                    case 25:
                        OutputTextBox.Text = year?.Day_25(input);
                        break;
                }
            }
        }

    }
}
using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_05(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long sumOfCorrectMiddlePageNumbers = 0;
            long sumOfCorrectedMiddlePageNumbers = 0;

            List<(int before, int after)> rules = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Contains('|'))
                {
                    List<int> pageNumbers = line.Split('|').Select(number => int.Parse(number)).ToList();
                    rules.Add((pageNumbers[0], pageNumbers[1]));
                }
                else if (line.Contains(','))
                {
                    List<int> pageNumbers = line.Split(',').Select(number => int.Parse(number)).ToList();

                    bool inOrder = true;

                    for (int before = 0; before < pageNumbers.Count - 1; before++)
                    {
                        for (int after = before + 1; after < pageNumbers.Count; after++)
                        {
                            if (rules.Any(rule => rule.before == pageNumbers[after] && rule.after == pageNumbers[before]))
                            {
                                inOrder = false;
                                (pageNumbers[after], pageNumbers[before]) = (pageNumbers[before], pageNumbers[after]);
                            }
                        }
                    }

                    if (inOrder)
                    {
                        sumOfCorrectMiddlePageNumbers += pageNumbers[(pageNumbers.Count - 1) / 2];
                    }
                    else
                    {
                        sumOfCorrectedMiddlePageNumbers += pageNumbers[(pageNumbers.Count - 1) / 2];
                    }
                }
            }

            stopwatch.Stop();

            return $"{sumOfCorrectMiddlePageNumbers:N0} is the sum of the middle page numbers for the correctly-ordered updates\r\n" +
                   $"{sumOfCorrectedMiddlePageNumbers:N0} is the sum of the middle page numbers for the incorrectly-ordered updates after correcting the order\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

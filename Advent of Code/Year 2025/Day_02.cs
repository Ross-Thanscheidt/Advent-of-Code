using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        public string Day_02(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long sum = 0;
            long sumNewRules = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                foreach (string range in line.Split(','))
                {
                    string[] ranges = range.Split('-');
                    long start = long.Parse(ranges[0]);
                    long end = long.Parse(ranges[1]);

                    for (long id = start; id <= end; id++)
                    {
                        string idString = id.ToString();

                        bool invalid = false;
                        bool invalidNewRules = false;

                        for (int i = 0; i < idString.Length / 2 && (!invalid || !invalidNewRules); i++)
                        {
                            string sub = idString[..(i + 1)];
                            string test = sub;

                            while (test.Length < idString.Length)
                            {
                                test += sub;
                            }

                            if (test == idString)
                            {
                                if ((i + 1) * 2 == idString.Length && !invalid)
                                {
                                    sum += id;
                                    invalid = true;
                                }

                                if (!invalidNewRules)
                                {
                                    sumNewRules += id;
                                    invalidNewRules = true;
                                }
                            }
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{sum:N0} is the sum of all of the invalid IDs\r\n" +
                   $"{sumNewRules:N0} is the sum of all of the invalid IDs using the new rules\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

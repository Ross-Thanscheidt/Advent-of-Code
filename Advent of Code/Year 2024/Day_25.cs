using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_25(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long pairsThatFit = 0;
            long part2 = 0;

            List<int[]> locks = [];
            List<int[]> keys = [];

            int rows = 7;
            int tumblers = 5;

            int row = 0;
            int[] build = new int[tumblers];
            bool lockMode = true;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    if (row == 0)
                    {
                        build = [0, 0, 0, 0, 0];
                        lockMode = line.Contains('#');
                    }
                    else if (row < rows - 1)
                    {
                        for (int tumbler = 0; tumbler < tumblers; tumbler++)
                        {
                            build[tumbler] += line[tumbler] == '#' ? 1 : 0;
                        }
                    }
                    else
                    {
                        if (lockMode)
                        {
                            locks.Add(build);
                        }
                        else
                        {
                            keys.Add(build);
                        }

                        build = [0, 0, 0, 0, 0];
                        row = -1;
                    }

                    row++;
                }
            }

            pairsThatFit = locks
                .SelectMany(lck => keys, (lck, key) => !Enumerable.Range(0, tumblers).Any(tumbler => lck[tumbler] + key[tumbler] >= rows - 1))
                .Count(fits => fits);

            stopwatch.Stop();

            return $"{pairsThatFit:N0}\r\n" +
                   $"{part2:N0}\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

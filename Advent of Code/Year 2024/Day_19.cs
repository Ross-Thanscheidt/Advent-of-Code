using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_19(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long designsPossible = 0;
            long waysPossible = 0;

            List<string> designs = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (designs.Count == 0)
                {
                    designs = [.. line.Split(", ")];
                }
                else if (line.Length > 0)
                {
                    PriorityQueue<string, int> remainders = new();

                    remainders.Enqueue(line, line.Length);

                    bool found = false;

                    while (remainders.Count > 0)
                    {
                        string remainder = remainders.Dequeue();


                        foreach (string design in designs.Where(design => remainder.StartsWith(design)))
                        {
                            if (design.Length == remainder.Length)
                            {
                                if (!found)
                                {
                                    designsPossible++;
                                    found = true;
                                }

                                waysPossible++;
                            }
                            else
                            {
                                remainders.Enqueue(remainder[design.Length..], remainder.Length - design.Length);
                            }
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{designsPossible:N0}\r\n" +
                   $"{waysPossible:N0}\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_04(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
            }

            stopwatch.Stop();

            return $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}

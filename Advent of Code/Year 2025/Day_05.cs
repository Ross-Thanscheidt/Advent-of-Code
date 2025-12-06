using System.Diagnostics;
using Range = Advent_of_Code.Year_2025_Day_05.Range;

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        public string Day_05(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long freshAvailableIngredients = 0;
            long freshIngredientIDs = 0;

            List<Range> ranges = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Contains('-'))
                {
                    List<string> boundaries = [.. line.Split('-')];
                    ranges.Add(new(long.Parse(boundaries[0]), long.Parse(boundaries[1])));
                }
                else if (long.TryParse(line, out long id))
                {
                    if (ranges.Any(range => range.Start <= id && id <= range.End))
                    {
                        freshAvailableIngredients++;
                    }
                }
            }

            Range[] rangeArray = [.. ranges];
            int indexOfLastElement = rangeArray.Length - 1;

            int i = 0;

            while (i <= indexOfLastElement)
            {
                bool startOver = false;

                for (int j = i + 1; j <= indexOfLastElement && !startOver; j++)
                {
                    if (rangeArray[i].Overlaps(rangeArray[j]) || rangeArray[i].Adjacent(rangeArray[j]))
                    {
                        rangeArray[i].Start = Math.Min(rangeArray[i].Start, rangeArray[j].Start);
                        rangeArray[i].End = Math.Max(rangeArray[i].End, rangeArray[j].End);
                        rangeArray.Skip(j + 1).Take(indexOfLastElement - j).ToArray().CopyTo(rangeArray, j);
                        indexOfLastElement--;
                        startOver = true;
                    }
                }

                i = startOver ? 0 : i + 1;
            }

            freshIngredientIDs = rangeArray.Take(indexOfLastElement + 1).Sum(range => range.End - range.Start + 1);

            stopwatch.Stop();

            return $"{freshAvailableIngredients:N0} available ingredient IDs are fresh\r\n" +
                   $"{freshIngredientIDs:N0} ingredient IDs are considered to be fresh\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

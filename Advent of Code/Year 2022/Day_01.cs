using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_01(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var caloriesPerElf = new List<long>();
            long currentElfCalories = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (long.TryParse(line, out var calories))
                {
                    currentElfCalories += calories;
                }
                else
                {
                    caloriesPerElf.Add(currentElfCalories);
                    currentElfCalories = 0;
                }
            }

            caloriesPerElf.Add(currentElfCalories);
            caloriesPerElf = caloriesPerElf.OrderDescending().Take(3).ToList();

            var endTimestamp = DateTime.Now;

            return $"{caloriesPerElf[0]:N0} calories is the most being carried by one Elf\r\n" +
                   $"{caloriesPerElf.Sum():N0} calories are being carried by the top three Elves\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

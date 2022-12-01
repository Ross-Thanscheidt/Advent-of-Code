using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_01(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var mostCalories = new List<long>();
            long currentElfCalories = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (long.TryParse(line, out var calories))
                {
                    currentElfCalories += calories;
                }
                else
                {
                    mostCalories.Add(currentElfCalories);
                    mostCalories = mostCalories.OrderByDescending(c => c).Take(3).ToList();
                    currentElfCalories = 0;
                }
            }

            mostCalories.Add(currentElfCalories);
            mostCalories = mostCalories.OrderByDescending(c => c).Take(3).ToList();

            var endTimestamp = DateTime.Now;

            return $"{mostCalories[0]:N0} calories is the most being carried by one Elf\r\n" +
                   $"{mostCalories.Sum():N0} calories are being carried by the top three Elves\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

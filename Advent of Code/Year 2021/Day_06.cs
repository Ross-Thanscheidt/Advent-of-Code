namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_06(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            long[] fishTimerCounts = new long[9];

            // Load initial fish timers
            var fishTimers = input.ReadLine().Split(',').Select(n => int.Parse(n)).ToList();
            foreach (var fishTimer in fishTimers)
            {
                fishTimerCounts[fishTimer]++;
            }

            // Initialize fish counts
            long fishCountInitial = fishTimerCounts.Sum();
            long fishCount80Days = 0;
            long fishCount256Days = 0;

            // Loop through each day
            for (var day = 0; day < 256; day++)
            {
                var birthingFishCount = fishTimerCounts[0];
                for (var fishTimerIndex = 0; fishTimerIndex < 8; fishTimerIndex++)
                {
                    fishTimerCounts[fishTimerIndex] = fishTimerCounts[fishTimerIndex + 1];
                }
                fishTimerCounts[6] += birthingFishCount;
                fishTimerCounts[8] = birthingFishCount;

                if (day + 1 == 80)
                {
                    fishCount80Days = fishTimerCounts.Sum();
                }
                else if (day + 1 == 256)
                {
                    fishCount256Days = fishTimerCounts.Sum();
                }
            }

            var endTimestamp = DateTime.Now;

            return $"Initially {fishCountInitial:N0} fish\r\n" +
                   $"{fishCount80Days:N0} fish after 80 days\r\n" +
                   $"{fishCount256Days:N0} fish after 256 days\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

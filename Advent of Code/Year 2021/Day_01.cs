namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_01(StringReader input)
        {
            int singleIncreaseCount = 0;
            int slidingIncreaseCount = 0;
            int? previousDepth1 = null;
            int? previousDepth2 = null;
            int? previousSum = null;
            int currentSum;
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var currentDepth = int.Parse(line);

                if (previousDepth1.HasValue && currentDepth > previousDepth1.Value)
                    singleIncreaseCount++;

                if (previousDepth1.HasValue && previousDepth2.HasValue)
                {
                    currentSum = currentDepth + previousDepth1.Value + previousDepth2.Value;

                    if (previousSum.HasValue && currentSum > previousSum.Value)
                    {
                        slidingIncreaseCount++;
                    }

                    previousSum = currentSum;
                }

                previousDepth2 = previousDepth1;
                previousDepth1 = currentDepth;
            }
            return $"{singleIncreaseCount:N0} increases using single depth measurement\r\n" +
                   $"{slidingIncreaseCount:N0} increases using sum of measurements sliding window";
        }

    }
}

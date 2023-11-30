using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_16(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.StartsWith("Valve "))
                {
                    var valve = line.Split(" ")[1];
                    var flowRate = int.Parse(line.Split("=")[1].Split(";")[0]);
                    var neighbors = line.Split("valve")[1].Split(",").Select(v => v.Split(" ")[v.Split(" ").Length-1]);
                    Debug.WriteLine($"Valve {valve} has a flow rate of {flowRate} and connects to {string.Join(", ", neighbors)}");
                }
            }

            var endTimestamp = DateTime.Now;

            return $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

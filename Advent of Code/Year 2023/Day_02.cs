using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {

        [GeneratedRegex("Game (?<GameID>\\d+): ((?<Cubes>\\d+ \\w+)(, |; )?)+")]
        private static partial Regex GameLineRegex();

        public string Day_02(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int gameIdSum = 0;
            int powerSum = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var matchGroups = GameLineRegex().Match(line).Groups;

                var allCubes = matchGroups["Cubes"].Captures
                    .Select(cubesCapture => new
                    {
                        Count = int.Parse(cubesCapture.Value.Split(" ")[0]),
                        Color = cubesCapture.Value.Split(" ")[1]
                    });

                var maxCubes = allCubes
                    .GroupBy(
                        cubes => cubes.Color,
                        (cubeColor, cubesInGroup) => cubesInGroup.OrderByDescending(c => c.Count).First())
                    .ToList();

                if ((maxCubes?.FirstOrDefault(c => c.Color == "red")?.Count ?? 0) <= 12 &&
                    (maxCubes?.FirstOrDefault(c => c.Color == "green")?.Count ?? 0) <= 13 &&
                    (maxCubes?.FirstOrDefault(c => c.Color == "blue")?.Count ?? 0) <= 14)
                {
                    gameIdSum += int.Parse(matchGroups["GameID"].Value);
                }

                int cubeSetPower = 1;
                maxCubes?.ForEach(maxCubesForColor => cubeSetPower *= maxCubesForColor.Count);
                powerSum += cubeSetPower;
            }

            stopwatch.Stop();

            return $"{gameIdSum:N0} is the sum of the IDs of the possible games\r\n" +
                   $"{powerSum:N0} is the sum over all games of the power of the minimum set of cubes for each game\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}

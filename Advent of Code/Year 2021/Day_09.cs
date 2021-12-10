using Advent_of_Code.Year_2021_Day_09;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_09(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            // Get input lines
            var lines = new List<string>();
            for (var line = input.ReadLine()?.Trim(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    lines.Add(line);
                }
            }

            // Load array of heights into HeightMap object
            var heightMap = new HeightMap(lines.Count, lines[0].Length);
            foreach (var line in lines)
            {
                var heightRow = line.Select(c => int.Parse(c.ToString())).ToList();
                heightMap.AddRow(heightRow);
            }

            var riskLevelSum = 0;
            var currentBasinId = 1;
            for (var rowIndex = 0; rowIndex < heightMap.Rows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < heightMap.Columns; columnIndex++)
                {
                    // Add to Risk Level for each low point 
                    if (heightMap.AdjacentLocations(rowIndex, columnIndex)
                        .Where(location => location.Height <= heightMap[rowIndex, columnIndex].Height)
                        .ToList()
                        .Count == 0)
                    {
                        riskLevelSum += heightMap[rowIndex, columnIndex].Height + 1;
                    }

                    // Recursively label location and adjacent locations as new basin if it is in a basin and hasn't yet been labeled
                    if (heightMap[rowIndex, columnIndex].Height != 9 &&
                        heightMap[rowIndex, columnIndex].BasinId == 0)
                    {
                        heightMap.SetBasinId(rowIndex, columnIndex, currentBasinId++);
                    }
                }
            }

            // Find the product of the 3 largest basin sizes
            var basinSizes = heightMap.BasinSizesLargestFirst();
            var basinSizesProduct = 1;
            for (var basinIndex = 0; basinIndex < 3 && basinIndex < basinSizes.Count(); basinIndex++)
            {
                basinSizesProduct *= basinSizes.ElementAt(basinIndex);
            }

            var endTimestamp = DateTime.Now;

            return $"The sum of the risk levels of all low points is {riskLevelSum:N0}\r\n" +
                   $"The product of the three largest basin sizes is {basinSizesProduct:N0}\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

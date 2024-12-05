using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_04(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int xmasCount = 0;
            int x_masCount = 0;

            Dictionary<(int X, int Y), char> grid = [];

            int rows = 0;
            int columns = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                for (int column = 0; column < line.Length; column++)
                {
                    grid.Add((column, rows), line[column]);
                }

                if (line.Length > columns)
                {
                    columns = line.Length;
                }

                rows++;
            }

            string wordToFind = "XMAS";

            List<(int dx, int dy)> directions = [(1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1), (0, -1), (1, -1)];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (grid[(column, row)] == wordToFind[0])
                    {
                        foreach (var (dx, dy) in directions)
                        {
                            int lastLetterX = column + (wordToFind.Length - 1) * dx;
                            int lastLetterY = row + (wordToFind.Length - 1) * dy;

                            if (lastLetterX >= 0 && lastLetterX < columns &&
                                lastLetterY >= 0 && lastLetterY < rows)
                            {
                                bool found = true;

                                for (int letter = 1; letter < wordToFind.Length; letter++)
                                {
                                    if (grid[(column + letter * dx, row + letter * dy)] != wordToFind[letter])
                                    {
                                        found = false;
                                        break;
                                    }
                                }

                                if (found)
                                {
                                    xmasCount++;
                                }
                            }
                        }
                    }

                    if (grid[(column, row)] == 'A' && column > 0 && column + 1 < columns && row > 0 && row + 1 < rows)
                    {
                        char NW = grid[(column - 1, row - 1)];
                        char NE = grid[(column + 1, row - 1)];
                        char SE = grid[(column + 1, row + 1)];
                        char SW = grid[(column - 1, row + 1)];

                        if (((NW == 'M' && SE == 'S') || (NW == 'S' && SE == 'M')) &&
                            ((NE == 'M' && SW == 'S') || (NE == 'S' && SW == 'M')))
                        {
                            x_masCount++;
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{xmasCount:N0} is how many times XMAS appears\r\n" +
                   $"{x_masCount:N0} is how many times an X-MAS appears\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

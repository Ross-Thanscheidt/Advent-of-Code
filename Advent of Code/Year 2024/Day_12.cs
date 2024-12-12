using System.Diagnostics;
using Position = (int X, int Y);
using Direction = (int dx, int dy);

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_12(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalPricePerimeter = 0;
            long totalPriceSides = 0;

            Dictionary<Position, char> map = [];

            int rows = 0;
            int columns = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                columns = line.Length;

                for (int column = 0; column < columns; column++)
                {
                    map.Add((column, rows), line[column]);
                }

                rows++;
            }

            List<char> plants = [.. map.Select(plot => plot.Value).Distinct()];
            List<Direction> directions = [ (0, -1), (1, 0), (0, 1), (-1, 0) ];

            foreach (char plant in plants)
            {
                while (map.Any(plot => plot.Value == plant))
                {
                    List<Position> search = [ map.First(plot => plot.Value == plant).Key ];
                    List<Position> region = [];

                    while (search.Count > 0)
                    {
                        Position candidate = search[0];
                        search.RemoveAt(0);

                        if (map.TryGetValue(candidate, out char candidatePlant) && candidatePlant == plant)
                        {
                            map.Remove(candidate);
                            region.Add(candidate);

                            foreach ((int dx, int dy) in directions)
                            {
                                Position neighbor = (candidate.X + dx, candidate.Y + dy);

                                if (neighbor.X >= 0 && neighbor.X < columns &&
                                    neighbor.Y >= 0 && neighbor.Y < rows)
                                {
                                    search.Add(neighbor);
                                }
                            }
                        }
                    }

                    int area = region.Count;

                    int perimeter = 0;

                    List<Position> leftFences = [];
                    List<Position> rightFences = [];
                    List<Position> topFences = [];
                    List<Position> bottomFences = [];

                    foreach ((int X, int Y) in region)
                    {
                        perimeter += 4;

                        foreach ((int dx, int dy) in directions)
                        {
                            Position neighbor = (X + dx, Y + dy);

                            if (neighbor.X >= 0 && neighbor.X < columns &&
                                neighbor.Y >= 0 && neighbor.Y < rows &&
                                region.Contains(neighbor))
                            {
                                perimeter--;
                            }
                            else if (neighbor.X < X)
                            {
                                leftFences.Add((X, Y));
                            }
                            else if (neighbor.X > X)
                            {
                                rightFences.Add((X, Y));
                            }
                            else if (neighbor.Y < Y)
                            {
                                topFences.Add((X, Y));
                            }
                            else if (neighbor.Y > Y)
                            {
                                bottomFences.Add((X, Y));
                            }
                        }
                    }

                    totalPricePerimeter += area * perimeter;

                    int sides = leftFences.Count(position => !leftFences.Contains((position.X, position.Y - 1))) +
                                rightFences.Count(position => !rightFences.Contains((position.X, position.Y - 1))) +
                                topFences.Count(position => !topFences.Contains((position.X - 1, position.Y))) +
                                bottomFences.Count(position => !bottomFences.Contains((position.X - 1, position.Y)));

                    totalPriceSides += area * sides;
                }
            }

            stopwatch.Stop();

            return $"{totalPricePerimeter:N0} is the total price of fencing all regions using the perimeter\r\n" +
                   $"{totalPriceSides:N0} is the total price of fencing all regions using the number of sides\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

using Advent_of_Code.Extensions.Year_2021.Day_13;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        
        public string Day_13(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var output = "";
            HashSet<(int, int)> dots = new();
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                // Read x,y of Dot
                var matchPoint = new Regex(@"(?<x>\d+),(?<y>\d+)").Match(line);
                if (matchPoint.Success)
                {
                    var x = matchPoint.GetInt("x");
                    var y = matchPoint.GetInt("y");
                    dots.Add((x, y));
                }
                else
                {
                    // Read and process fold command line
                    var matchFold = new Regex(@"fold along (?<axis>.)=(?<value>\d+)").Match(line);
                    if (matchFold.Success)
                    {
                        var axis = matchFold.Groups["axis"].Value;
                        var value = matchFold.GetInt("value");

                        var dotsToFold = dots
                            .Where(d => axis == "y" ? d.Item2 > value : axis == "x" && d.Item1 > value)
                            .Select(d => new
                                {
                                    oldDot = (d.Item1, d.Item2),
                                    newDot = (axis == "x" ? 2 * value - d.Item1 : d.Item1,
                                              axis == "y" ? 2 * value - d.Item2 : d.Item2)
                                })
                            .ToList();
                        foreach (var dot in dotsToFold)
                        {
                            dots.Add(dot.newDot);
                            dots.Remove(dot.oldDot);
                        }
                        output += $"{dots.Count:N0} after Fold Along {axis}={value}\r\n";
                    }
                }
            }

            // Display results
            for (var y = 0; y <= dots.Max(d => d.Item2); y++)
            {
                for (var x = 0; x <= dots.Max(d => d.Item1); x++)
                {
                    output += dots.Contains((x, y)) ? "#" : ".";
                }
                output += "\r\n";
            }

            var endTimestamp = DateTime.Now;

            return $"{output}" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
            }

        }
    }

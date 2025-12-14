using System.Diagnostics;
using Advent_of_Code.Year_2025_Day_08;

namespace Advent_of_Code
{
    public partial class Year_2025 : IYear12
    {
        public string Day_08(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long productOfLargestCircuits = 0;
            long lastConnectionXProduct = 0;

            List<Position> boxes = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                int[] coordinates = [.. line.Split(',').Select(s => int.Parse(s))];
                boxes.Add(new(coordinates[0], coordinates[1], coordinates[2]));
            }

            List<(int Box1Index, int Box2Index, double Distance)> boxDistances = [];

            for (int i = 0; i < boxes.Count - 1; i++)
            {
                for (int j = i + 1; j < boxes.Count; j++)
                {
                    boxDistances.Add((i, j, boxes[i].Distance(boxes[j])));
                }
            }

            List<List<int>> circuits = [];

            int connectionCount = 0;

            foreach (var (box1, box2, distance) in boxDistances.OrderBy(nb => nb.Distance))
            {
                int circuitBox1 = circuits.FindIndex(circuit => circuit.Contains(box1));
                int circuitBox2 = circuits.FindIndex(circuit => circuit.Contains(box2));

                if (circuitBox1 == -1 && circuitBox2 == -1)
                {
                    circuits.Add([box1, box2]);
                }
                else if (circuitBox2 == -1)
                {
                    circuits[circuitBox1].Add(box2);
                }
                else if (circuitBox1 == -1)
                {
                    circuits[circuitBox2].Add(box1);
                }
                else if (circuitBox1 != circuitBox2)
                {
                    circuits[circuitBox1].AddRange(circuits[circuitBox2].Where(i => !circuits[circuitBox1].Contains(i)));
                    circuits.RemoveAt(circuitBox2);
                }

                if (++connectionCount == (boxes.Count <= 20 ? 10 : 1_000))
                {
                    productOfLargestCircuits = circuits
                        .OrderByDescending(circuit => circuit.Count).Take(3).Select(circuit => circuit.Count)
                        .Aggregate(1, (product, count) => product * count);
                }

                if (circuits.Count == 1 && circuits[0].Count == boxes.Count)
                {
                    lastConnectionXProduct = boxes[box1].X * boxes[box2].X;
                    break;
                }
            }

            stopwatch.Stop();

            return $"{productOfLargestCircuits:N0} is the product of the sizes of the three largest circuits\r\n" +
                   $"{lastConnectionXProduct:N0} is the product of the X coordinates of the last two junction boxes to connect\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

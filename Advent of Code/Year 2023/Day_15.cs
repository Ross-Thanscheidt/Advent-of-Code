using System.Diagnostics;
using Advent_of_Code.Year_2023_Day_15;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        private int Day_15_Hash(string stringToHash)
        {
            int currentValue = 0;

            foreach (var c in stringToHash)
            {
                currentValue += c;
                currentValue *= 17;
                currentValue %= 256;
            }

            return currentValue;
        }

        public string Day_15(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long resultsSum = 0;
            long focusingPower = 0;

            Dictionary<int, List<Lens>> boxes = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var steps = line.Split(",").ToList();

                foreach (var step in steps)
                {
                    resultsSum += Day_15_Hash(step);


                    var label = step.Split([.. "=-"])[0];
                    var box = Day_15_Hash(label);
                    var operation = step.Contains('=') ? '=' : '-';

                    var lens = boxes.TryGetValue(box, out List<Lens>? value) ? value.Find(lens => lens.Label == label) : null;

                    if (operation == '-')
                    {
                        if (lens != null)
                        {
                            boxes[box].Remove(lens);
                        }
                    }
                    else if (operation == '=')
                    {
                        var focalLength = int.Parse(step.Split([.. "="])[1]);

                        if (lens == null)
                        {
                            lens = new()
                            {
                                Label = label,
                                FocalLength = focalLength
                            };

                            if (boxes.TryGetValue(box, out List<Lens>? boxElement))
                            {
                                boxElement.Add(lens);
                            }
                            else
                            {
                                boxes.Add(box, [lens]);
                            }
                        }
                        else
                        {
                            lens.FocalLength = focalLength;
                        }
                    }
                }
            }

            foreach (var box in boxes)
            {
                for (var slotIndex = 0; slotIndex < box.Value.Count; slotIndex++)
                {
                    var focusingPowerLens = (box.Key + 1) * (slotIndex + 1) * box.Value[slotIndex].FocalLength;
                    focusingPower += focusingPowerLens;
                }
            }

            stopwatch.Stop();

            return $"{resultsSum:N0} is the sum of the results\r\n" +
                   $"{focusingPower:N0} is the focusing power of the resulting lens configuration\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
using Advent_of_Code.Extensions.Year_2021.Day_14;
using System.Text;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_14(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var output = new StringBuilder();

            // Load in initial polymer template (list of elements) and list of pair insertion rules
            var template = new StringBuilder();
            var _insertionRules = new Dictionary<string, char>();
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    if (template.Length == 0)
                    {
                        template.Append(line);
                    }
                    else
                    {
                        var rule = line.Split("->", StringSplitOptions.TrimEntries);
                        _insertionRules.Add(rule[0], rule[1][0]);
                    }
                }
            }

            // Count initial elements and break up into element pairs
            var _elementCounts = new Dictionary<char, long>();
            var pairCountsBefore = new Dictionary<string, long>();
            for (var templateIndex = 0; templateIndex + 1 < template.Length; templateIndex++)
            {
                if (templateIndex == 0)
                {
                    _elementCounts.IncrementCount(template[templateIndex]);
                }
                _elementCounts.IncrementCount(template[templateIndex + 1]);
                pairCountsBefore.IncrementCount(template.ToString(templateIndex, 2));
            }

            // Go through a specified number of steps
            for (var step = 1; step <= 40; step++)
            {
                // Implement a step by applying the pair insertion rules, counting the new elements, and counting the new element pairs
                var pairCountsAfter = new Dictionary<string, long>();
                foreach (var elementPairBefore in pairCountsBefore)
                {
                    var newElement = _insertionRules[elementPairBefore.Key];
                    _elementCounts.AddCount(newElement, elementPairBefore.Value);
                    pairCountsAfter.AddCount($"{elementPairBefore.Key[0]}{newElement}", elementPairBefore.Value);
                    pairCountsAfter.AddCount($"{newElement}{elementPairBefore.Key[1]}", elementPairBefore.Value);
                }

                // Copy list of element pairs and their counts back to the pairCountsBefore array
                pairCountsBefore.Clear();
                foreach (var elementPairAfter in pairCountsAfter)
                {
                    pairCountsBefore.Add(elementPairAfter.Key, elementPairAfter.Value);
                }

                // Show information after specified steps
                if (step == 10 || step == 40)
                {
                    var mostCommonElement = _elementCounts.OrderBy(ec => ec.Value).Last();
                    var leastCommonElement = _elementCounts.OrderBy(ec => ec.Value).First();
                    output.Append($"After {step} steps, the Most Common Element is ({mostCommonElement.Key}, {mostCommonElement.Value:N0}), Least Common Element is ({leastCommonElement.Key}, {leastCommonElement.Value:N0}), Difference is {mostCommonElement.Value - leastCommonElement.Value:N0}\r\n");
                }
            }

            var endTimestamp = DateTime.Now;

            return $"{output.ToString()}" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}

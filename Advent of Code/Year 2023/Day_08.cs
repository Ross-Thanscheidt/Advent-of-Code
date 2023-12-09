using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex(@"(?<NodeKey>\w{3})\s=\s\((?<NodeLeft>\w{3}),\s(?<NodeRight>\w{3})\)")]
        private static partial Regex Day_08_NodeLineRegex();

        public string Day_08(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int steps = 0;
            int stepsMultiple = 0;

            string instructions = "";
            Dictionary<string, ValueTuple<string, string>> nodes = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Contains('='))
                {
                    var matchGroups = Day_08_NodeLineRegex().Match(line).Groups;

                    var nodeKey = matchGroups["NodeKey"].Captures[0].Value;
                    var nodeLeft = matchGroups["NodeLeft"].Captures[0].Value;
                    var nodeRight = matchGroups["NodeRight"].Captures[0].Value;
                    nodes.Add(nodeKey, (nodeLeft, nodeRight));
                }
                else if (line.Length > 0)
                {
                    instructions = line;
                }
            }

            var currentKey = "AAA";
            var instructionIndex = 0;

            while (currentKey != "ZZZ")
            {
                var instruction = instructions[instructionIndex++];

                if (instructionIndex >= instructions.Length)
                {
                    instructionIndex = 0;
                }

                currentKey = instruction switch
                    {
                        'L' => nodes[currentKey].Item1,
                        'R' => nodes[currentKey].Item2
                    };

                steps++;
            }

            var currentKeys = nodes.Where(n => n.Key.EndsWith("A")).Select(n => n.Key).ToList();
            instructionIndex = 0;

            while (!currentKeys.All(k => k.EndsWith('Z')))
            {
                var instruction = instructions[instructionIndex++];

                if (instructionIndex >= instructions.Length)
                {
                    instructionIndex = 0;
                }

                for (var currentKeyIndex = 0; currentKeyIndex < currentKeys.Count; currentKeyIndex++)
                {
                    currentKeys[currentKeyIndex] = instruction == 'L'
                        ? nodes[currentKeys[currentKeyIndex]].Item1
                        : nodes[currentKeys[currentKeyIndex]].Item2;
                }

                stepsMultiple++;
            }

            stopwatch.Stop();

            return $"{steps:N0} steps are required to reach ZZZ from AAA\r\n" +
                   $"{stepsMultiple:N0} steps are required to get to all nodes ending with Z\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}

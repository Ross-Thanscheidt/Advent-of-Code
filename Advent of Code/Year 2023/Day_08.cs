using System.Diagnostics;
using System.Text.RegularExpressions;
using Advent_of_Code.Year_2023_Day_08;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex(@"(?<NodeKey>\w{3})\s=\s\((?<NodeLeft>\w{3}),\s(?<NodeRight>\w{3})\)")]
        private static partial Regex Day_08_NodeLineRegex();

        public string Day_08(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long steps = 0;
            long stepsMultiple = 0;

            string instructions = "";
            Dictionary<string, Node> nodes = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Contains('='))
                {
                    var matchGroups = Day_08_NodeLineRegex().Match(line).Groups;

                    var nodeKey = matchGroups["NodeKey"].Captures[0].Value;
                    var nodeLeft = matchGroups["NodeLeft"].Captures[0].Value;
                    var nodeRight = matchGroups["NodeRight"].Captures[0].Value;

                    if (!nodes.ContainsKey(nodeKey))
                    {
                        nodes.Add(nodeKey, new Node(nodeKey));
                    }

                    if (!nodes.ContainsKey(nodeLeft))
                    {
                        nodes.Add(nodeLeft, new Node(nodeLeft));
                    }

                    if (!nodes.ContainsKey(nodeRight))
                    {
                        nodes.Add(nodeRight, new Node(nodeRight));
                    }

                    nodes[nodeKey].LeftNode = nodes[nodeLeft];
                    nodes[nodeKey].RightNode = nodes[nodeRight];
                }
                else if (line.Length > 0)
                {
                    instructions = line;
                }
            }

            var currentNode = nodes["AAA"];
            var instructionIndex = 0;

            while (currentNode.Key != "ZZZ")
            {
                var instruction = instructions[instructionIndex++];

                if (instructionIndex >= instructions.Length)
                {
                    instructionIndex = 0;
                }

                currentNode = instruction == 'L' ? currentNode.LeftNode : currentNode.RightNode;

                steps++;
            }

            //var currentNodes = nodes.Where(kv => kv.Key.EndsWith('A')).Select(kv => kv.Value).ToList();
            //instructionIndex = 0;

            //while (!currentNodes.All(n => n.Key.EndsWith('Z')))
            //{
            //    var instruction = instructions[instructionIndex++];

            //    if (instructionIndex >= instructions.Length)
            //    {
            //        instructionIndex = 0;
            //    }

            //    for (var currentNodesIndex = 0; currentNodesIndex < currentNodes.Count; currentNodesIndex++)
            //    {
            //        currentNodes[currentNodesIndex] = instruction == 'L'
            //            ? currentNodes[currentNodesIndex].LeftNode
            //            : currentNodes[currentNodesIndex].RightNode;
            //    }

            //    stepsMultiple++;
            //}

            //var currentNodes = nodes.Where(kv => kv.Key.EndsWith('A')).Select(kv => kv.Value).ToList();
            //List<long> initialSteps = [];
            //List<long> cycleSteps = [];

            //instructionIndex = 0;

            //for (var currentNodesIndex = 0; currentNodesIndex < currentNodes.Count; currentNodesIndex++)
            //{
            //    var node = currentNodes[currentNodesIndex];
            //    string endKey = "";

            //    Debug.Write($"{node.Key}");

            //    initialSteps.Add(0);
            //    cycleSteps.Add(0);

            //    instructionIndex = 0;

            //    while (endKey.Length == 0 || node.Key != endKey)
            //    {
            //        if (node.Key.EndsWith('Z'))
            //        {
            //            endKey = node.Key;
            //        }

            //        var instruction = instructions[instructionIndex++];

            //        if (instructionIndex >= instructions.Length)
            //        {
            //            instructionIndex = 0;
            //        }

            //        node = instruction == 'L' ? node.LeftNode : node.RightNode;

            //        if (endKey.Length == 0)
            //        {
            //            initialSteps[currentNodesIndex]++;
            //        }
            //        else
            //        {
            //            cycleSteps[currentNodesIndex]++;
            //        }
            //    }
            //    Debug.WriteLine($" to {node.Key} initially takes {initialSteps[currentNodesIndex]:N0} steps and then every {cycleSteps[currentNodesIndex]:N0} steps");
            //}

            //var stepsTracker = cycleSteps.ToList();

            //while (stepsTracker.Distinct().Count() > 1)
            //{
            //    long lowestSteps = stepsTracker.Min();
            //    for (int stepsTrackerIndex = 0;  stepsTrackerIndex < stepsTracker.Count; stepsTrackerIndex++)
            //    {
            //        if (stepsTracker[stepsTrackerIndex] == lowestSteps)
            //        {
            //            stepsTracker[stepsTrackerIndex] += cycleSteps[stepsTrackerIndex];
            //        }
            //    }
            //}

            //stepsMultiple = stepsTracker[0];

            stopwatch.Stop();

            return $"{steps:N0} steps are required to reach ZZZ from AAA\r\n" +
                   //$"{stepsMultiple:N0} steps are required to get to all nodes ending with Z\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}

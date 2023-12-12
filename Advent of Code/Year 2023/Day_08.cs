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

            // Part One - Count steps from AAA to ZZZ

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

            // Part Two - Count steps from each node ending with A to all nodes ending with Z

            // Create list of starting nodes for each A-to-Z path
            var currentNodeForPath = nodes.Where(kv => kv.Key.EndsWith('A')).Select(kv => kv.Value).ToList();

            // Determine the number of steps for each A-to-Z path
            List<long> cycleSteps = [];

            instructionIndex = 0;

            for (var pathIndex = 0; pathIndex < currentNodeForPath.Count; pathIndex++)
            {
                var node = currentNodeForPath[pathIndex];

                cycleSteps.Add(0);
                instructionIndex = 0;

                while (!node.Key.EndsWith('Z'))
                {
                    var instruction = instructions[instructionIndex++];

                    if (instructionIndex >= instructions.Length)
                    {
                        instructionIndex = 0;
                    }

                    node = instruction == 'L' ? node.LeftNode : node.RightNode;

                    cycleSteps[pathIndex]++;
                }
            }

            // Find the largest number of cycleSteps for any path
            var maxCycleSteps = cycleSteps.Max();

            // Get list of prime numbers from 2 to sqrt(maxCycleSteps)
            var primeNumbers =
                Enumerable.Range(2, (int)Math.Sqrt(maxCycleSteps) - 1)
                .AsParallel()
                .Where(candidate => Enumerable.Range(2, (int)Math.Sqrt(candidate)).All(divisor => candidate % divisor != 0))
                .Select(primeNumber => (long)primeNumber)
                .ToList();

            // This will be used to determine the Least Common Multiple of the cycleSteps for all paths
            List<long> commonFactorsAllPaths = [];

            // For each A-to-Z path
            foreach (var cycleStepsForPath in cycleSteps)
            {
                // Break cycleStepsForPath down into a list of prime number factors
                List<long> primeFactorsForPath = [];

                for (long remainingFactors = cycleStepsForPath; remainingFactors != 1; )
                {
                    var primeFactor = primeNumbers.Find(primeNumber => remainingFactors % primeNumber == 0);

                    if (primeFactor == 0)
                    {
                        primeFactor = remainingFactors;
                    }

                    primeFactorsForPath.Add(primeFactor);

                    remainingFactors /= primeFactor;
                }

                // Remove each number in primeFactorsForPath that is already in commonFactorsAllPaths
                foreach(var commonFactor in commonFactorsAllPaths)
                {
                    primeFactorsForPath.Remove(commonFactor);
                }

                // Add any remaining numbers from primeFactorsForPath into commonFactorsAllPaths
                commonFactorsAllPaths.AddRange(primeFactorsForPath);
            }

            // stepsMultiple is the product of all prime numbers in commonFactorsAllPaths
            if (commonFactorsAllPaths.Count > 0)
            {
                stepsMultiple = commonFactorsAllPaths.Aggregate(1L, (product, factor) => product * factor);
            }

            stopwatch.Stop();

            return $"{steps:N0} steps are required to reach ZZZ from AAA\r\n" +
                   $"{stepsMultiple:N0} steps are required to get to all nodes ending with Z\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}
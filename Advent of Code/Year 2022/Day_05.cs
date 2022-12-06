namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_05(StringReader input)
        {
            var lines = input.ReadToEnd().Split("\r\n");

            var startTimestamp = DateTime.Now;

            List<Stack<char>> stacks9000 = null;
            List<Stack<char>> stacks9001 = null;

            for (var idx = 0; idx < lines.Length; idx++)
            {
                var line = lines[idx];
                if (line.Trim().StartsWith("1"))
                {
                    var stackCount = int.Parse(line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last());
                    stacks9000 = new List<Stack<char>>(stackCount);
                    stacks9001 = new List<Stack<char>>(stackCount);
                    for (var stackIndex = 0; stackIndex < stackCount; stackIndex++)
                    {
                        stacks9000.Add(new Stack<char>());
                        stacks9001.Add(new Stack<char>());
                    }
                    for (var rowIndex = idx - 1; rowIndex >= 0; rowIndex--)
                    {
                        var row = lines[rowIndex];
                        for (var stackIndex = 0; stackIndex < stackCount; stackIndex++)
                        {
                            var crate = row[1 + stackIndex * 4];
                            if (!char.IsWhiteSpace(crate))
                            {
                                stacks9000[stackIndex].Push(crate);
                                stacks9001[stackIndex].Push(crate);
                            }
                        }
                    }
                }
                else if (line.StartsWith("move"))
                {
                    var moveCount = int.Parse(line.Split(" ")[1]);
                    var fromStack = int.Parse(line.Split(" ")[3]);
                    var toStack = int.Parse(line.Split(" ")[5]);

                    var tempStack = new Stack<char>();
                    while (moveCount > 0)
                    {
                        stacks9000[toStack - 1].Push(stacks9000[fromStack - 1].Pop());
                        tempStack.Push(stacks9001[fromStack - 1].Pop());
                        moveCount--;
                    }
                    while (tempStack.Count > 0)
                    {
                        stacks9001[toStack - 1].Push(tempStack.Pop());
                    }
                }
            }

            var topCrates9000 = "";
            foreach (var stack in stacks9000)
            {
                if (stack.Count > 0)
                {
                    topCrates9000 += stack.Pop();
                    stack.Clear();
                }
            }

            var topCrates9001 = "";
            foreach (var stack in stacks9001)
            {
                if (stack.Count > 0)
                {
                    topCrates9001 += stack.Pop();
                    stack.Clear();
                }
            }

            var endTimestamp = DateTime.Now;

            return $"CrateMover 9000 Top Crates are {topCrates9000}\r\n" +
                   $"CrateMover 9001 Top Crates are {topCrates9001}\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}

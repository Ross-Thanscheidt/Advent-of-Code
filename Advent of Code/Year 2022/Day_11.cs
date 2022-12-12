using System.Numerics;
using Advent_of_Code.Year_2022_Day_11;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_11(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var monkeys = new List<Monkey>();

            var monkeyNumber = 0;
            var items = "";
            var operation = "";
            var testDivisor = BigInteger.Zero;
            var trueMonkeyTarget = 0;
            var falseMonkeyTarget = 0;
            var newItem = BigInteger.Zero;
            var operand = BigInteger.Zero;

            // Load monkeys from input
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.StartsWith("Monkey "))
                {
                    monkeyNumber = int.Parse(line.Split(" ")[1].Split(":")[0]);
                    items = "";
                    operation = "";
                    testDivisor = BigInteger.Zero;
                    trueMonkeyTarget = 0;
                    falseMonkeyTarget = 0;
                }
                else if (line.Contains("Starting items:"))
                {
                    items = line.Split(":")[1].Replace(" ", "");
                }
                else if (line.Contains("Operation: new = "))
                {
                    operation = line.Split("= ")[1];
                }
                else if (line.Contains("Test: divisible by "))
                {
                    testDivisor = BigInteger.Parse(line.Split("by ")[1]);
                }
                else if (line.Contains("If true: throw to monkey "))
                {
                    trueMonkeyTarget = int.Parse(line.Split("monkey ")[1]);
                }
                else if (line.Contains("If false: throw to monkey "))
                {
                    falseMonkeyTarget = int.Parse(line.Split("monkey ")[1]);

                    var monkey = new Monkey(monkeyNumber, operation, testDivisor, trueMonkeyTarget, falseMonkeyTarget);
                    monkey.ItemsPartOne.AddRange(items.Split(",").Select(i => BigInteger.Parse(i)));
                    monkey.ItemsPartTwo.AddRange(items.Split(",").Select(i => BigInteger.Parse(i)));
                    monkeys.Add(monkey);
                }
            }

            // Use the Common Multiple of the Test Divisors for Part Two to keep worry level numbers from getting too large without affecting the monkey Operation and Test results
            BigInteger commonMultiplePartTwo = monkeys
                .Select(m => m.TestDivisor)
                .Aggregate(BigInteger.One, (product, testDivisor) => product * testDivisor);

            for (var round = 0; round < 10_000; round++)
            {
                // Part One - The Worry Level of them item is divided by 3 after the monkey inspects the item
                if (round < 20)
                {
                    foreach (var monkey in monkeys)
                    {
                        while (monkey.ItemsPartOne.Count > 0)
                        {
                            var item = monkey.ItemsPartOne[0];
                            var operations = monkey.Operation.Split(" ");
                            newItem = operations[0] == "old" ? item : BigInteger.Parse(operations[0]);
                            operand = operations[2] == "old" ? item : BigInteger.Parse(operations[2]);
                            newItem = operations[1] switch
                            {
                                "+" => newItem + operand,
                                "*" => newItem * operand,
                                _ => newItem
                            };
                            monkey.ItemsInspectedPartOne++;
                            newItem /= 3;
                            monkeys.Find(m => m.MonkeyNumber == ((newItem % monkey.TestDivisor).IsZero ? monkey.TrueMonkeyTarget : monkey.FalseMonkeyTarget))
                                .ItemsPartOne.Add(newItem);
                            monkey.ItemsPartOne.RemoveAt(0);
                        }
                    }
                }

                // Part Two - The Worry Level of the item is divided by the Common Multiple after the monkey inspects the item
                foreach (var monkey in monkeys)
                {
                    while (monkey.ItemsPartTwo.Count > 0)
                    {
                        var item = monkey.ItemsPartTwo[0];
                        var operations = monkey.Operation.Split(" ");
                        newItem = operations[0] == "old" ? item : BigInteger.Parse(operations[0]);
                        operand = operations[2] == "old" ? item : BigInteger.Parse(operations[2]);
                        newItem = operations[1] switch
                        {
                            "+" => newItem + operand,
                            "*" => newItem * operand,
                            _ => newItem
                        };
                        monkey.ItemsInspectedPartTwo++;
                        newItem %= commonMultiplePartTwo;
                        monkeys.Find(m => m.MonkeyNumber == ((newItem % monkey.TestDivisor).IsZero ? monkey.TrueMonkeyTarget : monkey.FalseMonkeyTarget))
                            .ItemsPartTwo.Add(newItem);
                        monkey.ItemsPartTwo.RemoveAt(0);
                    }
                }
            }

            var monkeyBusinessLevelPartOne = monkeys
                .OrderByDescending(m => m.ItemsInspectedPartOne)
                .Select(m => m.ItemsInspectedPartOne)
                .Take(2)
                .Aggregate(1L, (product, itemsInspected) => product * itemsInspected);

            var monkeyBusinessLevelPartTwo = monkeys
                .OrderByDescending(m => m.ItemsInspectedPartTwo)
                .Select(m => m.ItemsInspectedPartTwo)
                .Take(2)
                .Aggregate(1L, (product, itemsInspected) => product * itemsInspected);

            var endTimestamp = DateTime.Now;

            return $"{monkeyBusinessLevelPartOne:N0} is the level of monkey business after 20 rounds with Divide By Three worry relief\r\n" +
                   $"{monkeyBusinessLevelPartTwo:N0} is the level of monkey business after 10,000 rounds with Common Multiple worry relief\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}

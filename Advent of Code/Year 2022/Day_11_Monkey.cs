using System.Numerics;

namespace Advent_of_Code.Year_2022_Day_11
{
    public class Monkey
    {

        public List<BigInteger> ItemsPartOne { get; set; } = new();
        public List<BigInteger> ItemsPartTwo { get; set; } = new();

        public long ItemsInspectedPartOne { get; set; } = 0;
        public long ItemsInspectedPartTwo { get; set; } = 0;

        public Monkey(
            int monkeyNumber,
            string operation,
            BigInteger testDivisor,
            int trueMonkeyTarget,
            int falseMonkeyTarget)
        {
            MonkeyNumber = monkeyNumber;
            Operation = operation;
            TestDivisor = testDivisor;
            TrueMonkeyTarget = trueMonkeyTarget;
            FalseMonkeyTarget = falseMonkeyTarget;
        }

        public int MonkeyNumber { get; }

        public string Operation { get; }

        public BigInteger TestDivisor { get; }

        public int TrueMonkeyTarget { get; }

        public int FalseMonkeyTarget { get; }
    }
}

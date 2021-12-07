namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_07(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var positions = input.ReadLine().Split(',').Select(n => int.Parse(n)).ToList();

            var minFuelUsedConstant = int.MaxValue;
            var minFuelPositionConstant = 0;
            var minFuelUsedAccelerated = int.MaxValue;
            var minFuelPositionAccelerated = 0;
            for (var position = positions.Min(); position <= positions.Max(); position++)
            {
                var currentPositionFuelConstant = positions.Sum(n => Math.Abs(n - position));
                if (currentPositionFuelConstant < minFuelUsedConstant)
                {
                    minFuelUsedConstant = currentPositionFuelConstant;
                    minFuelPositionConstant = position;
                }

                var currentPositionFuelAccelerated = positions.Sum(n =>
                    {
                        var dx = Math.Abs(n - position);
                        return dx * (dx + 1) / 2;
                    });
                if (currentPositionFuelAccelerated < minFuelUsedAccelerated)
                {
                    minFuelUsedAccelerated = currentPositionFuelAccelerated;
                    minFuelPositionAccelerated = position;
                }
            }

            var endTimestamp = DateTime.Now;

            return $"{positions.Count():N0} horizontal positions from {positions.Min():N0} to {positions.Max():N0}\r\n" +
                   $"Position {minFuelPositionConstant:N0} costs the least fuel ({minFuelUsedConstant:N0}) for constant fuel rate movement\r\n" +
                   $"Position {minFuelPositionAccelerated:N0} costs the least fuel ({minFuelUsedAccelerated:N0}) for accelerated fuel rate movement\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

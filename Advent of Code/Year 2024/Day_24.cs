using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_24(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            BigInteger decimalNumber = 0;
            long part2 = 0;

            Dictionary<string, int> wires = [];
            List<(string Wire1, string Wire2, string Operation, string TargetWire)> operations = [];

            List<(string Wire1, string Wire2, string Operation, string TargetWire)> unresolved = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Contains(':'))
                {
                    wires.Add(line.Split(':')[0], int.Parse(line.Split(':')[1]));
                }
                else if (line.Length > 0)
                {
                    var parts = line.Split(' ');
                    operations.Add((parts[0], parts[2], parts[1], parts[4]));
                    unresolved.Add((parts[0], parts[2], parts[1], parts[4]));

                    while (unresolved.Any(u => wires.ContainsKey(u.Wire1) && wires.ContainsKey(u.Wire2)))
                    {
                        (string uWire1, string uWire2, string operation, string targetWire) = unresolved.First(u => wires.ContainsKey(u.Wire1) && wires.ContainsKey(u.Wire2));
                        unresolved.Remove((uWire1, uWire2, operation, targetWire));

                        int result = operation switch
                        {
                            "AND" => wires[uWire1] & wires[uWire2],
                            "XOR" => wires[uWire1] ^ wires[uWire2],
                            "OR" => wires[uWire1] | wires[uWire2]
                        };

                        if (!wires.TryAdd(targetWire, result))
                        {
                            wires[targetWire] = result;
                        }
                    }
                }
            }

            decimalNumber = wires
                .Where(w => w.Key.StartsWith('z') && w.Value > 0)
                .Select(kv => int.Parse(kv.Key[1..]))
                .Aggregate((BigInteger)0, (sum, power) => sum + ((BigInteger)1 << power));

            BigInteger xValue = wires
                .Where(w => w.Key.StartsWith('x') && w.Value > 0)
                .Select(kv => int.Parse(kv.Key[1..]))
                .Aggregate((BigInteger)0, (sum, power) => sum + ((BigInteger)1 << power));

            BigInteger yValue = wires
                .Where(w => w.Key.StartsWith('y') && w.Value > 0)
                .Select(kv => int.Parse(kv.Key[1..]))
                .Aggregate((BigInteger)0, (sum, power) => sum + ((BigInteger)1 << power));

            BigInteger sumValue = xValue + yValue;

            StringBuilder sb = new();

            foreach (var wire in wires
                .Where(w => w.Key.StartsWith('z'))
                .Select(kv => kv.Key)
                .Order())
            {
                sb.AppendLine($"{wire}: {wires[wire]} {sumValue >> int.Parse(wire[1..]) & 1}");
            }
            sb.AppendLine();

            foreach (var wire in wires
                .Where(w => w.Key.StartsWith('z'))
                .Select(kv => kv.Key)
                .Order())
            {
                int correctValue = (int)(sumValue >> int.Parse(wire[1..]) & 1);

                if (wires[wire] != correctValue)
                {
                    sb.AppendLine($"{wire} is {wires[wire]} but should be {correctValue}:");

                    List<string> pastWires = [wire];

                    while (operations.Any(o => pastWires.Contains(o.TargetWire) && (!pastWires.Contains(o.Wire1) || !pastWires.Contains(o.Wire2))))
                    {
                        var (wire1, wire2, _, _) = operations.First(o => pastWires.Contains(o.TargetWire) && (!pastWires.Contains(o.Wire1) || !pastWires.Contains(o.Wire2)));

                        if (!pastWires.Contains(wire1))
                        {
                            pastWires.Add(wire1);
                        }

                        if (!pastWires.Contains(wire2))
                        {
                            pastWires.Add(wire2);
                        }
                    }

                    foreach (var (wire1, wire2, operation, targetWire) in operations.Where(o => pastWires.Contains(o.TargetWire)))
                    {
                        sb.AppendLine($"{wire1} {operation} {wire2} -> {targetWire} ({wires[targetWire]})");
                    }
                }

                sb.AppendLine();
            }

            stopwatch.Stop();

            return $"{decimalNumber:N0}\r\n" +
                   $"{part2:N0}\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)" +
                   $"\r\n\r\nx={xValue:N0} y={yValue:N0} x+y={sumValue:N0}" +
                   $"\r\n{sb}";
        }
    }
}

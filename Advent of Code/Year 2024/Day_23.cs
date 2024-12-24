using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_23(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long tripleSetsStartingWithT = 0;
            string passwordForLANParty = "";

            List<(string Computer1, string Computer2)> map = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var computers = line.Split('-');

                map.Add((computers[0], computers[1]));
                map.Add((computers[1], computers[0]));
            }

            Dictionary<(string Computer1, string Computer2), List<string>> count = [];

            foreach (var c in map)
            {
                List<string> reachable = [c.Computer1, c.Computer2];

                string more;

                do
                {
                    more = map.FirstOrDefault(m => reachable.Contains(m.Computer1) && !reachable.Contains(m.Computer2) && reachable.All(r => map.Contains((m.Computer2, r)))).Computer2;

                    if (more is not null)
                    {
                        reachable.Add(more);
                    }
                } while (more is not null);

                count.Add(c, [.. reachable.Order()]);
            }

            tripleSetsStartingWithT = map
                .SelectMany(connection1 => map.Where(connection2 => connection2.Computer1 == connection1.Computer2 && connection2.Computer2 != connection1.Computer1), (connection1, connection2) => (connection1, connection2))
                .SelectMany(pair => map.Where(connection3 => connection3.Computer1 == pair.connection2.Computer2 && connection3.Computer2 == pair.connection1.Computer1), (pair, connection3) => (pair.connection1, pair.connection2, connection3))
                .Where(set => set.connection1.Computer1.StartsWith('t') || set.connection2.Computer1.StartsWith('t') || set.connection3.Computer1.StartsWith('t'))
                .Select(set => string.Join(",", new List<string> { set.connection1.Computer1, set.connection2.Computer1, set.connection3.Computer1 }.Order()))
                .Distinct()
                .Count();

            passwordForLANParty = count
                .Where(c => c.Value.Count == count.Max(c => c.Value.Count))
                .Select(c => string.Join(",", c.Value))
                .Distinct()
                .First();

            stopwatch.Stop();

            return $"{tripleSetsStartingWithT:N0} sets of three inter-connected computers that contain at least one computer with a name that starts with a t\r\n" +
                   $"{passwordForLANParty:N0} is the password to get into the LAN party\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

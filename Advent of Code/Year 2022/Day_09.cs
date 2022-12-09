namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_09(StringReader input)
        {
            var lines = input.ReadToEnd().Split("\r\n");

            var startTimestamp = DateTime.Now;

            var knotsPerRope = new[] { 2, 10 };
            var ropes = knotsPerRope.Length;
            var tailVisits = new List<HashSet<(int X, int Y)>>(ropes);

            foreach (var ropeIndex in Enumerable.Range(0, ropes))
            {
                var knots = knotsPerRope[ropeIndex];

                var rope = new List<(int X, int Y)>();
                rope.AddRange(Enumerable.Repeat((0, 0), knots));

                tailVisits.Add(new HashSet<(int X, int Y)>());
                tailVisits[ropeIndex].Add(rope[^1]);

                foreach (var line in lines)
                {
                    var direction = line[0];
                    var steps = int.Parse(line.Split(" ")[1]);
                    while (steps-- > 0)
                    {
                        foreach (var knotIndex in Enumerable.Range(0, knots))
                        {
                            var knot = rope[knotIndex];
                            if (knotIndex == 0)
                            {
                                switch (direction)
                                {
                                    case 'L':
                                        knot.X--;
                                        break;

                                    case 'R':
                                        knot.X++;
                                        break;

                                    case 'U':
                                        knot.Y++;
                                        break;

                                    case 'D':
                                        knot.Y--;
                                        break;
                                }
                            }
                            else
                            {
                                var lead = rope[knotIndex - 1];
                                if (Math.Abs(lead.X - knot.X) > 1 ||
                                    Math.Abs(lead.Y - knot.Y) > 1)
                                {
                                    if (knot.X != lead.X)
                                    {
                                        knot.X += lead.X > knot.X ? 1 : -1;
                                    }
                                    if (knot.Y != lead.Y)
                                    {
                                        knot.Y += lead.Y > knot.Y ? 1 : -1;
                                    }
                                    if (knotIndex == knots - 1)
                                    {
                                        tailVisits[ropeIndex].Add(knot);
                                    }
                                }
                            }
                            rope[knotIndex] = knot;
                        }
                    }
                }
            }

            var endTimestamp = DateTime.Now;

            return $"The tail of the {knotsPerRope[0]}-knot rope has visited {tailVisits[0].Count:N0} positions at least once\r\n" +
                   $"The tail of the {knotsPerRope[1]}-knot rope has visited {tailVisits[1].Count:N0} positions at least once\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

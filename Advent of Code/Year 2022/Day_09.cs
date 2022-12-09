namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_09(StringReader input)
        {
            var lines = input.ReadToEnd().Split("\r\n");

            var startTimestamp = DateTime.Now;

            // Part One is a rope with 2 knots, Part Two is a rope with 10 knots
            var knotsPerRope = new[] { 2, 10 };
            var ropes = knotsPerRope.Length;

            // Each rope has a HashSet that remembers all of the locations visited by the tail knot
            var tailVisits = new List<HashSet<(int X, int Y)>>(ropes);

            // Do this for each rope
            foreach (var ropeIndex in Enumerable.Range(0, ropes))
            {
                var knots = knotsPerRope[ropeIndex];

                // Each knot in this rope has a location that starts at (0, 0)
                var rope = new List<(int X, int Y)>();
                rope.AddRange(Enumerable.Repeat((0, 0), knots));

                // Remember each location visited by the tail knot of this rope, including its start position
                tailVisits.Add(new HashSet<(int X, int Y)>());
                tailVisits[ropeIndex].Add(rope[^1]);

                // Go through each line that tells the head knot where to move
                foreach (var line in lines)
                {
                    var direction = line[0];
                    var steps = int.Parse(line.Split(" ")[1]);

                    // Move the head knot in the specified direction for the specified number of steps
                    while (steps-- > 0)
                    {
                        // Go through each knot in the rope from the head to the tail and determine its movement
                        foreach (var knotIndex in Enumerable.Range(0, knots))
                        {
                            // Get the location for this knot
                            var knot = rope[knotIndex];

                            // Move the head knot in the direction indicated by the line
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
                                // Non-head knots will move depending on the location of the previous knot
                                var lead = rope[knotIndex - 1];

                                // Only move the knot if the previous knot X or Y is more than 1 ahead
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

                                    // If this is the tail knot then add its new location to tailVisits
                                    if (knotIndex == knots - 1)
                                    {
                                        tailVisits[ropeIndex].Add(knot);
                                    }
                                }
                            }

                            // Update the rope array with the possibly updated knot location
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

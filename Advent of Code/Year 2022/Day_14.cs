namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_14(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var map = new HashSet<(int X, int Y)>();

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                // Each line defines vertices of line segments
                var vertices = line
                    .Split(" -> ")
                    .Select(p => new {
                        X = int.Parse(p.Split(",")[0]),
                        Y = int.Parse(p.Split(",")[1]) })
                    .ToList();

                // Add each point in each line segment to the map
                foreach (var idx in Enumerable.Range(1, vertices.Count - 1))
                {
                    var source = vertices[idx - 1];
                    var target = vertices[idx];

                    if (source.X == target.X)
                    {
                        for (var Y = Math.Min(source.Y, target.Y); Y <= Math.Max(source.Y, target.Y); Y++)
                        {
                            map.Add((source.X, Y));
                        }
                    }
                    else if (source.Y == target.Y)
                    {
                        for (var X = Math.Min(source.X, target.X); X <= Math.Max(source.X, target.X); X++)
                        {
                            map.Add((X, source.Y));
                        }
                    }
                }
            }

            // Part One - stop counting the sand units once a sand unit reaches the Abyss Level (presuming no floor)
            var abyssLevel = map.Max(p => p.Y) + 1;

            // Part Two - stop counting the sand units once a sand unit has nowhere to fall (due to there being a floor)
            var floorLevel = map.Max(p => p.Y) + 2;

            // Add the floor to the map for Part Two
            for (var X = 500 - floorLevel; X <= 500 + floorLevel; X++)
            {
                map.Add((X, floorLevel));
            }

            // Count the sand units that have fallen to rest
            var sandUnitsRestingAbyss = 0;
            var sandUnitsRestingFloor = 0;

            // Keep counting until sand can no longer fall
            var abyssLevelReached = false;
            var sandSourceBlocked = false;

            while (!abyssLevelReached || !sandSourceBlocked)
            {
                (int X, int Y) sandUnit = (500, 0);

                // Here falls the next unit of sand - Whee!
                var sandFalling = true;
                while (sandFalling)
                {
                    if (!map.Contains((sandUnit.X, sandUnit.Y + 1)))
                    {
                        // Falling down one step
                        sandUnit.Y++;
                    }
                    else if (!map.Contains((sandUnit.X - 1, sandUnit.Y + 1)))
                    {
                        // Falling one step down and to the left
                        sandUnit.X--;
                        sandUnit.Y++;
                    }
                    else if (!map.Contains((sandUnit.X + 1, sandUnit.Y + 1)))
                    {
                        // Falling one step down and to the right
                        sandUnit.X++;
                        sandUnit.Y++;
                    }
                    else
                    {
                        // Comes to rest because there is nowhere to fall
                        sandFalling = false;
                        map.Add(sandUnit);

                        if (!abyssLevelReached)
                        {
                            sandUnitsRestingAbyss++;
                        }

                        sandUnitsRestingFloor++;

                        if (sandUnit == (500, 0))
                        {
                            sandSourceBlocked = true;
                        }
                    }

                    // It would fall forever into the abyss if there was no floor
                    if (sandUnit.Y >= abyssLevel)
                    {
                        abyssLevelReached = true;
                    }
                }
            }

            var endTimestamp = DateTime.Now;

            return $"{sandUnitsRestingAbyss:N0} units of sand has come to rest with no floor\r\n" +
                   $"{sandUnitsRestingFloor:N0} units of sand has come to rest with a floor\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

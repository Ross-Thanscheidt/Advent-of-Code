using System.Numerics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        [GeneratedRegex(".*x=(?<SensorX>-?\\d+), y=(?<SensorY>-?\\d+).*x=(?<BeaconX>-?\\d+), y=(?<BeaconY>-?\\d+)")]
        private static partial Regex SensorBeaconRegex();

        public string Day_15(StringReader input)
        {
            const int TEST_MAX_ROW_INDEX = 20;

            var startTimestamp = DateTime.Now;

            var maxRowIndex = TEST_MAX_ROW_INDEX;
            var rowIndexPartOne = 10;

            // beacons is the list of known beacon positions
            var beacons = new HashSet<(int X, int Y)>();

            // mapRanges is a list of rows, each row containing a list of column ranges where no other beacons can exist
            var mapRanges = new List<List<(int StartColumn, int EndColumn)>>();

            // Read input and build beacons and mapRanges for Part One and Part Two
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var groups = SensorBeaconRegex().Match(line).Groups;
                var sensor = new { X = int.Parse(groups["SensorX"].Value), Y = int.Parse(groups["SensorY"].Value) };
                var beacon = new { X = int.Parse(groups["BeaconX"].Value), Y = int.Parse(groups["BeaconY"].Value) };

                if (maxRowIndex == TEST_MAX_ROW_INDEX && sensor.Y > TEST_MAX_ROW_INDEX)
                {
                    maxRowIndex = 4_000_000;
                    rowIndexPartOne = 2_000_000;
                }

                // Add beacon position to list of beacon positions
                beacons.Add((beacon.X, beacon.Y));

                // Add column range for each row that there cannot be any other beacons (since the beacon detected is the closest beacon)
                var maxRange = Math.Abs(sensor.X - beacon.X) + Math.Abs(sensor.Y - beacon.Y);
                foreach (var Y in Enumerable.Range(sensor.Y - maxRange, 2 * maxRange + 1))
                {
                    // We're only tracking rows that are in a specific range
                    if (Y >= 0 && Y <= maxRowIndex)
                    {
                        while (mapRanges.Count - 1 < Y)
                        {
                            mapRanges.Add(new List<(int StartColumn, int EndColumn)>());
                        }

                        (int StartColumn, int EndColumn) range =
                            (sensor.X - (maxRange - Math.Abs(sensor.Y - Y)),
                             sensor.X + (maxRange - Math.Abs(sensor.Y - Y)));

                        var combined = false;
                        var rangeIndex = 0;
                        while (rangeIndex < mapRanges[Y].Count)
                        {
                            if (range.StartColumn <= (mapRanges[Y][rangeIndex].EndColumn + 1) &&
                                range.EndColumn >= (mapRanges[Y][rangeIndex].StartColumn - 1))
                            {
                                range =
                                    (Math.Min(range.StartColumn, mapRanges[Y][rangeIndex].StartColumn),
                                        Math.Max(range.EndColumn, mapRanges[Y][rangeIndex].EndColumn));
                                combined = true;
                                mapRanges[Y].RemoveAt(rangeIndex);
                            }
                            else
                            {
                                rangeIndex++;
                                if (rangeIndex >= mapRanges[Y].Count && combined)
                                {
                                    rangeIndex = 0;
                                    combined = false;
                                }
                            }
                        }

                        mapRanges[Y].Add(range);
                    }
                }
            }

            // Part One - Count the beaconless positions
            var beaconlessPositions = mapRanges[rowIndexPartOne]
                .Select(r => r.EndColumn - r.StartColumn + 1 - beacons.Count(b => b.X >= r.StartColumn && b.X <= r.EndColumn && b.Y == rowIndexPartOne))
                .Aggregate(0, (sum, rangeCount) => sum + rangeCount);

            // Part Two - Find the Distress Beacon location
            (int X, int Y) distressBeacon = (-1, -1);

            var freeRanges = new List<(int StartColumn, int EndColumn)>();
            int freeRangeIndex;

            foreach (var Y in Enumerable.Range(0, maxRowIndex + 1))
            {
                // For each row start with all positions free and then remove beaconless ranges and beacon positions
                freeRanges.Clear();
                freeRanges.Add((0, maxRowIndex));

                // Remove beaconless ranges from free ranges on this row
                foreach (var (beaconlessRangeStart, beaconlessRangeEnd) in mapRanges[Y])
                {
                    freeRangeIndex = 0;
                    while (freeRangeIndex < freeRanges.Count)
                    {
                        var (freeRangeStart, freeRangeEnd) = freeRanges[freeRangeIndex];
                        if (beaconlessRangeStart <= freeRangeEnd &&
                            beaconlessRangeEnd >= freeRangeStart)
                        {
                            if (beaconlessRangeStart <= freeRangeStart &&
                                beaconlessRangeEnd >= freeRangeEnd)
                            {
                                // Remove this free range (do not change the index in order to look at the next free range)
                                freeRanges.RemoveAt(freeRangeIndex);
                            }
                            else
                            {
                                if (beaconlessRangeStart <= freeRangeStart &&
                                    beaconlessRangeEnd < freeRangeEnd)
                                {
                                    // Change the start column of this free range
                                    freeRanges[freeRangeIndex] = (beaconlessRangeEnd + 1, freeRangeEnd);
                                }
                                else if (beaconlessRangeStart > freeRangeStart &&
                                         beaconlessRangeEnd >= freeRangeEnd)
                                {
                                    // Change the end column of this free range
                                    freeRanges[freeRangeIndex] = (freeRangeStart, beaconlessRangeStart - 1);
                                }
                                else if (beaconlessRangeStart > freeRangeStart &&
                                         beaconlessRangeEnd < freeRangeEnd)
                                {
                                    // Split this free range into 2 ranges (removing the beaconess range from the middle of this free range)
                                    freeRanges[freeRangeIndex] = (freeRangeStart, beaconlessRangeStart - 1);
                                    freeRanges.Add((beaconlessRangeStart + 1, freeRangeEnd));
                                }

                                // Look at the next free range to see how this beaconless range affects it
                                freeRangeIndex++;
                            }
                        }
                        else
                        {
                            // Look at the next free range since this beaconless range doesn't overlap with this free range
                            freeRangeIndex++;
                        }
                    }
                }

                // Remove known beacons from free ranges on this row
                freeRangeIndex = 0;
                while (freeRangeIndex < freeRanges.Count)
                {
                    var (freeRangeStart, freeRangeEnd) = freeRanges[freeRangeIndex];

                    // Remove all beacons in this free range
                    var beaconFound = true;
                    while (beaconFound)
                    {
                        var firstBeaconFound = beacons
                            .Where(b => b.Y == Y && b.X >= freeRangeStart && b.X <= freeRangeEnd)
                            .Select(b => b.X)
                            .Take(1);
                        beaconFound = firstBeaconFound.Any();
                        if (beaconFound)
                        {
                            var beaconX = firstBeaconFound.First();

                            // Remove the column position of this beacon from this free range
                            if (freeRangeStart == freeRangeEnd)
                            {
                                // Remove the entire free range since it only has one position and it is a beacon
                                freeRanges.RemoveAt(freeRangeIndex);
                            }
                            else if (beaconX == freeRangeStart)
                            {
                                // Remove the first position from the free range
                                freeRanges[freeRangeIndex] = (freeRangeStart + 1, freeRangeEnd);
                            }
                            else if (beaconX == freeRangeEnd)
                            {
                                // Remove the last position from the free range
                                freeRanges[freeRangeIndex] = (freeRangeStart, freeRangeEnd - 1);
                            }
                            else
                            {
                                // Split the free range into 2 ranges (left and right of this beacon)
                                freeRanges[freeRangeIndex] = (freeRangeStart, beaconX - 1);
                                freeRanges.Add((beaconX + 1, freeRangeEnd));
                            }
                        }
                    }
                    freeRangeIndex++;
                }

                // If there are any remaining free ranges then use the first position for the Distress Beacon
                if (distressBeacon == (-1, -1) && freeRanges.Count > 0)
                {
                    distressBeacon = (freeRanges[0].StartColumn, Y);
                }
            }

            // Part Two - Compute the Tuning Frequency based on the Distress Beacon position
            BigInteger tuningFrequency = 0;
            if (distressBeacon != (-1, -1))
            {
                tuningFrequency = ((BigInteger) distressBeacon.X) * 4_000_000 + distressBeacon.Y;
            }

            var endTimestamp = DateTime.Now;

            return $"There are {beaconlessPositions:N0} positions where a beacon cannot be present\r\n" +
                   $"The tuning frequency is {tuningFrequency:N0} for the distress beacon at ({distressBeacon.X:N0}, {distressBeacon.Y:N0})\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}

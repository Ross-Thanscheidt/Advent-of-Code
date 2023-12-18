using System.Diagnostics;
using Advent_of_Code.Year_2023_Day_16;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_16(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long tilesEnergizedTopLeft = 0;
            long tilesEnergizedLargest = 0;

            Dictionary<(int X, int Y), Tile> tiles = [];
            List<Beam> beams = [];

            int rows = 0;
            int columns = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    columns = line.Length;
                    for (var lineIndex = 0; lineIndex < columns; lineIndex++)
                    {
                        Tile tile = new()
                        {
                            Position = { X = lineIndex, Y = rows },
                            Symbol = line[lineIndex]
                        };

                        tiles.Add((tile.Position.X, tile.Position.Y), tile);
                    }

                    rows++;
                }
            }

            foreach (var edge in "NESW")
            {
                foreach (var edgeIndex in Enumerable.Range(0, "NS".Contains(edge) ? columns : rows))
                {
                    foreach (var tile in tiles)
                    {
                        tile.Value.Energized = false;
                        tile.Value.FromEast = false;
                        tile.Value.FromSouth = false;
                        tile.Value.FromWest = false;
                        tile.Value.FromNorth = false;
                    }

                    beams.Clear();
                    beams.Add(new()
                        {
                            Position = new(
                                    edge == 'W' ? -1 : edge == 'E' ? columns : edgeIndex,
                                    edge == 'S' ? rows : edge == 'N' ? -1 : edgeIndex),
                            Direction = edge switch
                                {
                                    'N' => 'S',
                                    'E' => 'W',
                                    'S' => 'N',
                                    'W' => 'E'
                                }
                        });

                    while (beams.Count > 0)
                    {
                        var beam = beams[0];

                        beam.Position.X += beam.Direction == 'E' ? 1 : beam.Direction == 'W' ? -1 : 0;
                        beam.Position.Y += beam.Direction == 'S' ? 1 : beam.Direction == 'N' ? -1 : 0;

                        if (!tiles.ContainsKey((beam.Position.X, beam.Position.Y)))
                        {
                            beams.RemoveAt(0);
                        }
                        else
                        {
                            var tile = tiles[(beam.Position.X, beam.Position.Y)];

                            bool dejaVu = false;

                            if (beam.Direction == 'E')
                            {
                                if (tile.FromWest)
                                {
                                    dejaVu = true;
                                }
                                else
                                {
                                    tile.FromWest = true;
                                }
                            }
                            else if (beam.Direction == 'S')
                            {
                                if (tile.FromNorth)
                                {
                                    dejaVu = true;
                                }
                                else
                                {
                                    tile.FromNorth = true;
                                }
                            }
                            else if (beam.Direction == 'W')
                            {
                                if (tile.FromEast)
                                {
                                    dejaVu = true;
                                }
                                else
                                {
                                    tile.FromEast = true;
                                }
                            }
                            else if (beam.Direction == 'N')
                            {
                                if (tile.FromSouth)
                                {
                                    dejaVu = true;
                                }
                                else
                                {
                                    tile.FromSouth = true;
                                }
                            }

                            if (dejaVu)
                            {
                                beams.RemoveAt(0);
                            }
                            else
                            {
                                tile.Energized = true;

                                if (tile.Symbol == '/')
                                {
                                    beam.Direction = beam.Direction switch
                                    {
                                        'E' => 'N',
                                        'S' => 'W',
                                        'W' => 'S',
                                        'N' => 'E'
                                    };
                                }
                                else if (tile.Symbol == '\\')
                                {
                                    beam.Direction = beam.Direction switch
                                    {
                                        'E' => 'S',
                                        'S' => 'E',
                                        'W' => 'N',
                                        'N' => 'W'
                                    };
                                }
                                else if (tile.Symbol == '|' && "EW".Contains(beam.Direction))
                                {
                                    beam.Direction = beam.Direction switch
                                    {
                                        'E' => 'N',
                                        'W' => 'N'
                                    };

                                    beams.Add(new() { Position = beam.Position, Direction = 'S' });
                                }
                                else if (tile.Symbol == '-' && "SN".Contains(beam.Direction))
                                {
                                    beam.Direction = beam.Direction switch
                                    {
                                        'S' => 'W',
                                        'N' => 'W'
                                    };

                                    beams.Add(new() { Position = beam.Position, Direction = 'E' });
                                }
                            }
                        }
                    }

                    var tilesEnergized = tiles.Count(tile => tile.Value.Energized);

                    if (edge == 'W' && edgeIndex == 0)
                    {
                        tilesEnergizedTopLeft = tilesEnergized;
                    }

                    tilesEnergizedLargest = Math.Max(tilesEnergizedLargest, tilesEnergized);
                }
            }

            stopwatch.Stop();

            return $"{tilesEnergizedTopLeft:N0} tiles energized from top left corner\r\n" +
                   $"{tilesEnergizedLargest:N0} tiles energized are the most from any edge position\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
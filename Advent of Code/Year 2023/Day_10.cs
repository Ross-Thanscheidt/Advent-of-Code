﻿using System.Diagnostics;
using Advent_of_Code.Year_2023_Day_10;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_10(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long maxSteps = 0;
            long tilesEnclosed = 0;

            Dictionary<(int X, int Y), Tile> tiles = [];
            int rows = 0;
            int columns = 0;

            Position start = new(-1, -1);

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
                            Symbol = line[lineIndex],
                            Type = 'U',
                            North = 'U',
                            South = 'U'
                        };

                        tiles.Add((tile.Position.X, tile.Position.Y), tile);

                        if (tile.Symbol == 'S')
                        {
                            start = tile.Position;
                        }
                    }

                    rows++;
                }
            }

            Position current = start;
            Position previous = start;

            while (current != start || maxSteps == 0)
            {
                Tile tile = tiles[(current.X, current.Y)];
                tile.Type = 'P';

                if (current == start)
                {
                    previous = current;

                    var north = tiles.ContainsKey((current.X, current.Y - 1)) && "|7F".Contains(tiles[(current.X, current.Y - 1)].Symbol);
                    var east = tiles.ContainsKey((current.X + 1, current.Y)) && "-J7".Contains(tiles[(current.X + 1, current.Y)].Symbol);
                    var south = tiles.ContainsKey((current.X, current.Y + 1)) && "|JL".Contains(tiles[(current.X, current.Y + 1)].Symbol);
                    var west = tiles.ContainsKey((current.X - 1, current.Y)) && "-LF".Contains(tiles[(current.X - 1, current.Y)].Symbol);

                    if (north)
                    {
                        tile.Symbol = east ? 'L' : south ? '|' : west ? 'J' : 'U';
                        tile.North = 'U';
                        tile.South = east ? 'L' : west ? 'R' : 'U';
                        current.Y--;
                    }
                    else if (east)
                    {
                        tile.Symbol = south ? 'F' : west ? '-' : 'U';
                        tile.North = south || west ? 'L' : 'U';
                        tile.South = west ? 'R' : 'U';
                        current.X++;
                    }
                    else if (south)
                    {
                        tile.Symbol = west ? '7' : 'U';
                        tile.North = west ? 'L' : 'U';
                        tile.South = 'U';
                        current.Y++;
                    }
                }
                else
                {
                    char tileSymbol = tiles[(current.X, current.Y)].Symbol;

                    if ("|JL".Contains(tileSymbol) && tiles.ContainsKey((current.X, current.Y - 1)) && (previous.X != current.X || previous.Y != current.Y - 1))
                    {
                        previous = current;
                        current.Y--;

                        tile = tiles[(current.X, current.Y)];
                        var nextTileIndex = "7|F".IndexOf(tile.Symbol);
                        tile.North = "RUL"[nextTileIndex];
                        tile.South = 'U';
                    }
                    else if ("-LF".Contains(tileSymbol) && tiles.ContainsKey((current.X + 1, current.Y)) && (previous.X != current.X + 1 || previous.Y != current.Y))
                    {
                        previous = current;
                        current.X++;

                        tile = tiles[(current.X, current.Y)];
                        var nextTileIndex = "J-7".IndexOf(tile.Symbol);
                        tile.North = "ULL"[nextTileIndex];
                        tile.South = "RRU"[nextTileIndex];
                    }
                    else if ("|7F".Contains(tileSymbol) && tiles.ContainsKey((current.X, current.Y + 1)) && (previous.X != current.X || previous.Y != current.Y + 1))
                    {
                        previous = current;
                        current.Y++;

                        tile = tiles[(current.X, current.Y)];
                        var nextTileIndex = "J|L".IndexOf(tile.Symbol);
                        tile.North = 'U';
                        tile.South = "LUR"[nextTileIndex];
                    }
                    else if ("-J7".Contains(tileSymbol) && tiles.ContainsKey((current.X - 1, current.Y)) && (previous.X != current.X - 1 || previous.Y != current.Y))
                    {
                        previous = current;
                        current.X--;

                        tile = tiles[(current.X, current.Y)];
                        var nextTileIndex = "L-F".IndexOf(tile.Symbol);
                        tile.North = "URR"[nextTileIndex];
                        tile.South = "LLU"[nextTileIndex];
                    }
                }

                maxSteps++;
            }

            maxSteps = (maxSteps + 1) / 2;

            Dictionary<char, char> mapLR = [];

            for (var column = 0; column < columns; column++)
            {
                var inLoop = false;

                for (var row = 0; row < rows; row++)
                {
                    var tile = tiles[(column, row)];

                    if (tile.Type != 'P' && inLoop)
                    {
                        tilesEnclosed++;
                    }

                    if (mapLR.Count == 0 && tile.Type == 'P' && "LR".Contains(tile.North))
                    {
                        mapLR.Add(tile.North, inLoop ? 'I' : 'O');
                        mapLR.Add(tile.North == 'L' ? 'R' : 'L', inLoop ? 'O' : 'I');
                    }

                    if (tile.Type == 'P' && "LR".Contains(tile.South) && mapLR.Count > 0)
                    {
                        inLoop = mapLR[tile.South] == 'I';
                    }
                }
            }

            stopwatch.Stop();

            return $"{maxSteps:N0} steps to the point farthest from the starting position\r\n" +
                   $"{tilesEnclosed:N0} tiles are enclosed by the loop\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

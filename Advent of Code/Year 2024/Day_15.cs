using System.Diagnostics;
using Direction = (int dx, int dy);
using Position = (int X, int Y);

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_15(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long sumGPS1 = 0;
            long sumGPS2 = 0;

            Dictionary<Position, char> map1 = [];
            Dictionary<Position, char> map2 = [];
            Position robot1 = (0, 0);
            Position robot2 = (0, 0);
            Position target = (0, 0);

            Dictionary<char, Direction> directions = [];
            directions.Add('^', (0, -1));
            directions.Add('v', (0, 1));
            directions.Add('<', (-1, 0));
            directions.Add('>', (1, 0));

            int rows = 0;
            int columns = 0;

            bool loadMap = true;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (loadMap)
                {
                    if (line.Length == 0)
                    {
                        robot1 = map1.First(m => m.Value == '@').Key;
                        robot2 = map2.First(m => m.Value == '@').Key;
                        loadMap = false;
                    }
                    else
                    {
                        columns = line.Length;

                        for (int column = 0; column < columns; column++)
                        {
                            char tile = line[column];
                            map1.Add((column, rows), tile);

                            map2.Add((column * 2, rows), tile == 'O' ? '[' : tile);
                            map2.Add((column * 2 + 1, rows), tile == '@' ? '.' : tile == 'O' ? ']' : tile);
                        }

                        rows++;
                    }
                }
                else
                {
                    foreach (char movement in line)
                    {
                        (int dx, int dy) = directions[movement];

                        // Part One

                        Position space = robot1;

                        for (Position position = robot1; map1[position] != '#'; position = (position.X + dx, position.Y + dy))
                        {
                            if (map1[position] == '.')
                            {
                                space = position;
                                break;
                            }
                        }

                        if (space != robot1)
                        {
                            bool robotReached = false;

                            for (target = (space.X - dx, space.Y - dy); !robotReached; target = (target.X - dx, target.Y - dy))
                            {
                                map1[space] = map1[target];
                                map1[target] = '.';

                                if (target == robot1)
                                {
                                    robot1 = space;
                                    robotReached = true;
                                }

                                space = target;
                            }
                        }

                        // Part Two

                        target = (robot2.X + dx, robot2.Y + dy);

                        if (map2[target] == '.')
                        {
                            map2[robot2] = '.';
                            map2[target] = '@';
                            robot2 = target;
                        }
                        else if ("[]".Contains(map2[target]))
                        {
                            int level = 1;

                            Dictionary<Position, int> obstaclesToMove = new()
                            {
                                { robot2, level++ }
                            };

                            bool blockFound = false;
                            bool canMove = false;

                            while (!blockFound && !canMove)
                            {
                                bool allSpaces = true;

                                var obstaclesToMoveThisLevel = obstaclesToMove.Where(o => o.Value == level - 1).ToList();

                                foreach (var obstacleToMove in obstaclesToMoveThisLevel)
                                {
                                    Position checkTile = (obstacleToMove.Key.X + dx, obstacleToMove.Key.Y + dy);

                                    if (map2[checkTile] == '#')
                                    {
                                        blockFound = true;
                                    }
                                    else if (map2[checkTile] == '[')
                                    {
                                        allSpaces = false;

                                        if ("<>".Contains(movement))
                                        {
                                            obstaclesToMove.Add(checkTile, level);
                                        }
                                        else
                                        {
                                            obstaclesToMove.TryAdd(checkTile, level);
                                            obstaclesToMove.TryAdd((checkTile.X + 1, checkTile.Y), level);
                                        }
                                    }
                                    else if (map2[checkTile] == ']')
                                    {
                                        allSpaces = false;

                                        if ("<>".Contains(movement))
                                        {
                                            obstaclesToMove.Add(checkTile, level);
                                        }
                                        else
                                        {
                                            obstaclesToMove.TryAdd((checkTile.X - 1, checkTile.Y), level);
                                            obstaclesToMove.TryAdd(checkTile, level);
                                        }
                                    }
                                }


                                if (allSpaces && !blockFound)
                                {
                                    canMove = true;
                                }
                                else
                                {
                                    level++;
                                }
                            }

                            if (!blockFound && canMove)
                            {
                                while (level > 0)
                                {
                                    foreach (var obstacleToMove in obstaclesToMove.Where(o => o.Value == level))
                                    {
                                        target = (obstacleToMove.Key.X + dx, obstacleToMove.Key.Y + dy);

                                        if (map2[target] == '.')
                                        {
                                            map2[target] = map2[obstacleToMove.Key];
                                            map2[obstacleToMove.Key] = '.';

                                            if (map2[target] == '@')
                                            {
                                                robot2 = target;
                                            }
                                        }
                                    }

                                    level--;
                                }
                            }
                        }
                    }
                }
            }

            sumGPS1 = map1.Where(m => m.Value == 'O').Sum(m => m.Key.X + 100 * m.Key.Y);
            sumGPS2 = map2.Where(m => m.Value == '[').Sum(m => m.Key.X + 100 * m.Key.Y);

            stopwatch.Stop();

            return $"{sumGPS1:N0} is the sum of the final GPS coordinates of all of the boxes in the first warehouse\r\n" +
                   $"{sumGPS2:N0} is the sum of the final GPS coordinates of all of the boxes in the second warehouse\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

using Advent_of_Code.Year_2021_Day_15;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {

        private CavernMap? _cavernMap;
        private int _lowestTotalRisk;

        public string Day_15(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            // Get input lines
            var lines = new List<string>();
            for (var line = input.ReadLine()?.Trim(); line != null; line = input.ReadLine())
            {
                if (line.Length > 0)
                {
                    lines.Add(line);
                }
            }

            // Load array of risk levels into CavernMap object
            _cavernMap = new CavernMap(lines.Count, lines[0].Length);
            foreach (var line in lines)
            {
                var riskLevelsRow = line.Select(c => int.Parse(c.ToString())).ToList();
                _cavernMap.AddRow(riskLevelsRow);
            }

            _lowestTotalRisk = _cavernMap.MaxTotalRisk;
            var startPosition = new Position { RowIndex = 0, ColumnIndex = 0 };
            var potentialPaths = new PriorityQueue<(List<Position>, int), int>();
            (List<Position>, int) startElement = new (new List<Position>() { startPosition }, (int)(_cavernMap?.Rows + _cavernMap?.Columns ?? 0));
            potentialPaths.Enqueue(startElement, 0);

            var keepLooking = true;
            while (keepLooking)
            {
                keepLooking = potentialPaths.TryDequeue(out (List<Position>, int) currentElement, out int currentPriority);
                if (keepLooking)
                {
                    var currentPath = currentElement.Item1;
                    var currentTotalRisk = currentElement.Item2;
                    var currentPosition = currentPath.Last();
                    foreach (var nextPosition in (_cavernMap?[currentPosition.RowIndex, currentPosition.ColumnIndex].PossibleMoves)
                                                .Where(nextPosition => !currentPath.Contains(nextPosition) &&
                                                       currentTotalRisk + _cavernMap?[nextPosition].RiskLevel <= _lowestTotalRisk))
                    {
                        int nextPositionTotalRisk = currentTotalRisk + (int)(_cavernMap?[nextPosition].RiskLevel ?? 0);
                        if (nextPosition.RowIndex == _cavernMap?.Rows - 1 &&
                            nextPosition.ColumnIndex == _cavernMap?.Columns - 1)
                        {
                            if (nextPositionTotalRisk < _lowestTotalRisk)
                            {
                                _lowestTotalRisk = nextPositionTotalRisk;
                            }
                        }
                        else
                        {
                            var nextPositionPath = currentPath.ToList();
                            nextPositionPath.Add(nextPosition);
                            potentialPaths.Enqueue((nextPositionPath, nextPositionTotalRisk), (int)(nextPositionTotalRisk + _cavernMap?.Rows - nextPosition.RowIndex + _cavernMap?.Columns - nextPosition.ColumnIndex));
                        }
                    }
                    keepLooking = currentTotalRisk <= _lowestTotalRisk;
                }
            }
                var endTimestamp = DateTime.Now;

                return $"CavernMap is {_cavernMap.Columns}x{_cavernMap.Rows} with a Max Total Risk of {_cavernMap.MaxTotalRisk:N0}\r\n" +
                       $"The lowest total risk of any path from the top left to the bottom right is {_lowestTotalRisk:N0}\r\n" +
                       $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
            }

            //private void FindAllPaths(Position position, List<Position> currentPath, int currentTotalRisk)
            //{
            //if (position.RowIndex == _cavernMap?.Rows - 1 && position.ColumnIndex == _cavernMap?.Columns - 1)
            //{
            //    if (currentTotalRisk < _lowestTotalRisk)
            //    {
            //        _lowestTotalRisk = currentTotalRisk;
            //    }
            //}
            //else
            //{
            //    foreach (var nextPosition in (_cavernMap?[position.RowIndex, position.ColumnIndex].PossibleMoves)
            //                                    .Where(nextPosition => !currentPath.Contains(nextPosition) &&
            //                                           currentTotalRisk + _cavernMap?[nextPosition].RiskLevel < _lowestTotalRisk &&
            //                                           (_cavernMap?[nextPosition].LowestRiskLevelHere == 0 ||
            //                                            currentTotalRisk + _cavernMap?[nextPosition].RiskLevel <= _cavernMap?[nextPosition].LowestRiskLevelHere)))
            //    {
            //        _cavernMap[nextPosition].LowestRiskLevelHere = currentTotalRisk + _cavernMap[nextPosition].RiskLevel;
            //        currentPath.Add(nextPosition);
            //        FindAllPaths(nextPosition, currentPath, currentTotalRisk + _cavernMap[nextPosition].RiskLevel);
            //        currentPath.Remove(nextPosition);
            //    }
            //}
            //}
    }
}

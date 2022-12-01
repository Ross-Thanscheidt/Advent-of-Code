using System.Text.RegularExpressions;
using Advent_of_Code.Extensions.Year_2021.Day_17;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_17(StringReader input)
        {
            int xTargetLeft = 0;
            int xTargetRight = 0;
            int yTargetTop = 0;
            int yTargetBottom = 0;

            var startTimestamp = DateTime.Now;

            var output = "";
            var line = input.ReadLine();
            if (line != null)
            {
                var matchPoint = new Regex(@"x=(?<x1>-?\d+)\.\.(?<x2>-?\d+)").Match(line);
                if (matchPoint.Success)
                {
                    xTargetLeft = matchPoint.GetInt("x1");
                    xTargetRight = matchPoint.GetInt("x2");
                    if (xTargetRight < xTargetLeft)
                    {
                        (xTargetLeft, xTargetRight) = (xTargetRight, xTargetLeft);
                    }
                    if (xTargetLeft < 0)
                    {
                        throw new Exception("x range must be 0 or greater");
                    }
                }
                
                matchPoint = new Regex(@"y=(?<y1>-?\d+)\.\.(?<y2>-?\d+)").Match(line);
                if (matchPoint.Success)
                {
                    yTargetTop = matchPoint.GetInt("y1");
                    yTargetBottom = matchPoint.GetInt("y2");
                    if (yTargetTop < yTargetBottom)
                    {
                        (yTargetTop, yTargetBottom) = (yTargetBottom, yTargetTop);
                    }
                    if (yTargetTop > 0)
                    {
                        throw new Exception("y range must be below 0");
                    }
                }

                var yHighestPosition = 0;
                for (var dx = (int)Math.Ceiling(Math.Sqrt(1 + 8 * xTargetLeft) / 2 - 0.5); dx <= xTargetRight; dx++)
                {
                    output += $"dx={dx}\r\n";
                    var dxHitTarget = false;
                    var dy = yTargetBottom;
                    var dyKeepGoing = true;
                    do
                    {
                        // Check out (dx,dy)
                        //   dxHitTarget?  If so, did y go higher than yHighestPosition?  dyKeepGoing = false if y < yTargetBottom right after y >= 0
                        //   If x1 <= x <= x2 and y1 >= y >= y2
                        //     dxHitTarget = true
                        //     if dxMaxy > yHighestPosition then yHighestPosition = dxmaxy
                        //     done with this dy
                        //   dyKeepGoing = false if y < y2 and previous y == 0
                        //   Until dxHitTarget or x > x2 or y < y2
                        dy++;
                    }
                    while (dyKeepGoing);
                }


            }

            var endTimestamp = DateTime.Now;

            return $"x1={xTargetLeft}, x2={xTargetRight}, y1={yTargetTop}, y2={yTargetBottom}\r\n" +
                   $"{output}\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}

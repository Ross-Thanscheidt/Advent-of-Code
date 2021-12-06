namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_02(StringReader input)
        {
            int horizontalPosition = 0;
            int depth = 0;
            int depthWithAim = 0;
            int aim = 0;
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                int units = int.Parse(line.Split(' ')[1]);
                switch (line.Split(' ')[0])
                {
                    case "forward":
                        horizontalPosition += units;
                        depthWithAim += aim * units;
                        break;

                    case "down":
                        depth += units;
                        aim += units;
                        break;

                    case "up":
                        depth -= units;
                        aim -= units;
                        break;
                }
            }
            return $"Horizontal Position is {horizontalPosition:N0}\r\n" +
                   $"Depth is {depth:N0}\r\n" +
                   $"Product is {horizontalPosition * depth:N0}\r\n" +
                   $"\r\n" +
                   $"With Aim:\r\n" +
                   $"Horizontal Position is {horizontalPosition:N0}\r\n" +
                   $"Depth is {depthWithAim:N0}\r\n" +
                   $"Product is {horizontalPosition * depthWithAim:N0}";
        }

    }
}

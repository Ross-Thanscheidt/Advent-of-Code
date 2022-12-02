namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_02(StringReader input)
        {
            var allLines = input.ReadToEnd().Split("\r\n");

            var startTimestamp = DateTime.Now;
            
            // Part One - XYZ is Object (Rock/Paper/Scissors)

            Dictionary<char, string> objectLoseDrawWin = new()
            {
                { 'X', "BAC" },
                { 'Y', "CBA" },
                { 'Z', "ACB" }
            };

            long objectScore = allLines
                .Select(line => new { opponent = line[0], myObject = line[2] })
                .Sum(line =>
                        "XYZ".IndexOf(line.myObject) + 1 + // Rock=1, Paper=2, Scissors = 3
                        objectLoseDrawWin[line.myObject].IndexOf(line.opponent) * 3); // Lose=0, Draw=3, Win=6

            // Part Two - XYZ is Outcome (Lose/Draw/Win)

            Dictionary<char, int[]> outcomeLoseDrawWin = new()
            {
                { 'A', new int[] { 3, 1, 2 } },
                { 'B', new int[] { 1, 2, 3 } },
                { 'C', new int[] { 2, 3, 1 } }
            };

            long outcomeScore = allLines
                .Select(line => new { opponent = line[0], myOutcome = line[2] })
                .Sum(line =>
                    outcomeLoseDrawWin[line.opponent]["XYZ".IndexOf(line.myOutcome)] + // Rock=1, Paper=2, Scissors=3
                    "XYZ".IndexOf(line.myOutcome) * 3); // Lose=0, Draw=3, Win=6

            var endTimestamp = DateTime.Now;

            return $"Score is {objectScore:N0} when X/Y/Z means object (Rock/Paper/Scissors)\r\n" +
                   $"Score is {outcomeScore:N0} when X/Y/Z means outcome (Lose/Draw/Win)\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}

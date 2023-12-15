using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        private string _Day_12_Line = "";

        private List<int> _Day_12_Groups = [];

        private int Day_12_Matches(int lineIndex, int groupIndex)
        {
            int matches = 0;

            while (lineIndex < _Day_12_Line.Length && !"?#".Contains(_Day_12_Line[lineIndex]))
            {
                lineIndex++;
            }

            if (lineIndex < _Day_12_Line.Length)
            {
                // We are at # or ?
                var groupCount = _Day_12_Groups[groupIndex];
                var lastGroup = groupIndex == _Day_12_Groups.Count - 1;

                // See if the next Group could work here (with . or ? following if this is not the last Group)
                var input = _Day_12_Line[lineIndex..];
                var pattern = @"^[#\?]" + (groupCount > 1 ? $"{{{groupCount}}}" : "") + (!lastGroup ? @"[\.\?]" : "");

                if (Regex.IsMatch(input, pattern))
                {
                    var newLineIndex = lineIndex + groupCount + (lastGroup ? 0 : 1);

                    if (lastGroup)
                    {
                        matches += newLineIndex < _Day_12_Line.Length && _Day_12_Line[newLineIndex..].Contains('#') ? 0 : 1;
                    }
                    else
                    {
                        matches += Day_12_Matches(newLineIndex, groupIndex + 1);
                    }
                }

                // For ? explore making this . instead of #
                if (_Day_12_Line[lineIndex] == '?')
                {
                    matches += Day_12_Matches(lineIndex + 1, groupIndex);
                }
            }

            return matches;
        }

        public string Day_12(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var totalArrangements = 0;
            //var totalArrangementsUnfolded = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                _Day_12_Line = line.Split(" ")[0];
                _Day_12_Groups = line.Split(" ")[1].Split(",").Select(n => int.Parse(n)).ToList();

                totalArrangements += Day_12_Matches(0, 0);

                //_Day_12_Line = string.Join('?', Enumerable.Repeat(_Day_12_Line, 5));
                //List<int> repeatedGroups = [];
                //for (var group = 0; group < 5; group++)
                //{
                //    repeatedGroups.AddRange(_Day_12_Groups);
                //}

                //_Day_12_Groups = repeatedGroups;

                //totalArrangementsUnfolded += Day_12_Matches(0, 0);
            }

            stopwatch.Stop();

            return $"{totalArrangements:N0} is the sum of the number of different arrangements of operational and broken springs\r\n" +
                   //$"{totalArrangementsUnfolded:N0} is the sum of the number of different arrangements of unfolded operational and broken springs\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}
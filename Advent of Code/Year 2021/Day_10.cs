namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {

        public string Day_10(StringReader input)
        {
            Dictionary<char, char> matchingChunkDelimiters = new()
                {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
                };

            Dictionary<char, int> syntaxErrorPoints = new()
                {
                    { ')', 3 },
                    { ']', 57 },
                    { '}', 1197 },
                    { '>', 25137 }
                };

            Dictionary<char, int> autocompletePoints = new()
                {
                    { ')', 1 },
                    { ']', 2 },
                    { '}', 3 },
                    { '>', 4 }
                };

            var startTimestamp = DateTime.Now;

            var syntaxErrorScore = 0;
            var autocompleteScores = new List<long>();
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var corruptedLine = false;
                var stack = new Stack<char>();
                foreach (var currentCharacter in line.Trim())
                {
                    if (matchingChunkDelimiters.ContainsKey(currentCharacter))
                    {
                        stack.Push(currentCharacter);
                    }
                    else if (matchingChunkDelimiters.ContainsValue(currentCharacter))
                    {
                        var openingChunkDelimiter = stack.Pop();
                        if (!matchingChunkDelimiters[openingChunkDelimiter].Equals(currentCharacter))
                        {
                            corruptedLine = true;
                            syntaxErrorScore += syntaxErrorPoints[currentCharacter];
                        }
                    }
                }

                if (!corruptedLine)
                {
                    var completionString = "";
                    long autocompleteScore = 0;
                    while (stack.Count > 0)
                    {
                        var openingChunkDelimiter = stack.Pop();
                        var closingChunkDelimiter = matchingChunkDelimiters[openingChunkDelimiter];
                        completionString += closingChunkDelimiter;
                        autocompleteScore = autocompleteScore * 5 + autocompletePoints[closingChunkDelimiter];
                    }
                    autocompleteScores.Add(autocompleteScore);
                }
            }

            autocompleteScores.Sort();

            var endTimestamp = DateTime.Now;

            return $"Total syntax error score is {syntaxErrorScore:N0}\r\n" +
                   $"The middle autocomplete score is {autocompleteScores[autocompleteScores.Count / 2]:N0}\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

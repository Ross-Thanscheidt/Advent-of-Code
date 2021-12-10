namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_08(StringReader input)
        {
            List<string> standardDigitSegments = new()
            {
                "abcefg",   // 0
                "cf",       // 1
                "acdeg",    // 2
                "acdfg",    // 3
                "bcdf",     // 4
                "abdfg",    // 5
                "abdefg",   // 6
                "acf",      // 7
                "abcdefg",  // 8
                "abcdfg"    // 9
            };

            List<int> uniqueSegmentLengths = new()
            {
                standardDigitSegments[1].Length,
                standardDigitSegments[4].Length,
                standardDigitSegments[7].Length,
                standardDigitSegments[8].Length
            };

            var startTimestamp = DateTime.Now;

            var uniqueSegmentLengthsCount = 0;
            var outputValuesSum = 0;
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                // Parse input line
                var uniqueSignalPatterns = line.Split("|")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var outputValueDigits = line.Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                // Count the number of Output Values Digits with unique segment lengths
                uniqueSegmentLengthsCount += outputValueDigits.Where(digit => uniqueSegmentLengths.Contains(digit.Length)).ToList().Count();

                // Store segment counts for each segment in the Unique Signal Pattern Digits for this line
                var segmentCounts = String.Join("", uniqueSignalPatterns)
                    .GroupBy(c => c)
                    .ToDictionary(c => c.Key, c => c.Count());

                // For each Unique Signal Pattern Digit in this line, determine its Digit Value and store in a Lookup Dictionary
                // (be able to look up the Digit Value using a sorted list of Digit Segments)
                var segmentsToDigit = new Dictionary<string, int>();
                var digit = -1;
                for (var digitIndex = 0; digitIndex < uniqueSignalPatterns.Length; digitIndex++)
                {
                    var digitSegments = new string(uniqueSignalPatterns[digitIndex].OrderBy(c => c).ToArray());
                    switch (digitSegments.Length)
                    {
                        case 2:
                            digit = 1;
                            break;
                        case 3:
                            digit = 7;
                            break;
                        case 4:
                            digit = 4;
                            break;
                        case 5:
                            digit = digitSegments.Where(segment => segmentCounts[segment] == 4).ToList().Count() > 0
                                    ? 2
                                    : digitSegments.Where(segment => segmentCounts[segment] == 6).ToList().Count() > 0
                                    ? 5
                                    : 3;
                            break;
                        case 6:
                            digit = digitSegments.Where(segment => segmentCounts[segment] == 7).ToList().Count() == 1
                                    ? 0
                                    : digitSegments.Where(segment => segmentCounts[segment] == 7).ToList().Count() == 2 &&
                                      digitSegments.Where(segment => segmentCounts[segment] == 4).ToList().Count() == 1
                                    ? 6
                                    : 9;
                            break;
                        case 7:
                            digit = 8;
                            break;
                    }
                    segmentsToDigit.Add(digitSegments, digit);
                }

                // For each Output Value Digit, look up its Digit Value and add to the Output Value for this line
                var outputValue = 0;
                for (var outputValueDigitIndex = 0; outputValueDigitIndex < outputValueDigits.Length; outputValueDigitIndex++)
                {
                    var digitSegments = new string(outputValueDigits[outputValueDigitIndex].OrderBy(c => c).ToArray());
                    outputValue = outputValue * 10 + segmentsToDigit[digitSegments];
                }
                outputValuesSum += outputValue;
                }

            var endTimestamp = DateTime.Now;

            return $"There are {uniqueSegmentLengthsCount:N0} output value digits with unique segment lengths\r\n" +
                   $"The sum of the output value digits is {outputValuesSum:N0}\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

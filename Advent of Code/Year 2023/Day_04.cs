using System.Diagnostics;
using System.Text.RegularExpressions;
using Advent_of_Code.Extensions.Year_2023.Day_04;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex("(.*?(?<Number>\\d+).*?)*")]
        private static partial Regex Day_04_CardNumbersRegex();

        public string Day_04(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int totalPoints = 0;
            int totalScratchcards = 0;

            var cardsWon = new List<int>();

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var winningNumbers = Day_04_CardNumbersRegex()
                    .Match(line.Split(":")[1].Split("|")[0])
                    .Groups["Number"]
                    .Captures
                    .Select(capture => int.Parse(capture.Value))
                    .ToList();

                var possessedNumbers = Day_04_CardNumbersRegex()
                    .Match(line.Split(":")[1].Split("|")[1])
                    .Groups["Number"]
                    .Captures
                    .Select(capture => int.Parse(capture.Value))
                    .ToList();

                var matchedNumbers = winningNumbers
                    .Join(
                        possessedNumbers,
                        winningNumber => winningNumber,
                        possessedNumber => possessedNumber,
                        (winningNumber, _) => winningNumber)
                    .ToList();

                int cardCopies = 1 + cardsWon.PopFirst(valueIfEmpty: 0);
                totalScratchcards += cardCopies;
                var numbersMatched = matchedNumbers.Count;

                if (numbersMatched > 0)
                {
                    totalPoints += 1 << (numbersMatched - 1);

                    var cardIndex = 0;
                    matchedNumbers.ForEach(_ => cardsWon.AddValue(cardIndex++, cardCopies));
                }
            }

            stopwatch.Stop();

            return $"{totalPoints:N0} points in total\r\n" +
                   $"{totalScratchcards:N0} total scratchcards\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}

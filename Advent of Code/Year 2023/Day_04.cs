using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex("(.*?(?<Number>\\d+).*?)*")]
        private static partial Regex Day_04_CardNumbersRegex();

        public string Day_04(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int pointSum = 0;
            int scratchcardCount = 0;

            var cardsWon = new List<int>();

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var winningMatchGroups = Day_04_CardNumbersRegex().Match(line.Split(":")[1].Split("|")[0]).Groups;
                var winningNumbers = winningMatchGroups["Number"].Captures
                    .Select(capture => int.Parse(capture.Value))
                    .ToList();

                var possessedMatchGroups = Day_04_CardNumbersRegex().Match(line.Split(":")[1].Split("|")[1]).Groups;
                var possessedNumbers = possessedMatchGroups["Number"].Captures
                    .Select(capture => int.Parse(capture.Value))
                    .ToList();

                var matchedNumbers = winningNumbers
                    .Join(
                        possessedNumbers,
                        winningNumber => winningNumber,
                        possessedNumber => possessedNumber,
                        (winningNumber, possessedNumber) => possessedNumber);

                int cardCopies = 1;

                if (cardsWon.Count > 0)
                {
                    cardCopies += cardsWon[0];
                    cardsWon.RemoveAt(0);
                }

                scratchcardCount += cardCopies;

                var numbersMatched = matchedNumbers.Count();

                if (numbersMatched > 0)
                {
                    pointSum += 1 << (numbersMatched - 1);

                    for (int cardIndex = 0; cardIndex < numbersMatched; cardIndex++)
                    {
                        if (cardIndex < cardsWon.Count)
                        {
                            cardsWon[cardIndex] += cardCopies;
                        }
                        else
                        {
                            cardsWon.Add(cardCopies);
                        }
                    }
                }
            }

            stopwatch.Stop();

            return $"{pointSum:N0} points in total\r\n" +
                   $"{scratchcardCount:N0} total scratchcards\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }

    }
}

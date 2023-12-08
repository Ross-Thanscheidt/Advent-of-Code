using System.Diagnostics;
using Advent_of_Code.Year_2023_Day_07;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        public string Day_07(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var totalWinnings = 0;

            List<Hand> hands = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var hand = new Hand
                {
                    Cards = line.Split(" ")[0],
                    Bid = int.Parse(line.Split(" ")[1]),
                    HandType = 0,
                    ComparableCards = ""
                };

                foreach (char card in hand.Cards)
                {
                    hand.ComparableCards += (char)("23456789TJQKA".IndexOf(card) + 'A');
                }

                var handType = string.Join("",
                    hand.ComparableCards
                        .GroupBy(
                            card => card,
                            (card, cards) => new { Card = card, Count = cards.Count() })
                        .OrderByDescending(c => c.Count)
                        .Select(c => c.Count.ToString()));

                hand.HandType = handType switch
                    {
                        "5" => 7,
                        "41" => 6,
                        "32" => 5,
                        "311" => 4,
                        "221" => 3,
                        "2111" => 2,
                        "11111" => 1,
                        _ => 0
                    };

                hands.Add(hand);
            }

            var rankedHands = hands
                .OrderBy(hand => hand.HandType)
                .ThenBy(hand => hand.ComparableCards)
                .ToList();

            for (var rank = 1; rank <= rankedHands.Count; rank++)
            {
                totalWinnings += rank * rankedHands[rank - 1].Bid;
            }

            stopwatch.Stop();

            return $"{totalWinnings:N0} are the total winnings\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

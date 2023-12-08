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
            var totalWinningsWild = 0;

            List<Hand> hands = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var hand = new Hand
                {
                    Cards = line.Split(" ")[0],
                    Bid = int.Parse(line.Split(" ")[1]),
                    HandType = 0,
                    HandTypeWild = 0,
                    ComparableCards = "",
                    ComparableCardsWild = ""
                };

                foreach (char card in hand.Cards)
                {
                    hand.ComparableCards += (char)("23456789TJQKA".IndexOf(card) + 'A');
                    hand.ComparableCardsWild += (char)("J23456789TQKA".IndexOf(card) + 'A');
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

                var handTypeWild = string.Join("",
                    hand.ComparableCardsWild.Replace("A", "")
                        .GroupBy(
                            card => card,
                            (card, cards) => new { Card = card, Count = cards.Count() })
                        .OrderByDescending(c => c.Count)
                        .Select(c => c.Count.ToString()));

                var wildcardsCount = hand.ComparableCardsWild.Count(c => c == 'A');

                if (wildcardsCount == 5)
                {
                    handTypeWild = "5";
                }
                else
                {
                    handTypeWild = (int.Parse(handTypeWild[0].ToString()) + wildcardsCount).ToString() + handTypeWild[1..];
                }

                hand.HandTypeWild = handTypeWild switch
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

            var rankedHandsWild = hands
                .OrderBy(hand => hand.HandTypeWild)
                .ThenBy(hand => hand.ComparableCardsWild)
                .ToList();

            for (var rank = 1; rank <= rankedHandsWild.Count; rank++)
            {
                totalWinningsWild += rank * rankedHandsWild[rank - 1].Bid;
            }

            stopwatch.Stop();

            return $"{totalWinnings:N0} are the total winnings\r\n" +
                   $"{totalWinningsWild:N0} are the total winnings with wildcards\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

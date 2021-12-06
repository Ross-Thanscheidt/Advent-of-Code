using Advent_of_Code.Year_2021_Day_04;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{

    public partial class Year_2021 : IYear
    {
        public string Day_04(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            List<int> numbersDrawn = new List<int>();
            List<Board> boards = new List<Board>();
            Board newBoard = new Board();
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (numbersDrawn.Count == 0)
                {
                    numbersDrawn = line.Split(',').Select(n => int.Parse(n)).ToList();
                }
                else
                {
                    if (line.Trim().Length > 0)
                    {
                        newBoard.AddRow(Regex.Replace(line.Trim(), @"\s+", " ").Split(' ').Select(n => int.Parse(n)).ToList());
                        if (newBoard.BoardDefined)
                        {
                            boards.Add(newBoard);
                            newBoard = new Board();
                        }
                    }
                }
            }

            var firstScore = 0;
            var lastScore = 0;
            var boardsLeft = boards.Count;
            foreach (var numberDrawn in numbersDrawn)
            {
                foreach (var board in boards)
                {
                    if (!board.BoardWins)
                    {
                        board.NumberDrawn(numberDrawn);
                        if (board.BoardWins)
                        {
                            if (firstScore == 0)
                            {
                                firstScore = board.UnMarkedSum * numberDrawn;
                            }
                            if (--boardsLeft == 0)
                            {
                                lastScore = board.UnMarkedSum * numberDrawn;
                            }
                        }
                    }
                }
            }

            var endTimestamp = DateTime.Now;

            return $"{numbersDrawn.Count} numbers, {boards.Count} boards\r\n" +
                   $"Score of First Board to Win is {firstScore:N0}\r\n" +
                   $"Score of Last Board to Win is {lastScore:N0}\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

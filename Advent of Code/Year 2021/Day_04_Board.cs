namespace Advent_of_Code.Year_2021_Day_04
{
    public class Board
    {
        private Square[,] _board;
        private int _currentRow = 0;

        public Board()
        {
            _board = new Square[5, 5];
        }

        public void AddRow(List<int> numbersInRow)
        {
            if (numbersInRow.Count == _board.GetLength(0))
            {
                for (var numberIndex = 0; numberIndex < numbersInRow.Count; numberIndex++)
                {
                    _board[_currentRow, numberIndex].Number = numbersInRow[numberIndex];
                }
                _currentRow++;
            }
        }

        public bool BoardDefined { get { return _currentRow == _board.GetLength(0); } }

        public bool NumberDrawn(int numberDrawn)
        {
            var numberFound = false;
            for (var rowIndex = 0; rowIndex < _board.GetLength(0); rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < _board.GetLength(1); columnIndex++)
                {
                    if (_board[rowIndex, columnIndex].Number == numberDrawn)
                    {
                        _board[rowIndex, columnIndex].Marked = true;
                        numberFound = true;
                    }
                }
            }
            return numberFound;
        }

        public bool BoardWins
        {
            get
            {
                for (var rowIndex = 0; rowIndex < _board.GetLength(0); rowIndex++)
                {
                    var win = true;
                    for (var columnIndex = 0; columnIndex < _board.GetLength(1); columnIndex++)
                    {
                        if (!_board[rowIndex, columnIndex].Marked)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win)
                    {
                        return true;
                    }
                }
                for (var columnIndex = 0; columnIndex < _board.GetLength(1); columnIndex++)
                {
                    var win = true;
                    for (var rowIndex = 0; rowIndex < _board.GetLength(0); rowIndex++)
                    {
                        if (!_board[rowIndex, columnIndex].Marked)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public int UnMarkedSum
        {
            get
            {
                var sum = 0;
                for (var rowIndex = 0; rowIndex < _board.GetLength(0); rowIndex++)
                {
                    for (var columnIndex = 0; columnIndex < _board.GetLength(1); columnIndex++)
                    {
                        var board = _board[rowIndex, columnIndex];
                        if (!board.Marked)
                        {
                            sum += board.Number;
                        }
                    }
                }
                return sum;
            }
        }
    }
}

namespace Advent_of_Code.Year_2021_Day_15
{
    public struct Position
    {
        public int RowIndex;
        public int ColumnIndex;
    }

    public class CavernPosition
    {
        public int RiskLevel { get; set; }
        public int LowestRiskLevelHere { get; set; }
        public List<Position> PossibleMoves { get; set; }
    }
    public class CavernMap
    {
        private CavernPosition[,] _cavernMap;
        private int _currentRowIndex = 0;
        private int _maxTotalRisk = 0;

        public CavernMap(int rows, int columns)
        {
            _cavernMap = new CavernPosition[rows, columns];
        }

        public int Rows
        {
            get { return _cavernMap.GetLength(0); }
        }

        public int Columns
        {
            get { return _cavernMap.GetLength(1); }
        }

        public int MaxTotalRisk
        {
            get { return _maxTotalRisk; }
        }

        public CavernPosition this[int row, int column]
        {
            get => _cavernMap[row, column];
            set => _cavernMap[row, column] = value;
        }

        public CavernPosition this[Position position]
        {
            get => _cavernMap[position.RowIndex, position.ColumnIndex];
            set => _cavernMap[position.RowIndex, position.ColumnIndex] = value;
        }

        public void AddRow(List<int> riskLevelsRow)
        {
            if (riskLevelsRow.Count == this.Columns)
            {
                for (var columnIndex = 0; columnIndex < riskLevelsRow.Count; columnIndex++)
                {
                    _cavernMap[_currentRowIndex, columnIndex] = new CavernPosition { RiskLevel = riskLevelsRow[columnIndex] };
                }
                _currentRowIndex++;

                if (_currentRowIndex == this.Rows)
                {
                    InitializeMap();
                }
            }
        }

        private List<Position> PossibleMoves(int rowIndex, int columnIndex)
        {
            var moves = new List<Position>();

            // Down
            if (rowIndex < this.Rows - 1)
            {
                moves.Add(new Position { RowIndex = rowIndex + 1, ColumnIndex = columnIndex });
            }

            // Right
            if (columnIndex < this.Columns - 1)
            {
                moves.Add(new Position { RowIndex = rowIndex, ColumnIndex = columnIndex + 1 });
            }

            // Left
            if (columnIndex > 0)
            {
                moves.Add(new Position { RowIndex = rowIndex, ColumnIndex = columnIndex - 1 });
            }

            // Up - Moving up is not an option if on the right side if never occupying the same position twice
            if (columnIndex < this.Columns - 1 && rowIndex > 0)
            {
                moves.Add(new Position { RowIndex = rowIndex - 1, ColumnIndex = columnIndex });
            }

            return moves;
        }

        private void InitializeMap()
        {
            // Initialize lowest total risk level here and define the list of possible moves for each cavern position
            for (var rowIndex = 0; rowIndex < this.Rows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < this.Columns; columnIndex++)
                {
                    _cavernMap[rowIndex, columnIndex].LowestRiskLevelHere = 0;
                    _cavernMap[rowIndex, columnIndex].PossibleMoves = PossibleMoves(rowIndex, columnIndex);
                }
            }

            // Calculate the Max Total Risk using a simple path (go down and then go right)
            _maxTotalRisk = Enumerable.Range(1, this.Rows - 1).Sum(rowIndex => _cavernMap[rowIndex, 0].RiskLevel) +
                            Enumerable.Range(1, this.Columns - 1).Sum(columnIndex => _cavernMap[this.Rows - 1, columnIndex].RiskLevel);
        }
    }
}

namespace Advent_of_Code.Year_2022_Day_12
{
    public struct Position
    {
        public int RowIndex;

        public int ColumnIndex;

        public static bool operator ==(Position left, Position right)
        {
            return left.RowIndex == right.RowIndex && left.ColumnIndex == right.ColumnIndex;
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }

    public class AreaPosition
    {
        public char ElevationLevel { get; set; }

        public int LeastStepsHere { get; set; }

        public List<Position> PossibleMoves { get; set; } = new();
    }

    public class AreaMap
    {
        private AreaPosition[,] _areaMap;
        private Position _endPosition;
        private int _currentRowIndex = 0;

        public AreaMap(int rows, int columns)
        {
            _areaMap = new AreaPosition[rows, columns];
        }

        public int Rows
        {
            get { return _areaMap.GetLength(0); }
        }

        public int Columns
        {
            get { return _areaMap.GetLength(1); }
        }

        public Position StartPosition { get; set; }

        public Position EndPosition => _endPosition;

        public AreaPosition this[int row, int column]
        {
            get => _areaMap[row, column];
            set => _areaMap[row, column] = value;
        }

        public AreaPosition this[Position position]
        {
            get => _areaMap[position.RowIndex, position.ColumnIndex];
            set => _areaMap[position.RowIndex, position.ColumnIndex] = value;
        }

        public void AddRow(string line)
        {
            var elevationLevelsRow = line.ToList();
            if (elevationLevelsRow.Count == this.Columns)
            {
                for (var columnIndex = 0; columnIndex < elevationLevelsRow.Count; columnIndex++)
                {
                    var elevationLevel = elevationLevelsRow[columnIndex];
                    if (elevationLevel == 'S')
                    {
                        StartPosition = new Position { RowIndex = _currentRowIndex, ColumnIndex = columnIndex }; 
                        elevationLevel = 'a';
                    }
                    else if (elevationLevel == 'E')
                    {
                        _endPosition.RowIndex = _currentRowIndex;
                        _endPosition.ColumnIndex = columnIndex;
                        elevationLevel = 'z';
                    }
                    _areaMap[_currentRowIndex, columnIndex] = new AreaPosition { ElevationLevel = elevationLevel };
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
            var maxElevationLevel = _areaMap[rowIndex, columnIndex].ElevationLevel + 1;

            var moves = new List<Position>();

            // Down
            if (rowIndex < this.Rows - 1 &&
                _areaMap[rowIndex + 1, columnIndex].ElevationLevel <= maxElevationLevel)
            {
                moves.Add(new Position { RowIndex = rowIndex + 1, ColumnIndex = columnIndex });
            }

            // Right
            if (columnIndex < this.Columns - 1 &&
                _areaMap[rowIndex, columnIndex + 1].ElevationLevel <= maxElevationLevel)
            {
                moves.Add(new Position { RowIndex = rowIndex, ColumnIndex = columnIndex + 1 });
            }

            // Left
            if (columnIndex > 0 &&
                _areaMap[rowIndex, columnIndex - 1].ElevationLevel <= maxElevationLevel)
            {
                moves.Add(new Position { RowIndex = rowIndex, ColumnIndex = columnIndex - 1 });
            }

            // Up
            if (rowIndex > 0 &&
                _areaMap[rowIndex - 1, columnIndex].ElevationLevel <= maxElevationLevel)
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
                    _areaMap[rowIndex, columnIndex].LeastStepsHere = this.Rows * this.Columns;
                    _areaMap[rowIndex, columnIndex].PossibleMoves = PossibleMoves(rowIndex, columnIndex);
                }
            }
        }

        public IEnumerable<Position> StartingPositions
        {
            get
            {
                var startingPositions = new List<Position>();
                foreach (var rowIndex in Enumerable.Range(0, this.Rows))
                {
                    foreach (var columnIndex in Enumerable.Range(0, this.Columns))
                    {
                        if (_areaMap[rowIndex, columnIndex].ElevationLevel == 'a')
                        {
                            startingPositions.Add(new Position() { RowIndex = rowIndex, ColumnIndex = columnIndex });
                        }
                    }
                }
                return startingPositions;
            }
        }
    }
}

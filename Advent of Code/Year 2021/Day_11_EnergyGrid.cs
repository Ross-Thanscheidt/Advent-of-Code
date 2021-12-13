namespace Advent_of_Code.Year_2021_Day_11
{
    public class EnergyGrid
    {
        private int[,] _energyGrid;
        private int _currentRowIndex = 0;
        private int _flashCount = 0;

        public EnergyGrid(int rows, int columns)
        {
            _energyGrid = new int[rows, columns];
        }

        public int Rows
        {
            get { return _energyGrid.GetLength(0); }
        }

        public int Columns
        {
            get { return _energyGrid.GetLength(1); }
        }

        public int this[int row, int column]
        {
            get => _energyGrid[row, column];
            set
            {
                _energyGrid[row, column] = value;
                if (value == 10)
                {
                    Flash(row, column);
                }
            }
        }

        public int FlashCount
        {
            get { return _flashCount; }
        }

        protected void Flash(int row, int column)
        {
            _flashCount++;
            for (var rowIndex = row - 1; rowIndex <= row + 1; rowIndex++)
            {
                for (var columnIndex = column - 1; columnIndex <= column + 1; columnIndex++)
                {
                    if (rowIndex >= 0 && rowIndex < this.Rows &&
                        columnIndex >= 0 && columnIndex < this.Columns &&
                        (rowIndex != row || columnIndex != column))
                    {
                        this[rowIndex, columnIndex]++;
                    }
                }
            }
        }

        public int ResetFlashEnergyLevels()
        {
            var currentFlashCount = 0;
            for (var rowIndex = 0; rowIndex < this.Rows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < this.Columns; columnIndex++)
                {
                    if (this[rowIndex, columnIndex] > 9)
                    {
                        this[rowIndex, columnIndex] = 0;
                        currentFlashCount++;
                    }
                }
            }
            return currentFlashCount;
        }

        public void AddRow(List<int> energyLevelsInRow)
        {
            if (energyLevelsInRow.Count == this.Columns)
            {
                for (var columnIndex = 0; columnIndex < energyLevelsInRow.Count; columnIndex++)
                {
                    _energyGrid[_currentRowIndex, columnIndex] = energyLevelsInRow[columnIndex];
                }
                _currentRowIndex++;
            }
        }
    }
}

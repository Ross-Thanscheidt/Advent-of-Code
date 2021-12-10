namespace Advent_of_Code.Year_2021_Day_09
{
    public class HeightMap
    {
        private Location[,] _heightMap;
        private int _currentRowIndex = 0;
        private Dictionary<int, int> _basinSizes;

        public HeightMap(int rows, int columns)
        {
            _heightMap = new Location[rows, columns];
            _basinSizes = new();
        }

        public int Rows
        {
            get { return _heightMap.GetLength(0); }
        }

        public int Columns
        {
            get { return _heightMap.GetLength(1); }
        }

        public Location this[int rowIndex, int columnIndex]
        {
            get => _heightMap[rowIndex, columnIndex];
        }

        public void AddRow(List<int> heightsInRow)
        {
            if (heightsInRow.Count == _heightMap.GetLength(1))
            {
                for (var columnIndex = 0; columnIndex < heightsInRow.Count; columnIndex++)
                {
                    _heightMap[_currentRowIndex, columnIndex].Height = heightsInRow[columnIndex];
                }
                _currentRowIndex++;
            }
        }

        public IEnumerable<Location> AdjacentLocations(int rowIndex, int columnIndex)
        {
            var adjacentLocations = new List<Location>();

            if (rowIndex > 0)
            {
                adjacentLocations.Add(_heightMap[rowIndex - 1, columnIndex]);
            }
            if (columnIndex < _heightMap.GetLength(1) - 1)
            {
                adjacentLocations.Add(_heightMap[rowIndex, columnIndex + 1]);
            }
            if (rowIndex < _heightMap.GetLength(0) - 1)
            {
                adjacentLocations.Add(_heightMap[rowIndex + 1, columnIndex]);
            }
            if (columnIndex > 0)
            {
                adjacentLocations.Add(_heightMap[rowIndex, columnIndex - 1]);
            }
            return adjacentLocations;
        }

        public void SetBasinId(int rowIndex, int columnIndex, int basinId)
        {
            if (rowIndex >= 0 && rowIndex < this.Rows &&
                columnIndex >= 0 && columnIndex < this.Columns &&
                _heightMap[rowIndex, columnIndex].Height != 9 &&
                _heightMap[rowIndex, columnIndex].BasinId == 0)
            {
                _heightMap[rowIndex, columnIndex].BasinId = basinId;
                if (_basinSizes.ContainsKey(basinId))
                {
                    _basinSizes[basinId]++;
                }
                else
                {
                    _basinSizes.Add(basinId, 1);
                }
                SetBasinId(rowIndex - 1, columnIndex, basinId);
                SetBasinId(rowIndex, columnIndex + 1, basinId);
                SetBasinId(rowIndex + 1, columnIndex, basinId);
                SetBasinId(rowIndex, columnIndex - 1, basinId);
            }
        }

        public IEnumerable<int> BasinSizesLargestFirst()
        {
            return _basinSizes.OrderByDescending(bs => bs.Value).Select(bs => bs.Value).ToList();
        }
    }
}

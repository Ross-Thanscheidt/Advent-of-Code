namespace Advent_of_Code.Year_2021_Day_12
{
    public class Cave
    {
        private string _name;
        private bool _bigCave;
        List<Cave> _adjacentCaves = new();

        public Cave(string name)
        {
            _name = name;
            _bigCave = char.IsUpper(name[0]);
        }

        public string Name
        {
            get => _name;
        }

        public bool BigCave { get => _bigCave; }

        public void AddAdjacentCave(Cave adjacentCave)
        {
            if (!_adjacentCaves.Contains(adjacentCave))
            {
                _adjacentCaves.Add(adjacentCave);
                adjacentCave.AddAdjacentCave(this);
            }
        }

        public IEnumerable<Cave> AdjacentCaves
        {
            get { return _adjacentCaves; }
        }
    }
}

using Advent_of_Code.Year_2021_Day_12;
using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        private Dictionary<string, Cave> _caves = new();
        private List<string> _pathsOnce = new();
        private List<string> _pathsTwice = new();

        protected Cave AddCave(string caveName)
        {
            if (!_caves.ContainsKey(caveName))
            {
                _caves.Add(caveName, new Cave(caveName));
            }
            return _caves[caveName];
        }

        protected List<string> AddCaveToPath(string basePath, string caveName, bool allowMultipleSmallCaveVisits)
        {
            List<string> paths = new();
            var currentPath = String.Join(",", basePath.Split(",", StringSplitOptions.RemoveEmptyEntries).Concat(new string[] { caveName }));
            if (caveName == "end")
            {
                paths.Add(currentPath);
            }
            else
            {
                foreach (var adjacentCave in _caves[caveName].AdjacentCaves.Where(ac => ac.Name != "start"))
                {
                    var caveVisitedBefore = currentPath.Split(",").Any(caveName => caveName == adjacentCave.Name);
                    if (adjacentCave.BigCave || allowMultipleSmallCaveVisits || !caveVisitedBefore)
                    {
                        var cavePaths = AddCaveToPath(currentPath, adjacentCave.Name, (adjacentCave.BigCave || !caveVisitedBefore) && allowMultipleSmallCaveVisits);
                        paths.AddRange(cavePaths);
                    }
                }
            }
            return paths;
        }

        public string Day_12(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var output = "";
            do
            {
                _caves.Clear();

                for (var line = input.ReadLine(); line != null && line.Length > 0; line = input.ReadLine())
                {
                    var caveNames = line.Split('-');
                    var cave1 = AddCave(caveNames[0]);
                    var cave2 = AddCave(caveNames[1]);
                    cave1.AddAdjacentCave(cave2);
                }

                if (_caves.ContainsKey("start") && _caves.ContainsKey("end"))
                {
                    _pathsOnce = AddCaveToPath("", "start", false);
                    output += $"There are {_pathsOnce.Count} paths through this cave system with visiting small caves at most once\r\n";

                    _pathsTwice = AddCaveToPath("", "start", true);
                    output += $"There are {_pathsTwice.Count} paths through this cave system with visiting a single small cave twice\r\n";

                    output += $"\r\n";
                }
            } while (_caves.Count > 0);

            var endTimestamp = DateTime.Now;

            return output +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        const int TOTAL_DISK_SPACE = 70_000_000;
        const int UPDATE_SPACE_NEEDED = 30_000_000;

        [GeneratedRegex("^\\d+ .+")]
        private static partial Regex FileSizeLineRegex();

        private static string ParentDirectory(string directory)
        {
            if (directory == "/")
            {
                return "";
            }
            else
            {
                var parent = directory[..directory.LastIndexOf("/")];
                return parent.Length == 0 ? "/" : parent;
            }
        }

        private static string BuildDirectoryPath(string baseDirectory, string subDirectory)
        {
            return baseDirectory + (baseDirectory == "/" ? "" : "/") + subDirectory;
        }

        public string Day_07(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            // Build dirSizes to be a list of directories with their total sizes

            var dirSizes = new Dictionary<string, long>();

            var currentDirectory = "/";
            dirSizes.Add(currentDirectory, 0);

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.StartsWith("$ cd "))
                {
                    var subDirectory = line.Split(" ")[2];
                    if (subDirectory == "/")
                    {
                        currentDirectory = subDirectory;
                    }
                    else if (subDirectory == "..")
                    {
                        currentDirectory = ParentDirectory(currentDirectory);
                    }
                    else
                    {
                        currentDirectory = BuildDirectoryPath(currentDirectory, subDirectory);
                        dirSizes.TryAdd(currentDirectory, 0);
                    }
                }
                else if (line.StartsWith("dir "))
                {
                    var subDirectory = BuildDirectoryPath(currentDirectory, line.Split(" ")[1]);
                    dirSizes.TryAdd(subDirectory, 0);
                }
                else if (FileSizeLineRegex().IsMatch(line))
                {
                    var fileSize = long.Parse(line.Split(" ")[0]);
                    dirSizes[currentDirectory] += fileSize;
                }
            }

            // Add the Total Size of each subdirectory into each of its parent directories

            foreach (var dirSize in dirSizes)
            {
                var parentDirectory = ParentDirectory(dirSize.Key);
                while (parentDirectory != "")
                {
                    dirSizes.TryAdd(parentDirectory, 0);
                    dirSizes[parentDirectory] += dirSize.Value;
                    parentDirectory = ParentDirectory(parentDirectory);
                }
            }

            // Part One - Sum all of the directories that have a total size that is not bigger than 10,000

            var dirSumUpTo100K = dirSizes
                .Where(ds => ds.Value <= 100_000)
                .Sum(ds => ds.Value);

            // Part Two - Find the smallest directory that can be deleted that would free up enough space for the update

            var spaceUsed = dirSizes["/"];
            var unusedSpace = TOTAL_DISK_SPACE - spaceUsed;
            var spaceNeeded = UPDATE_SPACE_NEEDED - unusedSpace;
            var dirToDelete = dirSizes
                .Where(ds => ds.Value >= spaceNeeded)
                .OrderBy(ds => ds.Value)
                .First();

            var endTimestamp = DateTime.Now;

            return $"{dirSumUpTo100K:N0} is the Total Size of all directories that are at most 100,000\r\n" +
                   $"Free up {dirToDelete.Value:N0} by deleting {dirToDelete.Key} (needed at least {spaceNeeded:N0})\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}
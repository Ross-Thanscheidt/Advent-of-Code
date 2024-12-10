using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_09(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long filesystemChecksumMovingBlocks = 0;
            long filesystemChecksumMovingFiles = 0;

            Queue<int> diskmapFreeBlocks = [];
            SortedDictionary<int, int> diskmapBlocks = [];

            SortedDictionary<int, int> diskmapFreeFiles = [];
            Dictionary<int, (int Blocks, int Position)> diskmapFiles = [];

            int maxID = 0;
            int positionBlocks = 0;
            int positionFiles = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                for (int idx = 0; idx < line.Length; idx++)
                {
                    int digit = line[idx] - '0';

                    if (idx % 2 == 0)
                    {
                        for (int i = 0; i < digit; i++)
                        {
                            diskmapBlocks.Add(positionBlocks++, maxID);
                        }

                        diskmapFiles.Add(maxID++, (digit, positionFiles));
                    }
                    else if (digit > 0)
                    {
                        for (int i = 0; i < digit; i++)
                        {
                            diskmapFreeBlocks.Enqueue(positionBlocks++);
                        }

                        diskmapFreeFiles.Add(positionFiles, digit);
                    }

                    positionFiles += digit;
                }
            }

            // Part One

            for (var last = diskmapBlocks.Last(); diskmapFreeBlocks.Count != 0 && last.Key > diskmapFreeBlocks.Peek(); last = diskmapBlocks.Last())
            {
                diskmapBlocks.Remove(last.Key);
                diskmapBlocks.Add(diskmapFreeBlocks.Dequeue(), last.Value);
            }

            foreach (var block in diskmapBlocks)
            {
                filesystemChecksumMovingBlocks += block.Key * block.Value;
            }

            // Part Two

            for (int id = maxID - 1; id >= 0; id--)
            {
                (int blocks, int oldPosition) = diskmapFiles[id];

                if (diskmapFreeFiles.Any(map => map.Value >= blocks))
                {
                    var freeSpaces = diskmapFreeFiles.First(map => map.Value >= blocks);
                    int newPosition = freeSpaces.Key;
                    int freeBlocks = freeSpaces.Value;

                    if (newPosition < oldPosition)
                    {
                        diskmapFreeFiles.Remove(newPosition);

                        if (freeBlocks > blocks)
                        {
                            diskmapFreeFiles.Add(newPosition + blocks, freeBlocks - blocks);
                        }

                        diskmapFiles.Remove(id);
                        diskmapFiles.Add(id, (blocks, newPosition));
                    }
                }
            }

            foreach (var file in diskmapFiles)
            {
                for (int block = 0; block < file.Value.Blocks; block++)
                {
                    filesystemChecksumMovingFiles += (file.Value.Position + block) * file.Key;
                }
            }

            stopwatch.Stop();

            return $"{filesystemChecksumMovingBlocks:N0} is the filesystem checksum when moving one block at a time\r\n" +
                   $"{filesystemChecksumMovingFiles:N0} is the filesystem checksum when moving whole files\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        private static int Priority(char itemType)
            => itemType - (char.IsLower(itemType) ? 'a' : 'A' - 26) + 1;

        public string Day_03(StringReader input)
        {
            var lines = input.ReadToEnd().Split("\r\n");

            var startTimestamp = DateTime.Now;

            var rearrangeItemsPriorities = lines
                .Select(line =>
                {
                    var compartment1 = line[..(line.Length / 2)];
                    var compartment2 = line[(line.Length / 2)..];
                    var commonItemType = compartment1
                        .Intersect(compartment2)
                        .First();
                    return Priority(commonItemType);
                });

            var badgeGroupPriorities = lines
                .Chunk(3)
                .Select(groupLines =>
                {
                    var commonItemType = groupLines[0]
                        .Intersect(groupLines[1])
                        .Intersect(groupLines[2])
                        .First();
                    return Priority(commonItemType);
                });

            var endTimestamp = DateTime.Now;

            return $"{rearrangeItemsPriorities.Sum():N0} is the sum of the Item Rearrangment Priorities\r\n" +
                   $"{badgeGroupPriorities.Sum():N0} is the sum of the Badge Group Priorities\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

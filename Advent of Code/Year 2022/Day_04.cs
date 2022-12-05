namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_04(StringReader input)
        {
            var lines = input.ReadToEnd().Split("\r\n");

            var startTimestamp = DateTime.Now;

            var rangePairs = lines
                .Select(line =>
                {
                    var range1 = line.Split(",")[0];
                    var range2 = line.Split(",")[1];
                    return new
                    {
                        range1 = new { Start = int.Parse(range1.Split("-")[0]), End = int.Parse(range1.Split("-")[1]) },
                        range2 = new { Start = int.Parse(range2.Split("-")[0]), End = int.Parse(range2.Split("-")[1]) }
                    };
                });

            var containingPairs = rangePairs
                .Where(pair =>
                    (pair.range1.Start >= pair.range2.Start && pair.range1.End <= pair.range2.End) ||
                    (pair.range2.Start >= pair.range1.Start && pair.range2.End <= pair.range1.End));

            var overlappingPairs = rangePairs
                .Where(pair =>
                    !((pair.range1.Start < pair.range2.Start && pair.range1.End < pair.range2.Start) ||
                      (pair.range1.Start > pair.range2.End && pair.range1.End > pair.range2.End)));
            
            var endTimestamp = DateTime.Now;

            return $"There are {containingPairs.Count():N0} assignment pairs where one range fully contains the other\r\n" +
                   $"There are {overlappingPairs.Count():N0} assignment pairs where the ranges overlap\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

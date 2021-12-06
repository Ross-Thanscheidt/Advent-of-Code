namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_12(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
            }

            var endTimestamp = DateTime.Now;

            return $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

    }
}

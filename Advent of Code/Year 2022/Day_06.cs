namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_06(StringReader input)
        {
            var line = input.ReadToEnd();

            var startTimestamp = DateTime.Now;

            var distinctCharacterLengths = new[] { 4, 14 };
            var charsProcessed = new List<int>();
            
            foreach (var chunkSize in distinctCharacterLengths)
            {
                var idx = 0;
                while (idx + chunkSize <= line.Length &&
                       line.AsSpan(idx, chunkSize).ToArray().Distinct().Count() != chunkSize)
                {
                    idx++;
                }
                charsProcessed.Add(idx + chunkSize);
            }

            var endTimestamp = DateTime.Now;

            return $"{charsProcessed[0]:N0} chars processed to reach the first start-of-packet marker ({line.Substring(charsProcessed[0] - distinctCharacterLengths[0], distinctCharacterLengths[0])})\r\n" +
                   $"{charsProcessed[1]:N0} chars processed to reach the first start-of-message marker ({line.Substring(charsProcessed[1] - distinctCharacterLengths[1], distinctCharacterLengths[1])})\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}

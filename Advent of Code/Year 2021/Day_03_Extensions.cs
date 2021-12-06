namespace Advent_of_Code.Extensions.Year_2021.Day_03
{
    public static class Day_03
    {
        // Determine if there are more 0's or 1's at specified string index for all binary strings in the list
        // (return 1 if there are the same number of 0's and 1's)
        public static int MostCommonBit(this IEnumerable<string> bitStringList, int bitPosition)
        {
            int bitCount = 0;
            foreach (string bitString in bitStringList)
            {
                bitCount += bitString[bitPosition] == '1'
                            ? 1
                            : bitString[bitPosition] == '0'
                              ? -1
                              : 0;
            }
            return bitCount >= 0 ? 1 : 0;
        }

        // Remove all binary strings that have the specified Bit Value (0 or 1) at the specified string index
        public static void TrimByBit(this List<string> bitStringList, int bitPosition, int bitValueToTrim)
        {
            var bitCharToTrim = bitValueToTrim.ToString()[0];
            for (var listIndex = 0; listIndex < bitStringList.Count;)
            {
                if (bitStringList[listIndex][bitPosition] == bitCharToTrim)
                    bitStringList.RemoveAt(listIndex);
                else
                    listIndex++;
            }
        }
    }
}

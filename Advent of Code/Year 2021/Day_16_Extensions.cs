using System.Text;

namespace Advent_of_Code.Extensions.Year_2021.Day_16
{
    public static class Day_16
    {
        public static string GetCharacters(this StringBuilder stringBuilder, int length)
        {
            var firstCharacters = stringBuilder.ToString(0, length);
            stringBuilder.Remove(0, length);
            return firstCharacters;
        }
    }
}

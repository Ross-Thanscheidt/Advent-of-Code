namespace Advent_of_Code.Extensions.Year_2023.Day_04
{
    public static class Day_04_Extensions
    {
        public static int PopFirst(this List<int> list, int valueIfEmpty)
        {
            int value = valueIfEmpty;

            if (list.Count > 0)
            {
                value = list[0];
                list.RemoveAt(0);
            }

            return value;
        }

        public static void AddValue(this List<int> list, int index, int value)
        {
            if (index < list.Count)
            {
                list[index] += value;
            }
            else
            {
                list.Add(value);
            }

        }
    }
}

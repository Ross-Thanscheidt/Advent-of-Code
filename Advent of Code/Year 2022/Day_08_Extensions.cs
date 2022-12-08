namespace Advent_of_Code.Extensions.Year_2022
{
    public static class Day_08
    {
        public static IEnumerable<T> TakeUntilIncluding<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach (T element in list)
            {
                yield return element;
                if (predicate(element))
                {
                    yield break;
                }
            }
        }
    }
}

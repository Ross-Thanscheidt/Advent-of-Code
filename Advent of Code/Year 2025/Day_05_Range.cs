namespace Advent_of_Code.Year_2025_Day_05
{
    public struct Range(long start, long end)
    {
        public long Start { get; set; } = start;

        public long End { get; set; } = end;

        public bool Overlaps(Range other)
            => this.Start <= other.End && this.End >= other.Start;

        public bool Adjacent(Range other)
            => this.End + 1 == other.Start || other.End + 1 == this.Start;
    }
}

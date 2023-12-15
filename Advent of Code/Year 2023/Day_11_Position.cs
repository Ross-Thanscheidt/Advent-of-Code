namespace Advent_of_Code.Year_2023_Day_11
{
    public class Position
    {
        public long X;

        public long Y;

        public Position(long x, long y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position left, Position right)
        {
            return (left.X == right.X) && (left.Y == right.Y);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}
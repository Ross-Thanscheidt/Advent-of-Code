namespace Advent_of_Code.Year_2023_Day_16
{
    public struct Position
    {
        public int X;

        public int Y;

        public Position(int x, int y)
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
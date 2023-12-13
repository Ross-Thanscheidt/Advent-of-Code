namespace Advent_of_Code.Year_2023_Day_10
{
    public struct Position(int initX = -1, int initY = -1)
    {
        public int X = initX;

        public int Y = initY;

        public static bool operator ==(Position left, Position right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}
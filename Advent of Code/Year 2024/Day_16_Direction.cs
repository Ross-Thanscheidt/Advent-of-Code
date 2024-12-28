namespace Advent_of_Code.Year_2024_Day_16
{
    public struct Direction(int dx, int dy)
    {
        public int dx { get; set; } = dx;

        public int dy { get; set; } = dy;

        public static bool operator ==(Direction left, Direction right)
        {
            return left.dx == right.dx && left.dy == right.dy;
        }

        public static bool operator !=(Direction left, Direction right)
        {
            return !(left == right);
        }
    }
}

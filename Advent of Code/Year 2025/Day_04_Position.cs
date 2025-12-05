namespace Advent_of_Code.Year_2025_Day_04
{
    public struct Position(int x, int y)
    {
        public int X { get; set; } = x;

        public int Y { get; set; } = y;

        public static Position operator +(Position position, Direction direction)
            => new(position.X + direction.dx, position.Y + direction.dy);

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

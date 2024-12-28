namespace Advent_of_Code.Year_2024_Day_18
{
    public struct Position(int x, int y)
    {
        public int X { get; set; } = x;

        public int Y { get; set; } = y;

        public int Distance(Position other) => Math.Abs(other.X - X) + Math.Abs(other.Y - Y);

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

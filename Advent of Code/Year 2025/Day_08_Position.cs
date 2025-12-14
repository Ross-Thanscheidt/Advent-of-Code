namespace Advent_of_Code.Year_2025_Day_08
{
    public struct Position(int x, int y, int z)
    {
        public int X { get; set; } = x;

        public int Y { get; set; } = y;

        public int Z { get; set; } = z;

        public static bool operator ==(Position left, Position right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public double Distance(Position other)
            => Math.Sqrt(Math.Pow(other.X - this.X, 2) + Math.Pow(other.Y - this.Y, 2) + Math.Pow(other.Z - this.Z, 2));

        //public double Distance(Position other)
        //    => Math.Abs(other.X - this.X) + Math.Abs(other.Y - this.Y) + Math.Abs(other.Z - this.Z);
    }
}

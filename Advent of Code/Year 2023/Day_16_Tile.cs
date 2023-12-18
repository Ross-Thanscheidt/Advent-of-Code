namespace Advent_of_Code.Year_2023_Day_16
{
    public class Tile
    {
        public Position Position;
        public char Symbol;
        public bool Energized = false;
        public bool FromEast = false;
        public bool FromSouth = false;
        public bool FromWest = false;
        public bool FromNorth = false;
    }
}
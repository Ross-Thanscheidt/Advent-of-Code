namespace Advent_of_Code.Year_2023_Day_08
{
    public class Node(string key)
    {
        public string Key { get; set; } = key;

        public Node? LeftNode { get; set; }

        public Node? RightNode { get; set; }
    }
}

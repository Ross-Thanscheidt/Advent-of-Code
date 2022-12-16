namespace Advent_of_Code.Year_2022_Day_13
{
    public interface IElement
    {
    }

    public class ValueElement : IElement
    {
        public int Value;

        public ValueElement(string value)
        {
            Value = int.Parse(value);
        }
    }

    public class ListElement : IElement
    {
        public List<IElement> List;

        public ListElement(List<IElement> list)
        {
            List = list;
        }
    }
}

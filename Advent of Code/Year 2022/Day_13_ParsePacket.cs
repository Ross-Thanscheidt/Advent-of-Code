using System.Text.RegularExpressions;
using Advent_of_Code.Year_2022_Day_13;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        [GeneratedRegex("\\[|\\]|,|\\d+")]
        private static partial Regex TokensRegex();

        [GeneratedRegex("\\d+")]
        private static partial Regex ValueRegex();

        private static List<IElement> ParsePacket(string line)
        {
            var listStack = new Stack<List<IElement>>();
            List<IElement> packet = null;

            foreach (var element in TokensRegex().Matches(line).Select(m => m.Value))
            {
                if (element == "[")
                {
                    if (packet != null)
                    {
                        listStack.Push(packet);
                    }
                    packet = new List<IElement>();
                }
                else if (element == "]")
                {
                    if (listStack.Any())
                    {
                        var parent = listStack.Pop();
                        if (packet != null)
                        {
                            parent.Add(new ListElement(packet));
                        }
                        packet = parent;
                    }
                }
                else if (ValueRegex().IsMatch(element))
                {
                    if (packet != null)
                    {
                        packet.Add(new ValueElement(element));
                    }
                }
            }

            return packet;
        }
    }
}

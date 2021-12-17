using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Advent_of_Code.Extensions.Year_2021.Day_16;
using Advent_of_Code.Year_2021_Day_16;

namespace Advent_of_Code
{
    public partial class Year_2021 : IYear
    {
        public string Day_16(StringReader input)
        {
            int bitsForPackets;

            var startTimestamp = DateTime.Now;

            var output = "";
            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                var binaryString = new StringBuilder(String.Join(String.Empty,
                    line.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2)
                    .PadLeft(4, '0'))));

                output += $"{line}\r\n";
                output += $"\r\n";

                var topPacket = ParsePackets(binaryString, out bitsForPackets);

                output += $"The Version Numbers add up to {topPacket.VersionSum:N0}\r\n";
                output += $"The Evaluated Expression is {topPacket.Value:N0}\r\n";
                output += $"\r\n";
            }

            var endTimestamp = DateTime.Now;

            return $"{output}" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }

        private Packet ParsePackets(StringBuilder binaryString, out int bitsParsed)
        {
            var newPacket = new Packet();

            bitsParsed = 0;
            if (binaryString.Length >= 3 && !Regex.IsMatch(binaryString.ToString(), @"^0{3,}$"))
            {
                newPacket.Version = Convert.ToInt32(binaryString.GetCharacters(3), 2);
                newPacket.TypeId = (Operator)Convert.ToInt32(binaryString.GetCharacters(3), 2);
                bitsParsed += 3 + 3;
                if (newPacket.TypeId == Operator.Value)
                {
                    newPacket.Value = 0;
                    var done = false;
                    while (!done)
                    {
                        var hexDigit = binaryString.GetCharacters(5);
                        bitsParsed += 5;
                        newPacket.Value = newPacket.Value * 16 + Convert.ToInt64(hexDigit[1..], 2);
                        done = hexDigit[0] == '0';
                    }
                }
                else
                {
                    int bitsForSubpacket;

                    var packetLengthTypeId = Convert.ToInt32(binaryString.GetCharacters(1), 2);
                    bitsParsed++;
                    if (packetLengthTypeId == 0)
                    {
                        var bitsForSubpackets = Convert.ToInt32(binaryString.GetCharacters(15), 2);
                        bitsParsed += 15;
                        while (bitsForSubpackets > 0)
                        {
                            newPacket.SubPackets.Add(ParsePackets(binaryString, out bitsForSubpacket));
                            bitsParsed += bitsForSubpacket;
                            bitsForSubpackets -= bitsForSubpacket;
                        }
                    }
                    else
                    {
                        var numberOfSubpackets = Convert.ToInt32(binaryString.GetCharacters(11), 2);
                        bitsParsed += 11;
                        while (numberOfSubpackets > 0)
                        {
                            Packet subPacket = ParsePackets(binaryString, out bitsForSubpacket);
                            bitsParsed += bitsForSubpacket;
                            newPacket.SubPackets.Add(subPacket);
                            numberOfSubpackets--;
                        }
                    }
                }
            }
            return newPacket;
        }
    }
}

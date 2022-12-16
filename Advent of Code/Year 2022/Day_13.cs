using Advent_of_Code.Year_2022_Day_13;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        public string Day_13(StringReader input)
        {
            var startTimestamp = DateTime.Now;

            var sumRightOrderIndexes = 0;

            var currentPairIndex = 1;

            var packetLine1 = "";
            var packets = new List<List<IElement>>();

            List<IElement> packet1 = null;

            var packetLine2 = "";
            List<IElement> packet2 = null;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (packetLine1.Length == 0)
                    {
                        packetLine1 = line;
                        packet1 = ParsePacket(packetLine1);
                        packets.Add(packet1);
                    }
                    else if (packetLine2.Length == 0)
                    {
                        packetLine2 = line;
                        packet2 = ParsePacket(packetLine2);
                        packets.Add(packet2);

                        // Part One - Compare packet pairs (packet1 and packet2)
                        (var rightOrder, _) = CompareListElements(packet1, packet2);

                        // Part One - Add to sum if the packet pair is in the right order
                        if (rightOrder)
                        {
                            sumRightOrderIndexes += currentPairIndex;
                        }

                        // Get ready for the next pair of packets
                        currentPairIndex++;
                        packetLine1 = "";
                        packetLine2 = "";
                    }
                }
            }

            // Part Two - Add Divider Packets and put packets in order
            var dividerPacket1 = ParsePacket("[[2]]");
            packets.Add(dividerPacket1);

            var dividerPacket2 = ParsePacket("[[6]]");
            packets.Add(dividerPacket2);

            // Part Two - Use Insertion Sort to add packets into sortedPackets list
            var sortedPackets = new List<List<IElement>>();
            foreach (var packet in packets)
            {
                var rightOrder = true;
                var idx = 0;
                while (idx < sortedPackets.Count && rightOrder)
                {
                    (rightOrder, _) = CompareListElements(sortedPackets[idx], packet);
                    if (rightOrder)
                    {
                        idx++;
                    }
                }
                if (idx < sortedPackets.Count)
                {
                    sortedPackets.Insert(idx, packet);
                }
                else
                {
                    sortedPackets.Add(packet);
                }
            }

            // Part Two - Compute Decoder Key as product of Divider Packet indices
            var dividerPacketIndex1 = sortedPackets.FindIndex(p => p == dividerPacket1) + 1;
            var dividerPacketIndex2 = sortedPackets.FindIndex(p => p == dividerPacket2) + 1;
            var decoderKey = dividerPacketIndex1 * dividerPacketIndex2;

            var endTimestamp = DateTime.Now;

            return $"{sumRightOrderIndexes:N0} is the sum of the indices of the pairs that are in the right order\r\n" +
                   $"{decoderKey:N0} is the decoder key for the distress signal\r\n" +
                   $"({(endTimestamp - startTimestamp) * 1000:s\\.ffffff} ms)";
        }
    }
}

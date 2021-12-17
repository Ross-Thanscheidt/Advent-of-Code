namespace Advent_of_Code.Year_2021_Day_16
{
    public enum Operator
    {
        Sum = 0,
        Product = 1,
        Minimum = 2, 
        Maximum = 3,
        Value = 4,
        GreaterThan = 5,
        LessThan = 6,
        EqualTo = 7
    }

    public class Packet
    {
        private long _value;

        public Packet()
        {
            SubPackets = new List<Packet>();
        }

        public int Version { get; set; }
        public Operator TypeId { get; set; }
        public long Value {
            get
            {
                long value;

                if (TypeId == Operator.Value)
                {
                    value = _value;
                }
                else
                {
                    if (TypeId == Operator.Sum ||
                        TypeId == Operator.Product ||
                        TypeId == Operator.Minimum ||
                        TypeId == Operator.Maximum)
                    {
                        if (SubPackets.Count < 1)
                        {
                            throw new ArgumentException($"The {nameof(TypeId)} operator (Type ID {(int)TypeId}) requires 1 or more SubPackets (instead of {SubPackets.Count} SubPackets)");
                        }

                        value = TypeId == Operator.Sum ? 0 : TypeId == Operator.Product ? 1 : SubPackets[0].Value;
                        for (var subPacketIndex = TypeId == Operator.Sum || TypeId == Operator.Product ? 0 : 1; subPacketIndex < SubPackets.Count; subPacketIndex++)
                        {
                            if (TypeId == Operator.Sum)
                            {
                                value += SubPackets[subPacketIndex].Value;
                            }
                            else if (TypeId == Operator.Product)
                            {
                                value *= SubPackets[subPacketIndex].Value;
                            }
                            else if (TypeId == Operator.Minimum)
                            {
                                value = Math.Min(value, SubPackets[subPacketIndex].Value);
                            }
                            else if (TypeId == Operator.Maximum)
                            {
                                value = Math.Max(value, SubPackets[subPacketIndex].Value);
                            }
                        }
                    }
                    else
                    {
                        if (SubPackets.Count != 2)
                        {
                            throw new ArgumentException($"The {nameof(TypeId)} operator (Type ID {(int)TypeId}) requires exactly 2 SubPackets (instead of {SubPackets.Count} SubPackets)");
                        }

                        value = (TypeId == Operator.GreaterThan && SubPackets[0].Value > SubPackets[1].Value) ||
                                (TypeId == Operator.LessThan && SubPackets[0].Value < SubPackets[1].Value) ||
                                (TypeId == Operator.EqualTo && SubPackets[0].Value == SubPackets[1].Value)
                                ? 1
                                : 0;
                    }
                }
                return value;
            }
            set
            {
                if (this.TypeId == Operator.Value)
                {
                    _value = value;
                }
            }
        }
        public List<Packet> SubPackets { get; set; }

        public int VersionSum
        {
            get
            {
                return Version + SubPackets.Sum(p => p.VersionSum);
            }
        }
    }
}

using System.Diagnostics;

namespace Advent_of_Code
{
    public partial class Year_2024 : IYear
    {
        public string Day_17(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            string outputPartOne = "";
            ulong lowestInitialA = 0;

            // 8 instructions - opcode is 3-bit number, operand is 3-bit number
            //   0,combo   - adv: A = A / 2**combo
            //   1,literal - bxl: B = B ^ literal
            //   2,combo   - bst: B = combo % 8
            //   3,literal - jnz: if A != 0 then jump to literal else nothing
            //   4,ignore  - bxc: B = B ^ C
            //   5,combo   - out: outputs combo (separated by commas if multiple values)
            //   6,combo   - bdv: B = A / 2**combo
            //   7,combo   - cdv: C = A / 2**combo
            // 2 types of operands - literal operand, combo operand = 0-3, 4=A, 5=B, 6=C, 7=reserved/invalid
            //   0-3 - literal
            //   4   - A
            //   5   - B
            //   6   - C
            //   7   - reserved/invalid

            Dictionary<char, ulong> registersInitial = new()
            {
                { 'A', 0 },
                { 'B', 0 },
                { 'C', 0 }
            };

            List<ulong> instructions = [];

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.StartsWith("Register"))
                {
                    registersInitial[line.Split(' ')[1].Split(':')[0][0]] = ulong.Parse(line.Split(':')[1]);
                }
                else if (line.StartsWith("Program"))
                {
                    instructions = line.Split(' ')[1].Split(',').Select(s => ulong.Parse(s)).ToList();
                }
            }

            Dictionary<char, ulong> registers = [];
            registers.Add('A', registersInitial['A']);
            registers.Add('B', registersInitial['B']);
            registers.Add('C', registersInitial['C']);

            ulong Combo(ulong operand) => operand switch
            {
                >= 0 and <= 3 => operand,
                4 => registers['A'],
                5 => registers['B'],
                6 => registers['C'],
                _ => ulong.MaxValue
            };

            bool firstTime = true;
            bool found = false;

            while (!found)
            {
                foreach (char register in "ABC")
                {
                    registers[register] = firstTime || register != 'A' ? registersInitial[register] : lowestInitialA++;
                }

                List<ulong> output = [];

                bool potential = true;
                int pointer = 0;

                while (!found && potential && pointer < instructions.Count - 1)
                {
                    ulong instruction = instructions[pointer];
                    ulong operand = instructions[pointer + 1];

                    bool incrementPointer = true;

                    switch (instruction)
                    {
                        case 0:
                            registers['A'] >>>= (int)Combo(operand);
                            break;

                        case 1:
                            registers['B'] ^= operand;
                            break;

                        case 2:
                            registers['B'] = Combo(operand) & 0b_111;
                            break;

                        case 3:
                            if (registers['A'] != 0)
                            {
                                pointer = (int)operand;
                                incrementPointer = false;
                            }
                            break;

                        case 4:
                            registers['B'] ^= registers['C'];
                            break;

                        case 5:
                            output.Add(Combo(operand) & 0b_111);

                            if (!firstTime)
                            {
                                if (output[^1] != instructions[output.Count - 1])
                                {
                                    potential = false;
                                    lowestInitialA++;
                                }
                                else if (output.Count == instructions.Count)
                                {
                                    found = true;
                                    lowestInitialA--;
                                }
                            }

                            break;

                        case 6:
                            registers['B'] = registers['A'] >>> (int)Combo(operand);
                            break;

                        case 7:
                            registers['C'] = registers['A'] >>> (int)Combo(operand);
                            break;
                    }

                    if (incrementPointer)
                    {
                        pointer += 2;
                    }
                }

                if (firstTime)
                {
                    outputPartOne = string.Join(',', output.Select(n => $"{n}").ToList());
                    firstTime = false;
                }
            }

            stopwatch.Stop();

            return $"{outputPartOne}\r\n" +
                   $"{lowestInitialA:N0}\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}

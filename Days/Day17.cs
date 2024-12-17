namespace AOC.Y2024
{
    public class Day17 : IProblem
    {
        private class Computer
        {
            private long A, B, C, I, halt;
            private int[] program;
            public List<int> output = new List<int>();

            public Computer() { program = new int[0]; }
            public Computer(long a, long b, long c, int[] program)
            {
                A = a; B = b; C = c; I = 0;
                this.program = program;
                halt = program.Length;
            }

            public override string ToString()
            {
                return $"Register A: {A}\nRegister B: {B}\nRegister C: {C}";
            }

            public bool Step()
            {
                bool jumped = false;
                uint op = (uint)program[I + 1];
                switch (program[I])
                {
                    case 0: A = A >> (int)GetCombo(op); break;
                    case 1: B ^= op; break;
                    case 2: B = GetCombo(op) % 8; break;
                    case 3: if (A != 0) { I = op; jumped = true; } break;
                    case 4: B ^= C; break;
                    case 5: output.Add((int)(GetCombo(op) % 8)); break;
                    case 6: B = A >> (int)GetCombo(op); break;
                    case 7: C = A >> (int)GetCombo(op); break;
                }

                //string bA = Convert.ToString(A, 2);
                //string bB = Convert.ToString(B, 2);
                //string bC = Convert.ToString(C, 2);
                //Console.WriteLine($"A: {bA.PadLeft(3, '0')} B: {bB.PadLeft(3, '0')} C: {bC.PadLeft(3, '0')}");

                if (!jumped) I += 2;

                return I < halt;
            }

            private uint GetCombo(long i)
            {
                return i switch
                {
                    4 => (uint)A,
                    5 => (uint)B,
                    6 => (uint)C,
                    7 => throw new InvalidOperationException(),
                    _ => (uint)i,
                };
            }

            public void Run()
            {
                while (Step()) { }
            }
        }

        private uint a, b, c;
        private int[] program = new int[0];

        public void Parse(string[] input)
        {
            a = uint.Parse(input[0].Substring(12));
            b = uint.Parse(input[1].Substring(12));
            c = uint.Parse(input[2].Substring(12));
            program = input[4].Substring(9).Split(',').Select(s => int.Parse(s)).ToArray();
        }

        public long PartOne()
        {
            long steps = 0;
            var computer = new Computer(a, b, c, program);
            do steps++; while (computer.Step());
            Console.WriteLine($"Part 1 Output: {string.Join(',', computer.output.Select(i => i.ToString()))}");

            return steps;
        }

        public long PartTwo()
        {

            var result = ProgramDFS(0, 0);

            /// print for sanity checking
            //var computer = new Computer(result.Min(), b, c, program);
            //computer.Run();
            //var output = computer.output.ToArray();
            //var sOutput = string.Join(',', output.Select(j => j.ToString()));
            //Console.WriteLine($"{sOutput}");

            return result.Min();
        }

        private long ProgramToNumber(List<uint> steps)
        {
            long result = 0;
            foreach (var step in steps)
            {
                result *= 8;
                result += step;
            }
            return result;
        }

        private List<long> ProgramDFS(long val, int depth)
        {
            var results = new List<long>();
            if (depth > program.Length) return results;

            long depthTest = val << 3;
            for (uint i = 0; i < 8; i++)
            {
                long test = depthTest + i;
                var computer = new Computer(test, b, c, program);
                computer.Run();
                if (computer.output.SequenceEqual(program.TakeLast(depth + 1)))
                {
                    if (depth + 1 == program.Length) results.Add(test);
                    results.AddRange(ProgramDFS(test, depth + 1));
                }
            }

            return results;
        }
    }
}
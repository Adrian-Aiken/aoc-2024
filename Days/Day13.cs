namespace AOC.Y2024
{
    public class Day13 : IProblem
    {
        private const long machineOffset = 10000000000000;

        private struct Machine
        {
            public int ax, ay, bx, by;
            public long targetX, targetY;
        }

        private List<Machine> machines = new List<Machine>();

        public void Parse(string[] input)
        {
            for (int i = 0; i < input.Length; i += 4)
            {
                var aSplit = input[i].Substring(12).Split(',');
                var bSplit = input[i + 1].Substring(12).Split(',');
                var tSplit = input[i + 2].Substring(9).Split(',');

                machines.Add(new Machine
                {
                    ax = int.Parse(aSplit[0]),
                    ay = int.Parse(aSplit[1].Substring(3)),
                    bx = int.Parse(bSplit[0]),
                    by = int.Parse(bSplit[1].Substring(3)),
                    targetX = long.Parse(tSplit[0]),
                    targetY = long.Parse(tSplit[1].Substring(3))
                });
            }
        }

        public long PartOne()
        {
            long sum = 0;

            foreach (var machine in machines)
            {
                var tokens = GetLeastTokensLinear(machine);

                if (tokens < long.MaxValue)
                {
                    sum += tokens;
                }
            }

            return sum;
        }

        public long PartTwo()
        {
            long sum = 0;

            foreach (var machine in machines)
            {
                var newMachine = new Machine
                {
                    ax = machine.ax,
                    ay = machine.ay,
                    bx = machine.bx,
                    by = machine.by,
                    targetX = machine.targetX + machineOffset,
                    targetY = machine.targetY + machineOffset
                };

                var tokens = GetLeastTokensLinear(newMachine);

                if (tokens < long.MaxValue)
                {
                    sum += tokens;
                }
            }

            return sum;
        }

        private int GetLeastTokensBrute(Machine machine)
        {
            int least = int.MaxValue;

            for (int a = 0; a <= 100; a++)
            {
                for (int b = 0; b <= 100; b++)
                {
                    if ((machine.ax * a) + (machine.bx * b) == machine.targetX &&
                        (machine.ay * a) + (machine.by * b) == machine.targetY)
                    {
                        var tokens = (a * 3) + b;
                        least = Math.Min(tokens, least);
                    }
                }
            }

            return least;
        }

        private long GetLeastTokensLinear2(Machine machine)
        {
            var denominator = (machine.ax * machine.by - machine.ay * machine.bx);
            if (denominator == 0) return long.MaxValue;

            var aPress = (machine.targetX * machine.by - machine.targetY * machine.bx);
            if (aPress * denominator != (machine.targetX * machine.by - machine.targetY * machine.bx)) return long.MaxValue;

            var bPress = (machine.targetY - machine.ay * aPress);
            if (bPress * aPress != (machine.targetY - machine.ay * aPress)) return long.MaxValue;

            return (aPress * 3) + bPress;
        }

        private long GetLeastTokensLinear(Machine m)
        {
            var bPress = (m.targetY * m.ax - m.targetX * m.ay) / (m.by * m.ax - m.bx * m.ay);
            var aPress = (m.targetX - bPress * m.bx) / m.ax;

            if (aPress > 0 &&
                bPress > 0 &&
                (aPress * m.ax + bPress * m.bx) == m.targetX &&
                (aPress * m.ay + bPress * m.by) == m.targetY)
            {
                return (aPress * 3) + bPress;
            }

            return 0;
        }
    }
}
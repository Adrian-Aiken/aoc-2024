namespace AOC.Y2024
{
    public class Day08 : IProblem
    {
        private Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();
        private int height, width;

        public void Parse(string[] input)
        {
            height = input.Count();
            width = input[0].Length;

            for (int x = 0; x < input.Count(); x++)
            {
                for (int y = 0; y < input[x].Count(); y++)
                {
                    if (input[x][y] != '.')
                    {
                        if (!antennas.ContainsKey(input[x][y]))
                        {
                            antennas[input[x][y]] = new List<(int, int)>();
                        }

                        antennas[input[x][y]].Add((x, y));
                    }
                }
            }
        }

        public object PartOne()
        {
            var antiNodes = new HashSet<(int, int)>();
            foreach (var antenna in antennas.Values)
            {
                for (int i = 0; i < antenna.Count - 1; i++)
                {
                    for (int j = i + 1; j < antenna.Count; j++)
                    {
                        int diffX = antenna[j].Item1 - antenna[i].Item1;
                        int diffY = antenna[j].Item2 - antenna[i].Item2;

                        if (Utils.IsInRange2d(antenna[i].Item1 - diffX, antenna[i].Item2 - diffY, height, width))
                        {
                            antiNodes.Add((antenna[i].Item1 - diffX, antenna[i].Item2 - diffY));
                        }

                        if (Utils.IsInRange2d(antenna[i].Item1 + diffX + diffX, antenna[i].Item2 + diffY + diffY, height, width))
                        {
                            antiNodes.Add((antenna[i].Item1 + diffX + diffX, antenna[i].Item2 + diffY + diffY));
                        }
                    }
                }
            }

            return antiNodes.Count;
        }

        public object PartTwo()
        {
            var antiNodes = new HashSet<(int, int)>();
            foreach (var antenna in antennas.Values)
            {
                for (int i = 0; i < antenna.Count - 1; i++)
                {
                    for (int j = i + 1; j < antenna.Count; j++)
                    {
                        int diffX = antenna[j].Item1 - antenna[i].Item1;
                        int diffY = antenna[j].Item2 - antenna[i].Item2;

                        // Test reverse
                        int testX = antenna[i].Item1;
                        int testY = antenna[i].Item2;

                        while (Utils.IsInRange2d(testX, testY, height, width))
                        {
                            antiNodes.Add((testX, testY));
                            testX -= diffX;
                            testY -= diffY;
                        }

                        // Test forward
                        testX = antenna[i].Item1;
                        testY = antenna[i].Item2;

                        while (Utils.IsInRange2d(testX, testY, height, width))
                        {
                            antiNodes.Add((testX, testY));
                            testX += diffX;
                            testY += diffY;
                        }
                    }
                }
            }

            return antiNodes.Count;
        }
    }
}
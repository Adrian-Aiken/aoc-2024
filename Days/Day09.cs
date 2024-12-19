namespace AOC.Y2024
{
    public class Day09 : IProblem
    {
        List<int> originalMemory = new List<int>();
        List<(int, int)> originalBlocks = new List<(int, int)>();

        public void Parse(string[] input)
        {
            int id = 0;
            bool isFreeSpace = false;
            for (int i = 0; i < input[0].Length; i++)
            {
                int len = input[0][i] - '0';
                if (isFreeSpace)
                {
                    for (int j = 0; j < len; j++)
                    {
                        originalMemory.Add(-1);
                    }
                }
                else
                {
                    int j = 0, start = originalMemory.Count;
                    for (; j < len; j++)
                    {
                        originalMemory.Add(id);
                    }
                    originalBlocks.Add((start, j));
                    id++;
                }

                isFreeSpace = !isFreeSpace;
            }
        }

        public object PartOne()
        {
            var memory = new List<int>(originalMemory);
            int firstBlank = 0;
            int lastFilled = memory.Count - 1;

            while (true)
            {
                while (memory[firstBlank] != -1) firstBlank++;
                while (memory[lastFilled] == -1) lastFilled--;

                if (lastFilled < firstBlank) break;

                memory[firstBlank] = memory[lastFilled];
                memory[lastFilled] = -1;
            }

            return getChecksum(memory);
        }

        public object PartTwo()
        {
            var memory = new List<int>(originalMemory);

            //PrintMemory(memory);

            for (int i = originalBlocks.Count - 1; i >= 0; i--)
            {
                var (blockStart, blockSize) = originalBlocks[i];
                var blankSlot = findBlank(memory, blockSize, blockStart);

                if (blankSlot == -1) continue;

                for (int j = 0; j < blockSize; j++)
                {
                    memory[blankSlot + j] = i;
                    memory[blockStart + j] = -1;
                }

                //PrintMemory(memory);
            }

            return getChecksum(memory);
        }

        public int findBlank(List<int> memory, int size, int end)
        {
            var curSize = 0;
            for (int i = 0; i < end; i++)
            {
                curSize = memory[i] == -1 ? curSize + 1 : 0;

                if (curSize == size) return i - curSize + 1;
            }

            return -1;
        }

        private void PrintMemory(List<int> memory)
        {
            foreach (var c in memory)
            {
                Console.Write(c == -1 ? '.' : c.ToString());
            }
            Console.WriteLine();
        }

        private long getChecksum(List<int> memory)
        {
            long checksum = 0;
            for (int i = 0; i < memory.Count; i++)
            {
                if (memory[i] == -1) continue;

                checksum += i * memory[i];
            }

            return checksum;
        }
    }
}
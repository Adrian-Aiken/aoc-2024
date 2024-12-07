namespace AOC
{
    public interface IProblem
    {
        public void Parse(string[] input);

        public long PartOne();
        public long PartTwo();
    }
}
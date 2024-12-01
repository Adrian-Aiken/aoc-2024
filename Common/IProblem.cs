namespace AOC
{
    public interface IProblem
    {
        public void Parse(string[] input);

        public int PartOne();
        public int PartTwo();
    }
}
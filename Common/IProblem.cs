namespace AOC
{
    public interface IProblem
    {
        public void Parse(string[] input);

        public object PartOne();
        public object PartTwo();
    }
}
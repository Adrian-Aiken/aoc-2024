using System.Collections;
using System.Xml.XPath;

namespace AOC.Y2024
{
    public class Day07 : IProblem
    {
        private class Equation
        {
            public long Result { get; private set; }
            public List<long> Terms { get; private set; }

            public Equation(string s)
            {
                var split = s.Split(": ");
                Result = long.Parse(split[0]);
                Terms = split[1].Split(' ').Select(t => long.Parse(t)).ToList();
            }
        }

        private static char[] Part1Operators = { '+', '*' };
        private static char[] Part2Operators = { '+', '*', '|' };
        private List<Equation> equations = new List<Equation>();

        public void Parse(string[] input)
        {
            equations = input.Select(s => new Equation(s)).ToList();
        }

        public object PartOne()
        {
            long sum = 0;
            foreach (var equation in equations)
            {
                if (IsValidEquation(equation.Result, equation.Terms, Part1Operators, 0))
                {
                    sum += equation.Result;
                }
            }

            return sum;
        }

        public object PartTwo()
        {
            long sum = 0;
            foreach (var equation in equations)
            {
                if (IsValidEquation(equation.Result, equation.Terms, Part2Operators, 0))
                {
                    sum += equation.Result;
                }
            }

            return sum;
        }

        private bool IsValidEquation(long target, List<long> terms, char[] operators, long result)
        {
            if (terms.Count == 0)
            {
                return result == target;
            }

            long curTerm = terms.First();
            var nextTerms = new List<long>(terms);
            nextTerms.RemoveAt(0);

            foreach (char op in operators)
            {
                var nextResult = op switch
                {
                    '+' => result + curTerm,
                    '*' => result * curTerm,
                    '|' => long.Parse(result.ToString() + curTerm.ToString()),
                    _ => throw new NotImplementedException()
                };

                if (IsValidEquation(target, nextTerms, operators, nextResult)) return true;
            }

            return false;
        }
    }
}
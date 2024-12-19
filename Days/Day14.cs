namespace AOC.Y2024
{
    public class Day14 : IProblem
    {
        private const int WIDTH = 101;
        private const int HEIGHT = 103;

        private class Robot
        {
            public int x, y, vx, vy;

            public Robot(string line)
            {
                string[] main = line.Split(' ');
                string[] positionString = main[0].Split(',');
                string[] velocityString = main[1].Split(',');

                x = int.Parse(positionString[0].Substring(2));
                y = int.Parse(positionString[1]);
                vx = int.Parse(velocityString[0].Substring(2));
                vy = int.Parse(velocityString[1]);
            }

            public void Move()
            {
                x = (x + vx) % WIDTH;
                y = (y + vy) % HEIGHT;

                if (x < 0) x += WIDTH;
                if (y < 0) y += HEIGHT;
            }
        }

        private List<Robot> robots = new List<Robot>();
        private int seconds = 0;

        public void Parse(string[] input)
        {
            foreach (var line in input)
            {
                robots.Add(new Robot(line));
            }

            bool isTree = false;
            while (!isTree)
            {
                foreach (var r in robots) r.Move();
                seconds++;
                isTree = HasLine(robots);
            }

            PrintFloor(robots);

            if (seconds < 100)
            {
                for (int i = seconds; i < 100; i++)
                {
                    foreach (var r in robots) r.Move();
                }
            }
        }

        public object PartOne()
        {
            return GetSafetyScore(robots);
        }

        public object PartTwo()
        {
            return seconds;
        }

        private void PrintFloor(List<Robot> robots)
        {
            List<List<char>> floor = new List<List<char>>();
            for (int i = 0; i < HEIGHT; i++)
            {
                var row = new List<char>();
                for (int j = 0; j < WIDTH; j++)
                {
                    row.Add('.');
                }
                floor.Add(row);
            }

            foreach (var r in robots)
            {
                if (floor[r.y][r.x] == '.') floor[r.y][r.x] = '1';
                else floor[r.y][r.x]++;
            }

            Utils.Print2D(floor);
        }

        private long GetSafetyScore(List<Robot> robots)
        {
            int midWidth = WIDTH / 2;
            int midHeight = HEIGHT / 2;

            int upperLeft = 0, upperRight = 0, lowerLeft = 0, lowerRight = 0;

            foreach (var r in robots)
            {
                if (r.x < midWidth && r.y < midHeight) upperLeft++;
                else if (r.x < midWidth && r.y > midHeight) lowerLeft++;
                else if (r.x > midWidth && r.y < midHeight) upperRight++;
                else if (r.x > midWidth && r.y > midHeight) lowerRight++;
            }

            return upperLeft * upperRight * lowerLeft * lowerRight;
        }

        private bool HasLine(List<Robot> robots)
        {
            List<List<char>> floor = new List<List<char>>();
            for (int i = 0; i < HEIGHT; i++)
            {
                var row = new List<char>();
                for (int j = 0; j < WIDTH; j++)
                {
                    row.Add('.');
                }
                floor.Add(row);
            }

            foreach (var r in robots)
            {
                floor[r.y][r.x] = 'X';
            }

            int maxRun = 0;
            for (int i = 0; i < HEIGHT; i++)
            {
                var curRun = 0;
                for (int j = 0; j < WIDTH; j++)
                {
                    if (floor[i][j] == '.')
                    {
                        maxRun = Math.Max(curRun, maxRun);
                        curRun = 0;
                    }
                    else
                    {
                        curRun++;
                    }
                }
                maxRun = Math.Max(curRun, maxRun);
            }

            return maxRun > 10;
        }
    }
}
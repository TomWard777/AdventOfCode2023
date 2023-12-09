namespace AdventOfCode2023;

public class Day6
{
    public void Run()
    {
        ////var input = FileParser.ReadInputFromFile("Test6.txt").ToArray();
        var input = FileParser.ReadInputFromFile("Day6.txt").ToArray();

        var times = GetValuesPart2(input[0]);
        var dists = GetValuesPart2(input[1]);

        var results = new List<double>();
        
        for (int i = 0; i < times.Count(); i++)
        {
            var t = times[i];
            var r = dists[i];

            var a = ((t*t)/4) - r;

            var upper = (t/2) + Math.Sqrt(a);
            var lower = (t/2) - Math.Sqrt(a);

            if (upper % 1 == 0)
            {
                upper -= 1;
            }

            if (lower % 1 == 0)
            {
                lower += 1;
            }

            Console.WriteLine("Upper: " + upper);
            Console.WriteLine("Lower: " + lower);

            var numberOfSolns = Math.Floor(upper) - Math.Ceiling(lower) + 1;
            results.Add(numberOfSolns);

            Console.WriteLine($"Time {t}, distance {r}, number of solutions {numberOfSolns}");
        }

        Console.WriteLine("SOLUTION: " + results.Aggregate((x, acc) => x * acc));
    }

    public double[] GetValuesPart1(string line)
    {
        return line.Substring(9)
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(t => double.Parse(t))
        .ToArray();
    }

    public double[] GetValuesPart2(string line)
    {
        var str = line.Substring(9).Replace(" ", string.Empty);
        var x = double.Parse(str);
        return new double[] {x};
    }
}

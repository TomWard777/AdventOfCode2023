namespace AdventOfCode2023;

public class Day9
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test9.txt").ToArray();
        var input = FileParser.ReadInputFromFile("Day9.txt").ToArray();

        var results = new List<int>();

        foreach(var line in input)
        {
            // For part 2, just include Reverse() here.
            var seq = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .Reverse()
            .ToArray();

            var next = GetNextElement(seq);
            results.Add(next);
            Console.WriteLine("Next element = " + next);
        }

        Console.WriteLine("\nANSWER:");
        Console.WriteLine(results.Select(n => (long)n).Sum());
    }

    public int GetNextElement(int[] sequence)
    {
        if (sequence.Any(x => x != 0))
        {
            var diffs = GetDifferences(sequence);
            return sequence.Last() + GetNextElement(diffs);
        }

        return 0;
    }

    public int[] GetDifferences(int[] sequence)
    {
        var ct = sequence.Length;

        return Enumerable.Range(0, ct - 1)
        .Select(i => sequence[i + 1] - sequence[i])
        .ToArray();
    }
}
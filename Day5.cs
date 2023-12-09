using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day5
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test5.txt").ToArray();
        var input = FileParser.ReadInputFromFile("Day5.txt").ToArray();

        var seedRanges = GetSeedRanges(input[0]);

        var lineBuffer = new List<string>();
        var sets = new List<RangeSet>();

        foreach (var line in input.Skip(2))
        {
            var match = Regex.Match(line, @"[0-9]+");

            if (match.Success)
            {
                lineBuffer.Add(line);
            }
            else if (lineBuffer.Any())
            {
                sets.Add(new RangeSet(lineBuffer));
                lineBuffer.Clear();
            }
        }

        if (lineBuffer.Any())
        {
            sets.Add(new RangeSet(lineBuffer));
        }

        //// TEST
        long min = 10000000000;
        long s;

        foreach (var seedRange in seedRanges)
        {
            s = seedRange.Item1;
            do
            {
                ////Console.WriteLine("Testing" + s);
                var x = s;
                foreach (var rangeSet in sets)
                {
                    x = rangeSet.RangesMap(x);
                }

                ////Console.WriteLine(x);
                if (x < min)
                {
                    min = x;
                }
                s++;

                ////Console.WriteLine(seedRange.Item1 + 10);
                ////Console.WriteLine(s < seedRange.Item1 + 10);

                /////  } while (s < seedRange.Item1 + 10);
            } while (s < seedRange.Item1 + seedRange.Item2);
        }

        Console.WriteLine("\nMIN:");
        Console.WriteLine(min);
    }

    public IEnumerable<(long, long)> GetSeedRanges(string line)
    {
        var seeds = new List<long>();

        var arr = line.Substring(7)
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(x => long.Parse(x))
        .ToArray();

        for (int i = 0; i < arr.Count(); i += 2)
        {
            yield return (arr[i], arr[i + 1]);
        }
    }

    public long[] GetSeedsPart1(string line)
    {
        return line.Substring(7)
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(x => long.Parse(x))
        .ToArray();
    }

    public long[] GetSeedsPart2(string line)
    {
        var seeds = new List<long>();

        var arr = line.Substring(7)
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(x => long.Parse(x))
        .ToArray();

        for (int i = 0; i < arr.Count(); i += 2)
        {
           //// seeds.AddRange(Maths.LongRange(arr[i], arr[i + 1]));
        }

        return seeds.ToArray();
    }
}

public class RangeSet
{
    public RangeSet(IEnumerable<string> lines)
    {
        Ranges = lines
        .Select(x => new Range(x))
        .ToList();
    }

    public List<Range> Ranges { get; set; }

    public long RangesMap(long n)
    {
        foreach (var range in Ranges)
        {
            var m = range.Map(n);
            ///Console.WriteLine($"{range.Dst} {range.Src} {range.Size}");
            ///Console.WriteLine($"{n} maps to {m}");
            if (m != n)
            {
                return m;
            }
        }

        return n;
    }
}

public class Range
{
    public Range(string rangeStr)
    {
        var arr = rangeStr.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(x => long.Parse(x))
        .ToArray();

        (Dst, Src, Size) = (arr[0], arr[1], arr[2]);
    }

    public long Src { get; set; }
    public long Dst { get; set; }
    public long Size { get; set; }

    public long Map(long n)
    {
        if (n >= Src && n < Src + Size)
        {
            return Dst + n - Src;
        }
        else
        {
            return n;
        }
    }
}
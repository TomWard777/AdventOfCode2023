using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day12
{
    public void Run()
    {
        //// 7807 too high

        //// ??.??????#?#?#??..? 1,8

        //var input = FileParser.ReadInputFromFile("Test12.txt").ToArray();
        var input = FileParser.ReadInputFromFile("Day12.txt").ToArray();

        var groups = input.Select(x => new SpringGroup(x))
        .ToList();

        var sum = 0;

        Console.WriteLine("abcde".RepeatString(5));

        foreach (var gp in groups)
        {
            Console.WriteLine(gp.Positions);
            foreach (var k in gp.SizeSequence)
            {
                Console.Write(k + ", ");
            }

            var r = NumberOfSolutions(gp.Positions, gp.SizeSequence);
            sum += r;

            Console.WriteLine("\nNumber of solutions = " + r + "\n");
        }

        Console.WriteLine("\nANSWER:");
        Console.WriteLine(sum);
    }

    public int NumberOfSolutions(string positions, int[] sequence)
    {
        if (string.IsNullOrEmpty(positions))
        {
            // If we have run out of positions, we're done if and only if we've run out of numbers to place.
            return !sequence.Any() ? 1 : 0;
        }
        else if (sequence.Length == 0)
        {
            // No numbers to place - 1 solution if there are no stray '#' characters, 0 otherwise.
            if (positions.Contains('#'))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        if (positions.Count(x => x == '?' || x == '#') < sequence.Sum())
        {
            // Not enough possible # characters left.
            return 0;
        }

        var size = sequence[0];

        return positions[0] switch
        {
            '.' => NumberOfSolutionsStartingWithDot(positions, sequence),
            '#' => NumberOfSolutionsStartingWithHash(positions, sequence),
            '?' => NumberOfSolutionsStartingWithDot(positions, sequence) + NumberOfSolutionsStartingWithHash(positions, sequence),
            _ => throw new NotSupportedException()
        };
    }

    public int NumberOfSolutionsStartingWithDot(string positions, int[] sequence)
    {
        return NumberOfSolutions(positions.Substring(1), sequence);
    }

    public int NumberOfSolutionsStartingWithHash(string positions, int[] sequence)
    {
        var size = sequence[0];

        // If first character is '#' then this must be part of a group of size sequence[0].
        if (!CanLineBeginWithGroup(positions, size))
        {
            return 0;
        }

        if (positions.Length == size)
        {
            // 1 solution - fill all remaining characters with '#'
            return 1;
        }
        else
        {
            return NumberOfSolutions(positions.Substring(size + 1), sequence.Skip(1).ToArray());
        }
    }

    public bool CanLineBeginWithGroup(string positions, int size)
    {
        // Return true if the given string can have a group of '#' of the given size at the front.
        if (positions.Length < size)
        {
            return false;
        }
        else if (positions.Substring(0, size).Contains('.'))
        {
            return false;
        }
        else if (positions.Length > size && positions[size] == '#')
        {
            return false;
        }
        else
        {
            // if (size == positions.Length)
            // {
            //     Console.WriteLine("This can begin with " + size);
            //     Console.WriteLine(positions);
            // }
            return true;
        }
    }
}

public class SpringGroup
{
    public SpringGroup(string line)
    {
        var arr = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        Positions = (arr[0] + "?")
        .RepeatString(5);

        Positions = Positions.Substring(0, Positions.Length - 1);

        SizeSequence = (arr[1] + ",")
        .RepeatString(5)
        .Split(",", StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToArray();
    }

    public string Positions { get; set; }
    public int[] SizeSequence { get; set; }
}
namespace AdventOfCode2023;

public class Day21
{
    // 902 too low
    private readonly Random _random;

    public Day21()
    {
        _random = new Random();
    }

    public void Run()
    {
        var numberOfSteps = 64;
        var attempts = 5000000;

        //var input = FileParser.ReadInputFromFile("Test21.txt");
        var input = FileParser.ReadInputFromFile("Day21.txt");

        var mat = Matrices.ReadToMatrix(input);
        var startingPosition = GetStartingPoint(mat);
        var steps = new List<((int, int), int)>();

        for (int count = 0; count < attempts; count++)
        {
            var path = new List<((int, int), int)>()
            {
                (startingPosition, 0)
            };

            var position = startingPosition;

            for (int k = 0; k < numberOfSteps; k++)
            {
                position = GetNextPosition(position, path, mat);
                if (position == (-1, -1))
                {
                    break;
                }
            }

            steps.AddRange(path);
            steps = steps.Distinct().ToList();

            if (count % 1000 == 0)
            {
                Console.WriteLine($"{attempts - count}");
            }
        }

        // foreach(var step in steps)
        // {
        //     Console.WriteLine($"({step.Item1.Item1} {step.Item1.Item2}) {step.Item2}");
        // }

        var evenSteps = steps
        .Where(x => x.Item2 == 1)
        .Select(x => x.Item1)
        .ToList();

        DrawPath(mat, evenSteps);

        Console.WriteLine("\nRESULT:");
        Console.WriteLine(evenSteps.Count);
    }

    public (int, int) GetStartingPoint(Matrix mat)
    {
        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount - 1; j++)
            {
                if (mat.Entries[i][j] == 'S')
                {
                    return (i, j);
                }
            }
        }

        throw new NotSupportedException("Can't find entry point");
    }

    public (int, int) GetNextPosition(
        (int, int) position,
        List<((int, int), int)> path,
        Matrix mat)
    {
        var (i, j) = position;
        var previousPositions = path.Select(x => x.Item1).ToArray();

        (int, int) next;

        var possibleSteps = new List<(int, int)>()
            {
                (i-1,j),
                (i+1,j),
                (i,j+1),
                (i,j-1)
            };

        possibleSteps = possibleSteps
        //.Where(p => !previousPositions.Contains(p))
        .Where(p => p.Item1 >= 0 && p.Item1 < mat.RowCount && p.Item2 >= 0 && p.Item2 < mat.ColCount)
        .Where(p => mat.Entries[p.Item1][p.Item2] != '#')
        .ToList();

        if (!possibleSteps.Any())
        {
            return (-1, -1);
        }

        next = GetRandomElementOfPositionArray(possibleSteps.ToArray());

        var numberOfSteps = path.Count + 1;

        path.Add((next, numberOfSteps % 2));
        return next;
    }

    public (int, int) GetRandomElementOfPositionArray((int, int)[] arr)
    {
        int index = _random.Next(0, arr.Length);
        return arr[index];
    }

    public static void DrawPath(Matrix mat, List<(int, int)> path)
    {
        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (path.Contains((i, j)))
                {
                    Console.Write("O");
                }
                else
                {
                    Console.Write(mat.Entries[i][j]);
                }
            }
            Console.Write("\n");
        }
    }
}
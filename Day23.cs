namespace AdventOfCode2023;

public class Day23
{
    // 5182 too low
    private readonly Random _random;

    public Day23()
    {
        _random = new Random();
    }

    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test23.txt");
        var input = FileParser.ReadInputFromFile("Day23.txt");

        var mat = Matrices.ReadToMatrix(input);
        var startingPosition = GetStartingPoint(mat);

        var max = 0;
        var attempts = 1000000;

        for (int count = 0; count < attempts; count++)
        {
            var k = GetPathLength(startingPosition, mat);
            max = Math.Max(k, max);
            
            if(count % 1000 == 0)
            {
                Console.WriteLine($"{attempts - count}  Max {max}");
            }
        }

        Console.WriteLine("\nRESULT:");
        Console.WriteLine(max - 1);
    }

    public int GetPathLength((int, int) startingPosition, Matrix mat)
    {
        var position = startingPosition;
        var path = new List<(int, int)>() { position };

        do
        {
            position = GetNextPosition(position, path, mat);
        }
        while (position != (-1, -1));

        // if(path.Count > 155)
        // {
        //     DrawPath(mat, path);
        // }
        return path.Count;
    }

    public (int, int) GetStartingPoint(Matrix mat)
    {
        for (int j = 0; j < mat.ColCount - 1; j++)
        {
            if (mat.Entries[0][j] == '.')
            {
                return (0, j);
            }
        }

        throw new NotSupportedException("Can't find entry point");
    }

    public (int, int) GetNextPosition(
        (int, int) position,
        List<(int, int)> path,
        Matrix mat)
    {
        var (i, j) = position;

        (int, int) next;

        if (i == 0)
        {
            next = (i + 1, j);
        }
        else if (i == mat.RowCount - 1)
        {
            return (-1, -1);
        }
        else
        {
            var possibleSteps = new List<(int, int)>()
            {
                (i-1,j),
                (i+1,j),
                (i,j+1),
                (i,j-1)
            };

            possibleSteps = possibleSteps.Where(p => !path.Contains(p))
            .Where(p => mat.Entries[p.Item1][p.Item2] != '#')
            .ToList();

            if (!possibleSteps.Any())
            {
                // We haven't reached the bottom row but have run out of possible steps. 
                // We have not reached the exit so the path is invalid.
                path.RemoveAll(x => true);
                return (-1, -1);
            }

            next = GetRandomElementOfPositionArray(possibleSteps.ToArray());
        }

        path.Add(next);
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
namespace AdventOfCode2023;

public static class Matrices
{
    public static Matrix ReadToMatrix(IEnumerable<string> input)
    {
        var m = input.Count();
        var n = input.First().Length;

        var mat = input
        .Select(x => x.ToCharArray())
        .ToArray();

        return new Matrix(m, n, mat);
    }

    public static (int, int, char[][]) ReadToMatrixTuple(IEnumerable<string> input)
    {
        var m = input.Count();
        var n = input.First().Length;

        var mat = input
        .Select(x => x.ToCharArray())
        .ToArray();

        return (m, n, mat);
    }

    public static IEnumerable<(int, int)> GetDirectlyAdjacentPlaces(int m, int n, int i, int j)
    {
        var places = new List<(int, int)>
        {
            (i - 1, j),
            (i + 1, j),
            (i, j + 1),
            (i, j - 1)
        };

        return places
        .Where(p => p.Item1 >= 0 && p.Item1 < m && p.Item2 >= 0 && p.Item2 < n)
        .ToArray();
    }

    public static IEnumerable<(int, int)> GetAdjacentPlaces(int m, int n, int i, int j)
    {
        var places = new List<(int, int)>
        {
            (i - 1, j),
            (i + 1, j),
            (i - 1, j + 1),
            (i + 1, j + 1),
            (i - 1, j - 1),
            (i + 1, j - 1),
            (i, j + 1),
            (i, j - 1)
        };

        return places
        .Where(p => p.Item1 >= 0 && p.Item1 < m && p.Item2 >= 0 && p.Item2 < n)
        .ToArray();
    }

    public static void Draw(Matrix mat)
    {
        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                Console.Write(mat.Entries[i][j]);
            }
            Console.Write("\n");
        }
    }

    public static void Draw(int m, int n, char[][] mat)
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(mat[i][j]);
            }
            Console.Write("\n");
        }
    }

    public static void DrawSubset(int m, int n, IEnumerable<(int, int)> set, char[][] mat)
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (set.Contains((i, j)))
                {
                    Console.Write(mat[i][j]);
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.Write("\n");
        }
    }
}

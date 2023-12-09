namespace AdventOfCode2023;

public static class Matrices
{
    public static (int, int, char[][]) ReadToMatrix(IEnumerable<string> input)
    {
        var m = input.Count();
        var n = input.First().Length;

        var mat = input
        .Select(x => x.ToCharArray())
        .ToArray();

        return (m, n, mat);
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
}

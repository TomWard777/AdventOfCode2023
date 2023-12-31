namespace AdventOfCode2023;

public class Day11
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test11.txt");
        var input = FileParser.ReadInputFromFile("Day11.txt");

        var mat = Matrices.ReadToMatrix(input);

        var emptyRows = GetEmptyRows(mat);
        var emptyCols = GetEmptyColumns(mat);

        var points = GetPoints(mat);

        var sum = 0L;
        foreach (var p in points)
        {
            foreach (var q in points)
            {
                sum += ExpandedDist(p, q, emptyRows, emptyCols, 1000000L);
            }
        }

        Matrices.Draw(mat);

        Console.WriteLine("\n" + sum / 2);
    }

    public int Dist((int, int) point1, (int, int) point2)
    {
        if (point1 == point2)
        {
            return 0;
        }
        return Math.Abs(point1.Item1 - point2.Item1) + Math.Abs(point1.Item2 - point2.Item2);
    }

    public long ExpandedDist(
        (long, long) point1,
        (long, long) point2,
        IEnumerable<long> emptyRows,
        IEnumerable<long> emptyCols,
        long expansionFactor)
    {
        if (point1 == point2)
        {
            return 0;
        }

        var numRows = emptyRows
        .Where(k => (point1.Item1 <= k && k < point2.Item1) || (point2.Item1 <= k && k < point1.Item1))
        .Count();

        var numCols = emptyCols
        .Where(k => (point1.Item2 <= k && k < point2.Item2) || (point2.Item2 <= k && k < point1.Item2))
        .Count();

        return Math.Abs(point1.Item1 - point2.Item1) + Math.Abs(point1.Item2 - point2.Item2)
        + (numRows + numCols) * (expansionFactor - 1);
    }

    public List<(long, long)> GetPoints(Matrix mat)
    {
        var result = new List<(long, long)>();

        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (mat.Entries[i][j] == '#')
                {
                    result.Add((i, j));
                }
            }
        }

        return result;
    }

    public List<long> GetEmptyRows(Matrix mat)
    {
        var result = new List<long>();

        for (int i = 0; i < mat.RowCount; i++)
        {
            var empty = true;
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (mat.Entries[i][j] != '.')
                {
                    empty = false;
                }
            }

            if (empty)
            {
                result.Add(i);
            }
        }

        return result;
    }

    public List<long> GetEmptyColumns(Matrix mat)
    {
        var result = new List<long>();

        for (int j = 0; j < mat.ColCount; j++)
        {
            var empty = true;
            for (int i = 0; i < mat.RowCount; i++)

            {
                if (mat.Entries[i][j] != '.')
                {
                    empty = false;
                }
            }

            if (empty)
            {
                result.Add(j);
            }
        }

        return result;
    }

    public Matrix InsertRow(int k, Matrix mat)
    {
        var m = mat.RowCount;
        var n = mat.ColCount;
        var newMat = new Matrix(m + 1, n, new char[m + 1][]);

        for (int i = 0; i < m + 1; i++)
        {
            newMat.Entries[i] = new char[n];
        }

        for (int i = 0; i < m + 1; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i < k)
                {
                    newMat.Entries[i][j] = mat.Entries[i][j];
                }
                else if (i == k)
                {
                    newMat.Entries[i][j] = '.';

                }
                else
                {
                    newMat.Entries[i][j] = mat.Entries[i - 1][j];
                }
            }
        }
        return newMat;
    }

    public Matrix InsertColumn(int k, Matrix mat)
    {
        var m = mat.RowCount;
        var n = mat.ColCount;
        var newMat = new Matrix(m, n + 1, new char[m][]);

        for (int i = 0; i < m; i++)
        {
            newMat.Entries[i] = new char[n + 1];
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n + 1; j++)
            {
                if (j < k)
                {
                    newMat.Entries[i][j] = mat.Entries[i][j];
                }
                else if (j == k)
                {
                    newMat.Entries[i][j] = '.';

                }
                else
                {
                    newMat.Entries[i][j] = mat.Entries[i][j - 1];
                }
            }
        }
        return newMat;
    }
}
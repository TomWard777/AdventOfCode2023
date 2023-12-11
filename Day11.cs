using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2023;

public class Day11
{
    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Test11.txt");
        //var input = FileParser.ReadInputFromFile("Day11.txt");

        var mat = Matrices.ReadToMatrix(input);

        var emptyRows = GetEmptyRows(mat);
        var emptyCols = GetEmptyColumns(mat);

        foreach (var k in emptyRows)
        {
            mat = InsertRow(k, mat);
            emptyRows = emptyRows.Select(i => i + 1).ToList();
            Console.WriteLine(k);
        }
        
        Console.WriteLine("Cols");

        foreach (var k in emptyCols)
        {
            mat = InsertColumn(k, mat);
            emptyCols = emptyCols.Select(i => i + 1).ToList();
            Console.WriteLine(k);
        }

        var points = GetPoints(mat);

        var sum = 0;
        foreach (var p in points)
        {
            foreach (var q in points)
            {
                sum += Dist(p, q);
                Console.WriteLine(p.Item1 + " " + p.Item2);
                Console.WriteLine(q.Item1 + " " + q.Item2);
                Console.WriteLine(Dist(p, q));
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
        return Math.Abs(point1.Item1 - point2.Item1) + Math.Abs(point1.Item2 - point2.Item2) -1;
    }

    public List<(int, int)> GetPoints(Matrix mat)
    {
        var result = new List<(int, int)>();

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

    public List<int> GetEmptyRows(Matrix mat)
    {
        var result = new List<int>();

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

    public List<int> GetEmptyColumns(Matrix mat)
    {
        var result = new List<int>();

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
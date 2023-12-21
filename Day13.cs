namespace AdventOfCode2023;

public class Day13
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test13.txt");
        var input = FileParser.ReadInputFromFile("Day13.txt");

        var lineBuffer = new List<string>();
        var matrices = new List<Matrix>();

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                matrices.Add(Matrices.ReadToMatrix(lineBuffer));
                lineBuffer = new List<string>();
            }
            else
            {
                lineBuffer.Add(line);
            }
        }

        if (lineBuffer.Any())
        {
            matrices.Add(Matrices.ReadToMatrix(lineBuffer));
            lineBuffer = new List<string>();
        }

        var result = 0;

        foreach (var mat in matrices)
        {
            Matrices.Draw(mat);
            var hor = GetHorizontalLineOfReflection(mat);

            if (hor.HasValue)
            {
                Console.WriteLine("Horizontal " + hor);
                result += 100 * hor.Value;
            }
            else
            {
                var vert = GetVerticalLineOfReflection(mat);
                Console.WriteLine("Vertical " + vert);
                result += vert.Value;
            }
        }

        Console.WriteLine(result);
    }

    public int? GetVerticalLineOfReflection(Matrix mat)
    {
        for (int j = 0; j < mat.ColCount - 1; j++)
        {
            if (IsVerticalLineOfReflection2(mat, j))
            {
                return j + 1;
            }
        }

        return null;
    }

    public int? GetHorizontalLineOfReflection(Matrix mat)
    {
        for (int i = 0; i < mat.RowCount - 1; i++)
        {
            if (IsHorizontalLineOfReflection2(mat, i))
            {
                return i + 1;
            }
        }

        return null;
    }

    public bool IsVerticalLineOfReflection2(Matrix mat, int col)
    {
        var seenFirstMismatch = false;

        Console.WriteLine(col);
        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < Math.Min(col, mat.ColCount - col - 2) + 1; j++)
            {
                if (mat.Entries[i][col - j] != mat.Entries[i][col + j + 1])
                {
                    if (seenFirstMismatch)
                    {
                        return false;
                    }
                    else
                    {
                        seenFirstMismatch = true;
                    }
                }
            }
        }

        return seenFirstMismatch;
    }

    public bool IsHorizontalLineOfReflection2(Matrix mat, int row)
    {
        var seenFirstMismatch = false;

        for (int i = 0; i < Math.Min(row, mat.RowCount - row - 2) + 1; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (mat.Entries[row - i][j] != mat.Entries[row + i + 1][j])
                {
                    if (seenFirstMismatch)
                    {
                        return false;
                    }
                    else
                    {
                        seenFirstMismatch = true;
                    }
                }
            }
        }

        return seenFirstMismatch;
    }

    public bool IsVerticalLineOfReflection(Matrix mat, int col)
    {
        Console.WriteLine(col);
        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < Math.Min(col, mat.ColCount - col - 2) + 1; j++)
            {
                if (mat.Entries[i][col - j] != mat.Entries[i][col + j + 1])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool IsHorizontalLineOfReflection(Matrix mat, int row)
    {
        for (int i = 0; i < Math.Min(row, mat.RowCount - row - 2) + 1; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (mat.Entries[row - i][j] != mat.Entries[row + i + 1][j])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
namespace AdventOfCode2023;

public class Matrix
{
    public Matrix(int m, int n, char[][] mat)
    {
        RowCount = m;
        ColCount = n;
        Entries = mat;
    }

    public int RowCount { get; set; }
    public int ColCount { get; set; }
    public char[][]? Entries { get; set; }
}
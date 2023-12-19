namespace AdventOfCode2023;

public class Day14
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test14.txt");
        var input = FileParser.ReadInputFromFile("Day14.txt");

        var mat = Matrices.ReadToMatrix(input);

        var scores = new List<int>();

        for (int k = 0; k < 1000; k++)
        {
            SpinCycle(mat);
            scores.Add(GetScore(mat));
        }

        var arr = scores.ToArray();
        int kprev = 0;
        int period = 0;

        for (int k = 0; k < 1000; k++)
        {
            if (arr[k] == arr.Last())
            {
                period = k - kprev;
                Console.WriteLine(period);
                kprev = k;
            }
        }

        mat = Matrices.ReadToMatrix(input);

        var offset = 1000000000 % period;

        for (int k = 0; k < 10 * period + offset; k++)
        {
            SpinCycle(mat);
        }

        Console.WriteLine("RESULT:");
        Console.WriteLine(GetScore(mat));
    }

    public void SpinCycle(Matrix mat)
    {
        bool changed;
        
        do
        {
            changed = MoveNorth(mat);
        }
        while (changed);

        do
        {
            changed = MoveWest(mat);
        }
        while (changed);

        do
        {
            changed = MoveSouth(mat);
        }
        while (changed);

        do
        {
            changed = MoveEast(mat);
        }
        while (changed);
    }

    public bool MoveNorth(Matrix mat)
    {
        var changed = false;

        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (i > 0 && mat.Entries[i][j] == 'O' && mat.Entries[i - 1][j] == '.')
                {
                    mat.Entries[i - 1][j] = 'O';
                    mat.Entries[i][j] = '.';
                    changed = true;
                }
            }
        }

        return changed;
    }

    public bool MoveSouth(Matrix mat)
    {
        var changed = false;

        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (i < mat.RowCount - 1 && mat.Entries[i][j] == 'O' && mat.Entries[i + 1][j] == '.')
                {
                    mat.Entries[i + 1][j] = 'O';
                    mat.Entries[i][j] = '.';
                    changed = true;
                }
            }
        }

        return changed;
    }

    public bool MoveEast(Matrix mat)
    {
        var changed = false;

        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (j < mat.ColCount - 1 && mat.Entries[i][j] == 'O' && mat.Entries[i][j + 1] == '.')
                {
                    mat.Entries[i][j + 1] = 'O';
                    mat.Entries[i][j] = '.';
                    changed = true;
                }
            }
        }

        return changed;
    }

    public bool MoveWest(Matrix mat)
    {
        var changed = false;

        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (j > 0 && mat.Entries[i][j] == 'O' && mat.Entries[i][j - 1] == '.')
                {
                    mat.Entries[i][j - 1] = 'O';
                    mat.Entries[i][j] = '.';
                    changed = true;
                }
            }
        }

        return changed;
    }

    public int GetScore(Matrix mat)
    {
        var score = 0;

        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (mat.Entries[i][j] == 'O')
                {
                    score += mat.RowCount - i;
                }
            }
        }

        return score;
    }
}
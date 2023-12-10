namespace AdventOfCode2023;

public class Day10
{
    private readonly int _m;
    private readonly int _n;
    private readonly char[][] _mat;

    public Day10()
    {
        //var input = FileParser.ReadInputFromFile("Test10.txt");
        var input = FileParser.ReadInputFromFile("Day10.txt");

        (_m, _n, _mat) = Matrices.ReadToMatrix(input);
    }

    public void Run()
    {
        var (starti, startj) = GetStartIndexes();

        var path = new List<(int, int)> { (starti, startj) };
        var leftHandPoints = new List<(int, int)> { };
        var pathCount = 1;

        // We have to inspect the grid by eye and see which ways we can go from the start.
        // We may need to try different directions to get the interior on the left hand side.
        // A better implementation would fix this.
        var facing = Facing.Down;

        do
        {
            facing = Step(path, leftHandPoints, facing);

            if (pathCount == path.Count())
            {
                break;
            }

            pathCount = path.Count();
        }
        while (true);

        leftHandPoints = leftHandPoints
        .Where(p => !path.Contains(p))
        .Where(p => p.Item1 >= 0 && p.Item1 < _m && p.Item2 >= 0 && p.Item2 < _n)
        .Distinct()
        .ToList();

        //_matrices.Draw(m, m, _mat);
        DrawResult(path, leftHandPoints);

        // This implementation doesn't count interior points inside the initial interior border.
        // To finish the puzzle I counted the one remaining 'blob' manually.
        Console.WriteLine("\nLoop length: " + path.Count());
        Console.WriteLine("\nInterior points found: " + leftHandPoints.Count());
    }

    public Facing Step(
        List<(int, int)> path,
        List<(int, int)> leftHandPoints,
        Facing facing)
    {
        var lastPoint = path.Last();

        var i = lastPoint.Item1;
        var j = lastPoint.Item2;

        var newFacing = (_mat[i][j], facing) switch
        {
            ('-', Facing.Left) => Facing.Left,
            ('-', Facing.Right) => Facing.Right,

            ('|', Facing.Up) => Facing.Up,
            ('|', Facing.Down) => Facing.Down,

            ('L', Facing.Left) => Facing.Up,
            ('L', Facing.Down) => Facing.Right,

            ('7', Facing.Right) => Facing.Down,
            ('7', Facing.Up) => Facing.Left,

            ('J', Facing.Right) => Facing.Up,
            ('J', Facing.Down) => Facing.Left,

            ('F', Facing.Left) => Facing.Down,
            ('F', Facing.Up) => Facing.Right,

            _ => facing, // A bit of a hack for the start.
        };

        var next = GetPointInFront(i, j, newFacing);
        var nextLeftHand1 = GetLeftHandPoint(i, j, newFacing);
        var nextLeftHand2 = GetLeftHandPoint(next.Item1, next.Item2, newFacing);

        if (!path.Contains(next))
        {
            path.Add(next);
        }

        leftHandPoints.Add(nextLeftHand1);
        leftHandPoints.Add(nextLeftHand2);

        return newFacing;
    }

    public (int, int) GetPointInFront(int i, int j, Facing facing)
    {
        return facing switch
        {
            Facing.Up => (i - 1, j),
            Facing.Down => (i + 1, j),
            Facing.Left => (i, j - 1),
            Facing.Right => (i, j + 1),
            _ => throw new NotSupportedException()
        };
    }

    public (int, int) GetLeftHandPoint(int i, int j, Facing facing)
    {
        return facing switch
        {
            Facing.Up => (i, j - 1),
            Facing.Down => (i, j + 1),
            Facing.Left => (i + 1, j),
            Facing.Right => (i - 1, j),
            _ => throw new NotSupportedException()
        };
    }

    public (int, int) GetStartIndexes()
    {
        var i = 0;

        foreach (var row in _mat)
        {
            if (row.Contains('S'))
            {
                break;
            }

            i++;
        }

        for (int j = 0; ; j++)
        {
            if (_mat[i][j] == 'S')
            {
                return (i, j);
            }
        }
    }

    public void DrawResult(IEnumerable<(int, int)> path, IEnumerable<(int, int)> interior)
    {
        for (int i = 0; i < _m; i++)
        {
            for (int j = 0; j < _n; j++)
            {
                if (path.Contains((i, j)))
                {
                    Console.Write(_mat[i][j]);
                    //Console.Write(' ');
                }
                else if (interior.Contains((i, j)))
                {
                    Console.Write('.');
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

public enum Facing
{
    Up,
    Down,
    Left,
    Right
}

namespace AdventOfCode2023;

public class Day16
{
    private const int _numberOfIterations = 750;
    private List<(int, int)> _visited = new List<(int, int)>();
    private List<Beam> _beams = new List<Beam>();

    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test16.txt");
        var input = FileParser.ReadInputFromFile("Day16.txt");

        var mat = Matrices.ReadToMatrix(input);
        var startingPoints = new List<Beam>();

        for (int i = 0; i < mat.RowCount; i++)
        {
            startingPoints.Add(new Beam(Facing.Right, (i, 0)));
            startingPoints.Add(new Beam(Facing.Left, (i, mat.ColCount - 1)));
        }

        for (int j = 0; j < mat.ColCount; j++)
        {
            startingPoints.Add(new Beam(Facing.Down, (0, j)));
            startingPoints.Add(new Beam(Facing.Up, (mat.RowCount - 1, j)));
        }

        var max = 0;
        foreach (var beam in startingPoints)
        {
            var score = GetVisitedCount(mat, beam);

            max = Math.Max(score, max);
            Console.WriteLine(max);
        }

        Console.WriteLine("RESULT:");
        Console.WriteLine(max);
    }

    public int GetVisitedCount(Matrix mat, Beam initialBeam)
    {
        _beams = new List<Beam>() { initialBeam };
        _visited = new List<(int, int)>() { initialBeam.Position };

        for (var ct = 0; ct < _numberOfIterations; ct++)
        {
            ProgressBeamsOneStep(_beams, mat);

            if (!_beams.Any())
            {
                break;
            }

            if (ct % 10 == 0)
            {
                Console.Write($"{_numberOfIterations - ct}  ");
            }
        }

        Console.WriteLine();

        _visited = _visited
        .Distinct()
        .Where(p => IsPositionValid(p, mat))
        .ToList();

        return _visited.Count;
    }

    public void ProgressBeamsOneStep(
        List<Beam> beams,
        Matrix mat)
    {
        if (!beams.Any())
        {
            return;
        }

        var newBeams = new List<Beam>();

        foreach (var beam in beams)
        {
            var processed = ProcessPosition(beam, mat);
            newBeams.AddRange(processed);
        }

        MoveBeamsForward(newBeams, mat);
        beams = newBeams;

        _visited.AddRange(GetNewBeamPositions(beams, _visited));
        _beams = beams;
    }

    public List<(int, int)> GetNewBeamPositions(
        List<Beam> beams,
        List<(int, int)> positionsVisited)
    {
        var beamPositions = beams
        .Select(x => x.Position)
        .Distinct()
        .ToList();

        return beamPositions.Except(positionsVisited).ToList();
    }

    public void MoveBeamsForward(List<Beam> beams, Matrix mat)
    {
        foreach (var beam in beams)
        {
            var position = GetPointInFront(beam.Position.Item1, beam.Position.Item2, beam.Facing);
            beam.Position = position;
        }
    }

    public List<Beam> ProcessPosition(
    Beam beam,
    Matrix mat)
    {
        var (i, j) = beam.Position;
        var newBeams = new List<Beam>();

        if (!IsPositionValid(beam.Position, mat))
        {
            return newBeams;
        }
        var tile = mat.Entries[i][j];

        if (tile == '-' && (beam.Facing == Facing.Up || beam.Facing == Facing.Down))
        {
            newBeams = Split(beam);
        }
        else if (tile == '|' && (beam.Facing == Facing.Left || beam.Facing == Facing.Right))
        {
            newBeams = Split(beam);
        }
        else if (tile == '\\')
        {
            beam.Facing = ReflectInBackSlash(beam.Facing);
            newBeams.Add(beam);
        }
        else if (tile == '/')
        {
            beam.Facing = ReflectInForwardSlash(beam.Facing);
            newBeams.Add(beam);
        }
        else
        {
            newBeams.Add(beam);
        }

        return newBeams;
    }

    public Facing ReflectInForwardSlash(Facing facing)
    {
        return facing switch
        {
            Facing.Left => Facing.Down,
            Facing.Right => Facing.Up,
            Facing.Up => Facing.Right,
            Facing.Down => Facing.Left,
            _ => throw new NotSupportedException()
        };
    }

    public Facing ReflectInBackSlash(Facing facing)
    {
        return facing switch
        {
            Facing.Left => Facing.Up,
            Facing.Right => Facing.Down,
            Facing.Up => Facing.Left,
            Facing.Down => Facing.Right,
            _ => throw new NotSupportedException()
        };
    }

    public List<Beam> Split(Beam beam)
    {
        if (beam.Facing == Facing.Left || beam.Facing == Facing.Right)
        {
            return new List<Beam>()
            {
                new Beam(Facing.Up, beam.Position),
                new Beam(Facing.Down, beam.Position)
            };
        }

        return new List<Beam>()
        {
            new Beam(Facing.Left, beam.Position),
            new Beam(Facing.Right, beam.Position)
        };
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

    public bool IsPositionValid((int, int) position, Matrix mat)
    {
        var (i, j) = position;

        if (i >= 0 && i < mat.RowCount && j >= 0 && j < mat.ColCount)
        {
            return true;
        }

        return false;
    }

    public static void DrawBeams(Matrix mat, List<Beam> beams)
    {
        DrawPath(mat, beams.Select(b => b.Position).ToList());
        Console.WriteLine(" ");
    }

    public static void DrawPath(Matrix mat, List<(int, int)> positions)
    {
        for (int i = 0; i < mat.RowCount; i++)
        {
            for (int j = 0; j < mat.ColCount; j++)
            {
                if (positions.Contains((i, j)))
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
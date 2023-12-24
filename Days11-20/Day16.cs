namespace AdventOfCode2023;

public class Day16
{
    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Test16.txt");
        //var input = FileParser.ReadInputFromFile("Day16.txt");

        var mat = Matrices.ReadToMatrix(input);

        Console.WriteLine("RESULT:");
        Console.WriteLine(0);
    }

    public List<Beam> ProcessPosition(
    Beam beam,
    Matrix mat)
    {
        var (i, j) = beam.Position;
        var tile = mat.Entries[i][j];
        var newBeams = new List<Beam>();

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

        return new List<Beam>();
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
            Facing.Left => Facing.Down,
            Facing.Right => Facing.Up,
            Facing.Up => Facing.Left,
            Facing.Down => Facing.Right,
            _ => throw new NotSupportedException()
        };
    }

    public List<Beam> Split(Beam beam)
    {
        if (beam.Facing == Facing.Left || beam.Facing == Facing.Right)
        {
            beam.Facing = Facing.Down;
            return new List<Beam>()
            {
                beam,
                new Beam(Facing.Up, beam.Position)
            };
        }

        beam.Facing = Facing.Left;

        return new List<Beam>()
        {
            beam,
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
}

public class Beam
{
    public Beam(Facing facing, (int, int) position)
    {
        Facing = facing;
        Position = position;
    }

    public Facing Facing { get; set; }
    public (int, int) Position { get; set; }
}
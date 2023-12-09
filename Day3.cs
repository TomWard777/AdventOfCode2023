namespace AdventOfCode2023;

public class Day3
{
    private readonly char[] _numerals = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private Dictionary<char, int> _numberDictionary;

    public Day3()
    {
        _numberDictionary = DataParser.GetNumberCharDictionary();
    }

    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Test3.txt");
        ////var input = FileParser.ReadInputFromFile("Day3.txt");

        var (m, n, mat) = Matrices.ReadToMatrix(input);

        var gears = GetGears(m, n, mat);
        var gearNumbers = GetNumbersAdjacentToGears(m, n, mat, gears);

        foreach (var a in gearNumbers)
        {
            Console.WriteLine(a.Item1);
            Console.WriteLine(a.Item2);
        }

        var ratios = gearNumbers
        .GroupBy(x => x.Item2)
        .Where(g => g.Count() > 1)
        .Select(g => g.Select(y => y.Item1).Aggregate((acc, val) => acc * val))
        .ToArray();

        foreach (var a in ratios)
        {
            Console.WriteLine(a);
        }

        Console.WriteLine("\nTOTAL:");
        Console.WriteLine(ratios.Sum() + "\n");

        Matrices.Draw(m, n, mat);
    }

    public List<(int, (int, int))> GetNumbersAdjacentToGears(int m, int n, char[][] mat, IEnumerable<Gear> gears)
    {
        var results = new List<(int, (int, int))>();
        var digits = new List<int>();
        var includeNumber = false;
        (int, int) gearPoint = (-1, -1);

        var includedPlaces = gears
        .SelectMany(g => g.Adjacent)
        .ToArray();

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var entry = mat[i][j];

                if (_numerals.Contains(entry))
                {
                    if (includedPlaces.Contains((i, j)))
                    {
                        includeNumber = true;
                        gearPoint = GetAdjacentGearPoint(i, j, gears);
                    }
                    includeNumber = includeNumber || includedPlaces.Contains((i, j));
                    digits.Add(_numberDictionary[entry]);
                }
                else
                {
                    if (digits.Any())
                    {
                        if (includeNumber)
                        {
                            results.Add((Maths.GetNumberFromDigits(digits), gearPoint));
                        }
                        digits = new List<int>();
                        includeNumber = false;
                    }
                }
            }

            if (digits.Any() && includeNumber)
            {
                results.Add((Maths.GetNumberFromDigits(digits), gearPoint));
            }
            digits = new List<int>();
            includeNumber = false;
        }

        return results;
    }

    public (int, int) GetAdjacentGearPoint(int i, int j, IEnumerable<Gear> gears)
    {
        var adjacentGears = gears
        .Where(g => g.Adjacent.Contains((i, j)));

        if (adjacentGears.SingleOrDefault() != null)
        {
            return adjacentGears.Single().Position;
        }

        return (-1, -1);
    }

    public List<Gear> GetGears(int m, int n, char[][] mat)
    {
        var gears = new List<Gear>();

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var entry = mat[i][j];
                if (entry == '*')
                {
                    gears.Add(new Gear(m, n, i, j));
                }
            }
        }

        return gears;
    }

    public void Draw(int m, int n, IEnumerable<(int, int)> places)
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (places.Contains((i, j)))
                {
                    Console.Write("@");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.Write("\n");
        }
    }
}

public class Gear
{
    public Gear(int m, int n, int i, int j)
    {
        Position = (i, j);
        Adjacent = Matrices.GetAdjacentPlaces(m, n, i, j);
    }

    public (int, int) Position { get; set; }
    public IEnumerable<(int, int)> Adjacent { get; set; }
}

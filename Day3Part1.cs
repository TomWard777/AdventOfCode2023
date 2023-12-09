namespace AdventOfCode2023;

public class Day3Part1
{
    private readonly char[] _numerals = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private Dictionary<char, int> _numberDictionary;

    public Day3Part1()
    {
        _numberDictionary = DataParser.GetNumberCharDictionary();
    }

    public void Run()
    {
        ////var input = FileParser.ReadInput("Test3.txt");
        var input = FileParser.ReadInputFromFile("Day3.txt");

        var (m, n, mat) = Matrices.ReadToMatrix(input);

        var includedPlaces = GetEngineAdjacentPlaces(m, n, mat);
        var numbers = GetIncludedNumbers(m, n, mat, includedPlaces);

        foreach (var a in numbers)
        {
            Console.WriteLine(a);
        }

        Console.WriteLine("\n");
        Draw(m, n, includedPlaces);

        Console.WriteLine("\nTOTAL:");
        Console.WriteLine(numbers.Sum());
    }

    public List<int> GetIncludedNumbers(int m, int n, char[][] mat, IEnumerable<(int, int)> includedPlaces)
    {
        var numbers = new List<int>();
        var digits = new List<int>();
        var includeNumber = false;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var entry = mat[i][j];

                if (_numerals.Contains(entry))
                {
                    includeNumber = includeNumber || includedPlaces.Contains((i, j));
                    digits.Add(_numberDictionary[entry]);
                }
                else
                {
                    if (digits.Any())
                    {
                        if (includeNumber)
                        {
                            numbers.Add(Maths.GetNumberFromDigits(digits));
                        }
                        digits = new List<int>();
                        includeNumber = false;
                    }
                }
            }

            if (digits.Any() && includeNumber)
            {
                numbers.Add(Maths.GetNumberFromDigits(digits));
            }
            digits = new List<int>();
            includeNumber = false;
        }

        return numbers;
    }

    public IEnumerable<(int, int)> GetEngineAdjacentPlaces(int m, int n, char[][] mat)
    {
        var places = new List<(int, int)>();

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var entry = mat[i][j];
                if (!_numerals.Contains(entry) && entry != '.')
                {
                    places.AddRange(Matrices.GetAdjacentPlaces(m, n, i, j));
                }
            }
        }

        return places.Distinct().ToArray();
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

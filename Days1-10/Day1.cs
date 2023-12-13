namespace AdventOfCode2023;

public class Day1
{
    private readonly char[] numerals = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    private Dictionary<string, int> _numberDictionary;

    public Day1()
    {
        _numberDictionary = DataParser.GetNumberStringDictionary();
    }

    public void Run()
    {
        ////var input = FileParser.ReadInput("Test1.txt");
        var input = FileParser.ReadInputFromFile("Day1.txt");

        var sum = 0;

        foreach (var line in input)
        {
            Console.WriteLine(line);

            var result = GetNumberFromLine(line);
            sum += result;

            Console.WriteLine(result);
            Console.WriteLine(sum);
        }
        Console.WriteLine("\nTOTAL = " + sum);
    }

    public int GetNumberFromLine(string line)
    {
        var numberStrings = _numberDictionary.Keys;
        
        var firstOccurrences = numberStrings
        .Select(s => new 
        {
            NumberString = s,
            Index = line.IndexOf(s)
        })
        .Where(x => x.Index > -1)
        .ToArray();

        var lastOccurrences = numberStrings
        .Select(s => new 
        {
            NumberString = s,
            Index = line.LastIndexOf(s)
        })
        .Where(x => x.Index > -1)
        .ToArray();

        var numberArray = firstOccurrences
        .Union(lastOccurrences)
        .OrderBy(x => x.Index)
        .Select(x => _numberDictionary[x.NumberString])
        .ToArray();

        var ct = numberArray.Count();

        return 10 * numberArray[0] + numberArray[ct-1];
    }

    public int GetNumberFromLinePart1(string line)
    {
        var numbers = line.ToCharArray()
        .Where(c => numerals.Contains(c))
        .Select(c => int.Parse(c.ToString()))
        .ToArray();

        var ct = numbers.Count();

        if (ct == 0)
        {
            return 0;
        }
        else
        {
            return 10 * numbers[0] + numbers[ct - 1];
        }
    }
}

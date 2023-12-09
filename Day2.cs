namespace AdventOfCode2023;

public class Day2
{
    private readonly string[] _colours = { "red", "blue", "green" };

    public void Run()
    {
        //var input = FileParser.ReadInput("Test2.txt");
        var input = FileParser.ReadInputFromFile("Day2.txt");

        var sum = 0;

        foreach (var line in input)
        {
            ////var x = GetLineContribution(line);
            var x = GetPowerFromLine(line);
            sum += x;

            Console.WriteLine(line);
            Console.WriteLine(x);
        }

        Console.WriteLine("\nTOTAL:");
        Console.WriteLine(sum);
    }

    public int GetPowerFromLine(string line)
    {
        var separators = new string[] {": ", "; "};

        var arr = line.Replace("Game ", "")
        .Split(separators, StringSplitOptions.None);

        var cubeSets = arr.Skip(1)
        .Select(x => GetCubesFromString(x));

        return GetPower(cubeSets);
    }

    public int GetPower(IEnumerable<Dictionary<string, int>> cubeSets)
    {
        var redMax = cubeSets.Select(v => v["red"]).Max();
        var blueMax = cubeSets.Select(v => v["blue"]).Max();
        var greenMax = cubeSets.Select(v => v["green"]).Max();
        return redMax * blueMax * greenMax;
    }


    public int GetLineContribution(string line)
    {
        var separators = new string[] {": ", "; "};

        var arr = line.Replace("Game ", "")
        .Split(separators, StringSplitOptions.None);

        var id = int.Parse(arr[0]);

        var invalid = arr.Skip(1)
        .Select(x => GetCubesFromString(x))
        .Any(cubes => !AreCubesPossible(cubes));

        return invalid ? 0 : id;
    }

    public bool AreCubesPossible(Dictionary<string, int> cubes)
    {
        if (cubes["red"] <= 12 && cubes["green"] <= 13 && cubes["blue"] <= 14)
        {
            return true;
        }

        return false;
    }

    public Dictionary<string, int> GetCubesFromString(string cubesString)
    {
        var dict = cubesString.Split(", ")
        .Select(x => x.Split(" "))
        .ToDictionary(v => v[1], v => int.Parse(v[0]));

        foreach (var colour in _colours)
        {
            if (!dict.ContainsKey(colour))
            {
                dict.Add(colour, 0);
            }
        }

        return dict;
    }
}
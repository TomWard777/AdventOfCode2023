namespace AdventOfCode2023;

public static class DataParser
{
    public static string RepeatString(this string text, int n)
    {
        return string.Concat(Enumerable.Repeat(text, n));
    }

    public static Dictionary<char, int> GetNumberCharDictionary()
    {
        return new Dictionary<char, int>
        {
            ['0'] = 0,
            ['1'] = 1,
            ['2'] = 2,
            ['3'] = 3,
            ['4'] = 4,
            ['5'] = 5,
            ['6'] = 6,
            ['7'] = 7,
            ['8'] = 8,
            ['9'] = 9
        };
    }

    public static Dictionary<string, int> GetNumberStringDictionary()
    {
        return new Dictionary<string, int>
        {
            ["zero"] = 0,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
            ["0"] = 0,
            ["1"] = 1,
            ["2"] = 2,
            ["3"] = 3,
            ["4"] = 4,
            ["5"] = 5,
            ["6"] = 6,
            ["7"] = 7,
            ["8"] = 8,
            ["9"] = 9
        };
    }
}

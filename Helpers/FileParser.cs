namespace AdventOfCode2023;

public static class FileParser
{
    public static IEnumerable<string> ReadInputFromFile(string fileName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput", fileName);
        return File.ReadLines(path);
    }
}

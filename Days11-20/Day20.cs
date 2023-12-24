namespace AdventOfCode2023;

public class Day20
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test20.txt").ToArray();
        var input = FileParser.ReadInputFromFile("Day20.txt").ToArray();

        var modules = input
        .Select(x => new Module(x))
        .ToList();

        foreach (var mod in modules)
        {
            var inputs = modules
            .Where(x => x.Outputs.Contains(mod.Id))
            .Select(x => x.Id)
            .ToList();

            mod.Inputs = inputs;
        }

        Console.WriteLine("\nANSWER:");
        Console.WriteLine(0);
    }
}

public class Module
{
    public Module(string line)
    {
        if (line.Contains('%'))
        {
            Type = Type.FlipFlop;
            line = line.Substring(1);
        }
        else if (line.Contains('&'))
        {
            Type = Type.Conjunction;
            line = line.Substring(1);
        }
        else
        {
            Type = Type.None;
        }

        var arr = line.Split(new char[] { '-', '>', ',' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Trim())
        .ToArray();

        Id = arr[0];
        Outputs = arr.Skip(1).ToList();
    }

    public string Id { get; set; }
    public List<string> Inputs { get; set; }
    public List<string> Outputs { get; set; }
    public Type Type { get; set; }
    public Pulse CurrentPulse { get; set; }
    public bool On { get; set; }
}

public enum Type
{
    None,
    FlipFlop,
    Conjunction
}

public enum Pulse
{
    None,
    Low,
    High
}
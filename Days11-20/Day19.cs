using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day19
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test19.txt");
        var input = FileParser.ReadInputFromFile("Day19.txt");

        var workflowInput = input
        .Where(x => !string.IsNullOrEmpty(x) && x[0] != '{')
        .ToArray();

        var workflows = workflowInput
        .Select(x => new Workflow(x))
        .ToArray();

        var partInput = input
        .Where(x => !string.IsNullOrEmpty(x) && x[0] == '{')
        .ToArray();

        var parts = partInput
        .Select(x => GetPartDictionary(x))
        .ToArray();

        var acceptedParts = parts
        .Where(p => IsAccepted(workflows, p))
        .ToArray();

        var result = acceptedParts
        .Select(p => p.Values.Sum())
        .Sum();

        Console.WriteLine("RESULT:");
        Console.WriteLine(result);
    }

    public bool IsAccepted(Workflow[] workflows, Dictionary<char, int> part)
    {
        var workflow = workflows.Where(w => w.Id == "in").First();
        var accepted = false;

        while (true)
        {
            var nextId = Process(workflow, part);
            if (nextId == "A" || nextId == "R")
            {
                accepted = nextId == "A";
                break;
            }
            workflow = workflows.Where(w => w.Id == nextId).First();
        }
        return accepted;
    }

    public string Process(Workflow workflow, Dictionary<char, int> part)
    {
        foreach (var rule in workflow.Rules)
        {
            if (Check(rule, part[rule.Key]))
            {
                return rule.DestinationId;
            }
        }

        return workflow.NoMatchDestinationId;
    }

    public bool Check(Rule rule, int value)
    {
        if (rule.Inequality == Inequality.LessThan)
        {
            return value < rule.Number;
        }

        return value > rule.Number;
    }

    public Dictionary<char, int> GetPartDictionary(string line)
    {
        var dict = new Dictionary<char, int>();
        var arr = line.Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var entry in arr)
        {
            var pair = entry.Split('=');
            dict.Add(pair[0][0], int.Parse(pair[1]));
        }
        return dict;
    }
}

public class Workflow
{
    public Workflow(string line)
    {
        var arr = line.Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);
        var n = arr.Length;

        Id = arr[0];
        NoMatchDestinationId = arr[n - 1];
        Rules = new List<Rule>();

        foreach (var ruleString in arr.Skip(1).Take(n - 2))
        {
            Rules.Add(new Rule(ruleString));
        }
    }

    public string Id { get; set; }
    public List<Rule> Rules { get; set; }
    public string NoMatchDestinationId { get; set; }
}

public class Rule
{
    public Rule(string line)
    {
        Key = line[0];
        Inequality = line[1] == '<' ? Inequality.LessThan : Inequality.GreaterThan;

        string numberString = Regex.Replace(line, @"[^\d]", "");
        Number = int.Parse(numberString);

        DestinationId = line.Split(':')[1];
    }

    public char Key { get; set; }
    public Inequality Inequality { get; set; }
    public int Number { get; set; }
    public string DestinationId { get; set; }
}

public enum Inequality
{
    LessThan,
    GreaterThan
}
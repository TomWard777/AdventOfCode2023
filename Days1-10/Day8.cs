namespace AdventOfCode2023;

public class Day8
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test8.txt").ToArray();
        var input = FileParser.ReadInputFromFile("Day8.txt").ToArray();

        var directions = input[0];

        var nodes = input.Skip(2)
        .Select(x => new Node(x))
        .ToArray();

        var startNodeIds = nodes
        .Where(x => x.Id[2] == 'A')
        .Select(x => x.Id)
        .ToList();

        var loopNumbers = new List<long>();

        foreach (var id in startNodeIds)
        {
            var (m, n) = GetRepeatData(nodes, directions, id);
            Console.WriteLine($"{m} until Z, {n} more to repeat Z");
            loopNumbers.Add((long)m);
        }

        var gcd = Maths.GCD(loopNumbers);
        var r = Maths.Product(loopNumbers.Select(n => n / gcd));

        Console.WriteLine("\nGCD:");
        Console.WriteLine(gcd);
        Console.WriteLine("\nCommon multiple of other bit:");
        Console.WriteLine(r);
        Console.WriteLine("\nANSWER:");
        Console.WriteLine(r * gcd);
    }

    public (int numberUntilZ, int numberUntilZRepeat) GetRepeatData(IEnumerable<Node> nodes, string directions, string startNodeId)
    {
        var currentNodeIds = nodes
        .Where(x => x.Id[2] == 'A')
        .Select(x => x.Id)
        .ToList();

        var i = 0;
        var steps = 0;
        var currentNodeId = startNodeId;

        var numberUntilZ = -1;
        var firstZ = "";

        do
        {
            var dir = directions[i];
            currentNodeId = GetNextNodeId(nodes, currentNodeId, dir);

            steps++;
            i = (i + 1) % directions.Length;

            if (currentNodeId[2] == 'Z')
            {
                if (numberUntilZ == -1)
                {
                    numberUntilZ = steps;
                }

                if (firstZ.Length < 3)
                {
                    firstZ = currentNodeId;
                }
                else
                {
                    break;
                }
            }
        }
        while (true);

        return (numberUntilZ, steps - numberUntilZ);
    }

    public string GetNextNodeId(IEnumerable<Node> nodes, string currentNodeId, char direction)
    {
        var currentNode = nodes.Where(x => x.Id == currentNodeId).First();

        if (direction == 'L')
        {
            return currentNode.Left;
        }
        else
        {
            return currentNode.Right;
        }
    }
}

public class Node
{
    public Node(string line)
    {
        Id = line.Substring(0, 3);
        Left = line.Substring(7, 3);
        Right = line.Substring(12, 3);
    }

    public string Id { get; set; }
    public string Left { get; set; }
    public string Right { get; set; }
}
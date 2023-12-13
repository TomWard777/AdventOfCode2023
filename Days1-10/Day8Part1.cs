namespace AdventOfCode2023;

public class Day8Part1
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test8.txt").ToArray();
        var input = FileParser.ReadInputFromFile("Day8.txt").ToArray();

        var directions = input[0];

        var nodes = input.Skip(2)
        .Select(x => new Node(x))
        .ToArray();

        var currentNodeId = "AAA";
        var i = 0;
        var steps = 0;
 
        do
        {
            var currentNode = nodes.Where(x => x.Id == currentNodeId).First();
            Console.WriteLine(currentNodeId);

            if(directions[i] == 'L')
            {
                currentNodeId = currentNode.Left;
            }
            else
            {
                currentNodeId = currentNode.Right;
            }

            steps++;
            i = (i+1) % directions.Length;
        }
        while(currentNodeId != "ZZZ");

        Console.WriteLine("STEPS:");
        Console.WriteLine(steps);
    }
}
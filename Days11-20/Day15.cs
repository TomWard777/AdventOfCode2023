using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2023;

public class Day15
{
    public void Run()
    {
        //var input = FileParser.ReadInputFromFile("Test15.txt").First();
        var input = FileParser.ReadInputFromFile("Day15.txt").First();

        var arr = input.Split(',');

        var boxes = Enumerable.Range(0, 256)
        .Select(x => new List<Lens>())
        .ToArray();

        string label;

        foreach (var str in arr)
        {
            if (str.Contains('='))
            {
                var v = str.Split('=');
                label = v[0];
                var focalLength = int.Parse(v[1]);
                AddLens(boxes, label, focalLength);
            }
            else
            {
                label = str.Replace("-", string.Empty);
                RemoveLens(boxes, label);
            }
        }

        var total = 0;

        for (int k = 0; k < 256; k++)
        {
            var m = 1;

            foreach (var lens in boxes[k])
            {
                total += (k + 1) * m * lens.FocalLength;

                Console.WriteLine($"Box {k} lens {m} {lens.Label} {(k + 1) * m * lens.FocalLength}");

                m++;
            }
        }

        Console.WriteLine("\nANSWER:");
        Console.WriteLine(total);
    }

    public void AddLens(List<Lens>[] boxes, string label, int focalLength)
    {
        var box = boxes[Hash(label)];

        if (!box.Any(x => x.Label == label))
        {
            box.Add(new Lens(label, focalLength));
        }
        else
        {
            foreach (var lens in box)
            {
                if (lens.Label == label)
                {
                    lens.FocalLength = focalLength;
                }
            }
        }
    }

    public void RemoveLens(List<Lens>[] boxes, string label)
    {
        //Console.WriteLine($"Removing {label} from {Hash(label)}");
        boxes[Hash(label)] = boxes[Hash(label)].Where(x => x.Label != label).ToList();
    }

    public int Hash(string str)
    {
        var val = 0;
        foreach (char character in str)
        {
            val += (int)character;
            val *= 17;
            val = val % 256;
        }

        return val;
    }
}

public class Lens
{
    public Lens(string label, int focalLength)
    {
        Label = label;
        FocalLength = focalLength;
    }
    public string Label { get; set; }
    public int FocalLength { get; set; }
}
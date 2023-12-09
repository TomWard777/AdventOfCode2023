namespace AdventOfCode2023;

public class Day7
{
    public void Run()
    {
        ///var input = FileParser.ReadInputFromFile("Test7.txt");
        var input = FileParser.ReadInputFromFile("Day7.txt").ToArray();

        var hands = input.Select(x => new Hand(x)).ToList();

        var n = 1;
        var result = 0;

        foreach (var hand in hands.OrderBy(x => x.Type).ThenBy(x => x))
        {
            Console.WriteLine($"{hand.Cards}  {hand.Type}");
            result += n * hand.Bid;
            n++;
        }

        Console.WriteLine("SOLUTION: " + result);
    }
}

public class Hand : IComparable
{
    public Hand(string input)
    {
        var arr = input.Split(" ");
        (Cards, Bid) = (arr[0], int.Parse(arr[1]));
        Type = GetHandType2(Cards);
    }

    public string Cards { get; set; }
    public int Bid { get; set; }
    public HandType Type { get; set; }

    public int CompareTo(object? other)
    {
        var otherCards = ((Hand)other).Cards;
        for (int i = 0; i < Cards.Length; i++)
        {
            if  (Cards[i] != otherCards[i])
            {
                return GetCardOrder2(Cards[i]) - GetCardOrder2(otherCards[i]);
            }
        }

        return 0;
    }

    public HandType GetHandType(string cards)
    {
        var groupSizes = cards.GroupBy(x => x)
        .Select(g => g.Count())
        .OrderByDescending(ct => ct)
        .ToArray();

        return groupSizes switch
        {
            [5] => HandType.Five,
            [4, 1] => HandType.Four,
            [3, 2] => HandType.FullHouse,
            [3, 1, 1] => HandType.Three,
            [2, 2, 1] => HandType.TwoPair,
            [2, 1, 1, 1] => HandType.Pair,
            [1, 1, 1, 1, 1] => HandType.HighCard,
            _ => throw new Exception("Problem with cards: " + cards)
        };
    }

    public HandType GetHandType2(string cards)
    {
        if (cards == "JJJJJ")
        {
            return HandType.Five;
        }

        var mostCommon = cards.Where(x => x != 'J')
        .GroupBy(x => x)
        .OrderByDescending(g => g.Count())
        .Select(g => g.Key)
        .First();
 
        var newCards = cards.Replace('J', mostCommon);

        var groupSizes = newCards.GroupBy(x => x)
        .Select(g => g.Count())
        .OrderByDescending(ct => ct)
        .ToArray();

        return groupSizes switch
        {
            [5] => HandType.Five,
            [4, 1] => HandType.Four,
            [3, 2] => HandType.FullHouse,
            [3, 1, 1] => HandType.Three,
            [2, 2, 1] => HandType.TwoPair,
            [2, 1, 1, 1] => HandType.Pair,
            [1, 1, 1, 1, 1] => HandType.HighCard,
            _ => throw new Exception("Problem with cards: " + cards)
        };
    }

    public int GetCardOrder(char card)
    {
        var str = card.ToString();
        return str switch
        {
            "A" => 14,
            "K" => 13,
            "Q" => 12,
            "J" => 11,
            "T" => 10,
            _ => int.Parse(str)
        };
    }

    public int GetCardOrder2(char card)
    {
        var str = card.ToString();
        return str switch
        {
            "A" => 14,
            "K" => 13,
            "Q" => 12,
            "J" => 1,
            "T" => 10,
            _ => int.Parse(str)
        };
    }
}

public enum HandType
{
    HighCard = 0,
    Pair = 1,
    TwoPair = 2,
    Three = 3,
    FullHouse = 4,
    Four = 5,
    Five = 6,
}
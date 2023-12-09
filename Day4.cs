namespace AdventOfCode2023;

public class Day4
{
    public void Run()
    {
        ///var input = FileParser.ReadInputFromFile("Test4.txt");
        var input = FileParser.ReadInputFromFile("Day4.txt");

        /*
        Regex.Replace(x, @"Card [\d]: ", string.Empty)
        */
        var cards1 = input.Select(x => ReadInputLineToCard(x)).ToList();
        var cards = input
        .Select(x => ReadInputLineToCard(x))
        .ToDictionary(c => c.Id, c => c);

        foreach (var card in cards)
        {
            Process(card.Value, cards);
        }

        foreach (var card in cards)
        {
            Console.WriteLine("Card" + card.Value.Id);
            Console.WriteLine(card.Value.NumberOfCopies);
        }

        var result = cards.Values
        .Select(x => x.NumberOfCopies)
        .Sum();

        Console.WriteLine("RESULT:");
        Console.WriteLine(result);
    }

    public void Process(Card card, Dictionary<int, Card> cards)
    {
        foreach (var id in card.CardsWon())
        {
            if (cards.ContainsKey(id))
            {
                ////Console.WriteLine($"Card {card.Id}, score {card.Matches} currently has {card.NumberOfCopies} copies and wins a copy of card {id}");
                cards[id].NumberOfCopies += card.NumberOfCopies;
            }
        }
    }

    public Card ReadInputLineToCard(string inputLine)
    {
        var spl = inputLine.Split(new string[] { "Card ", ": " }, StringSplitOptions.RemoveEmptyEntries);

        return new Card
        {
            Id = int.Parse(spl[0]),
            Matches = GetWinningNumbers(spl[1]).Count(),
            NumberOfCopies = 1
        };
    }

    public int GetScore(string card)
    {
        var n = GetWinningNumbers(card).Count();
        if (n == 0)
        {
            return 0;
        }
        return Maths.IntPower(2, n - 1);
    }

    public string[] GetWinningNumbers(string card)
    {
        var numberSets = card.Split("|")
        .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
        .ToArray();

        return numberSets[0]
        .Intersect(numberSets[1])
        .ToArray();
    }
}

public class Card
{
    public int Id { get; set; }
    public int Matches { get; set; }
    public int NumberOfCopies { get; set; }
    public IEnumerable<int> CardsWon() => Enumerable.Range(Id + 1, Matches);
}

namespace AdventOfCode2023;

public static class Maths
{
    public static int Product(IEnumerable<int> numbers)
    {
        return numbers.Aggregate(1, (acc, val) => acc * val);
    }

    public static int GCD(IEnumerable<int> numbers)
    {
        var ct = numbers.Count();

        if (ct == 0)
        {
            throw new Exception();
        }
        else if (ct == 1)
        {
            return numbers.First();
        }

        var a = numbers.First();
        var b = GCD(numbers.Skip(1));

        return GCD(a, b);
    }

    public static int GCD(int a, int b)
    {
        if (b > a)
        {
            (a, b) = (b, a);
        }

        do
        {
            (a, b) = (b, a % b);
        }
        while (b != 0);

        return a;
    }

        public static long Product(IEnumerable<long> numbers)
    {
        return numbers.Aggregate((long)1, (acc, val) => acc * val);
    }

    public static long GCD(IEnumerable<long> numbers)
    {
        var ct = numbers.Count();

        if (ct == 0)
        {
            throw new Exception();
        }
        else if (ct == 1)
        {
            return numbers.First();
        }

        var a = numbers.First();
        var b = GCD(numbers.Skip(1));

        return GCD(a, b);
    }

    public static long GCD(long a, long b)
    {
        if (b > a)
        {
            (a, b) = (b, a);
        }

        do
        {
            (a, b) = (b, a % b);
        }
        while (b != 0);

        return a;
    }

    public static int GetNumberFromDigits(IEnumerable<int> digits)
    {
        var sum = 0;
        var t = digits.Count() - 1;

        foreach (var d in digits)
        {
            sum += d * IntPower(10, t);
            t = t - 1;
        }
        return sum;
    }

    public static int IntPower(int number, int exponent)
    {
        var result = 1;
        for (int i = 0; i < exponent; i++)
        {
            result *= number;
        }
        return result;
    }
}

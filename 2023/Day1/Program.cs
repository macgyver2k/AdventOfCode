var inputLines = await File.ReadAllLinesAsync("input.txt");

var resultPartOne = inputLines.Sum(line => CalculateLineSumPartOne(line));

int CalculateLineSumPartOne(String line)
{
    var firstChar = line.First(c => Char.IsDigit(c));
    var lastChar = line.Last(c => Char.IsDigit(c));

    var first = Convert.ToInt32(firstChar - 48);
    var last = Convert.ToInt32(lastChar - 48);

    return first * 10 + last;
}

Console.WriteLine("The result is: {0}", resultPartOne);

Dictionary<int, string> numbers = new()
{
    { 0, "zero" },
    { 1, "one" },
    { 2, "two" },
    { 3, "three" },
    { 4, "four" },
    { 5, "five" },
    { 6, "six" },
    { 7, "seven" },
    { 8, "eight" },
    { 9, "nine" },
};

var resultPartTwo = inputLines.Sum(line => CalculateLineSumPartTwo(line));

int CalculateLineSumPartTwo(String line)
{
    var f = numbers.SelectMany(
            pair => new[]
            {
                (value: pair.Key, firstIndex: line.IndexOf(pair.Value), lastIndex: line.LastIndexOf(pair.Value)),
                (value: pair.Key, firstIndex: line.IndexOf(pair.Key.ToString()),
                    lastIndex: line.LastIndexOf(pair.Key.ToString()))
            }
        )
        .ToArray();

    var firstNumber = f.Where(x => x.firstIndex > -1).OrderBy(x => x.firstIndex).First();
    var lastNumber = f.Where(x => x.lastIndex > -1).OrderBy(x => x.lastIndex).Last();

    return firstNumber.value * 10 + lastNumber.value;
}

Console.WriteLine("The result is: {0}", resultPartTwo);

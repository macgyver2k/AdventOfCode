var inputLines = await File.ReadAllLinesAsync("input.txt");

var sum = CalculatePrioritySum(inputLines);
Console.WriteLine($"The sum of priorities is {sum}");

static int CalculatePrioritySum(string[] inputLines)
{
    var sum = 0;

    foreach (var line in inputLines)
    {
        var chars = line.ToArray();
        var mid = chars.Length / 2;
        var left = chars[..mid];
        var right = chars[mid..];

        var common = left.First(right.Contains);
        var priority = common - (Char.IsLower(common) ? 96 : 64 - 26);

        sum += priority;
    }

    return sum;
}

var groupSum = CalculatePrioritySumGrouped(inputLines);
Console.WriteLine($"The sum of grouped priorities is {groupSum}");


static int CalculatePrioritySumGrouped(string[] inputLines)
{
    var sum = 0;
    var buffer = new List<String>();

    foreach (var line in inputLines)
    {
        if( buffer.Count < 3 )
        {
            buffer.Add(line);
        }

        if (buffer.Count < 3)
        {
            continue;
        }

        var common = buffer.Select(_ => _.ToArray()).Aggregate((a, b) => a.Where(b.Contains).ToArray()).First();        
        var priority = common - (Char.IsLower(common) ? 96 : 64 - 26);

        sum += priority;
        buffer.Clear();
    }

    return sum;
}
var inputLines = await File.ReadAllLinesAsync("input.txt");

var sumFull = CalculateFullyOverlappingPairs(inputLines);
Console.WriteLine($"Amount of fully overlapping pairs is {sumFull}");

static int CalculateFullyOverlappingPairs(string[] inputLines)
{
    var sum = 0;

    foreach (var line in inputLines)
    {
        var ranges = line
            .Split(',')
            .Select(x =>
            {
                var y = x
                    .Split('-')
                    .Select(_ => Convert.ToInt32(_))
                    .ToArray();

                return (Left: y[0], Right: y[1]);
            })
            .OrderBy(_ => _.Left)
            .ThenByDescending(_ => _.Right)
            .ToArray();

        var isLeft = ranges[1].Left >= ranges[0].Left;
        var isRight = ranges[1].Right <= ranges[0].Right;

        if (isLeft && isRight)
        {
            sum++;
        }
    }

    return sum;
}

var sumOverlapping = CalculateOverlappingPairs(inputLines);
Console.WriteLine($"Amount of overlapping pairs is {sumOverlapping}");

static int CalculateOverlappingPairs(string[] inputLines)
{
    var sum = 0;

    foreach (var line in inputLines)
    {
        var ranges = line
            .Split(',')
            .Select(x =>
            {
                var y = x
                    .Split('-')
                    .Select(_ => Convert.ToInt32(_))
                    .ToArray();

                return (Left: y[0], Right: y[1]);
            })
            .OrderBy(_ => _.Left)
            .ThenByDescending(_ => _.Right)
            .ToArray();

        var isOverlapping = ranges[1].Left <= ranges[0].Right;        

        if (isOverlapping)
        {
            sum++;
        }
    }

    return sum;
}
var inputLines = await File.ReadAllLinesAsync("input.txt");

var mostCalories = FindMostCalories(inputLines);
Console.WriteLine($"Elve with most calories caries {mostCalories}");

static int FindMostCalories(string[] inputLines)
{
    var sum = 0;
    var max = 0;

    foreach (var line in inputLines)
    {
        if (!String.IsNullOrEmpty(line))
        {
            var number = Convert.ToInt32(line);
            sum += number;
            continue;
        }

        if (sum > max)
        {
            max = sum;
        }

        sum = 0;
    }

    return max;
}

var mostCaloriesTop3 = FindTop3MostCalories(inputLines);
Console.WriteLine($"3 Elves with most calories carry {mostCaloriesTop3}");

static int FindTop3MostCalories(string[] inputLines)
{
    var sum = 0;
    var maxes = new[] { 0, 0, 0 };

    foreach (var line in inputLines)
    {
        if (!String.IsNullOrEmpty(line))
        {
            var number = Convert.ToInt32(line);
            sum += number;
            continue;
        }

        maxes = maxes
            .Concat(new[] { sum })
            .OrderByDescending(_ => _)
            .Take(3)
            .ToArray();        
        
        sum = 0;
    }

    return maxes.Sum();
}
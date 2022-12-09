var inputLines = await File.ReadAllTextAsync("input.txt");

var result = FindMarkerPosition(inputLines, 4);
Console.WriteLine($"Index is {result}");

static int FindMarkerPosition(string inputLines, Int32 length)
{
    var result = 0;

    for (int index = length; index < inputLines.Length; index++)
    {
        var sub = inputLines[(index - length)..index];
        var x = sub.GroupBy(_ => _).Any(_ => _.Count() > 1);

        if (x)
        {
            continue;
        }

        result = index;
        break;
    }

    return result;
}

var resultMessage = FindMarkerPosition(inputLines, 14);
Console.WriteLine($"Message index is {resultMessage}");

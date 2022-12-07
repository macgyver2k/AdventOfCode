var inputLines = await File.ReadAllLinesAsync("input.txt");

var sum = 0;
var max = 0;

foreach (var line in inputLines)
{
    if ( !String.IsNullOrEmpty( line ) )
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

Console.WriteLine( $"Elve with most calories caries {max}" );
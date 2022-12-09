var inputLines = await File.ReadAllLinesAsync("input.txt");

var result = inputLines.Select(line => line.ToArray().Select(_ => (Int32) Char.GetNumericValue( _ )  ).ToArray()).ToArray();

var width = result[0].Length;
var height = inputLines.Length;

var columns = Enumerable.Range(0, width).Select(size => new Int32[height]).ToArray();

for (int indexX = 0; indexX < width; indexX++)
{
    for (int indexY = 0; indexY < height; indexY++)
    {
        columns[indexX][indexY] = result[indexY][indexX];
    }
}

var sum = 0;

for (int indexRow = 0; indexRow < height; indexRow++)
{
    for (int indexColumn = 0; indexColumn < width; indexColumn++)
    {
        var tree = result[indexRow][indexColumn];

        if ( indexColumn == 0 || indexColumn == height-1 || indexRow == 0 || indexRow == width - 1)
        {            
            sum++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(tree);
            continue;
        }

        var left = result[indexRow][..indexColumn];
        var right = result[indexRow][(indexColumn+1)..];

        var leftMax = left.Max();
        var rightMax = right.Max();

        var top = columns[indexColumn][..indexRow];
        var bottom = columns[indexColumn][(indexRow+1)..];

        var topMax = top.Max();
        var bottomMax = bottom.Max();        

        if(new[] { leftMax, rightMax, topMax, bottomMax }.Any( _ => _ < tree) )
        {
            sum++;
            Console.ForegroundColor= ConsoleColor.Green;
            Console.Write(tree);
            continue;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(tree);
    }

    Console.WriteLine();
}

Console.WriteLine($"Total visible trees: {sum}, width: {width}, height: {height}" );
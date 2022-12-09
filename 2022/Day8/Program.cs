using Pastel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

var inputLines = await File.ReadAllLinesAsync("input.txt");

var result = inputLines.Select(line => line.ToArray().Select(_ => (Int32)Char.GetNumericValue(_)).ToArray()).ToArray();

var width = result[0].Length;
var height = inputLines.Length;

var columns = BuildColumns(result, width, height);

var sum = CalculateVisibleTrees(result, width, height, columns);
Console.WriteLine($"Total visible trees: {sum}, width: {width}, height: {height}");


static int[][] BuildColumns(int[][] result, int width, int height)
{
    var columns = Enumerable.Range(0, width).Select(size => new Int32[height]).ToArray();

    for (int indexX = 0; indexX < width; indexX++)
    {
        for (int indexY = 0; indexY < height; indexY++)
        {
            columns[indexX][indexY] = result[indexY][indexX];
        }
    }

    return columns;
}

static int CalculateVisibleTrees(int[][] result, int width, int height, int[][] columns)    
{
    var sum = 0;

    for (int indexRow = 0; indexRow < height; indexRow++)
    {
        for (int indexColumn = 0; indexColumn < width; indexColumn++)
        {
            var tree = result[indexRow][indexColumn];

            if (indexColumn == 0 || indexColumn == height - 1 || indexRow == 0 || indexRow == width - 1)
            {
                sum++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(tree);
                continue;
            }

            var left = result[indexRow][..indexColumn];
            var right = result[indexRow][(indexColumn + 1)..];

            var leftMax = left.Max();
            var rightMax = right.Max();

            var top = columns[indexColumn][..indexRow];
            var bottom = columns[indexColumn][(indexRow + 1)..];

            var topMax = top.Max();
            var bottomMax = bottom.Max();

            if (new[] { leftMax, rightMax, topMax, bottomMax }.Any(_ => _ < tree))
            {
                sum++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(tree);
                continue;
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(tree);
        }

        Console.WriteLine();
    }

    return sum;
}

var maxView = CalculateBestView(result, width, height, columns);
Console.WriteLine($"Best tree view: {maxView}");

static int CalculateBestView(int[][] result, int width, int height, int[][] columns)
{
    var max = 0;

    for (int indexRow = 0; indexRow < height; indexRow++)
    {
        for (int indexColumn = 0; indexColumn < width; indexColumn++)
        {
            var tree = result[indexRow][indexColumn];            

            var left = result[indexRow][..indexColumn];
            var right = result[indexRow][(indexColumn + 1)..];

            var leftMax = TakeWhileInclusive( left.Reverse().ToArray(), tree ).Count();
            var rightMax = TakeWhileInclusive(right,tree).Count();

            var top = columns[indexColumn][..indexRow];
            var bottom = columns[indexColumn][(indexRow + 1)..];

            var topMax = TakeWhileInclusive(top.Reverse().ToArray(),tree).Count();
            var bottomMax = TakeWhileInclusive(bottom,tree).Count();

            var score = leftMax * rightMax * topMax * bottomMax;

            if( score > max )
            {
                max = score;
            }
           
            var s = (Int32)((255.0 / 496650.0) * score);
            Console.Write( "O".Pastel(Color.FromArgb( 0, s, 0)));
        }

        Console.WriteLine();
    }

    return max;
}

static int[] TakeWhileInclusive(int[] ints, int tree)
{
    var result = new List<int>();

    foreach (var item in ints)
    {
        result.Add( item );

        if( item >= tree ) { break; }
    }

    return result.ToArray();
}
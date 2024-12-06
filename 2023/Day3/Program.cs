using System.Linq;
using Pastel;

var inputLines = await File.ReadAllLinesAsync("input.txt");

var symbols = inputLines.SelectMany(line => line.Where(letter => !Char.IsDigit(letter) && letter != '.'))
    .Distinct()
    .ToArray();

Console.WriteLine("Symbols: {0}", String.Join("", symbols.Select(c => new String(c, 1)).ToArray()));

var sum = new List<Int32>();

for (int lineIndex = 0; lineIndex < inputLines.Length; lineIndex++)
{
    var currentLine = inputLines[lineIndex];
    var numberBuffer = new List<Char>();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("{0:D5}# ", lineIndex + 1);
    Console.ForegroundColor = ConsoleColor.DarkGray;

    for (int letterIndex = 0; letterIndex < currentLine.Length; letterIndex++)
    {
        var currentLetter = currentLine[letterIndex];
        var isSymbol = symbols.Contains(currentLetter);
        var isDot = currentLetter == '.';
        var isDigit = Char.IsDigit(currentLetter);

        if (isDigit)
        {
            numberBuffer.Add(currentLetter);
        }

        var bufferContainsDigits = numberBuffer.Count > 0;

        if (bufferContainsDigits && (isSymbol || isDot || letterIndex == currentLine.Length - 1))
        {
            var isSymbolAdjacent = FindIsSymbolAdjacent(
                lineIndex,
                letterIndex - numberBuffer.Count - 1,
                letterIndex + 1
            );

            var number = Convert.ToInt32(new String(numberBuffer.ToArray()));

            Console.ForegroundColor = isSymbolAdjacent ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write(number);

            if (isSymbolAdjacent)
            {
                sum.Add(number);
            }

            numberBuffer.Clear();
        }

        if (isSymbol || isDot)
        {
            Console.ForegroundColor = isSymbol ? ConsoleColor.Yellow : ConsoleColor.DarkGray;

            Console.Write(currentLetter);
        }
    }

    Console.WriteLine();
}

Console.WriteLine("Summe: {0}", sum.Sum());

foreach (var number in sum)
{
    //Console.WriteLine(number);
}


Boolean FindIsSymbolAdjacent(Int32 lineIndex, Int32 startIndex, Int32 endIndex)
{
    for (int index = lineIndex - 1; index < lineIndex + 2; index++)
    {
        if (index < 0 || index > inputLines.Length - 1)
        {
            continue;
        }

        var currentLine = inputLines[index];
        var range = currentLine[new Range(Math.Max(0, startIndex), Math.Min(endIndex, currentLine.Length))];
        var containsSymbol = range.Any(x => symbols.Contains(x));

        if (containsSymbol)
        {
            return true;
        }
    }

    return false;
}

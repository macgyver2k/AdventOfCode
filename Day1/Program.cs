using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync( "input.txt" );

            var numbers = input
                .Select((line, index) => (index,value: Convert.ToInt32(line)))
                .ToList();

            var sums = numbers
                .SelectMany(entry => numbers
                   .Where(second => second.index != entry.index)
                   .Select(second => (sum: second.value + entry.value, left: entry.value, right: second.value))
                ).ToList();

            var pair = sums.First(entry => entry.sum == 2020);
            var result = pair.left * pair.right;

            Console.WriteLine( "Result: {0}", result );
        }
    }
}

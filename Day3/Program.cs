using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var repeat = Enumerable.Range(0, 100).ToList();

            var rows = input
                .Select((line, index) => (index, row: String.Join("", repeat.Select(number => line))));
            
            int hops1 = CalculateHops1(rows);
            Console.WriteLine("Result #1: {0}", hops1);

            int hops = CalculateHops2(rows);
            Console.WriteLine("Result #2: {0}", hops);
        }

        private static int CalculateHops1(IEnumerable<(int index, string row)> rows)
        {
            return rows
                .Skip(1)
                .Aggregate(0, (agg, next) => agg + (next.row[(next.index * 3)] == '#' ? 1 : 0));
        }

        private static int CalculateHops2(IEnumerable<(int index, string row)> rows)
        {
            var slopes = new[] {
                (1,1),
                (3,1),
                (5,1),
                (7,1),
                (1,2)
            };

            return slopes
                .Select(slope => rows
                    .Skip(1)
                    .Where(pair => pair.index % slope.Item2 == 0)
                    .Aggregate(0, (agg, next) => agg + (next.row[(next.index * slope.Item1)] == '#' ? 1 : 0))
                )
                .Aggregate( (agg,next) => agg * next );
        }
    }
}

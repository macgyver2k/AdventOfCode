using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var numbers = input
                .Select((line, index) => (index, value: Convert.ToInt32(line)))
                .ToList();
            
            int result1 = CalculateProduct1(numbers);
            Console.WriteLine("Result #1: {0}", result1);

            int result2 = CalculateProduct2(numbers);
            Console.WriteLine("Result #2: {0}", result2);            
        }

        private static int CalculateProduct1(List<(int index, int value)> numbers)
        {
            var sums = numbers
                .SelectMany(entry => numbers
                    .Where(second => second.index != entry.index)
                    .Select(second => (sum: second.value + entry.value, left: entry.value, right: second.value))
                );

            var pair = sums.First(entry => entry.sum == 2020);
            var result = pair.left * pair.right;
            return result;
        }

        private static int CalculateProduct2(List<(int index, int value)> numbers)
        {
            var sums = numbers
                .SelectMany(entry => numbers
                    .Where(second => second.index != entry.index)
                    .SelectMany( second => numbers
                        .Where(third => second.index != entry.index && second.index != third.index)
                        .Select(third => (
                            sum: second.value + entry.value + third.value,
                            left: entry.value,
                            right: second.value, 
                            third: third.value
                        ))
                    )
                );

            var pair = sums.First(entry => entry.sum == 2020);
            var result = pair.left * pair.right * pair.third;
            return result;
        }
    }
}

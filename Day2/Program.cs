using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var validPasswords = input
                .Where(isPasswordValid)
                .Count();

            Console.WriteLine( "Valid: {0}", validPasswords );
        }

        private static Boolean isPasswordValid(string line)
        {
            var parts = line.Split( 
                new[] { '-', ' ', ':' },
                StringSplitOptions.RemoveEmptyEntries 
            );

            var min = Convert.ToInt32(parts[0]);
            var max = Convert.ToInt32(parts[1]);
            var key = parts[2].First();
            var password = parts[3];

            var amountOfKey = password
                .Where(c => c == key)
                .Count();

            return amountOfKey >= min && amountOfKey <= max;
        }
    }
}

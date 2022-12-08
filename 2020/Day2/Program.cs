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

            var passwords = input
                .Select( SplitPassword )
                .ToList();

            var validPasswordsPart1 = passwords
                .Where(line => isPasswordValidPart1(line.min, line.max, line.key, line.password))
                .Count();

            Console.WriteLine( "Valid #1: {0}", validPasswordsPart1 );

            var validPasswordsPart2 = passwords
                .Where(line => isPasswordValidPart2(line.min, line.max, line.key, line.password))
                .Count();

            Console.WriteLine("Valid #2: {0}", validPasswordsPart2);
        }

        private static Boolean isPasswordValidPart1( Int32 min, Int32 max, Char key, String password)
        {
            var amountOfKey = password
                .Where(c => c == key)
                .Count();

            return amountOfKey >= min && amountOfKey <= max;
        }

        private static Boolean isPasswordValidPart2(Int32 min, Int32 max, Char key, String password)
        {
            return (password[min - 1] == key) ^ (password[max - 1] == key);
        }

        private static (Int32 min, Int32 max, Char key, String password) SplitPassword(string line)
        {
            var parts = line.Split(
                new[] { '-', ' ', ':' },
                StringSplitOptions.RemoveEmptyEntries
            );

            return (
                Convert.ToInt32(parts[0]),
                Convert.ToInt32(parts[1]),
                parts[2].First(),
                parts[3]
            );
        }

    }
}

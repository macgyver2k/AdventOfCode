using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Day4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var indexes = input
                .Select((line, index) => (index, line))
                .Where(pair => pair.line == String.Empty)
                .Select(pair => pair.index)
                .ToList();

            var seed = new List<List<String>>() { new List<string>() };

            var chunks = input.Aggregate(seed, (agg, next) =>
            {
                if (next == String.Empty)
                {
                    agg.Add(new List<string>());
                }
                else
                {
                    agg.Last().Add(next);
                }

                return agg;
            });

            var passports = chunks
                .Select(chunk => chunk
                    .SelectMany(item => item
                        .Split(' ')
                    )
                    .Select(item => item
                        .Split(':')
                    ).ToDictionary(item => item[0], item => item[1])
                ).ToList();

            int validPassports1 = ValidatePassports1(passports);
            Console.WriteLine("Result #1: {0}", validPassports1);

            int validPassports2 = ValidatePassports2(passports);
            Console.WriteLine("Result #2: {0}", validPassports2);
        }

        private static int ValidatePassports1(IEnumerable<Dictionary<string, string>> passports)
        {           
            var mandatory = new[] {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid"
            };

            return passports
                .Where(passport => mandatory.All(passport.ContainsKey))
                .Count();
        }

        private static int ValidatePassports2(IEnumerable<Dictionary<string, string>> passports)
        {
            var mandatory = new Dictionary<String, Func<String, Boolean>> {
                { "byr", (value) => Convert.ToInt32( value ) >= 1920 && Convert.ToInt32( value ) <= 2002  },
                { "iyr", (value) => Convert.ToInt32( value ) >= 2010 && Convert.ToInt32( value ) <= 2020  },
                { "eyr", (value) => Convert.ToInt32( value ) >= 2020 && Convert.ToInt32( value ) <= 2030  },
                { "hgt", (value) => {
                     var ending = value.Substring( value.Length - 2, 2 );
                    var number = Convert.ToInt32( value[ 0..^2] );

                    if( ending == "cm" )
                    {
                        return number >= 150 && number <= 193;
                    }
                    else if( ending == "in" )
                    {
                        return number >= 59 && number <= 76;
                    }

                    return false;
                } },
                { "hcl", (value) => value[0] == '#' && value.Length == 7 && value.Substring( 1 ).All( c => (c >= 'a' && c <= 'f') || (c >= '0' && c <= '9') )  },
                { "ecl", (value) => new []{ "amb", "blu", "brn", "gry", "grn", "hzl", "oth"  }.Contains(value)  },
                { "pid", (value) =>value.Length == 9 && Int32.TryParse( value, out _ ) }
            };


            return passports
                .Where(passport => mandatory.Keys.All(passport.ContainsKey) && mandatory.All(m => m.Value(passport[m.Key])))
                .Count();
        }
    }
}

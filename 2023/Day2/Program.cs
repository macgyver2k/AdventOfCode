var inputLines = await File.ReadAllLinesAsync("input.txt");

Dictionary<String, Int32> target = new() { { "red", 12 }, { "green", 13 }, { "blue", 14 } };

var games = inputLines.Select(SplitLine).ToArray();

var validGamesFirstStep = games
    .Where(game => game.roundsCubes.All(cubes => cubes.All(cube => cube.cubeCount <= target[cube.cubeType])))
    .ToArray();

var resultFirstStep = validGamesFirstStep.Sum(game => game.gameNumber);

Console.WriteLine("The amount of valid games is {0}", resultFirstStep);

( Int32 gameNumber, (Int32 cubeCount, String cubeType)[][] roundsCubes) SplitLine(String line)
{
    var gameParts = line.Split(':');
    var game = gameParts[0];
    var gameNumber = Convert.ToInt32(game.Replace("Game", "").Trim());

    var rounds = gameParts[1].Split(';');

    var roundsCubes = rounds.Select(
            round => round.Split(',')
                .Select(
                    x =>
                    {
                        var cubeParts = x.Trim().Split(' ');
                        var cubeCount = Convert.ToInt32(cubeParts[0]);
                        var cubeType = cubeParts[1];

                        return (cubeCount, cubeType);
                    }
                )
                .ToArray()
        )
        .ToArray();

    return (gameNumber, roundsCubes);
}

var resultSecondStep = games.Select(
        game => game.roundsCubes.SelectMany(cubes => cubes)
            .GroupBy(cubes => cubes.cubeType)
            .Select(group => group.MaxBy(cubeGroup => cubeGroup.cubeCount).cubeCount)
            .Aggregate((x, y) => x * y)
    )
    .Sum();

Console.WriteLine("The sum of powers is {0}", resultSecondStep);

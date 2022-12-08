var inputLines = await File.ReadAllLinesAsync("input.txt");

var sum = CalculateTotalScore(inputLines);
Console.WriteLine($"Total score is {sum}");

var sumCorrected = CalculateTotalScoreCorrected(inputLines);
Console.WriteLine($"Total corrected score is {sumCorrected}");


Int32 GetShapeScore(Char me) => me switch
{
    'X' => 1,
    'Y' => 2,
    'Z' => 3,
};

Int32 GetOutcomeScore(Char opponent, Char me)
{
    return me switch
    {
        'X' => opponent switch
        {
            'A' => 3,
            'B' => 0,
            'C' => 6,
        },
        'Y' => opponent switch
        {
            'A' => 6,
            'B' => 3,
            'C' => 0,
        },
        'Z' => opponent switch
        {
            'A' => 0,
            'B' => 6,
            'C' => 3,
        },
    };
}

int CalculateTotalScore(string[] inputLines)
{
    var sum = 0;

    foreach (var line in inputLines)
    {
        var opponent = line[0];
        var me = line[2];

        var myScore = GetShapeScore(me);
        var outcomeScore = GetOutcomeScore(opponent, me);

        sum += (myScore + outcomeScore);
    }

    return sum;
}

int CalculateTotalScoreCorrected(string[] inputLines)
{
    var sum = 0;

    foreach (var line in inputLines)
    {
        var opponent = line[0];
        var me = line[2];

        var myPredictedShape = GetPredictedShape( opponent,me );

        var myScore = GetShapeScore(myPredictedShape);
        var outcomeScore = GetOutcomeScore(opponent, myPredictedShape);

        sum += (myScore + outcomeScore);
    }

    return sum;
}

Char GetPredictedShape(Char opponent, Char me)
{
    return me switch
    {
        'X' => opponent switch
        {
            'A' => 'Z',
            'B' => 'X',
            'C' => 'Y',
        },
        'Y' => opponent switch
        {
            'A' => 'X',
            'B' => 'Y',
            'C' => 'Z',
        },
        'Z' => opponent switch
        {
            'A' => 'Y',
            'B' => 'Z',
            'C' => 'X',
        },
    };
}
var inputLines = await File.ReadAllLinesAsync("input.txt");

var stacks = Enumerable
	.Range(0, 9)
	.Select(_ => new List<String>())
	.ToArray();

var isMoving = false;

foreach (var line in inputLines)
{
    if (String.IsNullOrEmpty(line) || line.StartsWith( " 1 " ))
    {
        isMoving = true;
        continue;
    }

    if (!isMoving)
	{
		for (int index = 0; index < 9; index++)
		{
			var crate = line.Substring(index * 4, 3).Trim('[', ']');

			if (String.IsNullOrEmpty(crate.Trim())) { continue; }

			stacks[index].Insert(0, crate);			
		}

        continue;
    }

	

	var parts = line.Split(' ');
	var amount = Convert.ToInt32(parts[1]);
	var from = Convert.ToInt32(parts[3]) -1;
	var to = Convert.ToInt32(parts[5]) -1;

	for (int moveIndex = 0; moveIndex < amount; moveIndex++)
	{
		var item = stacks[from].Last();
		stacks[from].RemoveAt(stacks[from].Count-1);

		stacks[to].Add(item);
    }
}

var result = String.Join("", stacks.Select(_ => _.Last()));
Console.WriteLine($"Crates on top: {result}");
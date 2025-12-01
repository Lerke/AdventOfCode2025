var moves = File.ReadAllLines(args[0])
	.Where(x => !string.IsNullOrWhiteSpace(x))
	.Select(x => x.Replace("L", "-"))
	.Select(x => x.Replace("R", "+"))
	.Select(int.Parse)
	.ToList();

int current = 50;
int numberTimesLandedZero = 0;
int numberTimesHitZero = 0;
foreach (var move in moves)
{
	foreach (var _ in Enumerable.Range(0, Math.Abs(move)))
	{
		current += (move < 0) ? -1 : 1;
		current = (current < 0 ? (100 - Math.Abs(current)) : current) % 100;
		numberTimesHitZero += current == 0 ? 1 : 0;
	}

	numberTimesLandedZero += current == 0 ? 1 : 0;
}

Console.WriteLine($"[*] Part 1: {numberTimesLandedZero}");
Console.WriteLine($"[**] Part 2: {numberTimesHitZero}");
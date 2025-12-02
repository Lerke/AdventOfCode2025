var input = File.ReadAllText(args[0])
    .Split(",")
    .Select(s => s.Split("-").Select(long.Parse).Chunk(2).First())
    .Select(x => (x[0], x[1]));

IEnumerable<long> Range(long s, long e)
{
    for (long i = s; i <= e; i++)
    {
        yield return i;
    }
}

var invalidIdsPerRange = input
    .Select(x => (x, Range(x.Item1, x.Item2)))
    .Select(range => (range.x, range.Item2.Select(x => x.ToString())
        .Where(x => x.Length % 2 == 0)
        .Select(x => (string[])[x[..(x.Length / 2)], x[(x.Length / 2)..]])
        .Where(x => x[0] == x[1])));

var sumOfInvalidIds = invalidIdsPerRange
    .Where(x => x.Item2.Any())
    .SelectMany(x => x.Item2.Select(z => string.Join("", z)))
    .Select(long.Parse)
    .Sum();

var allCombinations = (string s) => Enumerable.Range(1, s.Length)
    .Select(s.Chunk)
    .Where(x => x.All(y => y.Length == x.First().Length))
    .Where(x => x.Count() > 1);

var invalidIdsPerRangePartTwo = input
    .Select(x => (x, Range(x.Item1, x.Item2)))
    .Select(range => (range.x, range.Item2.Select(x => x.ToString())
        .SelectMany(allCombinations)
        .Where(x => x.All(y => y.SequenceEqual(x.First())))));

var sumOfInvalidIdsPartTwo = invalidIdsPerRangePartTwo
    .Where(x => x.Item2.Any())
    .SelectMany(x => x.Item2)
    .Select(x => string.Join("", x.Select(y => string.Join("", y))))
    .Distinct()
    .Select(long.Parse)
    .Sum();

Console.WriteLine($"[*] Part 1: {sumOfInvalidIds}");
Console.WriteLine($"[**] Part 2: {sumOfInvalidIdsPartTwo}");
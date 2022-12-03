var numElves = args.Any() && int.TryParse(args[0], out var value) ? value : 1;

var entries = File.ReadAllLines("input1.txt");
var chunkedEntries = ChunkEntries(entries);
var sums = chunkedEntries.Select(chunk => chunk.Sum(calories => Convert.ToInt32(calories)));
var max = sums.OrderDescending().Take(numElves).Sum();
Console.WriteLine(max);

IEnumerable<IEnumerable<string>> ChunkEntries(IEnumerable<string> entries)
{
    var enumerator = entries.GetEnumerator();
    while (enumerator.MoveNext())
        yield return CreateChunk(enumerator);
}

IEnumerable<string> CreateChunk(IEnumerator<string> enumerator)
{
    do
    {
        if (string.IsNullOrEmpty(enumerator.Current))
            yield break;
        yield return enumerator.Current;
    }
    while (enumerator.MoveNext());
}
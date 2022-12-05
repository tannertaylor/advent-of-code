var totalPriority = 0;
var lines = File.ReadAllLines("input.txt");

if (!args.Any() || args[0] == "part1")
{
    foreach (var line in lines)
    {
        var compartment1 = line.Take(line.Length / 2);
        var compartment2 = line.Skip(line.Length / 2);
        totalPriority += GetCommonItemPriority(compartment1, compartment2);
    }
}
else if (args.Any() && args[0] == "part2")
{
    foreach (var chunk in lines.Chunk(3))
        totalPriority += GetCommonItemPriority(chunk);
}

Console.WriteLine(totalPriority);

int GetCommonItemPriority(params IEnumerable<char>[] sets)
{
    var commonItem = sets.Aggregate((first, second) => first.Intersect(second)).Single();
    return (int)commonItem - (char.IsLower(commonItem) ?  96 : 38);
}
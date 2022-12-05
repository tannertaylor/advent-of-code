var moveInUnison = args.Any() && args[0] == "part2";

var lines = File.ReadAllLines("input.txt");

var crates = new List<Stack<char>>();
for (var i = 0; i < lines.First().Chunk(4).Count(); i++)
    crates.Add(new Stack<char>());

foreach (var line in lines)
{
    if (line.StartsWith(' '))
        break;

    var stackNum = 0;
    foreach (var crate in line.Chunk(4))
    {
        stackNum++;
        if (!string.IsNullOrWhiteSpace(new String(crate)))
            crates[stackNum - 1].Push(crate[1]);
    }
}

for (var i = 0; i < crates.Count; i++)
    crates[i] = new Stack<char>(crates[i]); // reapplying the stack happens from first to last, so this effectively reverses the stack.

foreach (var operation in lines.Skip(crates.Count + 1))
{
    var pieces = operation.Split(' ');
    var count = Convert.ToInt32(pieces[1]);
    var fromStack = Convert.ToInt32(pieces[3]);
    var toStack = Convert.ToInt32(pieces[5]);

    if (moveInUnison)
    {
        var cratesToMove = new List<char>();
        for (var i = 0; i < count; i++)
            cratesToMove.Add(crates[fromStack - 1].Pop());

        cratesToMove.Reverse();
        foreach (var crate in cratesToMove)
            crates[toStack - 1].Push(crate);
    }
    else
    {
        for (var i = 0; i < count; i++)
            crates[toStack - 1].Push(crates[fromStack - 1].Pop());
    }
}

Console.WriteLine(new String(crates.Where(x => x.Any()).Select(x => x.Peek()).ToArray()));
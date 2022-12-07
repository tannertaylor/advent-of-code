var allEntries = new List<DirectoryEntry>();
var currentPath = new LinkedList<DirectoryEntry>();
var runningListing = false;

foreach (var line in File.ReadAllLines("input.txt"))
{
    if (line.StartsWith("$"))
    {
        runningListing = false;
        ProcessCommand(new String(line.Skip(2).ToArray()));
    }
    else if (runningListing)
    {
        var entryItems = line.Split(' ');
        if (int.TryParse(entryItems[0], out var size))
            foreach (var entry in currentPath)
                entry.Size += size;
    }
}

if (args.Any() && args[0] == "part2")
{
    const int totalSpace = 70_000_000, unusedSpaceNeeded = 30_000_000;
    var currentUnusedSpace = totalSpace - allEntries.First().Size;
    Console.WriteLine(allEntries.Where(x => x.Size >= unusedSpaceNeeded - currentUnusedSpace).Min(x => x.Size));
}
else
    Console.WriteLine(allEntries.Where(x => x.Size <= 100000).Sum(x => x.Size));

void ProcessCommand(string command)
{
    if (command.StartsWith("cd"))
    {
        var targetDir = command.Split(' ')[1];
        if (targetDir == "..")
            currentPath.RemoveLast();
        else
        {
            var entry = new DirectoryEntry(targetDir);
            allEntries.Add(entry);
            currentPath.AddLast(new LinkedListNode<DirectoryEntry>(entry));
        }
    }
    else if (command == "ls")
        runningListing = true;
}

record DirectoryEntry(string Name) { public int Size { get; set; } }
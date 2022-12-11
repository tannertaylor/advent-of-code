IEnumerable<int> RunCpuCycles()
{
    var register = 1;
    foreach (var instruction in File.ReadAllLines("input.txt").Select(x => x.Split(' ')))
    {
        if (instruction[0] == "noop")
        {
            yield return register;
            continue;
        }
        else if (instruction[0] == "addx")
        {
            yield return register;
            yield return register;
            register += int.Parse(instruction[1]);
        }
    }
}

var cyclesToCheck = new[] { 20, 60, 100, 140, 180, 220 };
var signalStrengths = new List<int>();
var crt = new char[240];

var cycle = 1;
foreach (var register in RunCpuCycles())
{
    if (cyclesToCheck.Contains(cycle))
        signalStrengths.Add(register * cycle);
    crt[cycle - 1] = Enumerable.Range(register, 3).Contains(cycle % 40) ? '#' : ' ';
    cycle++;
}

Console.WriteLine($"Part 1: {signalStrengths.Sum()}");
Console.WriteLine();
Console.WriteLine("Part 2");
Console.Write(string.Join('\n', crt.Chunk(40).Select(x => new String(x))));
Console.WriteLine();
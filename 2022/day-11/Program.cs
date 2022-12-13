using Day11;

var lines = File.ReadAllLines("input.txt");
Monkey[] monkeys;

ulong CalculateMonkeyBusiness(int numRounds, Func<ulong, ulong> reliefFunction)
{
    for (var i = 0; i < numRounds; i++)
    {
        foreach (var monkey in monkeys)
        {
            foreach (var itemWorryLevel in monkey.ItemWorryLevels)
            {
                var newItemWorryLevel = reliefFunction.Invoke(monkey.RunOperation(itemWorryLevel));
                monkeys[monkey.GetTargetMonkey(newItemWorryLevel)].ItemWorryLevels.Add(newItemWorryLevel);
                monkey.InspectionCount++;
            }

            monkey.ItemWorryLevels.Clear();
        }
    }

    var topTwo = monkeys!.Select(x => x.InspectionCount).OrderByDescending(x => x).Take(2).ToArray();
    return (ulong)topTwo[0] * (ulong)topTwo[1];
}

void SetMonkeys() => monkeys = lines!.Where(x => !string.IsNullOrEmpty(x)).Chunk(6).Select(chunk => new Monkey(chunk)).ToArray();

SetMonkeys();
Console.WriteLine($"Part 1: {CalculateMonkeyBusiness(20, x => x / 3)}");

SetMonkeys();
var reliefFactor = monkeys.Select(x => x.Divisor).Aggregate(1, (first, second) => first * second);
Console.WriteLine($"Part 2: {CalculateMonkeyBusiness(10_000, x => x % (ulong)reliefFactor)}");
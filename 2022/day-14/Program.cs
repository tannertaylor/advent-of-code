using Day14;

const char rock = '#', sand = 'o';
const int startingX = 500;

static Dictionary<Coord, char> GetEmptyCave()
{
    var cave = new Dictionary<Coord, char> { [(startingX, 0)] = '+' };
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var coordStrs = line.Split(" -> ");
        for (var i = 0; i < coordStrs.Length - 1; i++)
            foreach (var coord in Coord.FromString(coordStrs[i]).PathTo(Coord.FromString(coordStrs[i + 1])))
                cave[coord] = rock;
    }
    return cave;
}

static Dictionary<Coord, char> GetSandFilledCave(Dictionary<Coord, char> emptyCave)
{
    var cave = new Dictionary<Coord, char>(emptyCave);    

    var maxY = cave.Keys.Select(x => x.Y).Max();
    while (true)
    {
        var previousCoord = new Coord(startingX, 0);
        while (previousCoord.Y < maxY && TryDropSand(cave, previousCoord, out var newCoord))
            previousCoord = newCoord;
    
        if (previousCoord.Y == maxY)
            break;

        cave[previousCoord] = sand;
        if (previousCoord == (startingX, 0))
            break;
    }

    return cave;
}

static bool TryDropSand(Dictionary<Coord, char> cave, Coord currentCoord, out Coord newCoord)
{
    var vector = new[] { (0, 1), (-1, 1), (1, 1) }.FirstOrDefault(x => !cave.ContainsKey(currentCoord + x));
    newCoord = currentCoord + vector;
    return newCoord != currentCoord;
}

static Dictionary<Coord, char> AddFloor(Dictionary<Coord, char> cave)
{
    var allXs = cave.Keys.Select(x => x.X);
    var maxY = cave.Keys.Select(x => x.Y).Max();

    var caveWithFloor = new Dictionary<Coord, char>(cave);
    for (var i = allXs.Min() - 1000; i <= allXs.Max() + 1000; i++)
        caveWithFloor[(i, maxY + 2)] = rock;
    return caveWithFloor;
}

var emptyCave = GetEmptyCave();

var part1Cave = GetSandFilledCave(emptyCave);
Console.WriteLine($"Part 1: {part1Cave.Values.Count(x => x == sand)}"); // 728

var part2Cave = GetSandFilledCave(AddFloor(emptyCave));
Console.WriteLine($"Part 2: {part2Cave.Values.Count(x => x == sand)}"); // 27623
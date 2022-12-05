var count = 0;

foreach (var line in File.ReadAllLines("input.txt"))
{
    var ranges = line.Split(',').Select(x => x.Split('-')).Select(x => new
    {
        Start = Convert.ToInt32(x[0]),
        End = Convert.ToInt32(x[1])
    }).ToArray();
    
    if (
        (!args.Any() || args[0] == "part1") &&
        (ranges[0].Start >= ranges[1].Start && ranges[0].End <= ranges[1].End ||
        ranges[1].Start >= ranges[0].Start && ranges[1].End <= ranges[0].End)
    ) count++;

    else if (
        args.Any() && args[0] == "part2" &&
        (ranges[0].Start.IsInRange(ranges[1].Start, ranges[1].End) ||
        ranges[0].End.IsInRange(ranges[1].Start, ranges[1].End) ||
        ranges[1].Start.IsInRange(ranges[0].Start, ranges[0].End) ||
        ranges[1].End.IsInRange(ranges[0].Start, ranges[0].End))
    ) count++;
    
}

Console.WriteLine(count);

static class Extensions
{
    public static bool IsInRange(this int value, int min, int max) => value >= min && value <= max;
}
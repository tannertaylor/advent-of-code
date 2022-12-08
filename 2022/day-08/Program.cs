var treeGrid = File.ReadAllLines("input.txt").Select(row => row.Select(tree => (short)char.GetNumericValue(tree)).ToArray()).ToArray();
var numRows = treeGrid.Length;
var numColumns = treeGrid.First().Length;

if (args.Any() && args[0] == "part2")
{
    var highestScenicScore = 0;
    foreach (var tree in GetInnerTrees())
    {
        var viewingDistances = tree.LinesOfSight.Select(line => line.TakeWhileInclusive(height => height < tree.Height).Count());
        var scenicScore = viewingDistances.Aggregate((first, second) => first * second);
        highestScenicScore = Math.Max(highestScenicScore, scenicScore);
    }

    Console.WriteLine(highestScenicScore);
}
else
{
    var numVisible = (numRows + numColumns) * 2 - 4;
    foreach (var tree in GetInnerTrees())
        if (tree.LinesOfSight.Any(line => line.All(height => height < tree.Height)))
            numVisible++;
    
    Console.WriteLine(numVisible);
}

IEnumerable<Tree> GetInnerTrees()
{
    for (var i = 1; i < numRows - 1; i++)
    {
        for (var j = 1; j < numColumns - 1; j++)
        {
            var treeHeight = treeGrid![i][j];
            var linesOfSight = new[]
            {
                treeGrid[i].Skip(j + 1),
                treeGrid[i].Take(j).Reverse(),
                treeGrid.Select(row => row[j]).Take(i).Reverse(),
                treeGrid.Select(row => row[j]).Skip(i + 1)
            };

            yield return new Tree(treeHeight, linesOfSight);
        }
    }
}

static class Extensions
{
    // equivalent to Enumerable.TakeWhile, but includes the element that fails the predicate.
    public static IEnumerable<T> TakeWhileInclusive<T>(this IEnumerable<T> source, Predicate<T> predicate)
    {
        foreach (var item in source)
        {
            yield return item;
            if (!predicate.Invoke(item))
                yield break;
        }
    }
}

record Tree(short Height, IEnumerable<short>[] LinesOfSight);
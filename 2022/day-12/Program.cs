using Day12;

// Local function to return all reachable neighbors of a node.
static IEnumerable<Node> GetNeighbors(List<Node> allNodes, Node currentNode, Func<Node, Node, bool> reachStepCriteria)
{
    foreach (var (rowDelta, columnDelta) in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
    {
        var row = currentNode.Row + rowDelta;
        var column = currentNode.Column + columnDelta;
        var matchingNode = allNodes.FirstOrDefault(x => x.Row == row && x.Column == column);
        if (matchingNode != null && !matchingNode.Visited && reachStepCriteria.Invoke(currentNode, matchingNode))
            yield return matchingNode;
    }
}

// Local function to find shortest distance (similar, but not equivalent, to Dijkstra's algorithm)
static void CalculateDistances(List<Node> allNodes, Node startNode, Func<bool> loopCriteria, Func<Node, Node, bool> reachStepCriteria)
{
    var currentNode = startNode;
    while (loopCriteria.Invoke())
    {
        if (currentNode == null) // shouldn't be possible based on loopCriteria, but just in case
            break;

        foreach (var neighbor in GetNeighbors(allNodes, currentNode, reachStepCriteria))
            neighbor.Distance = currentNode.Distance + 1;
        currentNode.Visited = true;   
        
        currentNode = allNodes.Where(x => !x.Visited && x.Distance != int.MaxValue).OrderBy(x => x.Distance).FirstOrDefault();
    }
}

var (nodes, start, destination) = Parser.Parse();
CalculateDistances(nodes, start, () => !destination.Visited, (current, neighbor) => neighbor.Elevation <= current.Elevation + 1);
Console.WriteLine($"Part 1: {destination.Distance}");

// reset nodes for part 2
foreach (var node in nodes)
    node.Reset();
destination.Distance = 0;

// starting from the end this time, find the lowest point on the map with the shortest distance to the destination
CalculateDistances(nodes, destination, () => nodes.Where(x => x.Elevation == 1).Any(x => !x.Visited), (current, neighbor) => neighbor.Elevation >= current.Elevation - 1);
var shortestDistanceToLowPoint = nodes.Where(x => x.Elevation == 1).OrderBy(x => x.Distance).First().Distance;
Console.WriteLine($"Part 2: {shortestDistanceToLowPoint}");
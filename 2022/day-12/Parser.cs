namespace Day12;

public static class Parser
{
    public static (List<Node> Nodes, Node StartNode, Node DestinationNode) Parse()
    {
        var lines = File.ReadAllLines("input.txt");
        var nodes = new List<Node>();

        Node startNode = null!, destinationNode = null!;

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var node = new Node(i, j, lines[i][j] switch { 'S' => 1, 'E' => 26, _ => lines[i][j] - 'a' + 1 });
                nodes.Add(node);

                if (lines[i][j] == 'S')
                {
                    startNode = node;
                    startNode.Distance = 0;
                }
                else if (lines[i][j] == 'E')
                    destinationNode = node;
            }
        }

        return (nodes, startNode, destinationNode);
    }
}
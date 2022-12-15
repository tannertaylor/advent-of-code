namespace Day12;

public record Node(int Row, int Column, int Elevation)
{
    public int Distance { get; set; } = int.MaxValue;

    public bool Visited { get; set; }

    public void Reset()
    {
        Distance = int.MaxValue;
        Visited = false;
    }
}
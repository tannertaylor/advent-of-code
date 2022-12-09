var movements = File.ReadAllLines("input.txt").Select(x => x.Split(' ')).Select(x => (x[0][0], int.Parse(x[1])));
Console.WriteLine($"Part 1: {MoveRope(2)}");
Console.WriteLine($"Part 2: {MoveRope(10)}");

int MoveRope(int knotCount)
{
    var rope = new List<Position>(Enumerable.Range(0, knotCount).Select(_ => new Position { X = 0, Y = 0 }));
    var visitedByTail = new HashSet<Position> { new Position { X = 0, Y = 0 } };

    foreach (var (dir, steps) in movements!)
    {
        for (var i = 0; i < steps; i++)
        {
            rope[0].X += dir switch { 'R' => 1, 'L' => -1, _ => 0 };
            rope[0].Y += dir switch { 'U' => 1, 'D' => -1, _ => 0 };
            DragRopeBehindHead(rope, visitedByTail, dir);
        }
    }

    return visitedByTail.Count();
}

void DragRopeBehindHead(List<Position> rope, HashSet<Position> visitedByTail, char dir)
{
    var previousKnot = rope[0];
    foreach (var knot in rope.Skip(1))
    {
        var (deltaX, signX) = (Math.Abs(previousKnot.X - knot.X), Math.Sign(previousKnot.X - knot.X));
        var (deltaY, signY) = (Math.Abs(previousKnot.Y - knot.Y), Math.Sign(previousKnot.Y - knot.Y));
        if (deltaX <= 1 && deltaY <= 1) // adjacent or covering
            return;

        knot.X += signX;
        knot.Y += signY;
        previousKnot = knot;
    }

    visitedByTail.Add(rope.Last() with { });
}

record Position()
{
    public required int X { get; set; }
    public required int Y { get; set; }
};
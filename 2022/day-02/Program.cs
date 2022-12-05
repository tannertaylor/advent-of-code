var lines = File.ReadAllLines("input.txt");

var xyzIsOutcome = args.Any() && args[0] == "outcome";

int score = 0;
foreach (var (other, me) in GetMoves())
{
    score += me switch { Move.Rock => 1, Move.Paper => 2, Move.Scissors => 3, _ => throw new Exception() };
    if (other == me)
        score += 3; // tie
    else if (me == GetWinningMove(other))
        score += 6; // win
}

Console.WriteLine(score);

IEnumerable<(Move Other, Move Me)> GetMoves()
{
    foreach (var (other, me) in lines.Select(x => x.Split(' ')).Select(x => (x[0], x[1])))
    {
        var otherMove = other switch { "A" => Move.Rock, "B" => Move.Paper, "C" => Move.Scissors, _ => throw new Exception() };

        var myMove = xyzIsOutcome ?
            me switch
            {
                "X" => GetLosingMove(otherMove),
                "Y" => otherMove, // force tie
                "Z" => GetWinningMove(otherMove),
                _ => throw new Exception()
            } :
            me switch { "X" => Move.Rock, "Y" => Move.Paper, "Z" => Move.Scissors, _ => throw new Exception() };

        yield return (otherMove, myMove);
    }
}

Move GetWinningMove(Move otherMove) => GetAllMoves().SkipWhile(x => x != otherMove).Skip(1).First();
Move GetLosingMove(Move otherMove) => GetAllMoves().Reverse().SkipWhile(x => x != otherMove).Skip(1).First();
IEnumerable<Move> GetAllMoves() => Enum.GetValues<Move>().Concat(new[] { Move.Rock });

enum Move { Rock, Paper, Scissors };
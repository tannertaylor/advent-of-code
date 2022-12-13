namespace Day11;

public class Monkey
{
    private readonly int _monkeyIfTrue, _monkeyIfFalse;
    private readonly Func<ulong, ulong> _operation;

    public List<ulong> ItemWorryLevels { get; }

    public int InspectionCount { get; set; }

    public int Divisor { get; }

    public Monkey(string[] chunk)
    {
        ItemWorryLevels = chunk[1].Trim().Replace("Starting items: ", "").Split(", ").Select(x => ulong.Parse(x)).ToList();
        Divisor = int.Parse(chunk[3].Split(" ").Last());

        _monkeyIfTrue = int.Parse(chunk[4].Split(" ").Last());
        _monkeyIfFalse = int.Parse(chunk[5].Split(" ").Last());

        var splitOperation = chunk[2].Trim().Replace("Operation: new = ", "").Split(" ");
        _operation = old =>
        {
            ulong GetOperand(string value) => value == "old" ? old : ulong.Parse(value);
            var operand1 = GetOperand(splitOperation[0]);
            var operand2 = GetOperand(splitOperation[2]);
            return splitOperation[1] switch
            {
                "+" => operand1 + operand2,
                "*" => operand1 * operand2,
                _ => throw new Exception()
            };
        };
    }

    public ulong RunOperation(ulong oldWorryLevel)
    {
        return _operation.Invoke(oldWorryLevel);
    }

    public int GetTargetMonkey(ulong newWorryLevel)
    {
        return newWorryLevel % (ulong)Divisor == 0 ? _monkeyIfTrue : _monkeyIfFalse;
    }
}
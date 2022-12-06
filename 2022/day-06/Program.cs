var numDistinctChars = args.Any() && args[0] == "part2" ? 14 : 4;

var chars = File.ReadAllLines("input.txt").Single().ToCharArray();

var last4Chars = new Queue<char>(chars.Take(numDistinctChars));
if (TryPrintMarker(numDistinctChars - 1))
    return;

for (var i = numDistinctChars; i < chars.Length; i++)
{
    last4Chars.Dequeue();
    last4Chars.Enqueue(chars[i]);
    if (TryPrintMarker(i))
        return;
}

bool TryPrintMarker(int index)
{
    if (last4Chars!.Distinct().Count() == numDistinctChars)
    {
        Console.WriteLine(index + 1);
        return true;
    }

    return false;
}
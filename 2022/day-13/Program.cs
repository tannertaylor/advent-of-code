using System.Text.Json;
using System.Text.Json.Nodes;

var lines = File.ReadAllLines("input.txt").Where(x => !string.IsNullOrEmpty(x));

// Returns 1 if the items are ordered correctly, -1 if they aren't, and 0 if it couldn't be determined.
static int Compare(JsonNode left, JsonNode right)
{
    if (left is JsonValue && right is JsonValue)
        return left.GetValue<int>() < right.GetValue<int>() ? 1 : left.GetValue<int>() > right.GetValue<int>() ? -1 : 0;
    
    var leftArray = left is JsonArray x ? x : new JsonArray(JsonValue.Create(left.GetValue<int>()));
    var rightArray = right is JsonArray y ? y : new JsonArray(JsonValue.Create(right.GetValue<int>()));

    int i;
    for (i = 0; i < leftArray.Count && i < rightArray.Count; i++)
    {
        var comparison = Compare(leftArray[i]!, rightArray[i]!);
        if (comparison != 0)
            return comparison;
    }
    
    return leftArray.Count == rightArray.Count ? 0 : i == leftArray.Count ? 1 : i == rightArray.Count ? -1 : 0;
}

var correctlyOrderedIndexSum = lines.Chunk(2).Select((pair, index) => new
{
    Index = index + 1,
    IsCorrect = Compare(
        JsonArray.Create(JsonDocument.Parse(pair[0]).RootElement)!,
        JsonArray.Create(JsonDocument.Parse(pair[1]).RootElement)!
    ) == 1
}).Where(x => x.IsCorrect).Sum(x => x.Index);

Console.WriteLine($"Part 1: {correctlyOrderedIndexSum}"); // 4734

var orderedPackets = lines.Concat(new[] { "[[2]]", "[[6]]" }).OrderDescending(Comparer<string>.Create((left, right) => Compare(
    JsonArray.Create(JsonDocument.Parse(left).RootElement)!,
    JsonArray.Create(JsonDocument.Parse(right).RootElement)!
))).ToList();
var decoderKey = (orderedPackets.IndexOf("[[2]]") + 1) * (orderedPackets.IndexOf("[[6]]") + 1);
Console.WriteLine($"Part 2: {decoderKey}"); // 21836
using AoC2025.Utils;

namespace AoC2025.Day5
{
    internal static class Day5
    {
        internal static class Part1
        {
            public static void Solve()
            {
                var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day5.txt";
                var database = InputDataParser.ParseInputTxtAsSingleString(dataTxtLocation);
                //var database = "3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32";

                var sections = database.Trim().Split(Environment.NewLine + string.Empty + Environment.NewLine);

                var ranges = sections[0].Split("\r\n");
                var ingredientIds = sections[1].Split("\r\n");

                var freshIngredientIdsCount = 0;

                for (int i = 0; i < ingredientIds.Length; i++)
                {
                    var ingredientId = ulong.Parse(ingredientIds[i]);

                    for (int j = 0; j < ranges.Length; j++)
                    {
                        var range = ConvertToUlongRange(ranges[j]);

                        if (range.RangeStart <= ingredientId &&
                            range.RangeEnd >= ingredientId)
                        {
                            freshIngredientIdsCount++;
                            break;
                        }
                    }
                }

                Console.WriteLine($"{freshIngredientIdsCount} ingredient IDs are fresh.");
            }
        }

        internal static class Part2
        {
            public static void Solve()
            {
                var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day5.txt";
                var database = InputDataParser.ParseInputTxtAsSingleString(dataTxtLocation);
                //var database = "3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32";

                var sections = database.Trim().Split(Environment.NewLine + string.Empty + Environment.NewLine);
                var ranges = sections[0].Split("\r\n").Select(ConvertToUlongRange).ToList();
                
                // sort by start value
                ranges.Sort((first, second) =>
                {
                    if (first.RangeStart < second.RangeStart)
                    {
                        return -1;
                    }
                    else if (first.RangeStart == second.RangeStart)
                    {
                        return 0;
                    }
                    return 1;
                });

                ulong freshIngredientIdsCount = 0;

                // initialize with the first range's start and end
                var currentRangeStart = ranges[0].RangeStart;
                var currentRangeEnd = ranges[0].RangeEnd;

                // skip first as it was already stored for current
                foreach (var range in ranges.Skip(1))
                {
                    // we can skip ranges which are part of current range
                    if (range.RangeEnd <= currentRangeEnd)
                    {
                        continue;
                    }

                    // if upcoming range intersects with the current range
                    if (range.RangeStart <= currentRangeEnd)
                    {
                        currentRangeEnd = range.RangeEnd;
                        continue;
                    }

                    // if upcoming range starts above current range
                    if (range.RangeStart > currentRangeStart)
                    {
                        // +1 is needed because in case of inclusive ranges that's how the number
                        // of contained elements is calculated
                        freshIngredientIdsCount += currentRangeEnd - currentRangeStart + 1;
                        
                        currentRangeStart = range.RangeStart;
                        currentRangeEnd = range.RangeEnd;
                    }
                }

                // need a calculation with the final range as well
                freshIngredientIdsCount += currentRangeEnd - currentRangeStart + 1;

                Console.WriteLine($"{freshIngredientIdsCount} ingredient IDs are considered to be fresh.");
            }
        }

        private static UlongRange ConvertToUlongRange(string? range)
        {
            var boundaries = range!.Split('-');

            var rangeStart = ulong.Parse(boundaries[0]);
            var rangeEnd = ulong.Parse(boundaries[1]);

            return new UlongRange(rangeStart, rangeEnd);
        }

        private readonly struct UlongRange(ulong start, ulong end)
        {
            public ulong RangeStart { get; init; } = start;

            public ulong RangeEnd { get; init; } = end;
        }
    }
}

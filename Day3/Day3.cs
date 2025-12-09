using AoC2025.Utils;

namespace AoC2025.Day3
{
    internal static class Day3
    {
        internal static class Part1_Part2
        {
            public static void Solve()
            {
                var dataTxtLocation = @"C:\Users\sonrisa\OneDrive - Sonrisa Kft\WS\AoC2025\Data\input_day3.txt";
                var batteries = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);
                //var batteries = new List<string>
                //{
                //    "987654321111111", "811111111111119", "234234234234278", "818181911112111"
                //};

                ulong jolts = 0;

                foreach (var battery in batteries)
                {
                    var joltage = string.Join(null, GetChars(battery, 2));
                    jolts += ulong.Parse(joltage!);
                }

                Console.WriteLine($"Total jolts is {jolts}.");
            }

            private static char[] GetChars(string battery, int charsNeeded)
            {
                var chars = new char[charsNeeded];
                var length = charsNeeded;
                var maxIndex = 0;

                while (charsNeeded > 0)
                {
                    var max = battery[maxIndex];
                    for (int k = maxIndex + 1; k < battery.Length - charsNeeded + 1; k++)
                    {
                        if (battery[k] > max)
                        {
                            max = battery[k];
                            maxIndex = k;
                        }
                    }

                    chars[length - charsNeeded] = max;
                    charsNeeded--;
                    maxIndex++;
                }

                return chars;
            }
        }
    }
}

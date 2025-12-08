using AoC2025.Utils;

namespace AoC2025.Day2
{
    internal static class Day2
    {
        internal static class Part1
        {

            internal static void Solve()
            {
                //var dataTxtLocation = @"C:\Users\sonrisa\OneDrive - Sonrisa Kft\WS\AoC2025\Data\input_day2.txt";
                //var inputText = InputDataParser.ParseSingleLineInputTxt(dataTxtLocation);
                var inputText = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862";

                ulong sillyPatternSum = 0;

                foreach (var range in inputText.Split(','))
                {
                    var boundaries = range.Split("-");

                    var start = ulong.Parse(boundaries[0]);
                    var end = ulong.Parse(boundaries[1]);

                    for (ulong i = start; i <= end; i++)
                    {
                        var number = i.ToString();
                        if (number.Length % 2 == 0)
                        {
                            var firstPart = number.Substring(0, number.Length / 2);
                            var secondPart = number.Substring(number.Length / 2);

                            if (firstPart == secondPart)
                            {
                                sillyPatternSum += i;
                            }
                        }
                    }
                }

                Console.WriteLine($"The sum of silly pattern IDs is '{sillyPatternSum}'.");
            }
        }

        internal static class Part2
        {

            internal static void Solve()
            {
                var dataTxtLocation = @"C:\Users\sonrisa\OneDrive - Sonrisa Kft\WS\AoC2025\Data\input_day2.txt";
                var inputText = InputDataParser.ParseSingleLineInputTxt(dataTxtLocation);
                //var inputText = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

                ulong sillyPatternSum = 0;

                foreach (var range in inputText.Split(','))
                {
                    var boundaries = range.Split("-");

                    var start = ulong.Parse(boundaries[0]);
                    var end = ulong.Parse(boundaries[1]);

                    for (ulong i = start; i <= end; i++)
                    {
                        if (IsInvalid(i))
                        {
                            sillyPatternSum += i;
                        }
                    }
                }

                Console.WriteLine($"The sum of silly pattern IDs is '{sillyPatternSum}'.");
            }

            // invalid means that the number is made up as a repeating sequence
            private static bool IsInvalid(ulong number)
            {
                var numberLength = number.ToString().Length;

                for (int numberLengthDivisor = 2; numberLengthDivisor <= numberLength; numberLengthDivisor++)
                {
                    if (numberLength % numberLengthDivisor == 0)
                    {
                        ulong sequence = 0;

                        // 11
                        for (int i = 0; i < numberLength / numberLengthDivisor; i++)
                        {
                            // build a sequence that will be tested
                            sequence = sequence * 10 + ulong.Parse(number.ToString()[i].ToString());

                            // remove every occurance of this sequence from the number
                            var remaining = number.ToString().Replace(sequence.ToString(), null);
                            if (string.IsNullOrWhiteSpace(remaining))
                            {
                                return true;
                            }
                        }
                    } 
                }

                return false;
            }
        }
    }
}

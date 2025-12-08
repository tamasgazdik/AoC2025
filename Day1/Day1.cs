using AoC2025.Utils;

namespace AoC2025.Day1
{
    internal static class Day1
    {
        private static int Value;

        internal static class Part1
        {

            internal static void Solve()
            {
                Value = 50;
                var zeroCounter = 0;

                var dataTxtLocation = @"C:\Users\sonrisa\OneDrive - Sonrisa Kft\WS\AoC2025\Data\input_day1.txt";
                var instructions = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);

                //var instructions = new List<string>
                //{
                //    "L68",
                //    "L30",
                //    "R48",
                //    "L5",
                //    "R60",
                //    "L55",
                //    "L1",
                //    "L99",
                //    "R14",
                //    "L82"
                //};

                foreach (var instruction in instructions)
                {
                    var direction = instruction[0];
                    var value = int.Parse(instruction.Substring(1));

                    SetValue(direction, value);
                    if (Value == 0)
                    {
                        zeroCounter++;
                    }
                }

                Console.WriteLine($"Safe lock was turned zero '{zeroCounter}' times.");
            }

            private static void SetValue(char direction, int value)
            {
                value = value % 100;

                if (direction == 'R')
                {
                    Value += value;
                    if (Value > 99)
                    {
                        Value = Value - 100;
                    }
                }
                else
                {
                    Value -= value;
                    if (Value < 0)
                    {
                        Value = Value + 100;
                    }
                }
            }
        }

        internal static class Part2
        {
            internal static void Solve()
            {
                Value = 50;

                var zeroCounter = 0;
                var zeroIsPassedCounter = 0;

                var dataTxtLocation = @"C:\Users\sonrisa\OneDrive - Sonrisa Kft\WS\AoC2025\Data\input_day1.txt";
                var instructions = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);

                foreach (var instruction in instructions)
                {
                    var direction = instruction[0];
                    var value = int.Parse(instruction.Substring(1));

                    zeroIsPassedCounter += SetValue(direction, value);
                    if (Value == 0)
                    {
                        zeroCounter++;
                    }
                }

                Console.WriteLine($"Safe lock passed zero or turned to zero '{zeroCounter + zeroIsPassedCounter}' times.");
            }

            private static int SetValue(char direction, int value)
            {
                var valueWithoutHundreds = value % 100;
                var hundreds = value / 100;

                var numberOfTimesZeroIsPassed = 0;
                var startedFromZero = Value == 0;

                if (direction == 'R')
                {
                    Value += valueWithoutHundreds;
                    if (Value > 99)
                    {
                        Value -= 100;
                        if (!startedFromZero && Value != 0)
                        {
                            numberOfTimesZeroIsPassed++;
                        }
                    }
                }
                else
                {
                    Value -= valueWithoutHundreds;
                    if (Value < 0)
                    {
                        Value += 100;
                        if (!startedFromZero)
                        {
                            numberOfTimesZeroIsPassed++;
                        }
                    }
                }

                return hundreds + numberOfTimesZeroIsPassed;
            }
        }
    }
}

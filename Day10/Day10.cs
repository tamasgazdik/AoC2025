using AoC2025.Utils;
using System.Text;

namespace AoC2025.Day10
{
    internal static class Day10
    {
        internal static class Part1
        {
            public static void Solve()
            {
                var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day10.txt";
                var input = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);
                //var input = new List<string>
                //{
                //    "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
                //    "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
                //    "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}"
                //};

                var machineLines = ProcessInput(input);

                var sumOfMinCombinationCount = 0;

                foreach (var machineLine in machineLines)
                {
                    // my original solution which is very long running
                    //var minCombinationCount = GetMinCombinationCount(machineLine);

                    // the idiomatic way which takes like 2s to execute
                    var minCombinationCount = GetMinCombinationCountTheIdiomaticWay(machineLine);
                    sumOfMinCombinationCount += minCombinationCount;
                }

                Console.WriteLine(sumOfMinCombinationCount);

                Console.ReadKey();
            }

            private static int GetMinCombinationCountTheIdiomaticWay(MachineLine machineLine)
            {
                var target = machineLine.Lights.Target;
                var buttonCount = machineLine.Buttons.Count;
                var minCombinationCount = buttonCount;

                // mask serves as the bit mask for either including (1) or excluding (0) the given button
                for (int mask = 1; mask < (1 << buttonCount); mask++)
                {
                    var combination = new List<Button>(buttonCount);

                    for (int i = 0; i < buttonCount; i++)
                    {
                        // (1 << i) produces a value where only the ith bit is one
                        // << is the left shift operation. In this case 1 is shifted by i places
                        // bitwise AND operation will return 0 if none of the bits match
                        if ((mask & (1 << i)) != 0)
                        {
                            combination.Add(machineLine.Buttons[i]);
                        }
                    }

                    var combinationCount = combination.Count;
                    var positionAfterCombinationExecuted = machineLine.Lights.Current;
                    foreach (var button in combination)
                    {
                        positionAfterCombinationExecuted = PressButton(button, positionAfterCombinationExecuted);
                    }

                    var positionsMatch = true;
                    for (int i = 0; i < target.Length; i++)
                    {
                        if (positionAfterCombinationExecuted[i] == target[i])
                        {
                            continue;
                        }

                        positionsMatch = false;
                        break;
                    }

                    if (positionsMatch &&
                        combinationCount < minCombinationCount)
                    {
                        minCombinationCount = combinationCount;
                    }
                }

                return minCombinationCount;
            }

            private static int GetMinCombinationCount(MachineLine machineLine)
            {
                var possibleCombinations = new List<List<Button>>()
                    {
                        new ()
                    };

                for (int j = 0; j < possibleCombinations.Count; j++)
                {
                    GetCombination(machineLine.Buttons, possibleCombinations[j], possibleCombinations);
                }

                var minCombinationCount = int.MaxValue;

                foreach (var possibleCombination in possibleCombinations)
                {
                    var positionAfterPushingButtons = new char[machineLine.Lights.Current.Length];
                    machineLine.Lights.Current.CopyTo(positionAfterPushingButtons, 0);

                    foreach (var button in possibleCombination)
                    {
                        positionAfterPushingButtons = PressButton(button, positionAfterPushingButtons);
                    }

                    var positionsMatch = true;
                    for (int i = 0; i < positionAfterPushingButtons.Length; i++)
                    {
                        if (positionAfterPushingButtons[i] != machineLine.Lights.Target[i])
                        {
                            positionsMatch = false;
                            break;
                        }
                    }

                    // found a combination that leads to the target
                    if (positionsMatch)
                    {
                        var numberOfButtonsInCombination = possibleCombination.Count;
                        if (numberOfButtonsInCombination < minCombinationCount)
                        {
                            minCombinationCount = numberOfButtonsInCombination;
                        }
                    }
                }

                return minCombinationCount;
            }

            private static List<MachineLine> ProcessInput(List<string> input)
            {
                var machineLines = new List<MachineLine>(input.Count);

                foreach (var line in input)
                {
                    var sections = line.Split(' ');
                    var targetLights = sections[0];
                    var target = new char[targetLights.Length - 2];
                    for (int i = 1; i < targetLights.Length - 1; i++)
                    {
                        target[i - 1] = targetLights[i];
                    }

                    var buttons = new List<Button>();
                    for (int i = 1; i < sections.Length - 1; i++)
                    {
                        var section = sections[i];
                        section = section[1..^1];

                        var indeces = section.Split(',');
                        buttons.Add(new Button
                        {
                            Indeces = indeces.Select(index => int.Parse(index)).ToList()
                        });
                    }

                    machineLines.Add(new MachineLine(
                        new Lights(target), buttons));
                }

                return machineLines;
            }

            private static void GetCombination(
                List<Button> buttons,
                List<Button> combination,
                List<List<Button>> possibleCombinations)
            {
                foreach (var button in buttons)
                {
                    if (combination.Contains(button))
                    {
                        continue;
                    }

                    var tempCombination = new List<Button>(combination)
                    {
                        button
                    };

                    if (possibleCombinations.Any(previousCombination => tempCombination.All(button => previousCombination.Contains(button))))
                    {
                        continue;
                    }
                    possibleCombinations.Add(tempCombination);
                }
            }

            private static void PickOneMoreButton(
                HashSet<Button> buttonsPicked, 
                int numberOfButtonsToPick,
                List<Button> buttonsAvailable,
                HashSet<HashSet<Button>> possibleCombinations)
            {
                foreach (var button in buttonsAvailable)
                {
                    if (buttonsPicked.Contains(button))
                    {
                        continue;
                    }

                    buttonsPicked.Add(button);
                    if (possibleCombinations.Any(previousCombination => buttonsPicked.All(button => previousCombination.Contains(button))))
                    {
                        continue;
                    }
                    if (buttonsPicked.Count == numberOfButtonsToPick)
                    {
                        possibleCombinations.Add(buttonsPicked); 
                    }
                    else
                    {
                        PickOneMoreButton(buttonsPicked, numberOfButtonsToPick, buttonsAvailable, possibleCombinations);
                    }
                }
            }

            private static char[] PressButton(Button button, char[] lights)
            {
                var result = new char[lights.Length];
                lights.CopyTo(result, 0);

                for (int i = 0; i < button.Indeces.Count; i++)
                {
                    var indexToSwitch = button.Indeces[i];
                    if (lights[indexToSwitch] == '#')
                    {
                        result[indexToSwitch] = '.';
                    }
                    else
                    {
                        result[indexToSwitch] = '#';
                    }
                }

                return result;
            }

            private static int CalculateDifference(char[] one, char[] theOther)
            {
                var difference = 0;
                for (int i = 0; i < one.Length; i++)
                {
                    if (one[i] != theOther[i])
                    {
                        difference++;
                    }
                }
                return difference;
            }

            private record MachineLine(Lights Lights, List<Button> Buttons);

            private record Lights
            {
                public Lights(char[] target)
                {
                    Target = target;
                    Capacity = target.Length;
                    Current = Enumerable.Repeat<char>('.', Capacity).ToArray();
                }

                public char[] Current { get; }

                public int Capacity { get; }

                public char[] Target { get; }

                public override string ToString()
                {
                    return $"Current: [{CharArrayToString(Current)}] " +
                        $"Target: [{CharArrayToString(Target)}]";
                }

                private static string CharArrayToString(char[] array)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < array.Length; i++)
                    {
                        sb.Append(array[i]);
                    }

                    return sb.ToString();
                }
            }

            private readonly struct Button
            {
                public List<int> Indeces { get; init; }

                public override string ToString()
                {
                    return $"({string.Join(',', Indeces)})";
                }
            }
        }
    }
}

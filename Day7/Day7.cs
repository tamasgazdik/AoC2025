namespace AoC2025.Day7
{
    internal static class Day7
    {
        internal static class Part1
        {
            public static void Solve()
            {
                //var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day7.txt";
                //var input = Utils.InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);

                var input = new List<string>
                {
                    ".......S.......",
                    "...............",
                    ".......^.......",
                    "...............",
                    "......^.^......",
                    "...............",
                    ".....^.^.^.....",
                    "...............",
                    "....^.^...^....",
                    "...............",
                    "...^.^...^.^...",
                    "...............",
                    "..^...^.....^..",
                    "...............",
                    ".^.^.^.^.^...^.",
                    "..............."
                };

                var beamStartsAt = input[0].IndexOf('S');

                var numberOfTimesBeamSplit = 0;

                var beamTraversesIndeces = new HashSet<int>
                {
                    beamStartsAt
                };

                foreach ( var row in input.Skip(1))
                {
                    for ( int i = 0; i < row.Length; i++)
                    {
                        if (row[i] == '^' &&
                            beamTraversesIndeces.Contains(i))
                        {
                            beamTraversesIndeces.Remove(i);
                            beamTraversesIndeces.Add(i - 1);
                            beamTraversesIndeces.Add(i + 1);

                            numberOfTimesBeamSplit++;
                        }
                    }
                }

                Console.WriteLine($"The beam was split {numberOfTimesBeamSplit} times.");
            }
        }

        internal static class Part2
        {
            public static void Solve()
            {
                //var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day7.txt";
                //var input = Utils.InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);

                var input = new List<string>
                {
                    ".......S.......",
                    "...............",
                    ".......^.......",
                    "...............",
                    "......^.^......",
                    "...............",
                    ".....^.^.^.....",
                    "...............",
                    "....^.^...^....",
                    "...............",
                    "...^.^...^.^...",
                    "...............",
                    "..^...^.....^..",
                    "...............",
                    ".^.^.^.^.^...^.",
                    "..............."
                };

                var beamStartsAtIndex = input[0].IndexOf('S');

                var beamTraversesIndeces = new HashSet<int>
                {
                    beamStartsAtIndex
                };

                var hitSplitters = new HashSet<(int, int)>();

                var rowIndex = 1;

                foreach (var row in input.Skip(1))
                {
                    for (int i = 0; i < row.Length; i++)
                    {
                        if (row[i] == '^' &&
                            beamTraversesIndeces.Contains(i))
                        {
                            beamTraversesIndeces.Remove(i);

                            beamTraversesIndeces.Add(i - 1);
                            beamTraversesIndeces.Add(i + 1);

                            hitSplitters.Add((rowIndex, i));
                        }
                    }

                    rowIndex++;
                }

                var timelineCount = 0;
                foreach (var beamTraverseIndex in beamTraversesIndeces)
                {
                    timelineCount += GetNumberOfPathsToNode(input, input.Count - 1, beamTraverseIndex);
                }

                Console.WriteLine($"The beam was split {hitSplitters.Count} times.");
            }

            private static int GetNumberOfPathsToNode(List<string> input, int rowIndex, int columnIndex)
            {
                if (rowIndex < 1)
                {
                    return 1;
                }

                var numberOfPathsToThisNode = 1;

                if (columnIndex - 1 >= 0 &&
                    columnIndex + 1 <= input[0].Length - 1 &&
                    input[rowIndex][columnIndex - 1] == '^' &&
                    input[rowIndex][columnIndex + 1] == '^')
                {
                    numberOfPathsToThisNode *= 2 *
                        GetNumberOfPathsToNode(input, rowIndex - 1, columnIndex - 1) *
                        GetNumberOfPathsToNode(input, rowIndex - 1, columnIndex + 1);
                }
                else if (
                    columnIndex - 1 >= 0 &&
                    input[rowIndex][columnIndex - 1] == '^')
                {
                    numberOfPathsToThisNode *= GetNumberOfPathsToNode(input, rowIndex - 1, columnIndex - 1);
                }
                else if (
                    columnIndex + 1 <= input[0].Length - 1 &&
                    input[rowIndex][columnIndex + 1] == '^')
                {
                    numberOfPathsToThisNode *= GetNumberOfPathsToNode(input, rowIndex - 1, columnIndex + 1);
                }
                else
                {
                    numberOfPathsToThisNode *= GetNumberOfPathsToNode(input, rowIndex - 1, columnIndex);
                }

                return numberOfPathsToThisNode;
            }
        }
    }
}
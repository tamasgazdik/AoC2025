using AoC2025.Utils;
using System.Text;

namespace AoC2025.Day4
{
    internal static class Day4
    {
        private const char AT_SIGN = '@';

        internal static class Part1
        {
            public static void Solve()
            {
                var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day4.txt";
                var positions = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);
                //var positions = new List<string>
                //{
                //    "..@@.@@@@.",
                //    "@@@.@.@.@@",
                //    "@@@@@.@.@@",
                //    "@.@@@@..@.",
                //    "@@.@@@@.@@",
                //    ".@@@@@@@.@",
                //    ".@.@.@.@@@",
                //    "@.@@@.@@@@",
                //    ".@@@@@@@@.",
                //    "@.@.@@@.@."
                //};

                var rowLength = positions[0].Length;

                var accessibleRollOfPaperCount = 0;

                for (int row = 0; row < positions.Count; row++)
                {
                    for (int column = 0; column < rowLength; column++)
                    {
                        // examine only paper rolls
                        if (positions[row][column] == AT_SIGN)
                        {
                            // in the case of corners 
                            if ((row == 0 || row == positions.Count - 1)
                                                &&
                                                (column == 0 || column == rowLength - 1))
                            {
                                accessibleRollOfPaperCount++;
                                continue;
                            }

                            var atSignNeighbourCount = 0;

                            // for first row
                            if (row == 0)
                            {
                                var neighbours = new char[5]
                                {
                                positions[row][column - 1],
                                positions[row + 1][column - 1],
                                positions[row + 1][column],
                                positions[row + 1][column + 1],
                                positions[row][column + 1]
                                };
                                for (int i = 0; i < 5; i++)
                                {
                                    if (neighbours[i] == AT_SIGN)
                                    {
                                        atSignNeighbourCount++;
                                    }
                                }
                            }

                            // for last row
                            else if (row == positions.Count - 1)
                            {
                                var neighbours = new char[5]
                                {
                                positions[row][column - 1],
                                positions[row - 1][column - 1],
                                positions[row - 1][column],
                                positions[row - 1][column + 1],
                                positions[row][column + 1]
                                };
                                for (int i = 0; i < 5; i++)
                                {
                                    if (neighbours[i] == AT_SIGN)
                                    {
                                        atSignNeighbourCount++;
                                    }
                                }
                            }

                            // for first column
                            else if (column == 0)
                            {
                                var neighbours = new char[5]
                                {
                                positions[row - 1][column],
                                positions[row - 1][column + 1],
                                positions[row][column + 1],
                                positions[row + 1][column + 1],
                                positions[row + 1][column]
                                };
                                for (int i = 0; i < 5; i++)
                                {
                                    if (neighbours[i] == AT_SIGN)
                                    {
                                        atSignNeighbourCount++;
                                    }
                                }
                            }

                            // for last column
                            else if (column == rowLength - 1)
                            {
                                var neighbours = new char[5]
                                {
                                positions[row - 1][column],
                                positions[row - 1][column - 1],
                                positions[row][column - 1],
                                positions[row + 1][column - 1],
                                positions[row + 1][column]
                                };
                                for (int i = 0; i < 5; i++)
                                {
                                    if (neighbours[i] == AT_SIGN)
                                    {
                                        atSignNeighbourCount++;
                                    }
                                }
                            }

                            // for the inner nodes
                            else
                            {
                                var neighbours = new char[8]
                                {
                                positions[row - 1][column],
                                positions[row - 1][column + 1],
                                positions[row][column + 1],
                                positions[row + 1][column + 1],
                                positions[row + 1][column],
                                positions[row + 1][column - 1],
                                positions[row][column - 1],
                                positions[row - 1][column - 1]
                                };
                                for (int i = 0; i < 8; i++)
                                {
                                    if (neighbours[i] == AT_SIGN)
                                    {
                                        atSignNeighbourCount++;
                                    }
                                }
                            }

                            if (atSignNeighbourCount < 4)
                            {
                                accessibleRollOfPaperCount++;
                            }
                        }
                    }
                }

                Console.WriteLine($"There are {accessibleRollOfPaperCount} rolls of paper that can be accessed by a forklift.");
            }
        }

        internal static class Part2
        {
            private const char DOT = '.';

            public static void Solve()
            {
                var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day4.txt";
                var positions = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);
                //var positions = new List<string>
                //{
                //    "..@@.@@@@.",
                //    "@@@.@.@.@@",
                //    "@@@@@.@.@@",
                //    "@.@@@@..@.",
                //    "@@.@@@@.@@",
                //    ".@@@@@@@.@",
                //    ".@.@.@.@@@",
                //    "@.@@@.@@@@",
                //    ".@@@@@@@@.",
                //    "@.@.@@@.@."
                //};

                var rowLength = positions[0].Length;

                var accessibleRollOfPaperCount = 0;
                bool rollOfPaperWasRemovedInRound;

                do
                {
                    var rollOfPapersToRemove = new HashSet<(int row, int column)>();

                    for (int row = 0; row < positions.Count; row++)
                    {
                        for (int column = 0; column < rowLength; column++)
                        {
                            // examine only paper rolls
                            if (positions[row][column] == AT_SIGN)
                            {
                                // in the case of corners 
                                if ((row == 0 || row == positions.Count - 1)
                                                    &&
                                                    (column == 0 || column == rowLength - 1))
                                {
                                    accessibleRollOfPaperCount++;

                                    rollOfPapersToRemove.Add((row, column));
                                    continue;
                                }

                                var atSignNeighbourCount = 0;

                                // for first row
                                if (row == 0)
                                {
                                    var neighbours = new char[5]
                                    {
                                        positions[row][column - 1],
                                        positions[row + 1][column - 1],
                                        positions[row + 1][column],
                                        positions[row + 1][column + 1],
                                        positions[row][column + 1]
                                    };
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (neighbours[i] == AT_SIGN)
                                        {
                                            atSignNeighbourCount++;
                                        }
                                    }
                                }

                                // for last row
                                else if (row == positions.Count - 1)
                                {
                                    var neighbours = new char[5]
                                    {
                                        positions[row][column - 1],
                                        positions[row - 1][column - 1],
                                        positions[row - 1][column],
                                        positions[row - 1][column + 1],
                                        positions[row][column + 1]
                                    };
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (neighbours[i] == AT_SIGN)
                                        {
                                            atSignNeighbourCount++;
                                        }
                                    }
                                }

                                // for first column
                                else if (column == 0)
                                {
                                    var neighbours = new char[5]
                                    {
                                        positions[row - 1][column],
                                        positions[row - 1][column + 1],
                                        positions[row][column + 1],
                                        positions[row + 1][column + 1],
                                        positions[row + 1][column]
                                    };
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (neighbours[i] == AT_SIGN)
                                        {
                                            atSignNeighbourCount++;
                                        }
                                    }
                                }

                                // for last column
                                else if (column == rowLength - 1)
                                {
                                    var neighbours = new char[5]
                                    {
                                        positions[row - 1][column],
                                        positions[row - 1][column - 1],
                                        positions[row][column - 1],
                                        positions[row + 1][column - 1],
                                        positions[row + 1][column]
                                    };
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (neighbours[i] == AT_SIGN)
                                        {
                                            atSignNeighbourCount++;
                                        }
                                    }
                                }

                                // for the inner nodes
                                else
                                {
                                    var neighbours = new char[8]
                                    {
                                        positions[row - 1][column],
                                        positions[row - 1][column + 1],
                                        positions[row][column + 1],
                                        positions[row + 1][column + 1],
                                        positions[row + 1][column],
                                        positions[row + 1][column - 1],
                                        positions[row][column - 1],
                                        positions[row - 1][column - 1]
                                    };
                                    for (int i = 0; i < 8; i++)
                                    {
                                        if (neighbours[i] == AT_SIGN)
                                        {
                                            atSignNeighbourCount++;
                                        }
                                    }
                                }

                                if (atSignNeighbourCount < 4)
                                {
                                    accessibleRollOfPaperCount++;
                                    rollOfPapersToRemove.Add((row, column));
                                }
                            }
                        }
                    }

                    foreach (var coords in rollOfPapersToRemove)
                    {
                        var row = positions[coords.row];
                        var newRow = new char[row.Length];
                        row.CopyTo(0, newRow, 0, row.Length);

                        newRow[coords.column] = DOT;

                        var stringBuilder = new StringBuilder();
                        for (int i = 0; i < newRow.Length; i++)
                        {
                            stringBuilder.Append(newRow[i]);
                        }

                        positions[coords.row] = stringBuilder.ToString();
                    }

                    rollOfPaperWasRemovedInRound = rollOfPapersToRemove.Count != 0;
                } while (rollOfPaperWasRemovedInRound);

                Console.WriteLine($"In total {accessibleRollOfPaperCount} rolls of paper can be removed.");
            }
        }
    }
}

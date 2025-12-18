using AoC2025.Utils;

namespace AoC2025.Day6
{
    internal class Day6
    {
        internal static class Part1
        {
            public static void Solve()
            {
                var dataTxtLocation = "C:\\Users\\sonrisa\\OneDrive - Sonrisa Kft\\WS\\AoC2025\\Data\\input_day6.txt";
                var mathProblems = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);
                //var mathProblems = new List<string> {
                //    "123 328  51 64 ",
                //    " 45 64  387 23 ",
                //    "  6 98  215 314",
                //    "*   +   *   +  "
                //};

                var operationsList = mathProblems[^1];
                var operations = new List<char>();
                foreach (var operationString in operationsList.Split(' ').Where(operation => !string.IsNullOrWhiteSpace(operation)))
                {
                    operations.Add(char.Parse(operationString.Trim()));
                }

                var inputRowsList = mathProblems.Except([operationsList]);
                var inputLists = new List<List<ulong>>();
                foreach (var inputRow in inputRowsList)
                {
                    var inputs = inputRow.Split(' ');
                    var inputsInRow = new List<ulong>();
                    foreach (var input in inputs.Where(input => !string.IsNullOrWhiteSpace(input)))
                    {
                        inputsInRow.Add(ulong.Parse(input.Trim()));
                    }

                    inputLists.Add(inputsInRow);
                }

                var sumOfAllMathProblems = 0UL;

                // calculate
                for (int i = 0; i < operations.Count; i++)
                {
                    if (operations[i] == '*')
                    {
                        var currentValue = 1UL;

                        foreach (var inputList in inputLists)
                        {
                            currentValue *= inputList[i];
                        }

                        sumOfAllMathProblems += (ulong)currentValue;
                    }
                    else
                    {
                        ulong currentValue = 0UL;

                        foreach (var inputList in inputLists)
                        {
                            currentValue += inputList[i];
                        }

                        sumOfAllMathProblems += currentValue;
                    }
                }

                Console.WriteLine($"The sum of all math problems is {sumOfAllMathProblems}");
            }
        }

        internal static class Part2
        {
            public static void Solve()
            {
                //var dataTxtLocation = "C:\\Users\\sonrisa\\OneDrive - Sonrisa Kft\\WS\\AoC2025\\Data\\input_day6.txt";
                //var mathProblems = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);
                var mathProblems = new List<string> {
                    "123 328  51 64 ",
                    " 45 64  387 23 ",
                    "  6 98  215 314",
                    " 27 512  10   6", // dummy line added by me for testing
                    "*   +   *   +  "
                };

                var operationsList = mathProblems[^1];
                var operations = new List<char>();
                foreach (var operationString in operationsList.Split(' ').Where(operation => !string.IsNullOrWhiteSpace(operation)))
                {
                    operations.Add(char.Parse(operationString.Trim()));
                }

                var inputRowsList = mathProblems.Except([operationsList]);
                var inputLists = new List<List<ulong>>();
                foreach (var inputRow in inputRowsList)
                {
                    var inputs = inputRow.Split(' ');
                    var inputsInRow = new List<ulong>();
                    foreach (var input in inputs.Where(input => !string.IsNullOrWhiteSpace(input)))
                    {
                        inputsInRow.Add(ulong.Parse(input.Trim()));
                    }

                    inputLists.Add(inputsInRow);
                }

                var sumOfAllMathProblems = 0UL;

                ConvertInputListsToCephalopodMath(inputLists);

                // calculate
                for (int i = 0; i < operations.Count; i++)
                {
                    if (operations[i] == '*')
                    {
                        var currentValue = 1UL;

                        foreach (var inputList in inputLists)
                        {
                            currentValue *= inputList[i];
                        }

                        sumOfAllMathProblems += (ulong)currentValue;
                    }
                    else
                    {
                        ulong currentValue = 0UL;

                        foreach (var inputList in inputLists)
                        {
                            currentValue += inputList[i];
                        }

                        sumOfAllMathProblems += currentValue;
                    }
                }

                Console.WriteLine($"The sum of all math problems is {sumOfAllMathProblems}");
            }

            private static void ConvertInputListsToCephalopodMath(List<List<ulong>> inputLists)
            {
                var resultLists = new List<List<ulong>>();
                var resultListLength = inputLists[0].Count;

                for (int i = 0; i < resultListLength; i++)
                {
                    var resultList = new List<ulong>();

                    // take columns
                    var inputArray = new ulong[inputLists.Count];
                    for (int j = 0; j < inputLists.Count; j++)
                    {
                        inputArray[j] = inputLists[j][i];
                    }

                    var quotient = 1UL;

                    foreach (var input in inputArray)
                    {
                        var divisor = input / quotient;
                        while (divisor > 0)
                        {
                            quotient *= 10;
                            divisor = input / quotient;
                        }
                    }

                    for (ulong k = 1; k < quotient; k *= 10)
                    {
                        var numberToAddToResultList = 0UL;

                        var lastNumber = inputArray[0] / k % 10;
                        for (int l = 0; l < inputArray.Length; l++)
                        {
                            var element = inputArray[l];
                            var digit = ((element / k) % 10);
                            if (digit > 0)
                            {
                                lastNumber *= 10;
                            }

                            numberToAddToResultList += (ulong)(digit * Math.Pow(10, inputArray.Length - l - 1));
                        }

                        resultList.Add(numberToAddToResultList); 
                    }


                    Console.WriteLine($"Quotient for {i + 1}th round of inputs is {quotient}.");
                }
            }
        }
    }
}

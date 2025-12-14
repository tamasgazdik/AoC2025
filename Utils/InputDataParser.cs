using System.Text;

namespace AoC2025.Utils
{
    internal class InputDataParser
    {
        internal static TOut ParseInputTxtLineByLine<TOut, TElement>(string location)
            where TOut : class, IList<TElement>, new()
            where TElement : class
        {
            var result = new TOut();

            using (var fileReader = new StreamReader(location))
            {
                while (!fileReader.EndOfStream)
                {
                    var readLine = fileReader.ReadLine();
                    if (readLine == null) continue;

                    if (readLine is TElement element)
                    {
                        result.Add(element);
                    }
                }
            }

            return result;
        }

        internal static string ParseSingleLineInputTxt(string location)
        {
            var stringBuilder = new StringBuilder();

            using (var fileReader = new StreamReader(location))
            {
                while (!fileReader.EndOfStream)
                {
                    var readLine = fileReader.ReadLine();
                    if (readLine == null) continue;

                    stringBuilder.Append(readLine);
                }
            }

            return stringBuilder.ToString();
        }

        internal static string ParseInputTxtAsSingleString(string location)
        {
            var stringBuilder = new StringBuilder();

            using (var fileReader = new StreamReader(location))
            {
                while (!fileReader.EndOfStream)
                {
                    var readLine = fileReader.ReadLine();
                    if (readLine == null) continue;

                    stringBuilder.AppendLine(readLine);
                }
            }

            return stringBuilder.ToString();
        }
    }
}

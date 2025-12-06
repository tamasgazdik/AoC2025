namespace AoC2025.Utils
{
    internal class InputDataParser
    {
        internal static TOut ParseInputTxt<TOut, TElement>(string location)
            where TOut : class, IList<TElement>, new()
            where TElement : class
        {
            var instructions = new TOut();

            using (var fileReader = new StreamReader(location))
            {
                while (!fileReader.EndOfStream)
                {
                    var readLine = fileReader.ReadLine();
                    if (readLine == null) continue;

                    if (readLine is TElement element)
                    {
                        instructions.Add(element);
                    }
                }
            }

            return instructions;
        }
    }
}

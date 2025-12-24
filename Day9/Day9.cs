namespace AoC2025.Day9
{
    internal static class Day9
    {
        internal static class Part1
        {
            public static void Solve()
            {
                var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day9.txt";
                var input = Utils.InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);
                //var input = new List<string>
                //{
                //    "7,1",
                //    "11,1",
                //    "11,7",
                //    "9,7",
                //    "9,5",
                //    "2,5",
                //    "2,3",
                //    "7,3"
                //};

                var parts = input.Select(line => line.Split(','));

                var redTileCoordinates = new List<Coordinate>();

                foreach (var part in parts)
                {
                    redTileCoordinates.Add(new Coordinate(long.Parse(part[1]), long.Parse(part[0])));
                }

                var maxArea = 0L;
                var coordinatePairs = new Coordinate[2];

                foreach (var redTileCoordinate in redTileCoordinates)
                {
                    foreach (var otherCoordinate in redTileCoordinates.Except([redTileCoordinate]))
                    {
                        long area = (Math.Abs(redTileCoordinate.x - otherCoordinate.x) + 1) *
                            (Math.Abs(redTileCoordinate.y - otherCoordinate.y) + 1);

                        if (area > maxArea)
                        {
                            maxArea = area;
                            coordinatePairs[0] = redTileCoordinate;
                            coordinatePairs[1] = otherCoordinate;
                        }
                    }
                }
            }

            private record Coordinate(long x, long y);
        }
    }
}

using System.Net.Mime;

namespace AoC2025.Day8
{
    internal class Day8
    {
        internal static class Part1
        {
            public static void Solve()
            {
                var inputTxtDestination = "C:\\WS\\AoC2025\\Data\\input_day8.txt";
                var input = Utils.InputDataParser.ParseInputTxtLineByLine<List<string>, string>(inputTxtDestination);

                //var input = new List<string>
                //{
                //    "162,817,812",
                //    "57,618,57",
                //    "906,360,560",
                //    "592,479,940",
                //    "352,342,300",
                //    "466,668,158",
                //    "542,29,236",
                //    "431,825,988",
                //    "739,650,466",
                //    "52,470,668",
                //    "216,146,977",
                //    "819,987,18",
                //    "117,168,530",
                //    "805,96,715",
                //    "346,949,466",
                //    "970,615,88",
                //    "941,993,340",
                //    "862,61,35",
                //    "984,92,344",
                //    "425,690,689"
                //};

                var coordinates = ConvertToCoordinates(input);
                var circuits = new List<List<Coordinates>>();

                //var iterations = 10;
                var iterations = 1000;

                for (int i = 0; i < iterations; i++)
                {
                    var minDistance = double.MaxValue;
                    (Coordinates One, Coordinates TheOther) minDistancePair = new ()
                    {
                        One = coordinates[0],
                        TheOther = coordinates[0]
                    };

                    foreach (var coordinate in coordinates)
                    {
                        foreach (var otherCoordinate in coordinates.Except([ coordinate ]))
                        {
                            var distance = GetDistanceBetween(coordinate, otherCoordinate);

                            if (distance < minDistance)
                            {
                                if (circuits.Any(circuit => circuit.Contains(coordinate) && circuit.Contains(otherCoordinate)))
                                {
                                    continue;
                                }

                                minDistance = distance;
                                minDistancePair = new()
                                {
                                    One = coordinate,
                                    TheOther = otherCoordinate
                                };
                            }
                        }
                    }

                    var one = minDistancePair.One;
                    var theOther = minDistancePair.TheOther;

                    var circuitContainingOne = circuits.FirstOrDefault(circuit => circuit.Contains(one));
                    
                    if (circuitContainingOne == null)
                    {
                        var circuitContainingTheOther = circuits.FirstOrDefault(circuit => circuit.Contains(theOther));
                        if (circuitContainingTheOther == null)
                        {
                            var circuit = new List<Coordinates>
                            {
                                one, theOther
                            };

                            circuits.Add(circuit);
                        }
                        else
                        {
                            circuitContainingTheOther.Add(one);
                        }
                    }
                    else
                    {
                        var circuitContainingTheOther = circuits.FirstOrDefault(circuit => circuit.Contains(theOther));
                        if (circuitContainingTheOther == null)
                        {
                            circuitContainingOne.Add(theOther);
                        }
                        else
                        {
                            // merge the two together
                            var mergedCircuit = circuitContainingOne.UnionBy(circuitContainingTheOther,
                                circuit => circuit).ToList();

                            circuits.Remove(circuitContainingOne);
                            circuits.Remove(circuitContainingTheOther);

                            circuits.Add(mergedCircuit);
                        }
                    }
                }
            }
        }

        private static List<Coordinates> ConvertToCoordinates(List<string> input)
        {
            var coordinates = new List<Coordinates>(input.Count);

            foreach (var coords in input)
            {
                var values = coords.Split(',');
                coordinates.Add(new Coordinates
                {
                    X = int.Parse(values[0]),
                    Y = int.Parse(values[1]),
                    Z = int.Parse(values[2])
                });
            }

            return coordinates;
        }

        private static double GetDistanceBetween(Coordinates a, Coordinates b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2) + Math.Pow(a.Z - b.Z, 2));
        }

        private readonly struct Coordinates
        {
            public int X { get; init; }
            public int Y { get; init; }
            public int Z { get; init; }

            public override string ToString()
            {
                return $"{X}, {Y}, {Z}";
            }
        }

    }
}

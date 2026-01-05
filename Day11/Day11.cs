using AoC2025.Utils;

namespace AoC2025.Day11
{
    internal static class Day11
    {
        internal static class Part1
        {
            public static void Solve()
            {
                var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day11.txt";
                var input = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);

                //var input = new List<string>
                //{
                //    "aaa: you hhh",
                //    "you: bbb ccc",
                //    "bbb: ddd eee",
                //    "ccc: ddd eee fff",
                //    "ddd: ggg",
                //    "eee: out",
                //    "fff: out",
                //    "ggg: out",
                //    "hhh: ccc fff iii",
                //    "iii: out"
                //};

                var nodes = GetNodesFromInput(input);

                var starterNode = nodes.First(node => node.Value == "you");

                var paths = new List<List<Node>>();

                foreach (var starterOutput in nodes.Where(node => starterNode.Outputs.Contains(node.Value)))
                {
                    var path = new List<Node> { starterNode };
                    TraversePath(starterOutput, path, paths, nodes);
                }
            }

            private static void TraversePath(Node node, List<Node> path, List<List<Node>> paths, List<Node> nodes)
            {
                var pathWithThisNode = new List<Node>();
                pathWithThisNode.AddRange(path);
                pathWithThisNode.Add(node);

                if (node.Outputs.Contains("out"))
                {
                    pathWithThisNode.Add(Node.OutNode);
                    paths.Add(pathWithThisNode);
                }

                foreach (var output in nodes.Where(otherNode => node.Outputs.Contains(otherNode.Value)))
                {
                    TraversePath(output, pathWithThisNode, paths, nodes);
                }
            }
        }

        internal static class Part2
        {
            public static void Solve()
            {
                var dataTxtLocation = "C:\\WS\\AoC2025\\Data\\input_day11.txt";
                var input = InputDataParser.ParseInputTxtLineByLine<List<string>, string>(dataTxtLocation);

                //var input = new List<string>
                //{
                //    "svr: aaa bbb",
                //    "aaa: fft",
                //    "fft: ccc",
                //    "bbb: tty",
                //    "tty: ccc",
                //    "ccc: ddd eee",
                //    "ddd: hub",
                //    "hub: fff",
                //    "eee: dac",
                //    "dac: fff",
                //    "fff: ggg hhh",
                //    "ggg: out",
                //    "hhh: out"
                //};

                var nodes = GetNodesFromInput(input);

                var nodesContainingFft = nodes.Where(node => node.Outputs.Contains("fft"));
                var nodesContainingDac = nodes.Where(node => node.Outputs.Contains("dac"));

                var totalNumberOfPathsContainingBothFftAndDac = 0;

                foreach (var nodeContainingFft in nodesContainingFft)
                {
                    var numberOfPathsContainingBothFftAndDac = 0;

                    var pathsLeadingUpToSvr = new List<List<Node>>();
                    var pathsLeadingDownToOut = new List<List<Node>>();

                    TraverseUpToSvr(nodeContainingFft, [], pathsLeadingUpToSvr, nodes);

                    var pathsLeadingUpToSvrContainingNeitherFftOrDac = pathsLeadingUpToSvr
                        .Where(path => path.All(node => node.Value != "fft" || node.Value != "dac"));

                    // in this case the path to srv contains neither fft or dac
                    // therefore the downward path must contain both to be valid
                    if (pathsLeadingUpToSvrContainingNeitherFftOrDac.Any())
                    {
                        TraverseDownToOut(nodeContainingFft, [], pathsLeadingDownToOut, nodes,
                            filter: path => path.Any(node => node.Value == "fft") &&
                                            path.Any(node => node.Value == "dac"));
                    }

                    var pathsLeadingUpToSvrContainingOnlyFft = pathsLeadingUpToSvr
                        .Where(path => path.Any(node => node.Value == "fft"));

                    // in this case the path to srv contains only fft
                    // therefore the downward path must contain dac to be valid
                    if (pathsLeadingUpToSvrContainingOnlyFft.Any())
                    {
                        TraverseDownToOut(nodeContainingFft, [], pathsLeadingDownToOut, nodes,
                            filter: path => path.Any(node => node.Value == "dac"));
                    }

                    var pathsLeadingUpToSvrContainingOnlyDac = pathsLeadingUpToSvr
                        .Where(path => path.Any(node => node.Value == "dac"));

                    // in this case the path to srv contains only dac
                    // therefore the downward path must contain fft to be valid
                    if (pathsLeadingUpToSvrContainingOnlyDac.Any())
                    {
                        TraverseDownToOut(nodeContainingFft, [], pathsLeadingDownToOut, nodes,
                            filter: path => path.Any(node => node.Value == "fft"));
                    }

                    var pathsLeadingUpToSvrContainingBothFftAndDac = pathsLeadingUpToSvr
                        .Where(path => path.Any(node => node.Value == "fft") &&
                                        path.Any(node => node.Value == "dac"));

                    // in this case the path to srv contains both fft and dac
                    // therefore there is no constraint for the downward path to be valid
                    if (pathsLeadingUpToSvrContainingOnlyDac.Any())
                    {
                        TraverseDownToOut(nodeContainingFft, [], pathsLeadingDownToOut, nodes,
                            filter: null);
                    }

                    foreach (var pathLeadingUpToSvr in pathsLeadingUpToSvr)
                    {
                        foreach (var pathLeadingDownToOut in pathsLeadingDownToOut)
                        {
                            var mergedPath = new List<Node>();
                            for (int j = pathLeadingUpToSvr.Count - 1; j > 0; j--)
                            {
                                mergedPath.Add(pathLeadingUpToSvr[j]);
                            }
                            mergedPath.AddRange(pathLeadingDownToOut);

                            if (mergedPath.Any(node => node.Value == "fft") &&
                                mergedPath.Any(node => node.Value == "dac"))
                            {
                                numberOfPathsContainingBothFftAndDac++;
                            }
                        }
                    }

                    totalNumberOfPathsContainingBothFftAndDac += numberOfPathsContainingBothFftAndDac;
                }
            }

            private static void TraverseUpToSvr(Node node, List<Node> path, List<List<Node>> paths, List<Node> nodes)
            {
                var pathWithThisNode = new List<Node>();
                pathWithThisNode.AddRange(path);
                pathWithThisNode.Add(node);

                var nodesContainingThisNode = nodes.Where(otherNode => otherNode.Outputs.Contains(node.Value));
                var svrNode = nodesContainingThisNode.FirstOrDefault(nodeContainingThisNode => nodeContainingThisNode.Value == "svr");
                
                if (svrNode.Value == "svr")
                {
                    pathWithThisNode.Add(svrNode);
                    paths.Add(pathWithThisNode);
                }

                foreach (var nodeContainingThisNode in nodesContainingThisNode)
                {
                    TraverseUpToSvr(nodeContainingThisNode, path, paths, nodes);
                }
            }

            private static void TraverseDownToOut(Node node, List<Node> path, List<List<Node>> paths, List<Node> nodes, Func<List<Node>, bool>? filter = null)
            {
                var pathWithThisNode = new List<Node>();
                pathWithThisNode.AddRange(path);
                pathWithThisNode.Add(node);

                if (node.Outputs.Contains("out"))
                {
                    if (filter == null)
                    {
                        pathWithThisNode.Add(Node.OutNode);
                        paths.Add(pathWithThisNode);
                    }
                    else
                    {
                        if (filter(pathWithThisNode))
                        {
                            pathWithThisNode.Add(Node.OutNode);
                            paths.Add(pathWithThisNode);
                        }
                    }
                }

                foreach (var output in nodes.Where(otherNode => node.Outputs.Contains(otherNode.Value)))
                {
                    TraverseDownToOut(output, pathWithThisNode, paths, nodes, filter);
                }
            }
        }

        private static List<Node> GetNodesFromInput(List<string> input)
        {
            var nodes = new List<Node>();

            foreach (var line in input)
            {
                var sections = line.Split(':');
                var nodeName = sections[0];
                var outputs = sections[1].Trim().Split(" ");

                nodes.Add(new Node
                {
                    Value = nodeName,
                    Outputs = [.. outputs]
                });
            }

            return nodes;
        }

        private readonly struct Node
        {
            internal static readonly Node OutNode = new Node
            {
                Value = "out",
                Outputs = []
            };

            public string Value { get; init; }

            public List<string> Outputs { get; init; }
        }
    }
}

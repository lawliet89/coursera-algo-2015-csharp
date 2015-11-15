using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using Utilities.Graphs;

namespace SCC
{
    public static class SCC
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Constructing graph");
            var graph = MakeGraphFromFile(
                        $"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/SCC.txt");
            Console.WriteLine("Calculating SCC");
            var sccSizes = TopFiveSCCSize(graph).ToList();
            var found = sccSizes.Count;

            if (found < 5)
            {
                for (var i = found - 1; i < 5; ++i)
                {
                    sccSizes.Add(0);
                }
            }
            Console.WriteLine("Answer: {0}", string.Join(",", sccSizes));
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        public static DirectedGraph<int> MakeGraphFromFile(string path)
        {
            var data = Files.ReadFileToCollection<string>(path);
            var graph = new DirectedGraph<int>();
            foreach (var line in data)
            {
                var lineEntries = line.Split(' ');
                var tailName = Convert.ToInt32(lineEntries[0]);
                var headName = Convert.ToInt32(lineEntries[1]);
                DirectedGraph<int>.Node head, tail;
                if (!graph.Nodes.ContainsKey(tailName))
                {
                    tail = new DirectedGraph<int>.Node(graph)
                    {
                        Name = tailName
                    };
                    graph.Nodes.Add(tailName, tail);
                }
                else
                {
                    tail = graph.Nodes[tailName];
                }
                if (!graph.Nodes.ContainsKey(headName))
                {
                    head = new DirectedGraph<int>.Node(graph)
                    {
                        Name = headName
                    };
                    graph.Nodes.Add(headName, head);
                }
                else
                {
                    head = graph.Nodes[headName];
                }

                var edge = new DirectedGraph<int>.Edge(tail, head);
                graph.Edges.Add(edge);

                tail.OutgoingEdges.Add(edge);
                head.IncomingEdges.Add(edge);
            }
            return graph;
        }

        public static IEnumerable<int> TopFiveSCCSize(DirectedGraph<int> graph)
        {
            var scc = FindSCC(graph);
            return scc.Select(nodes => nodes.Count()).OrderByDescending(n => n).Take(5);
        }

        public static IEnumerable<IEnumerable<DirectedGraph<int>.Node>> FindSCC(DirectedGraph<int> graph)
        {
           
            // Transpose pass
            var firstMetadata = DFSLoop(graph.Nodes.OrderByDescending(p => p.Key).Select(p => p.Value),
                EdgeDirection.Backward);
            var secondMetadata =
                DFSLoop(
                    graph.Nodes.OrderByDescending(p => firstMetadata[p.Key].FinishingTime).Select(p => p.Value),
                    EdgeDirection.Forward);

            var leaders = secondMetadata.Select(p => p.Value.Leader).Distinct();

            return
                leaders.Select(
                    leader => secondMetadata.Where(pair => pair.Value.Leader == leader).Select(pair => pair.Value.Node));
        }

        private static Dictionary<int, NodeMetadata> DFSLoop(IEnumerable<DirectedGraph<int>.Node> nodes, EdgeDirection direction)
        {
            var finishingTime = 0;
            var visitedNodes= new HashSet<DirectedGraph<int>.Node>();
            var metadata = new Dictionary<int, NodeMetadata>();

            foreach (var node in nodes)
            {
                if (visitedNodes.Add(node))
                {
                    finishingTime = DFS(node, visitedNodes, metadata, node, finishingTime, direction);
                }
            }

            return metadata;
        }

        private static int DFS(DirectedGraph<int>.Node node, 
            ISet<DirectedGraph<int>.Node> visitedNodes, Dictionary<int, NodeMetadata> metadata, 
            DirectedGraph<int>.Node leader, int finishingTime, EdgeDirection direction)
        {
            NodeMetadata nodeMetadata;
            if (!metadata.ContainsKey(node.Name))
            {
                nodeMetadata = new NodeMetadata()
                {
                    Node = node
                };
                metadata.Add(node.Name, nodeMetadata);
            }
            else
            {
                nodeMetadata = metadata[node.Name];
            }
            nodeMetadata.Leader = leader;

            var edges = direction == EdgeDirection.Forward
                ? node.OutgoingEdges
                : node.IncomingEdges;

            foreach (var edge in edges)
            {
                var endNode = direction == EdgeDirection.Forward
                    ? edge.Head
                    : edge.Tail;
                if (visitedNodes.Add(endNode))
                {
                    finishingTime = DFS(endNode, visitedNodes, metadata, leader, finishingTime, direction);
                    finishingTime++;
                    nodeMetadata.FinishingTime = finishingTime;
                }
            }
            return finishingTime;
        }

        public enum EdgeDirection { Forward, Backward }
    }
}

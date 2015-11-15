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
            Console.WriteLine("Answer: {0}", string.Join(",", sccSizes)); // 434821,968,459,313,211
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
            return scc.Select(p => p.Value.Leader).Distinct()
                .Select(leader => GetMetadata(scc, leader).Followers.Count)
                .OrderByDescending(n => n)
                .Take(5);
        }

        public static Dictionary<int, NodeMetadata> FindSCC(DirectedGraph<int> graph)
        {
           
            // Transpose pass
            var firstMetadata = DFSLoop(graph.Nodes.OrderByDescending(p => p.Key).Select(p => p.Value),
                EdgeDirection.Backward);
            var secondMetadata =
                DFSLoop(
                    graph.Nodes.OrderByDescending(p => firstMetadata[p.Key].FinishingTime).Select(p => p.Value),
                    EdgeDirection.Forward);

            return secondMetadata;
        }

        private static Dictionary<int, NodeMetadata> DFSLoop(IEnumerable<DirectedGraph<int>.Node> nodes, EdgeDirection direction)
        {
            var finishingTime = 0;
            var visitedNodes= new HashSet<DirectedGraph<int>.Node>();
            var metadata = new Dictionary<int, NodeMetadata>();

            foreach (var node in nodes)
            {
                finishingTime = DFS(node, visitedNodes, metadata, finishingTime, direction);
            }

            return metadata;
        }

        private static int DFS(DirectedGraph<int>.Node leader, 
            ISet<DirectedGraph<int>.Node> visitedNodes, Dictionary<int, NodeMetadata> metadata, 
            int finishingTime, EdgeDirection direction)
        {
            if (!visitedNodes.Add(leader))
                return finishingTime;

            var searchStack = new Stack<DirectedGraph<int>.Node>();
            searchStack.Push(leader);

            var leaderMetadata = GetMetadata(metadata, leader);
            while (searchStack.Count > 0)
            {
                var node = searchStack.Peek();
                var nodeMetadata = GetMetadata(metadata, node);
                nodeMetadata.Leader = leader;
                leaderMetadata.Followers.Add(node);

                var edges = direction == EdgeDirection.Forward
                    ? node.OutgoingEdges
                    : node.IncomingEdges;

                var edge = edges.FirstOrDefault(e => !visitedNodes.Contains(direction == EdgeDirection.Forward ? e.Head : e.Tail));
                if (edge == null)
                {
                    finishingTime++;
                    nodeMetadata.FinishingTime = finishingTime;
                    searchStack.Pop();
                }
                else
                {
                    var endNode = direction == EdgeDirection.Forward
                            ? edge.Head
                            : edge.Tail;
                    if (visitedNodes.Add(endNode))
                    {
                        searchStack.Push(endNode);
                    }
                }

            }
            
            return finishingTime;
        }

        private static NodeMetadata GetMetadata(IDictionary<int, NodeMetadata> metadata, DirectedGraph<int>.Node node)
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
            return nodeMetadata;
        }

        public enum EdgeDirection { Forward, Backward }
    }
}

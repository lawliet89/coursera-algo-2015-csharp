using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using Utilities.Graphs;

namespace Dijkstra
{
    class Dijkstra
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Constructing graph");
            var graph = MakeGraphFromFile(
                        $"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/dijkstraData.txt");
            var verticesOfInterest = new []
            {
                "7",
                "37",
                "59",
                "82",
                "99",
                "115",
                "133",
                "165",
                "188",
                "197"
            };
            Console.WriteLine("Calculating shortest path");
            var shortestPath = ShortestPath(graph, graph.Vertices["1"]);
            var distances = new List<int>(10);
            distances.AddRange(verticesOfInterest.Select(vertex => shortestPath[graph.Vertices[vertex]]));
            Console.WriteLine(string.Join(",", distances)); // 2599,2610,2947,2052,2367,2399,2029,2442,2505,3068
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        public static UndirectedGraph MakeGraphFromFile(string path)
        {
            var data = Files.ReadFileToCollection<string>(path);
            var graph = new UndirectedGraph();
            foreach (var line in data)
            {
                var lineEntries = line.Split('\t');
                var nodeName = lineEntries[0];
                var node = GetOrMakeNode(graph, nodeName);
                foreach (var pair in lineEntries.Skip(1).Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    var split = pair.Split(',');
                    var otherName = split[0];
                    var length = Convert.ToInt32(split[1]);
                    var otherNode = GetOrMakeNode(graph, otherName);
                    var edge = new UndirectedGraph.Edge(node, otherNode)
                    {
                        Length = length
                    };
                    graph.Edges.Add(edge);
                    node.Edges.Add(edge);
                    otherNode.Edges.Add(edge);
                }
            }
            return graph;
        }

        public static UndirectedGraph.Vertex GetOrMakeNode(UndirectedGraph graph, string name)
        {
            UndirectedGraph.Vertex node;
            if (!graph.Vertices.ContainsKey(name))
            {
                node = new UndirectedGraph.Vertex(name);
                graph.Vertices.Add(name, node);
            }
            else
            {
                node = graph.Vertices[name];
            }
            return node;
        }

        public static Dictionary<UndirectedGraph.Vertex, int> ShortestPath(UndirectedGraph graph,
            UndirectedGraph.Vertex source)
        {
            var result = new Dictionary<UndirectedGraph.Vertex, int>(graph.Vertices.Count)
            {
                [source] = 0
            };

            var vertices = new List<UndirectedGraph.Vertex>(graph.Vertices.Values);
            while (vertices.Count > 0)
            {
                var sourceNode = vertices
                    .OrderBy(v => result.TryOrGetDefault(v, int.MaxValue))
                    .First();
                vertices.Remove(sourceNode);
                var sourceNodeDistance = result[sourceNode];
                foreach (var edge in sourceNode.Edges)
                {
                    var distance = sourceNodeDistance + edge.Length;
                    var otherNode = edge.Left == sourceNode ? edge.Right : edge.Left;
                    if (distance < result.TryOrGetDefault(otherNode, int.MaxValue))
                    {
                        if (result.ContainsKey(otherNode))
                            result[otherNode] = distance;
                        else
                        {
                            result.Add(otherNode, distance);
                        }
                    }
                }
            }
            return result;
        } 
    }
}

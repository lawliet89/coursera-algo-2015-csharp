using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Utilities.Graphs
{
    public class UndirectedGraph
    {
        public Dictionary<string, Vertex> Vertices { get; private set; }
        public List<Edge> Edges { get; private set; }

        private UndirectedGraph()
        {
            Vertices = new Dictionary<string, Vertex>();
            Edges = new List<Edge>();
        }

        public static UndirectedGraph RandomContraction(UndirectedGraph graph)
        {
            var random = new Random();
            while (graph.Vertices.Count > 2)
            {
                var edge = graph.Edges[random.Next(graph.Edges.Count)];
                var left = edge.Left;
                var right = edge.Right;

                Assert.IsTrue(graph.Vertices.Remove(right.Name));
                left.SuperVertices.AddRange(right.SuperVertices);

                foreach (var loop in graph.Edges.Where(e => e.HasVertices(left, right)).ToList())
                {
                    graph.Edges.Remove(loop);
                }

                foreach (var danglingEdge in graph.Edges.Where(e => e.Left == right).ToList())
                {
                    danglingEdge.Left = left;
                }

                foreach (var danglingEdge in graph.Edges.Where(e => e.Right == right).ToList())
                {
                    danglingEdge.Right = left;
                }

                // Some assertions
                Assert.IsEmpty(graph.Edges.Where(e => e.HasVertices(left, right)));
                Assert.IsEmpty(graph.Edges.Where(e => e.Left == right || e.Right == right));
            }
            return graph;
        }

        public static UndirectedGraph MakeGraphFromFile(string path)
        {
            var data = Files.ReadFileToCollection<string>(path);
            var graph = new UndirectedGraph();

            foreach (var line in data)
            {
                var lineEntries = line.Split('\t');
                var name = lineEntries[0];
                Vertex vertex;
                if (!graph.Vertices.ContainsKey(name))
                {
                    vertex = new Vertex(name);
                    graph.Vertices.Add(name, vertex);
                }
                else
                {
                    vertex = graph.Vertices[name];
                }

                foreach (var otherVertexName in lineEntries.Skip(1))
                {
                    Vertex otherVertex;
                    if (!graph.Vertices.ContainsKey(otherVertexName))
                    {
                        otherVertex = new Vertex(otherVertexName);
                        graph.Vertices.Add(otherVertexName, otherVertex);
                    }
                    else
                    {
                        otherVertex = graph.Vertices[otherVertexName];
                    }


                    Edge edge = graph.Edges.FirstOrDefault(e => e.HasVertices(vertex, otherVertex));
                    if (edge == null)
                    {
                        edge = new Edge(vertex, otherVertex);
                        graph.Edges.Add(edge);
                    }

                    if (vertex.Edges.SingleOrDefault(e => e == edge) == null)
                        vertex.Edges.Add(edge);

                    if (otherVertex.Edges.SingleOrDefault(e => e == edge) == null)
                        otherVertex.Edges.Add(edge);
                }
            }

            return graph;
        }

        public class Edge
        {
            public Vertex Left { get; set; }
            public Vertex Right { get; set; }

            public Edge(Vertex a, Vertex b)
            {
                Left = a;
                Right = b;
            }

            public bool HasVertices(Vertex a, Vertex b)
            {
                return (Left == a && Right == b)
                       || (Left == b && Right == a);
            }

        }

        public class Vertex
        {
            public string Name { get; set; }
            public List<Edge> Edges { get; }
            public List<Vertex> SuperVertices { get; set; }

            public Vertex(string name)
            {
                Name = name;
                Edges = new List<Edge>();
                SuperVertices = new List<Vertex> {this};
            }

            public override string ToString()
            {
                return string.Join(" ", SuperVertices.Select(v => v.Name));
            }
        }

        public class SuperVertex
        {
            public List<Vertex> Vertices { get; }

            public SuperVertex()
            {
                Vertices = new List<Vertex>();
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Utilities.Graphs
{
    public class DirectedGraph<TName>
    {

        public Dictionary<TName, Node> Nodes { get; }
        public List<Edge> Edges { get; }

        public DirectedGraph()
        {
            Nodes = new Dictionary<TName, Node>();
            Edges = new List<Edge>();
        }

        public class Node
        {
            public TName Name { get; set; }
            public DirectedGraph<TName> Graph { get; }

            public Node(DirectedGraph<TName> graph)
            {
                Graph = graph;
            }

            public IEnumerable<Edge> OutgoingEdges
            {
                get { return Graph.Edges.Where(e => e.Tail == this); }
            }

            public IEnumerable<Edge> IncomingEdges
            {
                get { return Graph.Edges.Where(e => e.Head == this); }
            }

            public IEnumerable<Node> OutgoingNodes
            {
                get { return OutgoingEdges.Select(e => e.Head).Distinct(); }
            }

            public IEnumerable<Node> IncomingNodes
            {
                get { return IncomingEdges.Select(e => e.Tail).Distinct(); }
            } 
        }

        public class Edge
        {
            public Node Tail { get; }
            public Node Head { get; }

            public Edge(Node tail, Node head)
            {
                Tail = tail;
                Head = head;
            }
        }
    }
}

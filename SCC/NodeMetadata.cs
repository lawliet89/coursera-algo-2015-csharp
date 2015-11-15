using System.Collections.Generic;
using NUnit.Framework;
using Utilities.Graphs;

namespace SCC
{
    public class NodeMetadata
    {
        public DirectedGraph<int>.Node Node { get; set; }
        public int FinishingTime { get; set; }
        public DirectedGraph<int>.Node Leader { get; set; }
        public HashSet<DirectedGraph<int>.Node> Followers { get; }

        public NodeMetadata()
        {
            Followers = new HashSet<DirectedGraph<int>.Node>();
        }
    }
}

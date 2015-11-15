using Utilities.Graphs;

namespace SCC
{
    public class NodeMetadata
    {
        public DirectedGraph<int>.Node Node;
        public int FinishingTime;
        public DirectedGraph<int>.Node Leader;
    }
}

using System;
using Utilities.Graphs;

namespace RandomContraction
{
    class MinCut
    {
        static void Main(string[] args)
        {
            var minCut = MinimumCutFromFile("kargerMinCut.txt");
            Console.WriteLine("Minimum Cut is probably: {0}", minCut);
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        public static long MinimumCutFromFile(string path)
        {
            var minCut = long.MaxValue;
            for (var i = 0; i < 100; ++i)
            {
                var graph =
                    UndirectedGraph.MakeGraphFromFile(
                        $"{AppDomain.CurrentDomain.BaseDirectory}/../../../Blobs/{path}");
                var contracted = UndirectedGraph.RandomContraction(graph);
                var noEdges = contracted.Edges.Count;
                Console.WriteLine("Attempt {0}: {1} edges", i, noEdges);
                if (noEdges < minCut)
                {
                    Console.WriteLine("\tNew Minimum!");
                    minCut = noEdges;
                }
            }
            return minCut;
        }
    }
}

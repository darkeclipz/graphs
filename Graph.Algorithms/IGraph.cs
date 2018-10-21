using System.Collections.Generic;

namespace GraphAlgorithms
{
    public interface IGraph
    {
        int Edges { get; }
        bool ParallelEdgesOrSelfLoopsAllowed { get; }
        int Vertices { get; }
        void AddEdge(int v, int w);
        IEnumerable<int> Adjacent(int v);
        string ToString();
    }
}
using System.Collections.Generic;

namespace GraphAlgorithms
{
    public interface IGraph
    {
        int E { get; }
        bool ParallelEdgesOrSelfLoopsAllowed { get; }
        int V { get; }
        void AddEdge(int v, int w);
        IEnumerable<int> Adjacent(int v);
        string ToString();
    }
}
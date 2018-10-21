using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class Cycle
    {
        private readonly List<bool> _marked;

        public Cycle(Graph g)
        {
            if (g.ParallelEdgesOrSelfLoopsAllowed)
                throw new ParallelEdgesOrSelfLoopsAllowedException("Parallel edges or self-loops are not allowed for cyclic graph detection.");

            _marked = new List<bool>(new bool[g.Vertices]);
            for (var s = 0; s < g.Vertices; s++)
                if (!_marked[s])
                    DFS(g, s, s);
        }

        private void DFS(Graph g, int v, int u)
        {
            _marked[v] = true;
            foreach (var w in g.Adjacent(v))
                if (!_marked[w])
                    DFS(g, w, v);
                else if (w != u)
                    HasCycle = true;
        }

        public bool HasCycle { get; private set; }
    }

    public class ParallelEdgesOrSelfLoopsAllowedException : Exception { public ParallelEdgesOrSelfLoopsAllowedException(string message) : base(message) { } };
}

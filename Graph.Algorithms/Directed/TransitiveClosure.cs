using GraphAlgorithms.Undirected;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Directed
{
    public class TransitiveClosure
    {
        private readonly List<DirectedDFS> _all;

        public TransitiveClosure(Digraph g)
        {
            _all = new List<DirectedDFS>();
            for (int v = 0; v < g.V; v++)
                _all.Add(new DirectedDFS(g, v));
        }

        public TransitiveClosure(SymbolDigraph g) : this(g.Digraph) { }

        public bool Reachable(int v, int w) => _all[v].Marked(w);

    }
}

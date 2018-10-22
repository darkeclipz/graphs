using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class EdgeWeightedGraph
    {
        private List<List<Edge>> _adj = new List<List<Edge>>();

        public EdgeWeightedGraph(int vertices)
        {
            V = vertices;
            E = 0;
            for (int v = 0; v < V; v++)
                _adj.Add(new List<Edge>());
        }

        public int V { get; }
        public int E { get; private set; }

        public void AddEdge(Edge e)
        {
            int v = e.Either(), w = e.Other(v);
            _adj[v].Add(e);
            _adj[w].Add(e);
            E++;
        }

        public IEnumerable<Edge> Adjacent(int v)
        {
            foreach (var w in _adj[v])
                yield return w;
        }

        public IEnumerable<Edge> Edges()
        {
            for (int v = 0; v < V; v++)
                foreach (var e in Adjacent(v))
                    if (e.Other(v) > v) yield return e;
        }
    }
}

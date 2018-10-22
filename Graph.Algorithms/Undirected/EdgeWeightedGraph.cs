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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Edge weighted graph (V: {V}, E: {E})");
            foreach(var e in Edges())
            {
                var v = e.Either();
                var w = e.Other(v);
                sb.AppendLine($"Edge {v} to {w} with weight {e.Weight}");
            }
            return sb.ToString();
        }
    }
}

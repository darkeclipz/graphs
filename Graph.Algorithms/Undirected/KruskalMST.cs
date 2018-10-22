using GraphAlgorithms.Searching;
using GraphAlgorithms.Sorting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class KruskalMST
    {
        private readonly Queue<Edge> _mst;

        public KruskalMST(EdgeWeightedGraph g)
        {
            _mst = new Queue<Edge>();
            var pq = new MinPQ<Edge>(g.E);
            foreach (var e in g.Edges())
                pq.Insert(e);
            var uf = new UF(g.V);

            while(!pq.IsEmpty && _mst.Count < g.V - 1)
            {
                var e = (Edge)pq.DeleteMin();
                int v = e.Either(), w = e.Other(v);
                if (uf.Connected(v, w)) continue;
                uf.Union(v, w);
                _mst.Enqueue(e);
            }
        }

        public IEnumerable<Edge> Edges() => _mst;

        public double Weight()
        {
            double t = 0f;
            foreach (var e in Edges())
                t += e.Weight;
            return t;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Minimum spanning tree (Kruskal, E: {_mst.Count})");
            double t = 0;
            foreach (var e in Edges())
            {
                var v = e.Either();
                var w = e.Other(v);
                t += e.Weight;
                sb.AppendLine($"Edge {v} to {w} with weight {e.Weight}");
            }
            sb.AppendLine($"Total: {t}");
            return sb.ToString();
        }
    }
}

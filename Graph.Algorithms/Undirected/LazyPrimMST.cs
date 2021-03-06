﻿using GraphAlgorithms.Sorting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class LazyPrimMST
    {
        private readonly List<bool> _marked;
        private readonly Queue<Edge> _mst;
        private readonly MinPQ<Edge> _pq;

        public LazyPrimMST(EdgeWeightedGraph g)
        {
            _pq = new MinPQ<Edge>(g.E);
            _marked = new List<bool>(new bool[g.V]);
            _mst = new Queue<Edge>();

            Visit(g, 0);
            while (!_pq.IsEmpty)
            {
                var e = (Edge)_pq.DeleteMin();
                int v = e.Either(), w = e.Other(v);
                if (_marked[v] && _marked[w]) continue;
                _mst.Enqueue(e);
                if (!_marked[v]) Visit(g, v);
                if (!_marked[w]) Visit(g, w);
            }
        }

        private void Visit(EdgeWeightedGraph g, int v)
        {
            _marked[v] = true;
            foreach (var e in g.Adjacent(v))
                if (!_marked[e.Other(v)]) _pq.Insert(e);
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
            sb.AppendLine($"Minimum spanning tree (LazyPrim, E: {_mst.Count})");
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

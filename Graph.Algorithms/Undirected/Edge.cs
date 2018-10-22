using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class Edge : IComparable<Edge>
    {
        private readonly int _v;
        private readonly int _w;

        public Edge(int v, int w, double weight)
        {
            _v = v;
            _w = w;
            Weight = weight;
        }

        public double Weight { get; }

        public int Either() => _v;
        public int Other(int v)
        {
            if (v == _v) return _w;
            else if (v == _w) return _v;
            else throw new InconsistentEdgeException($"Vertex {v} is not in this edge. (v: {_v}, w: {_w})");
        }

        public int CompareTo(Edge other)
        {
            if (Weight < other.Weight) return -1;
            else if (Weight > other.Weight) return 1;
            else return 0;
        }
    }
}

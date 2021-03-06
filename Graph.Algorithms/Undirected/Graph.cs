﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class Graph : IGraph
    {
        private List<List<int>> _adj = new List<List<int>>();
        private readonly bool _allowSelfLoops;
        private readonly bool _allowParallelEdges;

        public Graph(int vertices, bool allowSelfLoops = true, bool allowParallelEdges = true)
        {
            V = vertices;
            _allowSelfLoops = allowSelfLoops;
            _allowParallelEdges = allowParallelEdges;

            for (int i = 0; i < vertices; i++) 
                _adj.Add(new List<int>());
        }

        public int V { get; }
        public int E { get; private set; }

        public void AddEdge(int v, int w)
        {
            if (!_allowParallelEdges && _adj[v].Contains(w))
                throw new ParallelEdgeException($"Unable to add an edge from {v} to {w} because it would create a parallel edge.");

            if (!_allowSelfLoops && v == w)
                throw new SelfLoopException($"Unable to add an edge from {v} to {w} because it would create a self loop.");

            _adj[v].Add(w);
            _adj[w].Add(v);
            E++;
        }

        public bool HasEdge(int v, int w) => _adj[v].Contains(w);

        public IEnumerable<int> Adjacent(int v)
        {
            foreach (int i in _adj[v])
                yield return i;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _adj.Count; i++)
            {
                sb.Append($"{i}:");
                foreach (var edge in _adj[i])
                    sb.Append($" {edge}");
                sb.Append($" (degree: {Degree(this, i)})");
                sb.Append("\r\n");
            }
            sb.AppendLine($"Max degree: {MaxDegree(this)}");
            sb.AppendLine($"Average degree: {AverageDegree(this)}");
            return sb.ToString();
        }

        public static int Degree(Graph g, int v)
        {
            var degree = 0;

            foreach (var edge in g.Adjacent(v))
                degree++;

            return degree;
        }

        public static int MaxDegree(Graph g)
        {
            var maxDegree = 0;

            for (var i = 0; i < g.V; i++)
                maxDegree = Math.Max(maxDegree, Degree(g, i));

            return maxDegree;
        }

        public static double AverageDegree(Graph g)
        {
            return 2 * (double)g.E / g.V;
        }

        public bool ParallelEdgesOrSelfLoopsAllowed
        {
            get => _allowSelfLoops || _allowParallelEdges;
        }
    }
}

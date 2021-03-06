﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Directed
{
    public class Digraph : IGraph
    {
        private List<List<int>> _adj = new List<List<int>>();
        private readonly bool _allowSelfLoops;
        private readonly bool _allowParallelEdges;

        public Digraph(int vertices, bool allowSelfLoops = true, bool allowParallelEdges = true)
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
            if (v >= V)
                throw new VertexIndexOutOfRangeException($"Unable to add edge because vertex {v} doesn't exist (total vertices: {V}).");

            if (!_allowParallelEdges && _adj[v].Contains(w))
                throw new ParallelEdgeException($"Unable to add an edge from {v} to {w} because it would create a parallel edge.");

            if (!_allowSelfLoops && v == w)
                throw new SelfLoopException($"Unable to add an edge from {v} to {w} because it would create a self loop.");

            _adj[v].Add(w);
            E++;
        }

        public bool HasEdge(int v, int w) => _adj[v].Contains(w);

        public IEnumerable<int> Adjacent(int v)
        {
            foreach (int i in _adj[v])
                yield return i;
        }

        public Digraph Reverse()
        {
            var reversedGraph = new Digraph(V, _allowSelfLoops, _allowParallelEdges);
            for (int v = 0; v < V; v++)
                foreach (var w in Adjacent(v))
                    reversedGraph.AddEdge(w, v);
            return reversedGraph;
        }

        public static int DegreeOut(Digraph g, int v)
        {
            int degree = 0;
            foreach (var w in g.Adjacent(v)) degree++;
            return degree;
        }

        public static int MaxDegreeOut(Digraph g)
        {
            int maxDegreeOut = 0;
            for (var i = 0; i < g.V; i++)
                maxDegreeOut = Math.Max(maxDegreeOut, DegreeOut(g, i));
            return maxDegreeOut;
        }

        public static double AverageDegreeOut(Digraph g)
        {
            return (double)g.E / g.V;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _adj.Count; i++)
            {
                sb.Append($"{i}:");
                foreach (var edge in _adj[i])
                    sb.Append($" {edge}");
                sb.Append($" (degree out: {DegreeOut(this, i)})");
                sb.Append("\r\n");
            }
            sb.AppendLine($"Max degree out: {MaxDegreeOut(this)}");
            sb.AppendLine($"Average degree out: {AverageDegreeOut(this)}");
            return sb.ToString();
        }

        public bool ParallelEdgesOrSelfLoopsAllowed
        {
            get => _allowSelfLoops || _allowParallelEdges;
        }
    }
}

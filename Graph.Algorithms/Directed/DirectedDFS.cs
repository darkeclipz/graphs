using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Directed
{
    public class DirectedDFS
    {
        private readonly List<bool> _marked;
        private readonly Digraph _dg;
        private readonly List<int> _sources;

        public DirectedDFS(Digraph g, int s)
        {
            _dg = g;
            _marked = new List<bool>(new bool[g.Vertices]);
            _sources = new List<int>();
            _sources.Add(s);
            DFS(g, s);
        }

        public DirectedDFS(Digraph g, List<int> sources)
        {
            _dg = g;
            _marked = new List<bool>(new bool[g.Vertices]);
            _sources = sources;
            foreach (var s in sources)
                if(!_marked[s]) DFS(g, s);
        }

        private void DFS(Digraph g, int v)
        {
            _marked[v] = true;
            foreach (var w in g.Adjacent(v))
                if (!_marked[w]) DFS(g, w);
        }

        public bool Marked(int v)
        {
            return _marked[v];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Directed DFS:");
            sb.AppendLine("Source(s): " + String.Join(", ", _sources));
            for(int v = 0; v < _marked.Count; v++)
            {
                sb.AppendLine($"{v} is {(_marked[v] ? "reachable" : "not reachable")}.");
            }
            return sb.ToString();
        }
    }

}

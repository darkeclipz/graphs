using GraphAlgorithms.Directed;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Directed
{
    public class SymbolDigraph
    {
        private Dictionary<string, int> _st;
        private List<string> _keys;

        public SymbolDigraph(List<Tuple<string, string>> input, bool allowSelfLoops = true, bool allowParallelEdges = true)
        {
            _st = new Dictionary<string, int>();
            foreach(var connection in input)
            {
                if (!_st.ContainsKey(connection.Item1))
                    _st.Add(connection.Item1, _st.Count);

                if (!_st.ContainsKey(connection.Item2))
                    _st.Add(connection.Item2, _st.Count);
            }
            _keys = new List<string>();
            foreach (var key in _st.Keys)
                _keys.Add(key);
            Digraph = new Digraph(_st.Count, allowSelfLoops, allowParallelEdges);
            foreach(var connection in input)
            {
                int v = _st[connection.Item1];
                int u = _st[connection.Item2];
                Digraph.AddEdge(v, u);
            }
        }

        public bool Contains(string s) => _st.ContainsKey(s);
        public IEnumerable<string> Keys => _keys;
        public int Index(string s) => _st[s];
        public string Name(int v) => _keys[v];
        public Digraph Digraph { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Digraph.V; i++)
            {
                sb.Append($"{Name(i)}:");
                foreach (var edge in Digraph.Adjacent(i))
                    sb.Append($" {Name(edge)}");
                sb.Append($" (degree (out): {Digraph.DegreeOut(Digraph, i)})");
                sb.Append("\r\n");
            }
            sb.AppendLine($"Max degree (out): {Digraph.MaxDegreeOut(Digraph)}");
            sb.AppendLine($"Average degree (out): {Digraph.AverageDegreeOut(Digraph)}");
            return sb.ToString();
        }
    }
}

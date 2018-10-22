using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class SymbolGraph
    {
        private Dictionary<string, int> _st;
        private List<string> _keys;

        public SymbolGraph(List<Tuple<string, string>> input, bool allowSelfLoops = true, bool allowParallelEdges = true)
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
            Graph = new Graph(_st.Count, allowSelfLoops, allowParallelEdges);
            foreach(var connection in input)
            {
                int v = _st[connection.Item1];
                int u = _st[connection.Item2];
                Graph.AddEdge(v, u);
            }
        }

        public bool Contains(string s) => _st.ContainsKey(s);
        public IEnumerable<string> Keys => _keys;
        public int Index(string s) => _st[s];
        public string Name(int v) => _keys[v];
        public Graph Graph { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Graph.V; i++)
            {
                sb.Append($"{Name(i)}:");
                foreach (var edge in Graph.Adjacent(i))
                    sb.Append($" {Name(edge)}");
                sb.Append($" (degree: {Graph.Degree(Graph, i)})");
                sb.Append("\r\n");
            }
            sb.AppendLine($"Max degree: {Graph.MaxDegree(Graph)}");
            sb.AppendLine($"Average degree: {Graph.AverageDegree(Graph)}");
            return sb.ToString();
        }
    }
}

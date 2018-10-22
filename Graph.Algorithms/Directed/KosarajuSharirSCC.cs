using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgorithms.Directed
{
    public class KosarajuSharirSCC
    {
        private readonly List<bool> _marked;
        private readonly List<int> _id;

        public KosarajuSharirSCC(Digraph g)
        {
            _marked = new List<bool>(new bool[g.V]);
            _id = new List<int>(new int[g.V]);
            var order = new DepthFirstOrder(g.Reverse());
            foreach(var s in order.ReversePost())
                if (!_marked[s])
                {
                    DFS(g, s);
                    Count++;
                }
        }

        private void DFS(Digraph g, int v)
        {
            _marked[v] = true;
            _id[v] = Count;
            foreach (var w in g.Adjacent(v))
                if (!_marked[w])
                    DFS(g, w);
        }

        public bool StronglyConnected(int v, int w) => _id[v] == _id[w];
        public int Id(int v) => _id[v];
        public int Count { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Strongly connected components (SCC):");
            for(var i = 0; i < Count; i++)
            {
                sb.Append($"{i}: ");

                var component = new List<int>();
                for (var j = 0; j < _id.Count; j++)
                    if (_id[j] == i)
                        component.Add(j);

                sb.AppendLine(String.Join(" ", component));
            }
            
            sb.Append($"There are {Count} strongly connected components.");
            return sb.ToString();
        }
    }
}

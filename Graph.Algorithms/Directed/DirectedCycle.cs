using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Directed
{
    public class DirectedCycle
    {
        private readonly List<bool> _marked;
        private readonly List<int> _edgeTo;
        private Stack<int> _cycle;
        private readonly List<bool> _onStack;

        public DirectedCycle(Digraph g)
        {
            _onStack = new List<bool>(new bool[g.V]);
            _edgeTo = new List<int>(new int[g.V]);
            _marked = new List<bool>(new bool[g.V]);
            for (int v = 0; v < g.V; v++)
                if (!_marked[v]) DFS(g, v);
        }

        private void DFS(Digraph g, int v)
        {
            _onStack[v] = true;
            _marked[v] = true;
            foreach (var w in g.Adjacent(v))
                if (HasCycle()) return;
                else if (!_marked[w])
                {
                    _edgeTo[w] = v;
                    DFS(g, w);
                }
                else if (_onStack[w])
                {
                    _cycle = new Stack<int>();
                    for (int x = v; x != w; x = _edgeTo[x])
                        _cycle.Push(x);
                    _cycle.Push(w);
                    _cycle.Push(v);
                }
            _onStack[v] = false;
         }

        public bool HasCycle() => _cycle != null;
        public IEnumerable<int> Cycle() => _cycle;
    }
}

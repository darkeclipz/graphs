using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class DepthFirstPaths
    {
        private readonly List<bool> _marked;
        private readonly List<int> _edgeTo;
        private readonly int _source;
        private readonly bool _detailedTrace;

        public DepthFirstPaths(Graph g, int s, bool detailedTrace = false)
        {
            _marked = new List<bool>(new bool[g.Vertices]);
            _edgeTo = new List<int>(new int[g.Vertices]);
            _source = s;
            _detailedTrace = detailedTrace;
            if(detailedTrace)
            {
                Console.WriteLine("DFS - detailed trace:");
            }
            DFS(g, s);
        }

        private void DFS(Graph g, int v, int depth = 0)
        {
            if (_detailedTrace) Console.WriteLine("".PadLeft(depth * 2, ' ') + $"dfs({v})");

            _marked[v] = true;

            foreach(var w in g.Adjacent(v))
            {
                if (!_marked[w])
                {
                    _edgeTo[w] = v;
                    DFS(g, w, depth + 1);
                }
                else if(_detailedTrace)
                {
                    Console.WriteLine("".PadLeft(depth * 2, ' ') + $"check {w}");
                }
            }

            if (_detailedTrace) Console.WriteLine("".PadLeft(depth * 2, ' ') + $"done {v}");
        }

        public bool HasPathTo(int v) => _marked[v];

        public IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Stack<int>();

            for(int x = v; x != _source; x = _edgeTo[x])
                path.Push(x);

            path.Push(_source);
            return path;
        }
    } 
}

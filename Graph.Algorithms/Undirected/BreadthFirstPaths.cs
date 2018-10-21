using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class BreadthFirstPaths
    {
        private readonly List<bool> _marked;
        private readonly List<int> _edgeTo;
        private readonly int _source;

        public BreadthFirstPaths(Graph g, int s)
        {
            _marked = new List<bool>(new bool[g.Vertices]);
            _edgeTo = new List<int>(new int[g.Vertices]);
            _source = s;

            BFS(g, s);
        }

        private void BFS(Graph g, int s, int depth = 0)
        {
            var queue = new Queue<int>();
            _marked[s] = true;
            queue.Enqueue(s);
            while(queue.Count > 0)
            {
                var v = queue.Dequeue();

                foreach (var w in g.Adjacent(v))
                {
                    if(!_marked[w])
                    {
                        _edgeTo[w] = v;
                        _marked[w] = true;
                        queue.Enqueue(w);
                    }
                }
            }
        }

        public bool HasPathTo(int v) => _marked[v];

        public IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Stack<int>();

            for (int x = v; x != _source; x = _edgeTo[x])
                path.Push(x);

            path.Push(_source);
            return path;
        }

        public int DistanceTo(int v)
        {
            var distance = 0;
            foreach (var u in PathTo(v)) distance++;
            return distance;
        }
    }
}

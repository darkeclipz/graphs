using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Directed
{
    public class DepthFirstOrder
    {
        private readonly List<bool> _marked;

        private readonly Queue<int> _pre;
        private readonly Queue<int> _post;
        private readonly Stack<int> _reversePost;

        public DepthFirstOrder(Digraph g)
        {
            _pre = new Queue<int>();
            _post = new Queue<int>();
            _reversePost = new Stack<int>();
            _marked = new List<bool>(new bool[g.V]);

            for(int v=0; v < g.V; v++)
                if(!_marked[v]) DFS(g, v);
        }

        private void DFS(Digraph g, int v)
        {
            _pre.Enqueue(v);

            _marked[v] = true;
            foreach (var w in g.Adjacent(v))
                if (!_marked[w]) DFS(g, w);

            _post.Enqueue(v);
            _reversePost.Push(v);
        }

        public IEnumerable<int> Pre() => _pre;
        public IEnumerable<int> Post() => _post;
        public IEnumerable<int> ReversePost() => _reversePost;
    }
}

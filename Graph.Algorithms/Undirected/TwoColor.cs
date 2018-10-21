using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class TwoColor
    {
        private List<bool> _marked;
        private List<bool> _color;

        public TwoColor(Graph g)
        {
            _marked = new List<bool>(new bool[g.Vertices]);
            _color = new List<bool>(new bool[g.Vertices]);
            for(var s=0; s < g.Vertices; s++)
            {
                if (!_marked[s])
                    DFS(g, s);
            }
        }

        private void DFS(Graph g, int v)
        {
            _marked[v] = true;
            foreach(var w in g.Adjacent(v))
            {
                if (!_marked[w])
                {
                    _color[w] = !_color[v];
                    DFS(g, w);
                }
                else if (_color[w] == _color[v]) IsBipartite = false;
            }
        }

        public bool IsBipartite { get; private set; } = true;
    }
}

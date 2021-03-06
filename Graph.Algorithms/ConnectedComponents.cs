﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms
{
    public class ConnectedComponents
    {
        private readonly List<bool> _marked;
        private readonly List<int> _id;

        public ConnectedComponents(IGraph g)
        {
            _marked = new List<bool>(new bool[g.V]);
            _id = new List<int>(new int[g.V]);
            for(int s = 0; s < g.V; s++)
                if(!_marked[s])
                {
                    DFS(g, s);
                    Count++;
                }
            
        }

        private void DFS(IGraph g, int v)
        {
            _marked[v] = true;
            _id[v] = Count;

            foreach (var w in g.Adjacent(v))
                if (!_marked[w])
                    DFS(g, w);
        }

        public bool Connected(int v, int w) => _id[v] == _id[w];
        public int Id(int v) => _id[v];
        public int Count { get; }
        public bool IsConnected
        {
            get => Count == 1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Connected components ({Count} component{(Count != 1 ? "s" : "")}):");
            for(var i = 0; i < Count; i++)
            {
                sb.Append($"{i}:");
                for(var id = 0; id < _id.Count; id++)
                {
                    if (_id[id] == i) sb.Append($" {id}");
                }
                sb.AppendLine();
            }
            sb.AppendLine($"The graph is {(Count == 1 ? "connected" : "disconnected")}.");
            return sb.ToString();
        }

    }
}

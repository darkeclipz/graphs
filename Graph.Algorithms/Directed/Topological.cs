using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Directed
{
    public class Topological
    {
        private IEnumerable<int> _order;

        public Topological(Digraph g)
        {
            var cycleFinder = new DirectedCycle(g);
            if (!cycleFinder.HasCycle())
            {
                var dfs = new DepthFirstOrder(g);
                _order = dfs.ReversePost();
            }
        }

        public IEnumerable<int> Order() => _order;
        public bool IsDAG() => _order != null;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms.Undirected
{
    public class GraphProperties
    {
        private Graph _g;

        public GraphProperties(Graph g)
        {
            var connectedComponents = new ConnectedComponents(g);
            if (!connectedComponents.IsConnected)
                throw new NotConnectedGraphException("Graph properties require a connected graph.");

            _g = g;
        }

        /// <summary>
        /// The eccentricity of a vertex v is the length of the shortest path
        /// from that vertex to the furthest vertex from v.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int Eccentricity(int v)
        {
            var bfs = new BreadthFirstPaths(_g, v);
            var eccentricity = 0;
            for(int u = 0; u < _g.Vertices; u++)
                eccentricity = Math.Max(eccentricity, bfs.DistanceTo(u));
            return eccentricity;
        }

        /// <summary>
        /// The diameter of a graph is the maximum eccentricity of any vertex.
        /// </summary>
        /// <returns></returns>
        public int Diameter()
        {
            var diameter = 0;
            for (var v = 0; v < _g.Vertices; v++)
                diameter = Math.Max(diameter, Eccentricity(v));
            return diameter;
        }

        /// <summary>
        /// The radius of a graph is the minimum eccentricity of any vertex.
        /// </summary>
        /// <returns></returns>
        public int Radius()
        {
            var radius = int.MaxValue;
            for (var v = 0; v < _g.Vertices; v++)
                radius = Math.Min(radius, Eccentricity(v));
            return radius;
        }
        
        /// <summary>
        /// The center is a vertex whose eccentricity is the radius.
        /// </summary>
        /// <returns></returns>
        public int Center() {
            var radius = Radius();
            for (var v = 0; v < _g.Vertices; v++)
                if (Eccentricity(v) == radius)
                    return v;
            return -1;
        }

        /// <summary>
        /// The Wiener index of a graph is the sum of the lengths of the shortest possible
        /// paths between all pair of vertices.
        /// </summary>
        /// <returns></returns>
        public int WienerIndex()
        {
            int index = 0;

            for(int v = 0; v < _g.Vertices; v++)
            {
                var bfs = new BreadthFirstPaths(_g, v);

                for (int u = 0; u < _g.Vertices; u++)
                {
                    if (v == u) continue;
                    index += bfs.DistanceTo(u);
                }
            }

            // All the paths have been counted twice.
            return index / 2;
        }

        /// <summary>
        /// Determines if the graph is cyclic or acyclic.
        /// </summary>
        /// <returns></returns>
        public bool Cyclic() => new Cycle(_g).HasCycle;

        /// <summary>
        /// The girth is the length of the shortest cycle in the graph.
        /// </summary>
        /// <returns></returns>
        public int Girth()
        {
            int EndOfPath(IEnumerable<int> path)
            {
                int lastElement = 0;
                foreach (var v in path) lastElement = v;
                return lastElement;
            }

            if (!Cyclic()) return int.MaxValue;
            var girth = int.MaxValue;

            for (int s = 0; s < _g.Vertices; s++)
            {
                var bfs = new BreadthFirstPaths(_g, s);

                for(int v = 0; v < _g.Vertices; v++)
                {
                    if (s == v) continue;
                    var path = bfs.PathTo(v);
                    var end = EndOfPath(path);

                    foreach(var u in _g.Adjacent(end))
                        if(u == s && v != u)
                            girth = Math.Min(girth, bfs.DistanceTo(v) + 1);
                }
            }

            return girth;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Diameter: {Diameter()}");
            sb.AppendLine($"Radius: {Radius()}");
            sb.AppendLine($"Center: {Center()}");
            sb.AppendLine($"Wiener-index: {WienerIndex()}");
            sb.AppendLine($"Cyclic: {(Cyclic() ? "yes" : "no")}");
            sb.AppendLine($"Girth: {Girth()}");
            sb.AppendLine($"Eccentricities:");
            for (int v = 0; v < _g.Vertices; v++)
                sb.AppendLine($"  {v} with eccentricity {Eccentricity(v)}");
            return sb.ToString();
        }
    }

    
}

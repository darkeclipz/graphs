using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphAlgorithms.Directed;
using GraphAlgorithms.Sorting;
using GraphAlgorithms.Undirected;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            //var graph = GraphBuilder.GenerateGraph("../../../graph1.txt", allowSelfLoop: false, allowParallelEdges: false);
            //AnalyseGraph(graph);

            //var symbolGraph = GraphBuilder.GenerateSymbolGraph("../../../routes.txt", allowSelfLoop: false, allowParallelEdges: false);
            //AnalyseSymbolGraph(symbolGraph);


            //var graph = GraphBuilder.GenerateSymbolGraph("../../../routes_nl.txt", allowSelfLoop: false, allowParallelEdges: false);
            //Console.WriteLine(graph.ToString());

            //var cc = new ConnectedComponents(graph.Graph);
            //Console.WriteLine("\r\n" + cc.ToString());

            //var bfs = new BreadthFirstPaths(graph.Graph, graph.Index("DRA"));
            //var sb = new StringBuilder();
            //foreach (var v in bfs.PathTo(graph.Index("UTR")))
            //    sb.Append($"{graph.Name(v)}->");
            //Console.WriteLine(sb.ToString().Substring(0, sb.ToString().Length - 2));

            //var prop = new GraphProperties(graph.Graph);
            //Console.WriteLine(prop.ToString());

            //var digraph = GraphBuilder.GenerateDigraph("../../../Graphs/digraph1.txt");
            //Console.WriteLine(digraph.ToString());
            //Console.WriteLine(digraph.Reverse().ToString());

            //var ddfs = new DirectedDFS(digraph, 0);
            //Console.WriteLine(ddfs.ToString());

            //var cc = new ConnectedComponents(digraph);
            //Console.WriteLine(cc.ToString());

            //var directedCycle = new DirectedCycle(digraph);
            //Console.WriteLine($"Directed graph contains {(directedCycle.HasCycle() ? "a" : "no")} cycle.");
            //if (directedCycle.HasCycle())
            //{
            //    Console.Write("There is a cycle in: ");
            //    var cycle = new List<int>();
            //    foreach (var v in directedCycle.Cycle())
            //        cycle.Add(v);
            //    Console.WriteLine(String.Join("-", cycle));
            //}

            //var digraph = GraphBuilder.GenerateDigraph("../../../Graphs/digraph2.txt", allowParallelEdges: false, allowSelfLoop: false);
            //var topological = new Topological(digraph);

            //Console.WriteLine($"Directed acyclic graph: {(topological.IsDAG() ? "yes" : "no")}");
            //Console.Write("PIN code: ");
            //foreach (var o in topological.Order())
            //{
            //    if (o == 4 || o == 5) continue;
            //    Console.Write(o.ToString());
            //}
            //Console.WriteLine();

            //var scc = new KosarajuSharirSCC(digraph);
            //Console.WriteLine(scc.ToString());

            //var digraph = GraphBuilder.GenerateSymbolDigraph("../../../Graphs/prerequisite_scheduling.txt", allowParallelEdges: false, allowSelfLoop: false);
            //var topological = new Topological(digraph.Digraph);

            //Console.WriteLine($"Directed acyclic graph: {(topological.IsDAG() ? "yes" : "no")}");
            //Console.WriteLine("Precedence-constrained schedule (DAG in topological order):");

            //foreach(int v in topological.Order())
            //{
            //    Console.WriteLine($"{v}: {digraph.Name(v)}");
            //}

            //Console.ReadKey();

            //var rng = new Random();

            //var edges = new List<Edge>();
            //var pq = new MaxPQ<Edge>(16);
            //for (int i = 0; i < 16; i++)
            //{
            //    var edge = new Edge(0, 0, rng.Next(0, 100));
            //    pq.Insert(edge);
            //    Console.WriteLine($"{i}: {edge.Weight}");
            //}

            //Console.WriteLine("PriorityQueue output:");
            //while(!pq.IsEmpty)
            //{
            //    var e = (Edge)pq.DeleteMax();
            //    Console.WriteLine($"{e.Weight}");
            //}

            var ewg = GraphBuilder.GenerateEdgeWeightedGraph("../../../Graphs/tiny_ewg.txt");
            var mst = new LazyPrimMST(ewg);
            var kruskal = new KruskalMST(ewg);
            Console.WriteLine(ewg.ToString());
            Console.WriteLine(mst.ToString());
            Console.WriteLine(kruskal.ToString());

            Console.ReadKey();
        }

        //static string IEnumerableToString<T>(IEnumerable<T> elements)
        //{
        //    if (elements == null) return "no path";
        //    var sb = new StringBuilder();
        //    foreach(var e in elements)
        //    {
        //        sb.Append($"{e}-");
        //    }
        //    var str = sb.ToString();
        //    return str.Substring(0, str.Length-1);
        //}

        //static void AnalyseGraph(Graph g)
        //{
        //    Console.WriteLine($"Graph has {g.Vertices} vertices and {g.Edges} edges.");
        //    Console.WriteLine(g.ToString());

        //    var dfs = new DepthFirstPaths(g, 0, detailedTrace: true);

        //    Console.WriteLine("\r\nPath to (DFS):");
        //    for (int v = 0; v < g.Vertices; v++)
        //    {
        //        Console.WriteLine($"{0} to {v}: {IEnumerableToString(dfs.PathTo(v))}");
        //    }

        //    var bfs = new BreadthFirstPaths(g, 0);

        //    Console.WriteLine("\r\nPath to (BFS):");
        //    for (int v = 0; v < g.Vertices; v++)
        //    {
        //        Console.WriteLine($"{0} to {v}: {IEnumerableToString(bfs.PathTo(v))}");
        //    }

        //    var cc = new ConnectedComponents(g);
        //    Console.WriteLine("\r\n" + cc.ToString());

        //    if(cc.Count == 1)
        //    {
        //        Console.WriteLine("\r\nGraph properties:");
        //        var properties = new GraphProperties(g);
        //        Console.WriteLine($"Diameter: {properties.Diameter()}");
        //        Console.WriteLine($"Radius: {properties.Radius()}");
        //        Console.WriteLine($"Center: {properties.Center()}");
        //        Console.WriteLine($"Wiener-index: {properties.WienerIndex()}");
        //        Console.WriteLine($"Cyclic: {(properties.Cyclic() ? "yes" : "no")}");
        //        Console.WriteLine($"Girth: {properties.Girth()}");
        //        Console.WriteLine($"Eccentricities:");
        //        for(int v = 0; v < g.Vertices; v++)
        //            Console.WriteLine($"  {v} with eccentricity {properties.Eccentricity(v)}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Graph properties not available because it is not a connected graph.");
        //    }

        //    if (!g.ParallelEdgesOrSelfLoopsAllowed)
        //    {
        //        var cycle = new Cycle(g);
        //        Console.WriteLine($"The graph has {(cycle.HasCycle ? "a" : "no")} cycle.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Unable to detect cycle because parallel edges or self loops are allowed.");
        //    }

        //    var twoColor = new TwoColor(g);
        //    Console.WriteLine($"The graph is {(twoColor.IsBipartite ? "bipartite" : "not bipartite")}.");

        //    // EuclidianGraph.ToImage(g);
        //}

        //static void AnalyseSymbolGraph(SymbolGraph g)
        //{
        //    Console.WriteLine("\r\nSymbol graph:");
        //    Console.WriteLine($"Graph has {g.Graph.Vertices} vertices and {g.Graph.Edges} edges.");
        //    Console.WriteLine(g.ToString());

        //    var cc = new ConnectedComponents(g.Graph);
        //    Console.WriteLine(cc.ToString());
        //    Console.WriteLine("Shortest path from LAX to JFK is:");
        //    var bfs = new BreadthFirstPaths(g.Graph, g.Index("LAX"));
        //    var sb = new StringBuilder();
        //    foreach (var v in bfs.PathTo(g.Index("JFK")))
        //        sb.Append($"{g.Name(v)}->");
        //    Console.WriteLine(sb.ToString().Substring(0, sb.ToString().Length - 2));

        //    if (cc.Count == 1)
        //    {
        //        Console.WriteLine("\r\nGraph properties:");
        //        var properties = new GraphProperties(g.Graph);
        //        Console.WriteLine($"Diameter: {properties.Diameter()}");
        //        Console.WriteLine($"Radius: {properties.Radius()}");
        //        Console.WriteLine($"Center: {g.Name(properties.Center())}");
        //        Console.WriteLine($"Wiener-index: {properties.WienerIndex()}");
        //        Console.WriteLine($"Cyclic: {(properties.Cyclic() ? "yes" : "no")}");
        //        Console.WriteLine($"Girth: {properties.Girth()}");
        //        Console.WriteLine($"Eccentricities:");
        //        foreach (var key in g.Keys)
        //            Console.WriteLine($"  {key} with eccentricity {properties.Eccentricity(g.Index(key))}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Graph properties not available because it is not a connected graph.");
        //    }

        //    if (!g.Graph.ParallelEdgesOrSelfLoopsAllowed)
        //    {
        //        var cycle = new Cycle(g.Graph);
        //        Console.WriteLine($"The graph has {(cycle.HasCycle ? "a" : "no")} cycle.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Unable to detect cycle because parallel edges or self loops are allowed.");
        //    }

        //    // EuclidianGraph.ToImage(g.Graph);
        //}
    }
}
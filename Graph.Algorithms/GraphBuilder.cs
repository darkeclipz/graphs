using GraphAlgorithms.Directed;
using GraphAlgorithms.Undirected;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphAlgorithms
{
    public class GraphBuilder
    {
        public static Graph GenerateGraph(string file, bool allowSelfLoop = true, bool allowParallelEdges = true)
        {
            var lines = File.ReadAllLines(file);
            var vertices = int.Parse(lines[0]);
            var graph = new Graph(vertices, allowSelfLoop, allowParallelEdges);

            for(var i=2; i < lines.Length; i++)
            {
                var connection = lines[i].Split(' ').Select(v => int.Parse(v)).ToList();
                graph.AddEdge(connection[0], connection[1]);
            }

            return graph;
        }

        public static Digraph GenerateDigraph(string file, bool allowSelfLoop = true, bool allowParallelEdges = true)
        {
            var lines = File.ReadAllLines(file);
            var vertices = int.Parse(lines[0]);
            var digraph = new Digraph(vertices, allowSelfLoop, allowParallelEdges);

            for (var i = 2; i < lines.Length; i++)
            {
                var connection = lines[i].Split(' ').Select(v => int.Parse(v)).ToList();
                digraph.AddEdge(connection[0], connection[1]);
            }

            return digraph;
        }


        public static SymbolGraph GenerateSymbolGraph(string file, bool allowSelfLoop = true, bool allowParallelEdges = true)
        {
            var lines = File.ReadAllLines(file);
            var input = new List<Tuple<string, string>>();
            foreach(var line in lines)
            {
                var connection = line.Split(' ');
                input.Add(new Tuple<string, string>(connection[0], connection[1]));
            }
            var symbolGraph = new SymbolGraph(input, allowSelfLoop, allowParallelEdges);
            return symbolGraph;
        }
    }
}

using GraphAlgorithms.Directed;
using GraphAlgorithms.Undirected;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                var connection = lines[i].Split(' ').Take(2).Select(v => int.Parse(v)).ToList();
                graph.AddEdge(connection[0], connection[1]);
            }

            return graph;
        }

        public static EdgeWeightedGraph GenerateEdgeWeightedGraph(string file)
        {
            var lines = File.ReadAllLines(file);
            var vertices = int.Parse(lines[0]);
            var graph = new EdgeWeightedGraph(vertices);

            for (var i = 2; i < lines.Length; i++)
            {
                var str = lines[i].Split(' ');
                var v = int.Parse(str[0]);
                var w = int.Parse(str[1]);
                double weight = GetDoubleDot(str[2]);
                var e = new Edge(v, w, weight);
                graph.AddEdge(e);
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
                var connection = lines[i].Split(' ').Take(2).Select(v => int.Parse(v)).ToList();
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

        public static SymbolDigraph GenerateSymbolDigraph(string file, bool allowSelfLoop = true, bool allowParallelEdges = true)
        {
            var lines = File.ReadAllLines(file);
            var input = new List<Tuple<string, string>>();
            foreach (var line in lines)
            {
                var connection = line.Split(' ');
                input.Add(new Tuple<string, string>(connection[0], connection[1]));
            }
            var symbolGraph = new SymbolDigraph(input, allowSelfLoop, allowParallelEdges);
            return symbolGraph;
        }

        public static double GetDoubleDot(string value, double defaultValue = 0f)
        {
            double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out double result);
            return result;
        }

        public static double GetDouble(string value, double defaultValue)
        {
            double result;

            // Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                // Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                // Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }
            return result;
        }
    }
}

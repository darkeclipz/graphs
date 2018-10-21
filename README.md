# Graph algorithms

This repository contains various graph algorithms.

## Terminology

This is an overview of the terminology:

 * _degree_: the number of edges for a vertex.
 * _connected_: a graph is _connected_ if all vertices can be reached from any vertex `v`.
 * _cyclic_: a graph is called _cyclic_ if there is a path from `v` to itself with a minimum path length that is greater than 2. If a graph is not cyclic, it is called _acyclic_.
 * _eccentricity_: the _eccentricity_ of a vertex `v` is the length of the shortest path from that vertex to the furthest vertex from `v`.
 * _diameter_: the _diameter_ of a graph is the maximum eccentricity of any vertex.
 * _radius_: the _radius_ of a graph is the minimum eccentricity of any vertex.
 * _center_: any vertex is a _center_ if it's eccentricity is the radius.
 * _Wiener index_: the _Wiener index_ of a graph is the sum of the lengths of the shortest possible paths between all pair of vertices.
 * _girth_: the _girth_ of a graph is the length of the shortest cycle in the graph.

## Example

### Example graph 1

![graph1](img/graph1.png)

The following text can be used with the `GraphBuilder` to generate the graph.

```
12
16
8 4
2 3
1 11
0 6
3 6
10 3
7 11
7 8
11 8
2 0
6 2
5 2
5 10
5 0
8 1
4 1
```

Load the graph in the following way:

```csharp
var graph = GraphBuilder.GenerateGraph("../../../graph2.txt", allowSelfLoop: false, allowParallelEdges: false);
Console.WriteLine(graph.ToString());
```

This yields the following information about the graph:

```
0: 6 2 5 (degree: 3)
1: 11 8 4 (degree: 3)
2: 3 0 6 5 (degree: 4)
3: 2 6 10 (degree: 3)
4: 8 1 (degree: 2)
5: 2 10 0 (degree: 3)
6: 0 3 2 (degree: 3)
7: 11 8 (degree: 2)
8: 4 7 11 1 (degree: 4)
9: (degree: 0)
10: 3 5 (degree: 2)
11: 1 7 8 (degree: 3)
Max degree: 4
Average degree: 2,66666666666667
```

To find all the connected components in this graph, use the `ConnectedComponent` object:

```csharp
var cc = new ConnectedComponents(graph);
Console.WriteLine("\r\n" + cc.ToString());
```

Which result in the followng connected components:

```
Connected components (3 components):
0: 0 2 3 5 6 10
1: 1 4 7 8 11
2: 9
The graph is disconnected.
```

A graph is said to be _disconnected_ if it has more than a single component (multiple subgraphs).

## API

### Graph

The `Graph` object requires a number of `vertices`. If `allowSelfLoops` is set to `true`, the graph will not check for self-loops when calling `AddEdge`. If `allowParallelEdges` is set to `true`, the graph will not check if the edge already exists when calling `AddEdge`. Both checks are set to `false` by default.

The `Graph` object contains the following properties/methods:

 * `Vertices` returns the number of vertices.
 * `Edges` returns the number of edges.
 * `void AddEdge(int v, int u)` adds an edge from `v` to `u`.
 * `IEnumerable<int> Adjacent(int v)` returns an `IEnumerable` for the adjacent vertices of `v`.
 * `string ToString()` returns a string representation of the graph.
 * `int Degree(int v)` returns the degree of vertex `v`.
 * `int MaxDegree()` returns the max degree of the graph.
 * `int AverageDegree()` returns the average degree of the graph, which is `2 * E / V`.
 * `ParallelEdgesOrSelfLoopsAllowed` returns `true` if `allowSelfLoops` or `allowParallelEdges` is set.

 The graph can throw the following exceptions:

 * `SelfLoopException` is called when a self-loop is created and `allowSelfLoops` is `false`.
 * `ParallelEdgeException` is called when a duplicate edge is created and `allowParallelEdges` is `false`.

### DepthFirstPaths

The `DepthFirstPaths` object will run the DFS algorithm on the graph. The object requires a `Graph` and a source vertex `s`. If `detailedTrace` is set to `true`, the algorithm will print a detailed trace.

The `DepthFirstPaths` object contains the following methods:

 * `bool HasPathTo(int v)` returns `true` if there is a path from the source `s` to `v`.
 * `IEnumerable<int> PathTo(int v)` returns an `IEnumerable` with the path from the source `s` to `v`.

### BreadthFirstPaths

The `BreadthFirstPaths` object will run the BFS algorithm on the graph. The paths from BFS are shortest paths. The object requires a `Graph` and a source vertex `s`. 

The `BreadthFirstPaths` object contains the following methods:

 * `bool HasPathTo(int v)` returns `true` if there is a path from the source `s` to `v`.
 * `IEnumerable<int> PathTo(int v)` returns an `IEnumerable` with the path from the source `s` to `v`.
 * `int DistanceTo(int v)` returns the distance from the source `s` to `v`.

### Cycle

The `Cycle` object will detect if a graph is cyclic. This assumes that the graph doesn't have any parallel edges or self-loops. The object requires a `Graph` object.

The `Cycle` object has the following property:

 * `HasCycle` return `true` if the graph is cyclic.

### TwoColor

The `TwoColor` object will check if the graph is bipartite, which means it can be colored with two colors, such that no adjacent vertices have the same color. The object requires a `Graph` object.

The `TwoColor` object has the following property:

 * `IsBipartite` returns `true` if the graph is two colorable.

### GraphProperties

The `GraphProperties` object gives additional information about a graph. This requires the graph to be connected (no subgraphs). The object requires a `Graph` object.

The `GraphProperties` contains the following properties/methods:

 * `int Eccentricity(int v)` returns the eccentricity of a vertex `v`.
 * `int Diameter()` returns the diameter of the graph.
 * `int Radius()` returns the radius of the graph.
 * `int Center()` returns a center vertex.
 * `int WienerIndex()` returns the Wiener index of the graph.
 * `bool Cyclic()` returns `true` if the graph is cyclic. Uses the `Cycle` class.
 * `int Girth()` returns the girth of the graph.

The `Cyclic()` method requires the graph to have `ParallelEdgesOrSelfLoopsAllowed` evaluate to `false`.

### GraphBuilder

The `GraphBuilder` object contains static methods to generate graph objects.


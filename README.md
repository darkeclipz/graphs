# Graph algorithms

This repository contains various graph algorithms.

## Examples

 * [Example with `Graph`](/example_graph.md)
 * [Example with `SymbolGraph`](/example_symbolgraph.md)

## Applied examples

 1. [Passcode derivation (Project Euler)](/passcode_derivation.md)
 2. Mathematics prerequisite scheduling
 3. ...

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
 * _DAG_: a _DAG_ is a directed acyclic graph.
 * _strong connectivity_: in a digraph, two vertices are strong connected if it is possible to visit both `w` from `v`, and `v` to `w`.
 * _reachability_: if there is a directed path from a vertex `v` to another given vertex `w`.

## API

The implementations of the algorithms are far from complete. The goal of this repository is to have a set of biolerplate algorithms. They should be extended in context to whichever problem they are being used to solve. 

It could occur that a certain method is implemented for a graph, and not a digraph, which you need. In this case you are left alone to implement the new algorithm for the other graph. The repository should provide plenty of examples to implement new algorithms.

### Indirected graphs

#### Graph

The `Graph` object requires a number of `vertices`. If `allowSelfLoops` is set to `true`, the graph will not check for self-loops when calling `AddEdge`. If `allowParallelEdges` is set to `true`, the graph will not check if the edge already exists when calling `AddEdge`. Both are set to `true` by default.

The `Graph` object contains the following properties/methods:

 * `Vertices` returns the number of vertices.
 * `Edges` returns the number of edges.
 * `void AddEdge(int v, int u)` adds an edge from `v` to `u`.
 * `IEnumerable<int> Adjacent(int v)` returns an `IEnumerable` for the adjacent vertices of `v`.
 * `override string ToString()` returns a string representation of the graph.
 * `int Degree(int v)` returns the degree of vertex `v`.
 * `int MaxDegree()` returns the max degree of the graph.
 * `int AverageDegree()` returns the average degree of the graph, which is `2 * E / V`.
 * `ParallelEdgesOrSelfLoopsAllowed` returns `true` if `allowSelfLoops` or `allowParallelEdges` is set.

 The graph can throw the following exceptions:

 * `SelfLoopException` is raised when a self-loop is created and `allowSelfLoops` is `false`.
 * `ParallelEdgeException` is raised when a duplicate edge is created and `allowParallelEdges` is `false`.

 
#### SymbolGraph

The `SymbolGraph` object is a symbol table wrapper on the `Graph` object. It will map strings to an index-based graph. The constructor requires a `List<Tuple<string, string>>` of inputs, where each tuple is a from/to edge. It also has the `allowSelfLoops` and `allowParallelEdges` checks.

The `SymbolGraph` object contains the following properties/methods:

 * `bool Contains(string s)` returns `true` if the graph contains the symbol `s`.
 * `IEnumerable<string> Keys` returns an `IEnumberable` with the keys.
 * `int Index(string s)` returns the integer index of the symbol `s`.
 * `string Name(int v)` returns the symbol for the integer index `v`.
 * `Graph Graph` returns the graph with integer indices.
 * `override string ToString()` returns a string representation of the graph.

#### DepthFirstPaths

The `DepthFirstPaths` object will run the DFS algorithm on the graph. The object requires a `Graph` and a source vertex `s`. If `detailedTrace` is set to `true`, the algorithm will print a detailed trace.

The `DepthFirstPaths` object contains the following methods:

 * `bool HasPathTo(int v)` returns `true` if there is a path from the source `s` to `v`.
 * `IEnumerable<int> PathTo(int v)` returns an `IEnumerable` with the path from the source `s` to `v`.

#### BreadthFirstPaths

The `BreadthFirstPaths` object will run the BFS algorithm on the graph. The paths from BFS are shortest paths. The object requires a `Graph` and a source vertex `s`. 

The `BreadthFirstPaths` object contains the following methods:

 * `bool HasPathTo(int v)` returns `true` if there is a path from the source `s` to `v`.
 * `IEnumerable<int> PathTo(int v)` returns an `IEnumerable` with the path from the source `s` to `v`.
 * `int DistanceTo(int v)` returns the distance from the source `s` to `v`.

#### Cycle

The `Cycle` object will detect if a graph is cyclic. This assumes that the graph doesn't have any parallel edges or self-loops. The object requires a `Graph` object.

The `Cycle` object has the following property:

 * `HasCycle` returns `true` if the graph is cyclic.

#### DirectedCycle

The `DirectedCycle` object will detect cycles in a digraph. It will the path of the cycle. The object requires a `Digraph` object.

The `DirectedCycle` object has the following methods:

 * `bool HasCycle()` return `true` if the digraph has a cycle.
 * `IEnumerator<int> Cycle()` returns an enumerator for the path.

#### TwoColor

The `TwoColor` object will check if the graph is bipartite, which means it can be colored with two colors, such that no adjacent vertices have the same color. The object requires a `Graph` object.

The `TwoColor` object has the following property:

 * `IsBipartite` returns `true` if the graph is two colorable.

#### GraphProperties

The `GraphProperties` object gives additional information about a graph. This requires the graph to be connected (no subgraphs). The object requires a `Graph` object.

The `GraphProperties` contains the following properties/methods:

 * `int Eccentricity(int v)` returns the eccentricity of a vertex `v`.
 * `int Diameter()` returns the diameter of the graph.
 * `int Radius()` returns the radius of the graph.
 * `int Center()` returns a center vertex.
 * `int WienerIndex()` returns the Wiener index of the graph.
 * `bool Cyclic()` returns `true` if the graph is cyclic. Uses the `Cycle` object.
 * `int Girth()` returns the girth of the graph.

The `Cyclic()` method requires the graph to have `ParallelEdgesOrSelfLoopsAllowed` evaluate to `false`. If the graph is not connected, it will raise an `NotConnectedGraphException` exception.

#### ConnectedComponents

The `ConnectedComponents` object detects all the connected components within the graph. It will return a list of subgraphs. The constructor requires an `IGraph` object, either a `Graph` or `Digraph`.

The `ConnectedComponents` object contains the following properties/methods:

 * `bool Connected(int v, int w)` returns `true` if the vertices `v` and `w` are connected.
 * `int Id(int v)` returns the component id of the vertex `v`.
 * `int Count` returns the count of the components.
 * `bool IsConnected` will return `true` if the graph is connected, i.e. there is only one component.

## Directed graphs

#### Digraph

The `Digraph` object requires a number of `vertices`. If `allowSelfLoops` is set to `true`, the graph will not check for self-loops when calling `AddEdge`. If `allowParallelEdges` is set to `true`, the graph will not check if the edge already exists when calling `AddEdge`. Both are set to `true` by default.

The `Diraph` object contains the following properties/methods:

 * `Vertices` returns the number of vertices.
 * `Edges` returns the number of edges.
 * `void AddEdge(int v, int u)` adds an edge from `v` to `u`.
 * `IEnumerable<int> Adjacent(int v)` returns an `IEnumerable` for the adjacent vertices of `v`.
 * `override string ToString()` returns a string representation of the graph.
 * `Digraph Reverse()` returns the digraph with reversed edges.
 * `int DegreeOut(int v)` returns the degree of vertex `v`.
 * `int MaxDegreeOut()` returns the max degree of the graph.
 * `int AverageDegreeOut()` returns the average degree of the graph, which is `E / V`.
 * `ParallelEdgesOrSelfLoopsAllowed` returns `true` if `allowSelfLoops` or `allowParallelEdges` is set.

 The graph can throw the following exceptions:

 * `SelfLoopException` is raised when a self-loop is created and `allowSelfLoops` is `false`.
 * `ParallelEdgeException` is raised when a duplicate edge is created and `allowParallelEdges` is `false`.

#### DirectedDFS

The `DirectedDFS` object will run the DFS algorithm on a digraph. The object requires a `Digraph` and a source vertex `s`, or a list of vertices.

The `DirectedDFS` object contains the following methods:

 * `bool Marked(int v)` return `true` if a vertex `v` is accessible from `s`.
 * `override string ToString()` returns a string representation of the reachable vertices.

#### DepthFirstOrder

The `DepthFirstOrder` object will generate various orders of the digraph. These methods are useful in advanced graph-processing algorithms. The constructor requires a `Digraph` object.

The `DepthFirstOrder` object contains the following methods:

 * `IEnumerable<int> Pre()` returns an enumerator with the vertices in preorder.
 * `IEnumerable<int> Post()` returns an enumerator with the vertices in postorder.
 * `IEnumerable<int> ReversePost()` returns an enumerator with the vertices in reverse postorder.

#### Topological

The `Topological` object will sort a directed acyclic graph (DAG) in topological order. The constructor requires a `Digraph` object.

The `Topological` object contains the following methods:

 * `IEnumerable<int> Order()` returns an enumerator with the vertices of the DAG in topological order.


#### KosarajuSharirSCC

The `KosarajuSharirSCC` object will find the strongly connected components with a digraph. The constructor requires a `Digraph` object.

The `KosarajuSharirSCC` object contains the following properties/methods:

 * `bool StronglyConnected(int v, int w)` returns `true` if the vertices `v` and `w` are strongly connected.
 * `int Id(int v)` returns the identifier of the strongly connected component.
 * `int Count` returns the number of strongly connected components.
 * `override string ToString()` returns a string representation of the SCC.

 The Kosaraju-Sharir algorithm helps to answer questions such as: _Are two given vertices strong connected?_ and _How many strong components does the digraph have?_.

### Graph builders

#### GraphBuilder

The `GraphBuilder` object contains static methods to generate graphs. The `GraphBuilder` object contains the following methods:

 * `Graph GenerateGraph(string file, bool allowSelfLoop = true, bool allowParallelEdges = true)`.
 * `SymbolGraph GenerateSymbolGraph(string file, bool allowSelfLoop = true, bool allowParallelEdges = true)`.

##### `Graph` format

The text file format for a `Graph` is:
```
vertice_count
edge_count
from to
from to
...
```

##### `SymbolGraph` format

The text file format for a `SymbolGraph` is:

```
from to
from to
from to
...
```

More examples can be found in the `Graph.Algorithms/Graphs` folder.

### Other

#### EuclideanGraph

The `EuclideanGraph` object will create an ugly image of a `Graph`.

The `EuclideanGraph` object contains the following static method:

 * `static void ToImage(Graph g)` which will save the image to `graph_{DateTime.Now.ToString("yyyyMMdd_hhmmss_ffff")}.jpg` in the current working directory.

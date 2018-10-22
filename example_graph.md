# Example with `Graph`

The following example will show the basics.

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
Console.WriteLine(cc.ToString());
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

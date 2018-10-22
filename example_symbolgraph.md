# Example with `SymbolGraph`

The following graph is a -- not very accurate -- map of The Netherlands.

![graph 2](img/graph2.png)

The graph can be created with `GraphBuilder` and the following text:

```
LWD DRA
DRA GRO
ASN DRA
ALM DRA
ASN EMM
ZWL EMM
ZWL ASN
DRA ZWL
AMS ALM
AME ZWL
AME ARN
ARN ZWL
UTR ARN
AMS UTR
AMS DNH
DNH ROT
UTR AME
```

If we create the graph, and run the simple analysis:

```csharp
var graph = GraphBuilder.GenerateSymbolGraph("../../../routes_nl.txt", allowSelfLoop: false, allowParallelEdges: false);
Console.WriteLine(graph.ToString());

var cc = new ConnectedComponents(graph.Graph);
Console.WriteLine("\r\n" + cc.ToString());
```

We will get the following result:

```
LWD: DRA (degree: 1)
DRA: LWD GRO ASN ALM ZWL (degree: 5)
GRO: DRA (degree: 1)
ASN: DRA EMM ZWL (degree: 3)
ALM: DRA AMS (degree: 2)
EMM: ASN ZWL (degree: 2)
ZWL: EMM ASN DRA AME ARN (degree: 5)
AMS: ALM UTR DNH (degree: 3)
AME: ZWL ARN UTR (degree: 3)
ARN: AME ZWL UTR (degree: 3)
UTR: ARN AMS AME (degree: 3)
DNH: AMS ROT (degree: 2)
ROT: DNH (degree: 1)
Max degree: 5
Average degree: 2,61538461538462

Connected components (1 component):
0: 0 1 2 3 4 5 6 7 8 9 10 11 12
The graph is connected.
```

If we want to find the shortest path from `DRA` to `UTR`:

```csharp
var bfs = new BreadthFirstPaths(graph.Graph, graph.Index("DRA"));
var sb = new StringBuilder();
foreach (var v in bfs.PathTo(graph.Index("UTR")))
    sb.Append($"{graph.Name(v)}->");
Console.WriteLine(sb.ToString().Substring(0, sb.ToString().Length - 2));
```

Which gives the following route:

```
DRA->ALM->AMS->UTR
```

We can also find more useful information about a graph with `GraphProperties`:

```csharp
var prop = new GraphProperties(graph.Graph);
Console.WriteLine(prop.ToString());
```

Which gives the following information:

```
Diameter: 7
Radius: 4
Center: 4
Wiener-index: 277
Cyclic: yes
Girth: 3
Eccentricities:
  0 with eccentricity 6
  1 with eccentricity 5
  2 with eccentricity 6
  3 with eccentricity 6
  4 with eccentricity 4
  5 with eccentricity 7
  6 with eccentricity 6
  7 with eccentricity 5
  8 with eccentricity 5
  9 with eccentricity 5
  10 with eccentricity 5
  11 with eccentricity 6
  12 with eccentricity 7
```
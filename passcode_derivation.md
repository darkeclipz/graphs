# Passcode derivation

The following problem is problem 79 from [Project Euler](http://projecteuler.net).

## Problem

A common security method used for online banking is to ask the user for three random characters from a passcode. For example, if the passcode was `531278`, they may ask for the 2nd, 3rd, and 5th characters; the expected reply would be: `317`.

Given that the three characters are always asked for in order, analyse the file so as to determine the shortest possible secret passcode of unknown length.

## Verification codes

```
319 680 180 690 129 620 762 689 762 318
368 710 720 710 629 168 160 689 716 731
736 729 316 729 729 710 769 290 719 680 
318 389 162 289 162 718 729 319 790 680
890 362 319 760 316 729 380 319 728 716
```

## Analysis

Because the characters are always asked in order, this problem can be represented with a directed acyclic graph. If we sort the graph in topological order, the result is the passphrase. If we visualize the problem:

![passphrase](/img/passphrase.png)

If we would have added more verification codes to the diagram, it is easily solvable by hand.

## `Digraph` text format

We will create a graph with `10` vertices, although `4` and `5` are not used. We will filter out `4` and `5` later, when printing the resulting passphrase.

The following `Digraph` text format is used to load the graph:

```
10
100
3 1
1 9
6 8
8 0
1 8
8 0
6 9
9 0
1 2
2 9
6 2
2 0
7 6
6 2
6 8
8 9
7 6
6 2
3 1
1 8
3 6
6 8
7 1
1 0
7 2
2 0
7 1
1 0
6 2
2 9
1 6
6 8
1 6
6 0
6 8
8 9
7 1
1 6
7 3
3 1
7 3
3 6
7 2
2 9
3 1
1 6
7 2
2 9
7 2
2 9
7 1
1 0
7 6
6 9
2 9
9 0
7 1
1 9
6 8
8 0
3 1
1 8
3 8
8 9
1 6
6 2
2 8
8 9
1 6
6 2
7 1
1 8
7 2
2 9
3 1
1 9
7 9
9 0
6 8
8 0
8 9
9 0
3 6
6 2
3 1
1 9
7 6
6 0
3 1
1 6
7 2
2 9
3 8
8 0
3 1
1 9
7 2
2 8
7 1
1 6
```

## Solving the problem

Now we can load the graph into a `Digraph`:

```csharp
var digraph = GraphBuilder.GenerateDigraph("../../../Graphs/pin.txt", allowParallelEdges: true, allowSelfLoop: false);
```

And sort in topological order:

```csharp
var topological = new Topological(digraph);
```

If we print the result, this is the passphrase. We should skip `4` and `5` because those aren't in the passphrase at all.

```csharp
Console.WriteLine($"Directed acyclic graph: {(topological.IsDAG() ? "yes" : "no")}");
Console.Write("PIN code: ");
foreach (var o in topological.Order())
{
    if (o == 4 || o == 5) continue;
    Console.Write(o.ToString());
}
Console.WriteLine();
```

Which results in:

```
Directed acyclic graph: yes
PIN code: 73162890
```

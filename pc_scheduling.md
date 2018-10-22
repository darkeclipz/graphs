# Precedence-constrained scheduling

A widely applicable problem-solving model has to do with arranging for the completion of a set of jobs, under a set a set of constraints, by specifying when and how the jobs are to be performed. Constraints might involve functions of time taken or other resources consumed by the jobs. The most important type of constraints is _precendence constraints_, which specify that certain jobs must be performed before certain others.

## Planning a course schedule

Many courses have other courses as a prerequisite. In the graph below we can see the problem for this set of courses. The problem is to find an order in which to follow the courses, such that all the prerequisites are met.

## Digraph text format

The following text can be imported with the `GraphBuilder` object to create the digraph for the above stated graph.

```
Advanced_Programming Scientific_Computing
Scientific_Computing Computational_Biology
Introduction_to_CS Algorithms
Introduction_to_CS Advanced_Programming
Theoretical_CS Computational_Biology
Theoretical_CS Artifical_Intelligence
Artifical_Intelligence Robotics
Artifical_Intelligence Neural_Networks
Artifical_Intelligence Machine_Learning
Machine_Learning Neural_Networks
Algorithms Theoretical_CS
Algorithms Databases
Algorithms Scientific_Computing
Calculus Linear_Algebra
Linear_Algebra Theoretical_CS
```

## Scheduling

The graph is a directed acyclic graph (DAG). This means that if we sort the graph in topological order, we get the schedule that satisfies the precedence constraint.

We should also verify if it is indeed a DAG, we can do this with the `Topological.IsDAG()` method.

```csharp
var digraph = GraphBuilder.GenerateSymbolDigraph("../../../Graphs/prerequisite_scheduling.txt", allowParallelEdges: false, allowSelfLoop: false);
var topological = new Topological(digraph.Digraph);

Console.WriteLine($"Directed acyclic graph: {(topological.IsDAG() ? "yes" : "no")}");
Console.WriteLine("Precedence-constrained schedule (DAG in topological order):");

foreach(int v in topological.Order())
{
    Console.WriteLine($"{v}: {digraph.Name(v)}");
}

Console.ReadKey();
```

Which gives the following result:

```
Directed acyclic graph: yes
Precedence-constrained schedule (DAG in topological order):
11: Calculus
12: Linear_Algebra
3: Introduction_to_CS
4: Algorithms
10: Databases
5: Theoretical_CS
6: Artifical_Intelligence
9: Machine_Learning
8: Neural_Networks
7: Robotics
0: Advanced_Programming
1: Scientific_Computing
2: Computational_Biology
```
Graph Generator in C#
=================

Student project for Algorithms & Data Structures. There are two implemented algorithms: Dijkstra and Prim.

### External libraries

I am using [MSAGL](https://github.com/Microsoft/automatic-graph-layout).

## How it works

You need file like below

    # number of Nodes
    Nodes = 4
    # every node is made of 3 different variables (id, X coordinate, Y coordinate)
    1 0 50
    2 30 56
    3 45 32
    4 50 23
    # number of Edges
    Edges = 5
    # every edge is made of 3 different variables (id, begin Node, end Node)
    # in the same time begin Node is end Node but in different direction (2 way edge: A->B == B->A)
    1 1 2
    2 2 3
    3 3 4
    4 4 2
    5 1 3

Every line with '#' sign will be ignored, so you can use it easly like // in C# to comment something. **Do not start ID of Nodes and Edges from 0. It's numbered from 1.** Number after '=' sign is the number of Nodes or Edges that graph contains.




using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace AISDEProject
{
    //I truly recommend that you should read about Prim's algorithm before seeing code below.
    class Prim
    {
        //
        // Summary:
        //      Gets list of all Nodes and their connected Edges contained in Graph.    
        //
        // Returns:
        //      List of Edges and list of Nodes contained in the Graph.
        public MyGraph MyGraph { get; set; }

        //
        // Summary:
        //      Gets list of all available Nodes contained in Prim's Algorithm.    
        //
        // Returns:
        //      List of available Nodes contained in the Prim's Algorithm.
        public List<Node> Nodes { get; set; }

        //
        // Summary:
        //      Gets list of all available Edges contained in Prim's Algorithm.    
        //
        // Returns:
        //      List of available Edges contained in the Prim's Algorithm.
        public List<Edge> Edges { get; set; }

        //
        // Summary:
        //     Gets list of all edges contained in the MST.
        //
        // Returns:
        //     Edges contained in the MST(Minimum Spanning Tree) after Prim's algorithm.
        public List<Edge> MST { get; set; }

        public Prim()
        {
            MyGraph = new MyGraph();
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public Prim(MyGraph myGraph)
        {
            MyGraph = myGraph;
            Nodes = new List<Node>(myGraph.Nodes);
            Edges = new List<Edge>(myGraph.Edges);
        }

        //
        // Summary:
        //      
        //
        // Parameters:
        //   node1:
        //      One of all available nodes from List of Nodes.
        //
        //   node2:
        //      Second remaining node from List of Nodes.
        //
        // Returns:
        //      The Edge which is one from following configuration node1->node2 or node2->node1.
        //
        // Exceptions:
        //      None
        Edge GetEdge(Node node1, Node node2) => Edges.
                First(e => (e.Begin.Equals(node1) && e.End.Equals(node2)) || (e.Begin.Equals(node2) && e.End.Equals(node1)));
        
        //
        // Summary:
        //      Using Prim algorithm searches MST (Minimum Spanning Tree).
        //      For more just google Prim algorithm
        //
        // Parameters:
        //   node:
        //      The Node class object to start algorithm.
        public void PrimAlgo(Node node)
        {
            MST = new List<Edge>();
            var tree = new List<Node>();
            var queue = new List<Tuple<Node, Node, double>>();

            //var neighbours = new Dictionary<Node, Node>();

            var current = node;
            var previous = current;

            tree.Add(current);

            foreach (var neighbour in MyGraph.NeighborsNodes(current))
            {
                queue.Add(new Tuple<Node, Node, double>(current, neighbour, current.Weight(neighbour)));
            }

            queue.Sort((n1, n2) => n1.Item3.CompareTo(n2.Item3));
            while (tree.Count != Nodes.Count)
            {
                Node neigh = null;
                int i = 0;
                while (tree.Contains(current))
                {
                    current = queue[i].Item2;
                    neigh = queue[i].Item1;
                    i++;
                }

                queue.RemoveAll(n => n.Item2.Equals(current));

                foreach (var neighbour in MyGraph.NeighborsNodes(current))
                {
                    queue.Add(new Tuple<Node, Node, double>(current, neighbour, current.Weight(neighbour)));
                }

                MST.Add(GetEdge(current, neigh));
                previous = current;

                tree.Add(current);
                queue.Sort((n1, n2) => n1.Item3.CompareTo(n2.Item3));
            }

            foreach (var edge in MyGraph.Edges)
            {
                if (MST.Contains(edge))
                    edge.Color = Microsoft.Msagl.Drawing.Color.Green;
            }
        }

        //
        // Summary:
        //     Initialize menu in console for generate result as image.
        //      For more, go to GraphMenu method in Graph Class.
        public void PrimMenu()
        {
            if (MyGraph.Nodes.Count == 0 || MyGraph.Nodes == null || MyGraph.Edges.Count == 0 || MyGraph.Edges == null)
            {
                Console.WriteLine("Something went wrong with reading from file.\nTry upload your file one more time.\nI returned you to main menu.\n");
                return;
            }

            Random rnd = new Random();

            int RandomID = rnd.Next(1, MyGraph.Nodes.Count);

            Console.WriteLine($"Youe Random ID: {RandomID}");

            Node Start = MyGraph.Nodes.First(x => x.ID == RandomID);

            PrimAlgo(Start);

            MyGraph.GraphMenu("Prim", MST);
        }
    }
}

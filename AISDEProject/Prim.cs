using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace AISDEProject
{
    /// <summary>
    /// This class describes Prim's algorithm
    /// I truly recommend that you should read about Prim's algorithm before seeing code below.
    /// </summary>
    class Prim
    {
        /// <summary>
        /// My Graph property.
        /// </summary>
        /// <value> List of Edges and Nodes contained in the Graph.</value>
        /// <seealso cref="AISDEProject.MyGraph"/>
        public MyGraph MyGraph { get; set; }

        /// <summary>
        /// List of Nodes.
        /// </summary>
        /// <value>
        /// List of available Nodes which can be used in Prim's algorithm.
        /// </value>
        /// <seealso cref="AISDEProject.Node"/>
        public List<Node> Nodes { get; set; }

        /// <summary>
        /// List of Edges.
        /// </summary>
        /// <value>
        /// List of available Edges which can be used in Prim's algorithm.
        /// </value>
        /// <seealso cref="AISDEProject.Edge"/>
        public List<Edge> Edges { get; set; }

        /// <summary>
        /// Minimum Spanning Tree in Graph.
        /// </summary>
        /// <value>
        /// Edges contained in the MST(Minimum Spanning Tree) after Prim's algorithm.
        /// </value>
        /// <seealso cref="AISDEProject.Edge"/>
        public List<Edge> MST { get; set; }

        /// <summary>
        /// The default class constructor.
        /// </summary>
        public Prim()
        {
            MyGraph = new MyGraph();
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="myGraph">Nodes and Edges containedin the myGraph. 
        /// <seealso cref="AISDEProject.MyGraph"/>
        /// </param>
        public Prim(MyGraph myGraph)
        {
            MyGraph = myGraph;
            Nodes = new List<Node>(myGraph.Nodes);
            Edges = new List<Edge>(myGraph.Edges);
        }

        /// <summary>
        /// Get edge which is connection between two nodes.
        /// </summary>
        /// <param name="node1">One of all available nodes from List of Nodes.
        /// <seealso cref="AISDEProject.Node"/>
        /// </param>
        /// <param name="node2">Second remaining node from List of Nodes.
        /// <seealso cref="AISDEProject.Node"/>
        /// </param>
        /// <returns>Method returns Edge with these two nodes.<seealso cref="AISDEProject.Edge"/></returns>
        Edge GetEdge(Node node1, Node node2) => Edges.
                First(e => (e.Begin.Equals(node1) && e.End.Equals(node2)) || (e.Begin.Equals(node2) && e.End.Equals(node1)));

        /// <summary>
        /// Method searches MST (Minimum Spanning Tree) as available edges from List of Edge using Prim's algorithm.
        /// <see href="https://en.wikipedia.org/wiki/Prim%27s_algorithm">HERE, algorithm on wikipedia.</see>
        /// </summary>
        /// <param name="node">The Node class object to start algorithm. <seealso cref="AISDEProject.Node"/></param>
        public void PrimAlgo(Node node)
        {
            MST = new List<Edge>();
            var tree = new List<Node>();
            var queue = new List<Tuple<Node, Node, double>>();

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

        /// <summary>
        /// Initialize menu in console for generate result as image.
        /// </summary>
        /// <seealso cref="AISDEProject.MyGraph.GraphMenu(string, List{Edge})"/>
        public void PrimMenu()
        {
            if (MyGraph.Nodes.Count == 0 || MyGraph.Nodes == null || MyGraph.Edges.Count == 0 || MyGraph.Edges == null)
            {
                Console.WriteLine("Something went wrong with reading from file.\nTry upload your file one more time.\nI returned you to main menu.\n");
                return;
            }

            Random rnd = new Random();

            int RandomID = rnd.Next(1, MyGraph.Nodes.Count);

            Console.WriteLine($"Your Random ID: {RandomID}");

            Node Start = MyGraph.Nodes.First(x => x.ID == RandomID);

            PrimAlgo(Start);

            MyGraph.GraphMenu("Prim", MST);
        }
    }
}

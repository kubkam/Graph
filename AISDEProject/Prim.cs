using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace AISDEProject
{
    class Prim
    {
        #region Properties

        public MyGraph MyGraph { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        public Node Start { get; set; } = new Node();
        public List<Node> Neighbours { get; set; } = new List<Node>();
        public List<Edge> PrimEdges { get; set; } = new List<Edge>();
        public List<Node> MST { get; set; } = new List<Node>();

        #endregion 

        #region Constructors

        public Prim()
        {
            MyGraph = new MyGraph();

            Nodes = new List<Node>(MyGraph.Nodes);
            Edges = new List<Edge>(MyGraph.Edges);
        }

        public Prim(MyGraph myGraph)
        {
            MyGraph = myGraph;
            Nodes = new List<Node>(myGraph.Nodes);
            Edges = new List<Edge>(myGraph.Edges);
        }

        #endregion

        public List<Node> NeighborsNodes(Node node)
        {
            Neighbours = new List<Node>();
            foreach (var edge in Edges)
            {
                if (edge.Begin == node && Nodes.Contains(edge.End))
                {
                    Neighbours.Add(edge.End);
                }
                if (edge.End == node && Nodes.Contains(edge.Begin))
                {
                    Neighbours.Add(edge.Begin);
                }
            }
            Neighbours.Remove(node);

            return Neighbours;
        }

        Edge GetEdge(Node node1, Node node2)
        {
            return Edges.First(e => (e.Begin.Equals(node1) && e.End.Equals(node2)) || (e.Begin.Equals(node2) && e.End.Equals(node1)));
        }

        public void PrimAlgo(Node node)
        {
            var tree = new List<Node>();
            var queue = new List<Tuple<Node, Node, double>>();

            var neighbours = new Dictionary<Node, Node>();

            var current = node;
            var previous = current;

            tree.Add(current);

            foreach (var neighbour in NeighborsNodes(current))
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

                foreach (var neighbour in NeighborsNodes(current))
                {
                    queue.Add(new Tuple<Node, Node, double>(current, neighbour, current.Weight(neighbour)));
                }

                PrimEdges.Add(GetEdge(current, neigh));
                previous = current;

                tree.Add(current);
                queue.Sort((n1, n2) => n1.Item3.CompareTo(n2.Item3));
            }

            foreach (var edge in MyGraph.Edges)
            {
                if (PrimEdges.Contains(edge))
                    edge.Color = Microsoft.Msagl.Drawing.Color.Green;
            }
        }

        public void PrimMenu()
        {
            List<int> availableNodes = new List<int>();

            if (MyGraph.Nodes.Count == 0 || MyGraph.Nodes == null || MyGraph.Edges.Count == 0 || MyGraph.Edges == null)
            {
                Console.WriteLine("Something went wrong with reading from file.\nTry upload your file one more time.\nI returned you to main menu.\n");
                return;
            }

            foreach (var node in MyGraph.Nodes)
            {
                availableNodes.Add(node.ID);
            }

            Random rnd = new Random();
            int RandomID = rnd.Next(1, availableNodes.Count);

            Console.WriteLine($"Random ID: {RandomID}");

            Start = MyGraph.Nodes.First(x => x.ID == RandomID);

            PrimAlgo(Start);

            MyGraph.GraphMenu("Prim", PrimEdges);

        }
    }
}

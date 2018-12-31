using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;
using System.Drawing.Imaging;

namespace AISDEProject
{
    /// <summary>
    /// This class describes Dijkstra's algorithm
    /// I truly recommend that you should read about Dijkstra's algorithm before seeing code below.
    /// </summary>
    class Dijkstra
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
        /// List of available Nodes which can be used in Dijkstra's algorithm.
        /// </value>
        /// <seealso cref="AISDEProject.Node"/>
        public List<Node> Nodes { get; set; }

        /// <summary>
        /// List of Edges.
        /// </summary>
        /// <value>
        /// List of available Edges which can be used in Dijkstra's algorithm.
        /// </value>
        /// <seealso cref="AISDEProject.Edge"/>
        public List<Edge> Edges { get; set; }

        /// <summary>
        /// Shortest path between two nodes in Graph.
        /// </summary>
        /// <value>
        /// List of Edges contained in Shortest Path after Dijkstra's algorithm.
        /// </value>
        /// <seealso cref="AISDEProject.Dijkstra.DijkstraAlgo(Node, Node)"/>
        public List<Edge> DijkstraPath { get; set; }

        /// <summary>
        /// The default class constructor.
        /// </summary>
        public Dijkstra()
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
        public Dijkstra(MyGraph myGraph)
        {
            MyGraph = myGraph;
            Nodes = new List<Node>(myGraph.Nodes);
            Edges = new List<Edge>(myGraph.Edges);
        }

        /// <summary>
        /// Change label in all neighboring nodes from parameter node as a shortest distance to.
        /// </summary>
        /// <param name="node">The Node class object to start algorithm to find shortest path to all neighboring nodes.</param>
        public void GetShortestPath(Node node)
        {
            if (MyGraph.NeighborsNodes(node).Count == 0)
                return;

            foreach (var neigh in MyGraph.NeighborsNodes(node))
            {
                var edge = MyGraph.Edges.First(x => (x.Begin.ID == neigh.ID && x.End.ID == node.ID) || (x.Begin.ID == node.ID && x.End.ID == neigh.ID));

                if (neigh.Label > node.Label + edge.Cost)
                {
                    neigh.Label = node.Label + edge.Cost;
                    neigh.IDOfClosetNode = node.ID;
                }
            }
            Nodes.Remove(node);
        }

        /// <summary>
        /// Method searches Shortest path from startNode to endNode from available edges from List of Edges using Dijkstra's algorithm.
        /// </summary>
        /// <see href="https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm">HERE, algorithm on wikipedia.</see>
        /// <param name="startNode">The Node class object which is a start node.</param>
        /// <param name="endNode">The Node class object which is a end node.</param>
        /// <seealso cref="AISDEProject.Node"/>
        public void DijkstraAlgo(Node startNode, Node endNode)
        {
            DijkstraPath = new List<Edge>();
            Nodes = new List<Node>(MyGraph.Nodes);

            var prioQueue = new List<Node>();

            foreach (var node in MyGraph.Nodes)
            {
                if (node == startNode)
                {
                    node.Label = 0.0;
                    node.IDOfClosetNode = node.ID;
                }
                else
                {
                    node.Label = (double)Int32.MaxValue;
                    node.IDOfClosetNode = 0;
                }
            }

            prioQueue.Add(startNode);
            do
            {
                Node next = prioQueue.First();

                foreach (var node in MyGraph.NeighborsNodes(next))
                {
                    prioQueue.Add(node);
                }

                foreach (var node in prioQueue)
                {
                    GetShortestPath(node);
                }

                prioQueue.Remove(next);

            } while (Nodes.Count() != 0 && prioQueue.Count() != 0);

            Node tmp = endNode;
            var Visited = new List<Node>();
            Visited.Add(tmp);

            do
            {
                foreach (var node in MyGraph.Nodes)
                {
                    if (node.ID == tmp.IDOfClosetNode)
                    {
                        Visited.Add(node);
                        var edge = MyGraph.Edges.First(x => (x.Begin == tmp && x.End == node) || (x.End == tmp && x.Begin == node));
                        DijkstraPath.Add(edge);
                        tmp = node;
                    }
                }
            } while (tmp.ID != startNode.ID);

            foreach (var edge in MyGraph.Edges)
            {
                if (DijkstraPath.Contains(edge))
                    edge.Color = Microsoft.Msagl.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Initialize menu in console for generate result as image.
        /// </summary>
        /// <seealso cref="AISDEProject.MyGraph.GraphMenu(string, List{Edge})"/>
        public void DijkstraMenu()
        {
            int start = 0, end = 0;
            List<int> availableNodes = new List<int>();

            if (MyGraph.Nodes.Count == 0 || MyGraph.Nodes == null || MyGraph.Edges.Count == 0 || MyGraph.Edges == null)
            {
                Console.WriteLine("Something went wrong with reading from file.\nTry upload your file one more time.\nI returned you to main menu.\n");
                return;
            }
            Console.WriteLine("Available Nodes:\n");
            foreach (var node in MyGraph.Nodes)
            {
                Console.Write($"{node.ID}\t");
                availableNodes.Add(node.ID);
            }
            Console.WriteLine("\nNow you must enter 2 numbers, which are IDs of Nodes. You cannot enter twice the same Node, because there is no track between same Node\n");

            do
            {
                Console.WriteLine("Enter ID of Start Node: ");
                try
                {
                    start = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }

            } while (!availableNodes.Contains(start));

            Node Start = MyGraph.Nodes.First(x => x.ID == start);

            availableNodes.Remove(start);

            do
            {
                Console.WriteLine("Enter ID of End Node: ");
                try
                {
                    end = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }

            } while (!availableNodes.Contains(end));

            Node End = MyGraph.Nodes.First(x => x.ID == end);

            DijkstraAlgo(Start, End);

            MyGraph.GraphMenu("Dijkstra", DijkstraPath);

        }
    }
}

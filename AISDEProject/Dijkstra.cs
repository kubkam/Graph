using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;
using System.Drawing.Imaging;

namespace AISDEProject
{
    //I truly recommend that you should read about Dijkstra's algorithm before seeing code below.
    class Dijkstra
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
        //      Gets list of all available Nodes contained in Dijkstra's Algorithm.    
        //
        // Returns:
        //      List of available Nodes contained in the Dijkstra's Algorithm.
        public List<Node> Nodes { get; set; }

        //
        // Summary:
        //      Gets list of all available Edges contained in Dijkstra's Algorithm.    
        //
        // Returns:
        //      List of available Edges contained in the Dijkstra's Algorithm.
        public List<Edge> Edges { get; set; }

        //
        // Summary:
        //     Gets list of all edges contained in the Shortest Path from one certain node to another after Dijkstra's algorithm.
        //
        // Returns:
        //     Edges contained in the Shortest Path between two different nodes after Dijkstra's algorithm.
        public List<Edge> DijkstraPath { get; set; }

        public Dijkstra()
        {
            MyGraph = new MyGraph();
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public Dijkstra(MyGraph myGraph)
        {
            MyGraph = myGraph;
            Nodes = new List<Node>(myGraph.Nodes);
            Edges = new List<Edge>(myGraph.Edges);
        }
        
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

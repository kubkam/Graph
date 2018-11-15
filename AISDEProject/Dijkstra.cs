using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;
using System.Drawing.Imaging;

namespace AISDEProject
{

    class Dijkstra
    {
        public MyGraph MyGraph { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public Node Start { get; set; } = new Node();
        public Node End { get; set; } = new Node();
        public List<Node> Neighbours { get; set; } = new List<Node>();
        public List<Edge> DijkstraEdges { get; set; } = new List<Edge>();

        public Dijkstra()
        {
            MyGraph = new MyGraph();

            Nodes = new List<Node>(MyGraph.Nodes);
            Edges = new List<Edge>(MyGraph.Edges);
        }

        public Dijkstra(MyGraph myGraph)
        {
            MyGraph = myGraph;
            Nodes = new List<Node>(myGraph.Nodes);
            Edges = new List<Edge>(myGraph.Edges);
        }

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

        public void GetShortestPath(Node node)
        {
            if (Neighbours.Count == 0)
                return;

            foreach (var neigh in NeighborsNodes(node))
            {
                if (neigh.Label > node.Label + neigh.Weight(node))
                {
                    neigh.Label = node.Label + neigh.Weight(node);
                    neigh.IDOfClosetNode = node.ID;
                }
            }
            Nodes.Remove(node);
        }

        public void DijkstraAlgo(Node startNode, Node endNode)
        {
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

                foreach (var node in NeighborsNodes(next))
                {
                    prioQueue.Add(node);
                }

                foreach (var node in prioQueue)
                {
                    GetShortestPath(node);
                }

                prioQueue.Remove(next);

            } while (Nodes.Count() != 0 && prioQueue.Count() != 0);

            Node tmp = new Node();
            tmp = endNode;
            int count = 1;
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
                        DijkstraEdges.Add(edge);
                        tmp = node;

                        count++;
                    }
                }
            } while (tmp.ID != startNode.ID);

            foreach (var edge in MyGraph.Edges)
            {
                if (DijkstraEdges.Contains(edge))
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

            Start = MyGraph.Nodes.First(x => x.ID == start);

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

            End = MyGraph.Nodes.First(x => x.ID == end);

            DijkstraAlgo(Start, End);

            MyGraph.GraphMenu("Dijkstra", DijkstraEdges);

        }
    }
}

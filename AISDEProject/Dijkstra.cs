using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEProject
{

    class Dijkstra
    {
        public MyGraph MyGraph { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public Node Start { get; set; } = new Node();
        public List<Node> Neighbours { get; set; }
        public List<Edge> DijkstraEdges { get; set; }


        public Dijkstra()
        {
            MyGraph = new MyGraph();
            MyGraph.GraphFromFile(Global.PATH);

            Nodes = new List<Node>(MyGraph.Nodes);

            Edges = new List<Edge>(MyGraph.Edges);
        }


        public List<Node> NeighborsNodes(Node node)
        {
            Neighbours = new List<Node>();
            DijkstraEdges = new List<Edge>();
            foreach (var edge in Edges)
            {
                if (edge.Begin == node && Nodes.Contains(edge.End))
                {
                    Neighbours.Add(edge.End);
                    DijkstraEdges.Add(edge);
                }
                if (edge.End == node && Nodes.Contains(edge.Begin))
                {
                    Neighbours.Add(edge.Begin);
                    DijkstraEdges.Add(edge);
                }
            }
            Neighbours.Remove(node);

            return Neighbours;
        }
        
        public void GetShortestPath(Node node)
        {

            var neighs = NeighborsNodes(node);

            if (neighs.Count == 0)
                return;


            foreach (var neigh in neighs)
            {
                if (neigh.Label > node.Label + neigh.Weight(node))
                {
                    neigh.Label = node.Label + neigh.Weight(node);

                }

            }

            Nodes.Remove(node);

        }

        public void TestLabel()
        {
            Start = MyGraph.Nodes.First(x => x.ID == 1);

            foreach (var node in Nodes)
            {
                if (node == Start)
                    node.Label = 0.0;
                else
                    node.Label = (double)Int32.MaxValue;
            }

            foreach (var node in MyGraph.Nodes)
            {
                GetShortestPath(node);
            }

            foreach (var node in MyGraph.Nodes)
            {
                Console.WriteLine($"ID: {node.ID} ; Label: {node.Label}\n");
            }
        }

        public void DijkstraAlgo()
        {
            Start = MyGraph.Nodes.First(x => x.ID == 1);
            Node tmp = new Node();

            tmp = Start;

            foreach (var node in Nodes)
            {
                if (node == Start)
                    node.Label = 0.0;
                else
                    node.Label = (double)Int32.MaxValue;
            }

            var prioQueue = new List<Node>();




        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl;
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
        public List<Node> Neighbours { get; set; }
        public List<Edge> DijkstraEdges { get; set; } = new List<Edge>();


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

            var neighs = NeighborsNodes(node);

            if (neighs.Count == 0)
                return;


            foreach (var neigh in neighs)
            {
                if (neigh.Label > node.Label + neigh.Weight(node))
                {
                    neigh.Label = node.Label + neigh.Weight(node);
                    neigh.IDOfClosetNode = node.ID;

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
                {
                    node.Label = 0.0;
                    node.IDOfClosetNode = 1;
                }
                else
                {
                    node.Label = (double)Int32.MaxValue;
                    node.IDOfClosetNode = 0;
                }
                    
            }

            foreach (var node in MyGraph.Nodes)
            {
                GetShortestPath(node);
            }

            foreach (var node in MyGraph.Nodes)
            {
                Console.WriteLine($"ID: {node.ID} ; Label: {node.Label.ToString("0.00")}   ;   Closet Node: {node.IDOfClosetNode}\n");
            }
        }

        public void DijkstraAlgo()
        {
            Start = MyGraph.Nodes.First(x => x.ID == 1);
            foreach (var node in Nodes)
            {
                if (node == Start)
                {
                    node.Label = 0.0;
                    node.IDOfClosetNode = 1;
                }
                else
                {
                    node.Label = (double)Int32.MaxValue;
                    node.IDOfClosetNode = 0;
                }

            }

            foreach (var node in MyGraph.Nodes)
            {
                GetShortestPath(node);
            }

            End = MyGraph.Nodes.First(x => x.ID == 9);
            Node tmp = new Node();
            tmp = End;
            int count = 1;
            var prioQueue = new List<Node>();
            prioQueue.Add(tmp);

            do
            {
                foreach (var node in MyGraph.Nodes)
                {
                    if (node.ID == tmp.IDOfClosetNode)
                    {
                        prioQueue.Add(node);
                        var edge = MyGraph.Edges.First(x => (x.Begin == tmp && x.End == node) || (x.End == tmp && x.Begin == node));
                        DijkstraEdges.Add(edge);
                        tmp = node;
                        
                        count++;
                    }
                }
                

            } while (tmp.ID != 1);

            foreach (var edge in MyGraph.Edges)
            {
                if (DijkstraEdges.Contains(edge))
                    edge.Color = Microsoft.Msagl.Drawing.Color.Red;
            }


        }


        public Graph CreateGraph()
        {
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph();

            foreach (var node in MyGraph.Nodes)
            {
                graph.AddNode(node.ID.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
            }

            foreach (var edge in MyGraph.Edges)
            {
                var ed = graph.AddEdge(edge.Begin.ID.ToString(),
                    Edge.Weight(edge.Begin, edge.End).ToString("#.00"),
                    edge.End.ID.ToString());


                if (DijkstraEdges.Exists(x => x.Color == edge.Color))
                    ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                else
                    ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;

                ed.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }

            return graph;
        }

        public void SaveGraphAsImage(string path)
        {
            Graph tmp = CreateGraph();

            GraphRenderer graphRenderer = new GraphRenderer(tmp);

            GraphRenderer renderer = new GraphRenderer(tmp);
            renderer.CalculateLayout();
            int width = 1000;
            Bitmap bitmap = new Bitmap(width, (int)(tmp.Height * (width / tmp.Width)), PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);
            bitmap.Save(path);
        }



    }
}

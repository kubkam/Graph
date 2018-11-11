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
using System.IO;

namespace AISDEProject
{
    class Prim
    {
        public MyGraph MyGraph { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public List<Edge> EdgesNeig { get; set; }
        public List<Node> Neighbours { get; set; }
        public Node Start { get; set; } = new Node();
        public List<Edge> PrimEdges { get; set; }
        public List<Node> PrimNodes { get; set; }


        public Prim()
        {
            MyGraph = new MyGraph();
            MyGraph.GraphFromFile(Global.PATH);

            Nodes = new List<Node>(MyGraph.Nodes);
 
            Edges = new List<Edge>(MyGraph.Edges);
        }

        public List<Node> Neighbors(Node node)
        {
            Neighbours = new List<Node>();
            EdgesNeig = new List<Edge>();
            foreach (var edge in Edges)
            {
                if (edge.Begin == node && Nodes.Contains(edge.End))
                {
                    Neighbours.Add(edge.End);
                    EdgesNeig.Add(edge);
                }
                if (edge.End == node && Nodes.Contains(edge.Begin))
                {
                    Neighbours.Add(edge.Begin);
                    EdgesNeig.Add(edge);
                }
            }

            return Neighbours;
        }

        private bool IsNeighbors(List<Node> prim, Node node)
        {
            var neigbors = new List<Node>();
            neigbors = Neighbors(node);

            foreach (var neighbor in neigbors)
            {
                if (prim.Contains(neighbor))
                    continue;
                else
                    return false;

            }
            return true;
        }

        public void AlgoPrim()
        {
            Start = Nodes.First(x => x.ID == 1);

            PrimNodes = new List<Node>();
            PrimEdges = new List<Edge>();

            var tmp = Start;
            PrimNodes.Add(tmp);
            int count = 1;
            Nodes.Remove(tmp);

            do
            {
                var orderby = from x in Neighbors(tmp)
                            orderby x.Weight(tmp)
                            select x;

                if (orderby.Count() == 0)
                    break;
                
                var edge = Edges.First(x => x.Begin == tmp || x.End == tmp);
                Edges.Remove(edge);

                var next = orderby.First();

                PrimEdges.Add(new Edge(count, tmp, next, tmp.Weight(next), Microsoft.Msagl.Drawing.Color.Red));
                count++;
                Nodes.Remove(tmp);
                
                tmp = next;
                PrimNodes.Add(tmp);

            } while (true);

            /*
            var tempPrimNode = new List<Node>();
            tempPrimNode = PrimNodes;

            foreach (var prim in tempPrimNode)
            {
                var list = from x in Neighbors(tmp)
                           orderby x.Weight(tmp)
                           select x;

                foreach (var item in list)
                {
                    if (PrimNodes.Contains(item))
                    {
                        PrimNodes.Add(item);

                    }
                }
                

            }
            */


        }

        public Graph CreateGraph(List<Node> nodes, List<Edge> edges)
        {
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph();

            foreach (var node in nodes)
            {
                graph.AddNode(node.ID.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
            }

            foreach (var edge in edges)
            {
                var ed = graph.AddEdge(edge.Begin.ID.ToString(),
                    Edge.Weight(edge.Begin, edge.End).ToString("#.00"),
                    edge.End.ID.ToString());


                if(PrimEdges.Exists(x => x.Begin == edge.Begin || x.End == edge.End))
                    ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                else
                    ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;

                ed.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }

            foreach (var edge in PrimEdges)
            {
                var ed = graph.AddEdge(edge.Begin.ID.ToString(),
                    Edge.Weight(edge.Begin, edge.End).ToString("#.00"),
                    edge.End.ID.ToString());

                ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                ed.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }

            return graph;
        }

        public void SaveGraphAsImage(string path, List<Node> nodes, List<Edge> edges)
        {
            Graph tmp = CreateGraph(nodes, edges);

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

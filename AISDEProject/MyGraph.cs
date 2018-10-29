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
    class MyGraph
    {
        #region Public Default Properties

        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        #endregion

        #region Constructors

        public MyGraph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public MyGraph(List<Node> nodes, List<Edge> edges)
        {
            Nodes = nodes;
            Edges = edges;  
        }

        #endregion

        public override string ToString() => $"Nodes: {Nodes}\n Edges: {Edges}";

        public Graph CreateGraph()
        {
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph();

            foreach (var node in Nodes)
            {
                graph.AddNode(node.ID.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
            }

            foreach (var edge in Edges)
            {
                var ed = graph.AddEdge(edge.Begin.ID.ToString(), Edge.Weight(edge.Begin, edge.End).ToString("#.00"), edge.End.ID.ToString());
                ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                ed.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;

            }

            return graph;
        }

        public void SaveGraphAsImage()
        {
            Graph tmp = CreateGraph();

            GraphRenderer graphRenderer = new GraphRenderer(tmp);

            GraphRenderer renderer = new GraphRenderer(tmp);
            renderer.CalculateLayout();
            int width = 1000;
            Bitmap bitmap = new Bitmap(width, (int)(tmp.Height * (width / tmp.Width)), PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);
            bitmap.Save("test.png");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;
using System.Drawing.Imaging;

namespace AISDEProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ////create a form 
            //System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            ////create a viewer object 
            //Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            ////create a graph object 
            //Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            ////create the graph content 
            //graph.AddEdge("A", "B");
            //graph.AddEdge("B", "C");
            //graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            //graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            //graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            //Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            //c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            //c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            ////bind the graph to the viewer 
            //viewer.Graph = graph;
            ////associate the viewer with the form 
            //form.SuspendLayout();
            //viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            //form.Controls.Add(viewer);
            //form.ResumeLayout();
            ////show the form 
            //form.ShowDialog();

            //Microsoft.Msagl.Drawing.Graph graph = new
            //Microsoft.Msagl.Drawing.Graph("");
            //graph.AddEdge("A", "B");
            //graph.AddEdge("A", "B");
            //graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            //graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Blue;
            //GraphRenderer renderer = new GraphRenderer(graph);
            //renderer.CalculateLayout();
            //int width = 200;
            //Bitmap bitmap = new Bitmap(width, (int)(graph.Height * (width / graph.Width)), PixelFormat.Format32bppPArgb);
            //renderer.Render(bitmap);
            //bitmap.Save("test.png");

            Network network = new Network();

            network.GraphFromFile(@"C:\Users\Kuba\Desktop\AIXDE\AISDEProject\AISDEProject\network.txt");

            network.showNodes();

            network.showEdges();

            Console.WriteLine("End");

            Console.ReadKey();
        }
    }
}

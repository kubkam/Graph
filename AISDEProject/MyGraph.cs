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
    static class Global
    {
        public static string PATH = @"C:\Users\Kuba\Desktop\AIXDE\AISDEProject\AISDEProject\network.txt";
    }

    class MyGraph
    {
        #region Public Default Properties

        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public int NumberOfNodes { get; set; }
        public int NumberOfEdges { get; set; }

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
                var ed = graph.AddEdge(edge.Begin.ID.ToString(),
                    Edge.Weight(edge.Begin, edge.End).ToString("#.00"),
                    edge.End.ID.ToString());

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

        public void GraphFromFile(string Path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    bool flagNode = false;
                    bool flagEdge = false;
                    int edges = 0;
                    int nodes = 0;
                    string line;

                    //Make it better :)
                    while ((line = sr.ReadLine()) != null)
                    {
                        StringBuilder sb = new StringBuilder();

                        if (line.StartsWith("#"))
                        {
                            continue;
                        }
                        else if (line.StartsWith("WEZLY"))
                        {

                            int tmp = 0;
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] == ' ')
                                {
                                    tmp++;
                                }
                                if (tmp == 2)
                                {
                                    sb.Append(line[i]);
                                }
                            }
                            int numberOfNodes = Int32.Parse(sb.ToString());
                            NumberOfNodes = numberOfNodes;

                            //Console.WriteLine($"Number of Nodes: {MyGraph.NumberOfNodes}");
                            flagNode = true;
                        }
                        else if (flagNode && (nodes < NumberOfNodes))
                        {
                            int id = 0, x = 0, y = 0;
                            int j = 0, count = 0;
                            StringBuilder stringBuilder = new StringBuilder();
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] != ' ')
                                {
                                    stringBuilder.Append(line[i]);
                                    j++;
                                }
                                else
                                {
                                    if (count == 0)
                                    {

                                        id = Int32.Parse(stringBuilder.ToString());
                                        j = 0;
                                        stringBuilder.Clear();
                                    }
                                    else if (count == 1)
                                    {
                                        x = Int32.Parse(stringBuilder.ToString());
                                        j = 0;
                                        stringBuilder.Clear();
                                    }
                                    count++;
                                }
                            }
                            y = Int32.Parse(stringBuilder.ToString());
                            j = 0;
                            stringBuilder.Clear();
                            Nodes.Add(new Node() { ID = id, X = x, Y = y });
                            nodes++;

                        }
                        else if (line.StartsWith("LACZA"))
                        {
                            flagNode = false;
                            int tmp = 0;
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] == ' ')
                                {
                                    tmp++;
                                }
                                if (tmp == 2)
                                {
                                    sb.Append(line[i]);
                                }
                            }
                            int numberOfEdges = Int32.Parse(sb.ToString());
                            NumberOfEdges = numberOfEdges;

                            flagEdge = true;
                            //Console.WriteLine($"Number of Links: {MyGraph.NumberOfLinks}");
                        }
                        else if (flagEdge && (edges < NumberOfEdges))
                        {
                            int id = 0, begin = 0, end = 0;
                            int j = 0, count = 0;
                            StringBuilder stringBuilder = new StringBuilder();
                            //add number of Edges to variable

                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] != ' ')
                                {
                                    stringBuilder.Append(line[i]);
                                    j++;
                                }
                                else
                                {
                                    if (count == 0)
                                    {

                                        id = Int32.Parse(stringBuilder.ToString());
                                        j = 0;
                                        stringBuilder.Clear();
                                    }
                                    else if (count == 1)
                                    {
                                        begin = Int32.Parse(stringBuilder.ToString());
                                        j = 0;
                                        stringBuilder.Clear();
                                    }
                                    count++;
                                }
                            }
                            end = Int32.Parse(stringBuilder.ToString());
                            j = 0;
                            stringBuilder.Clear();

                            Node tmp1 = Nodes.Find(x => x.ID == begin);
                            Node tmp2 = Nodes.Find(x => x.ID == end);


                            Edges.Add(new Edge(id, tmp1, tmp2, Edge.Weight(tmp1, tmp2), Microsoft.Msagl.Drawing.Color.Black));
                            edges++;
                        }

                        sb.Clear();
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Exception:" + e.Message);
            }
        }
    }
}

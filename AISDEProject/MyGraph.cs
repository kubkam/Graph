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
using System.Text.RegularExpressions;

namespace AISDEProject
{
    static class Global
    {
        public static string PATH = @"C:\Users\Kuba\Desktop\AIXDE\AISDEProject\AISDEProject\network.txt";
    }

    class MyGraph
    {
        #region Public Default Properties

        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Edge> Edges { get; set; } = new List<Edge>();
        public int NumberOfNodes { get; set; }
        public int NumberOfEdges { get; set; }

        #endregion

        #region Constructors

        public MyGraph()
        {
            //Nodes = new List<Node>();
            //Edges = new List<Edge>();
        }

        public MyGraph(List<Node> nodes, List<Edge> edges)
        {
            Nodes = new List<Node>(nodes);
            Edges = new List<Edge>(edges);  
        }

        #endregion

        public override string ToString() => $"Nodes: {Nodes}\n Edges: {Edges}";
        
        public Graph CreateGraph(List<Edge> edges)
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

                if (edges == null)
                {
                    ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    ed.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                }
                else
                {
                    if (edges.Exists(x => x.Color == edge.Color))
                        ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    else
                        ed.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    ed.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                }
            }

            return graph;
        }

        public void SaveGraphAsImage(string path, List<Edge> edges)
        {
            Graph tmp = CreateGraph(edges);

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

        public void GraphMenu(string Name, List<Edge> edges)
        {
            
            string filename = null;
            string fullpath = null;

            if (Nodes == null || Edges == null || Nodes.Count() == 0 || Edges.Count() == 0)
                Console.WriteLine("Something went wrong with reading from file.\nTry upload your file one more time.\nI returned you to main menu.\n");
            else
            {

                Console.WriteLine($"\nWelcome to {Name} Menu\n");

                do
                {
                    Console.WriteLine($"Please enter filename. I will save {Name} as picture");
                    try
                    {
                        filename = Console.ReadLine();
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                    }

                } while (filename != null && !IsValidFilename(filename));

                int choice = -1;
                
                do
                {
                    Console.WriteLine(@"Now you must choose file format:
        [1] JPG
        [2] PNG
        [3] GIF
        [0] I changed my mind. I want to quit");
                
                try
                {
                    Console.Write("\nYour choice is?: ");
                    choice = int.Parse(Console.ReadLine());

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                
                    switch (choice)
                    {
                        case 1:
                            fullpath = String.Concat(filename, ".jpg");
                            break;

                        case 2:
                            fullpath = String.Concat(filename, ".png");
                            break;

                        case 3:
                            fullpath = String.Concat(filename, ".gif");
                            break;

                        case 0:
                            Console.WriteLine($"You quit {Name} menu");
                            return;

                        default:
                            Console.Clear();
                            Console.WriteLine("Wrong option.\n");
                            break;
                    }
                } while ((choice != 1) && (choice != 2) && (choice != 3));

                SaveGraphAsImage(fullpath, edges);
            }
        }

        bool IsValidFilename(string testName)
        {
            Regex containsABadCharacterPath = new Regex("["
                + Regex.Escape(new string(System.IO.Path.GetInvalidPathChars())) + "]");

            Regex containsABadCharacterName = new Regex("["
                + Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars())) + "]");

            if (containsABadCharacterPath.IsMatch(testName) || containsABadCharacterName.IsMatch(testName)) { return false; };

            return true;
        }
    }
}

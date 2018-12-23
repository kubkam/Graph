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
    class MyGraph
    {
        #region Public Properties

        public List<Node> Nodes { get; set; } 
        public List<Edge> Edges { get; set; }
        public int NumberOfNodes { get; set; }
        public int NumberOfEdges { get; set; }

        #endregion

        #region Constructor

        public MyGraph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
            NumberOfEdges = 0;
            NumberOfNodes = 0;
        }

        #endregion

        #region Creating Graph from List of Edges

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
                    edge.Cost.ToString("#.00"),
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

        #endregion

        #region Saving Graph as Image with given name as a parameter

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

        #endregion

        #region Reading Edges and Nodes from file to make Graph

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
                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("#"))
                        {
                            continue;
                        }
                        else if (line.StartsWith("Nodes"))
                        {
                            string[] words = line.Split();

                            NumberOfNodes = int.Parse(words[2]);

                            flagNode = true;
                        }
                        else if (flagNode && (nodes < NumberOfNodes))
                        {
                            string[] variables = line.Split();

                            Nodes.Add(new Node(int.Parse(variables[0]), int.Parse(variables[1]), int.Parse(variables[2])));

                            nodes++;
                        }
                        else if (line.StartsWith("Edges"))
                        {
                            string[] words = line.Split();

                            flagNode = false;
                            
                            NumberOfEdges = int.Parse(words[2]);

                            flagEdge = true;
                        }
                        else if (flagEdge && (edges < NumberOfEdges))
                        {
                            string[] variables = line.Split();

                            Node begin, end;

                            begin = Nodes.First(x => x.ID == int.Parse(variables[1]));
                            end = Nodes.First(x => x.ID == int.Parse(variables[2]));

                            Edges.Add(new Edge(int.Parse(variables[0]), begin, end));

                            edges++;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Exception:" + e.Message);
            }
        }

        #endregion

        #region GraphMenu

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

        #endregion

        public override string ToString() => $"Nodes: {Nodes}\n Edges: {Edges}";

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

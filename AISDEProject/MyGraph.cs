﻿using System;
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
    /// <summary>
    /// This class describes Graph
    /// </summary>
    class MyGraph
    {
        public List<Node> Nodes { get; set; } 
        public List<Edge> Edges { get; set; }
        public int NumberOfNodes { get; set; }
        public int NumberOfEdges { get; set; }

        public MyGraph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
            NumberOfEdges = 0;
            NumberOfNodes = 0;
        }

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

        public void GraphMenu(string name, List<Edge> edges)
        {
            if (Nodes == null || Edges == null || Nodes.Count() == 0 || Edges.Count() == 0)
                Console.WriteLine("Something went wrong with reading from file.\nTry upload your file one more time.\nI returned you to main menu.\n");
            else
            {
                string tmp = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
                string fileName = $"yourFiles\\{name}.png";

                string fullPath = String.Concat(tmp, fileName);

                SaveGraphAsImage(fullPath, edges);

                Console.WriteLine($"Generated graph was saved in yourFiles folder successfully. Image of your graph has name {name}.png");
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

        //
        // Summary:
        //      Searches the graph for neighbouring nodes.
        //      When none are found, returns empty list.
        //
        // Parameters:
        //   node:
        //      The node to search its neighbouring nodes.
        //
        // Returns:
        //      List of neighbouring nodes from node.
        //
        // Exceptions:
        //      None.
        public List<Node> NeighborsNodes(Node node)
        {
            List<Node> Neighbours = new List<Node>();
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
    }
}

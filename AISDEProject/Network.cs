﻿using System;
using System.IO;
using System.Collections;
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
    class Network
    {
        #region Public Properties

        public MyGraph MyGraph { get; set; }

        #endregion

        #region Constructor

        public Network()
        {
            MyGraph = new MyGraph();
        }

        #endregion

        //temp methods
        /*
        public void showNodes()
        {
            foreach (var node in MyGraph.Nodes)
            {
                Console.WriteLine(node);
            }
        }

        public void showEdges()
        {
            foreach (var edge in MyGraph.Edges)
            {
                Console.WriteLine(edge);
            }
        }
        */

        public void GraphFromFile(string Path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    string line;
                    int NumberOfHashSigns = 0;

                    //Make it better :)
                    while ((line = sr.ReadLine()) != null)
                    {
                        StringBuilder sb = new StringBuilder();

                        if (line.StartsWith("#"))
                        {
                            NumberOfHashSigns++;
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
                            int NumberOfNodes = Int32.Parse(sb.ToString());
                            //MyGraph.NumberOfNodes = NumberOfNodes;

                            //Console.WriteLine($"Number of Nodes: {MyGraph.NumberOfNodes}");
                        }
                        else if (NumberOfHashSigns == 4)//Console.WriteLine(NumberOFNodes);
                        {
                            int id = 0, x = 0, y = 0;

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
                            MyGraph.Nodes.Add(new Node() { ID = id, X = x, Y = y });
                            
                        }
                        else if (line.StartsWith("LACZA"))
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
                            int NumberOfLinks = Int32.Parse(sb.ToString());
                            //MyGraph.NumberOfLinks = NumberOfLinks;

                            //Console.WriteLine($"Number of Links: {MyGraph.NumberOfLinks}");
                        }
                        else if (NumberOfHashSigns == 6)
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

                            Node tmp1 = MyGraph.Nodes.Find(x => x.ID == begin);
                            Node tmp2 = MyGraph.Nodes.Find(x => x.ID == end);

                            MyGraph.Edges.Add(new Edge(id, tmp1, tmp2, Edge.Weight(tmp1, tmp2), Microsoft.Msagl.Drawing.Color.Black ));
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
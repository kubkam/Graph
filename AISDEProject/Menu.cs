using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AISDEProject
{
    /// <summary>
    /// This class describes Console Menu
    /// </summary>
    class Menu
    {
        public Dijkstra Dijkstra { get; set; }
        public Prim Prim { get; set; }
        public MyGraph MyGraph { get; set; }

        private readonly string menu = @"Chose one from following option:
        [1] Read file network.txt from yourFiles folder. If you want to change it, you should go to yourFiles folder and change network.txt
        [2] Graph menu <- Generate Graph from network.txt file
        [3] Dijkstra menu <- Generate Graph and Shortest Path from 2 nodes (Dijkstra's algorithm)
        [4] Prim menu <- Generate Graph and Minimum Spanning Tree from random node (Prim's algorithm)
        [0] Quit and close";

        public Menu()
        {
            //MyGraph = new MyGraph();
            //Dijkstra = new Dijkstra();
            //Prim = new Prim();
        }

        public void ContextMenu()
        {
            do
            {
                int choice = -1;
                Console.WriteLine(menu);

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
                        string tmp = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
                        string fullPath = String.Concat(tmp, @"yourFiles\network.txt");
                        MyGraph = new MyGraph();
                        MyGraph.GraphFromFile(fullPath);
                        break;

                    case 2:
                        Console.Clear();
                        if (MyGraph == null)
                        {
                            Console.WriteLine("Something went wrong. Try again or/and check text file (network.txt) in yourFiles folder.\n");
                            break;
                        }
                        MyGraph.GraphMenu("Graph", null);
                        break;

                    case 3:
                        Console.Clear();
                        Dijkstra = new Dijkstra(MyGraph);
                        Dijkstra.DijkstraMenu();
                        break;

                    case 4:
                        Console.Clear();
                        Prim = new Prim(MyGraph);
                        Prim.PrimMenu();
                        break;

                    case 0:
                        Console.WriteLine("See you later :)");
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong option.\n");
                        break;
                }

            } while (true);

        }
    }
}

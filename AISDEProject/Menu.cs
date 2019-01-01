using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [1] Drag'n'Drop file to read Graph from File
        [2] Graph menu
        [3] Dijkstra menu
        [4] Prim menu
        [0] Quit and close";

        public Menu()
        {
            //MyGraph = new MyGraph();
            //Dijkstra = new Dijkstra();
            //Prim = new Prim();
        }

        public void ContextMenu()
        {
            Console.WriteLine(@"Welcome to our project :)");

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
                        Console.WriteLine(@"Drag'n'Drop your file, paste full path of your file or write your filename, which is in bin\Debug folder of your project:");
                        string path = Console.ReadLine();
                        MyGraph = new MyGraph();
                        MyGraph.GraphFromFile(path);
                        break;

                    case 2:
                        Console.Clear();
                        if (MyGraph == null)
                        {
                            Console.WriteLine("Something went wrong. Try to upload file one more time.\n");
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

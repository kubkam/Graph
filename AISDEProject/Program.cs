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

            //If u want to use it, uncomment these methods in Network Class
            /*
            network.showNodes();

            network.showEdges();
            */

            
            //MyGraph myGraph = new MyGraph();

            //myGraph.GraphFromFile(Global.PATH);
            //myGraph.CreateGraph(myGraph.Nodes, myGraph.Edges);
            //myGraph.SaveGraphAsImage("test.png");
            

            //Dijkstra dijkstra = new Dijkstra();

            //dijkstra.Algorithm();

            Dijkstra dijkstra = new Dijkstra();

            //dijkstra.MyGraph.GraphFromFile(Global.PATH);

            Console.WriteLine("Enter Node ID: ");
            int nodeID = Convert.ToInt32(Console.ReadLine());
            Console.ReadKey();

            Node node = new Node();
            //dijkstra.MyGraph.GraphFromFile(Global.PATH);
            node = dijkstra.MyGraph.Nodes.First(x => x.ID == nodeID);

            //dijkstra.MyGraph.CreateGraph(dijkstra.Nodes, dijkstra.Edges);
            dijkstra.MyGraph.SaveGraphAsImage("dijkstra.png", dijkstra.PrevNodes(node), dijkstra.Edges);

            Console.WriteLine("End");

            Console.ReadKey();
        }
    }
}

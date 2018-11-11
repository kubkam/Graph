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
            
            MyGraph myGraph = new MyGraph();
            myGraph.GraphFromFile(Global.PATH);
            myGraph.CreateGraph();

            myGraph.SaveGraphAsImage("network.jpg");


            Dijkstra dijkstra = new Dijkstra();

            //dijkstra.MyGraph.GraphFromFile(Global.PATH);

            dijkstra.TestLabel();
            
            /*
            Prim prim = new Prim();
            
            //prim.MyGraph.GraphFromFile(Global.PATH);

            prim.AlgoPrim();

            //prim.CreateGraph(prim.PrimNodes, prim.PrimEdges);
            prim.CreateGraph(myGraph.Nodes, myGraph.Edges);

            //prim.SaveGraphAsImage("prim.jpg", prim.PrimNodes, prim.PrimEdges);
            prim.SaveGraphAsImage("prim.jpg", myGraph.Nodes, myGraph.Edges);

            */

            Console.WriteLine("End");

            Console.ReadKey();
            
        }
    }
}

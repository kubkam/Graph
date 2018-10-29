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
            Network network = new Network();

            network.GraphFromFile(@"C:\Users\Kuba\Desktop\AIXDE\AISDEProject\AISDEProject\network.txt");

            //If u want to use it, uncomment these methods in Network Class
            /*
            network.showNodes();

            network.showEdges();
            */

            network.MyGraph.SaveGraphAsImage();

            Console.WriteLine("End");

            Console.ReadKey();
        }
    }
}

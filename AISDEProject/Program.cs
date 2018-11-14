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

            Menu menu = new Menu();

            menu.ContextMenu();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
        }
    }
}

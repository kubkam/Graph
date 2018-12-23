using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

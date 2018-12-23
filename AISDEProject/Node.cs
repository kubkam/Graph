using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace AISDEProject
{
    public class Node
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Label { get; set; }
        public int IDOfClosetNode { get; set; }
        //public int IsObligatory { get; set; }

        public Node(int iD, int x, int y)
        {
            ID = iD;
            X = x;
            Y = y;
        }

        public Node()
        {
            ID = 0;
            X = 0;
            Y = 0;
        }

        //
        // Summary:
        //      Weight is calculated from coordinates X and Y of Begin and End Node.
        //      It is a Pythagoras form sqrt[(Begin.X - End.X)^2 + (Begin.Y - End.Y)^2].
        //      This is auxiliary method to get Cost variable in Edge Class.
        //
        // Parameters:
        //   End:
        //      Node which is other node in Edge.
        //
        // Returns:
        //      The number as a double which is distance from one Node to second Node.
        //
        // Exceptions:
        //      None.
        public double Weight(Node End) => (double)Math.Sqrt(Math.Pow(Math.Abs(this.X - End.X), 2) + Math.Pow(Math.Abs(this.Y - End.Y), 2));

        public override string ToString() => $"ID: {ID} ; X: {X} ; Y: {Y}";
    }
}

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
        //
        // Summary:
        //      Gets the number which represented node's ID.
        //      It started from 1.
        //
        // Returns:
        //      The number which represented node's ID.
        public int ID { get; set; }

        //
        // Summary:
        //      Gets the number which represented node's X (coordinate X in Cartesian coordinate system).
        //
        // Returns:
        //      The number which represented node's X.
        public int X { get; set; }

        //
        // Summary:
        //      Gets the number which represented node's Y (coordinate Y in Cartesian coordinate system).
        //
        // Returns:
        //      The number which represented node's Y.
        public int Y { get; set; }

        //
        // Summary:
        //      Gets the number which represented Label.
        //      It's an auxiliary variable which represented distance to particular Node
        //
        // Returns:
        //      The number which represented Label, distance to particular Node.
        public double Label { get; set; }

        //
        // Summary:
        //      Gets the number which represented ID of a closet available Node which is connected with Edge.
        //
        // Returns:
        //      The number which represented ID of a closet available Node which is connected with Edge.
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
    }
}

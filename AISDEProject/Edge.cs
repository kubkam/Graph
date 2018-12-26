using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;

namespace AISDEProject
{
    public class Edge
    {
        //
        // Summary:
        //      Gets the number which represented edge's ID.
        //      It started from 1.
        //
        // Returns:
        //      The number which represented edge's ID.
        public int ID { get; set; }

        //
        // Summary:
        //      Gets the Node which represented one end of Edge, Begin.
        //
        // Returns:
        //      The number which represented one end of Edge, Begin.
        public Node Begin { get; set; }

        //
        // Summary:
        //      Gets the Node which represented second end of Edge, End.
        //
        // Returns:
        //      The number which represented second end of Edge, End.
        public Node End { get; set; }

        //
        // Summary:
        //      Gets number which represent distance which is calculated from coordinates X and Y of Begin and End Node.
        //      See Weight method in Node class.
        //
        // Returns:
        //      The number which represented distance between to nodes.
        public double Cost { get; set; }

        //
        // Summary:
        //      Gets the color of element contained in the Microsoft.Msagl.Drawing.Color.
        //
        // Returns:
        //      The color of element contained in the Microsoft.Msagl.Drawing.Color.
        public Color Color { get; set; }

        public Edge()
        {
            Begin = new Node();
            End = new Node();
            Color = Color.Black;
            Cost = 0.0;
        }

        public Edge(int id, Node begin, Node end)
        {
            Begin = begin;
            End = end;
            Color = Color.Black;
            Cost = begin.Weight(end);
        }

        //public override string ToString() => $"Begin Node:\n{Begin}\n End Node:\n{End}\n ; Weight: {Cost.ToString("0.00")} ; Color: {Color}";
    }
}

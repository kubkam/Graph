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
        public int ID { get; set; }
        public Node Begin { get; set; }
        public Node End { get; set; }

        //
        // Summary:
        //      Cost is calculated from coordinates X and Y of Begin and End Node.
        //      See Weight method in Node class.
        //
        public double Cost { get; set; }

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

        public override string ToString() => $"Begin Node:\n{Begin}\n End Node:\n{End}\n ; Weight: {Cost.ToString("0.00")} ; Color: {Color}";
    }
}

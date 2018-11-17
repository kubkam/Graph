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
        #region Public Default Properties

        public int ID { get; set; }
        public Node Begin { get; set; }
        public Node End { get; set; }

        public double Cost { get; set; }

        public Color Color { get; set; }

        #endregion

        static public double Weight(Node node1, Node node2) => (double)Math.Sqrt(Math.Pow(Math.Abs(node1.X - node2.X), 2) + Math.Pow(Math.Abs(node1.Y - node2.Y), 2));

        #region Contructiors

        public Edge()
        {
            Begin = new Node();
            End = new Node();
            Color = Color.Black;
            Cost = 0.0;
        }

        public Edge(int id, Node begin, Node end, Color color)
        {
            Begin = begin;
            End = end;
            Color = color;
            Cost = begin.Weight(end);
        }

        #endregion

        public override string ToString() => $"Begin Node:\n{Begin}\n End Node:\n{End}\n ; Weight: {Weight(Begin, End).ToString("0.00")} ; Color: {Color}";
    }
}

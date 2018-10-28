using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;

namespace AISDEProject
{
    class Edge : Link
    {
        #region Public Default Properties

        public Link Link { get; set; }
        public double Weight { get; set; }
        public Color Color { get; set; }

        #endregion

        #region Contructiors

        public Edge()
        {
            Link = new Link();
            Weight = 0.0;
            Color = Color.Black;
        }

        public Edge(double weight, Color color)
        {
            Link = new Link();
            Weight = weight;
            Color = color;
        }

        public Edge(int id, int begin, int end, double weight, Color color)
        {
            Link = new Link(id, begin, end);
            Weight = weight;
            Color = color;
        }

        #endregion

        public override string ToString()
        {
            return $"{Link} ; Weight: {Weight.ToString("0.00")} ; Color: {Color}";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;

namespace AISDEProject
{
    /// <summary>
    /// This class describes Edges
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// ID property.
        /// </summary>
        /// <value>
        /// The number which represented edge's ID. Starts from 1.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Node Begin property.
        /// </summary>
        /// <value>
        /// The number which represented one end of Edge, Begin.
        /// </value>
        public Node Begin { get; set; }

        /// <summary>
        /// Node End property.
        /// </summary>
        /// <value>
        /// The number which represented second end of Edge, End.
        /// </value>
        public Node End { get; set; }

        /// <summary>
        /// Cost property.
        /// </summary>
        /// <value>
        /// Gets number which represent distance which is calculated from coordinates X and Y of Begin and End Node.
        /// </value>
        /// <seealso cref="AISDEProject.Node.Weight(Node)">
        /// Notice the Pythagoras form.
        /// </seealso>
        public double Cost { get; set; }

        /// <summary>
        /// Color property.
        /// </summary>
        /// <value>
        /// The color of element contained in the Microsoft.Msagl.Drawing.Color.
        /// <seealso cref="Microsoft.Msagl.Drawing.Color"/>
        /// </value>
        public Color Color { get; set; }

        /// <summary>
        /// The default class constructor.
        /// </summary>
        public Edge()
        {
            ID = 1;
            Begin = new Node();
            End = new Node();
            Color = Color.Black;
            Cost = 0.0;
            ID++;
        }

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="id">ID of Edge. <seealso cref="AISDEProject.Edge.ID"/></param>
        /// <param name="begin">Begin of Edge. <seealso cref="AISDEProject.Node"/></param>
        /// <param name="end">End of Edge. <seealso cref="AISDEProject.Node"/></param>
        public Edge(int id, Node begin, Node end)
        {
            ID = id;
            Begin = begin;
            End = end;
            Color = Color.Black;
            Cost = begin.Weight(end);
        }
    }
}

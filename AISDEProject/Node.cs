using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace AISDEProject
{
    /// <summary>
    /// This class describes Nodes
    /// </summary>
    public class Node
    {
        /// <summary>
        /// ID property.
        /// </summary>
        /// <value>
        /// The number which represented node's ID. Starts from 1.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Node's coordinate X
        /// </summary>
        /// <value>
        /// The number which represented node's X (coordinate X in Cartesian coordinate system).
        /// </value>
        public int X { get; set; }

        /// <summary>
        /// Node's coordinate Y
        /// </summary>
        /// <value>
        /// The number which represented node's Y (coordinate Y in Cartesian coordinate system).
        /// </value>
        public int Y { get; set; }

        /// <summary>
        /// Label property
        /// </summary>
        /// <value>
        /// The number which represented Label, distance to particular Node.
        /// </value>
        public double Label { get; set; }

        /// <summary>
        /// ID of closet Node
        /// </summary>
        /// <value>
        /// The number which represented ID of a closet available Node which is connected with Edge.
        /// </value>
        public int IDOfClosetNode { get; set; }

        //public int IsObligatory { get; set; }

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="id">ID of Node. <seealso cref="AISDEProject.Node.ID"/></param>
        /// <param name="x">Coordinate X of Node. <seealso cref="AISDEProject.Node.X"/></param>
        /// <param name="y">Coordinate Y of Node. <seealso cref="AISDEProject.Node.Y"/></param>
        public Node(int id, int x, int y)
        {
            ID = id;
            X = x;
            Y = y;
        }

        /// <summary>
        /// The default class constructor.
        /// </summary>
        public Node()
        {
            ID = 0;
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Weight is calculated from coordinates X and Y of Begin and End Node.
        /// It is a Pythagoras form sqrt[(Begin.X - End.X)^2 + (Begin.Y - End.Y)^2].
        /// This is auxiliary method to get Cost variable in Edge Class.
        /// </summary>
        /// <param name="End">Second node in edge.</param>
        /// <returns>Distance between this and End.</returns>
        public double Weight(Node End) => (double)Math.Sqrt(Math.Pow(Math.Abs(this.X - End.X), 2) + Math.Pow(Math.Abs(this.Y - End.Y), 2));
    }
}

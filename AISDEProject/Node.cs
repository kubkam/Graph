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
        #region Public Default Properties

        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        //public int IsObligatory { get; set; }
        public double Label { get; set; }
        public List<Node> PrevNodes { get; set; }

        public double Weight(Node End) => (double)Math.Sqrt(Math.Pow(Math.Abs(this.X - End.X), 2) + Math.Pow(Math.Abs(this.Y - End.Y), 2));

        #endregion

        #region Constructors

        public Node(int iD, int x, int y)
        {
            ID = iD;
            X = x;
            Y = y;
            //IsObligatory = isObligatory;
        }

        public Node()
        {
            ID = 0;
            X = 0;
            Y = 0;
        }

        public Node(int id)
        {
            this.ID = id;
            X = 0;
            Y = 0;
        }

        #endregion

        public override string ToString() => $"ID: {ID} ; X: {X} ; Y: {Y}";


    }
}

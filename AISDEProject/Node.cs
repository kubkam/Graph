using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEProject
{
    public class Node
    {
        #region Public Default Properties

        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        //public int IsObligatory { get; set; }

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

        #endregion

        public override string ToString()
        {
            return $"ID: {ID} ; X: {X} ; Y: {Y}";
        }

    }
}

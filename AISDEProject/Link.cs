using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEProject
{
    class Link
    {
        #region Public Default Properties

        public int ID { get; set; }
        public int Begin { get; set; }
        public int End { get; set; }

        #endregion

        #region Constructors

        public Link()
        {
            ID = 0;
            Begin = 0;
            End = 0;
        }

        public Link(int id, int begin, int end)
        {
            ID = id;
            Begin = begin;
            End = end;
        }

        #endregion

        public override string ToString()
        {
            return $"ID: {ID} ; Begin: {Begin} ; End: {End}";
        }
    }
}

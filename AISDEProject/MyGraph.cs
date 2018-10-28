using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEProject
{
    class MyGraph
    {
        #region Public Default Properties

        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        #endregion

        #region Constructors

        public MyGraph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public MyGraph(List<Node> nodes, List<Edge> edges)
        {
            Nodes = nodes;
            Edges = edges;  
        }

        #endregion

        public override string ToString()
        {
            return $"Nodes: {Nodes}\n Edges: {Edges}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEProject
{

    class Dijkstra
    {
        public MyGraph MyGraph { get; set; }
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Edge> Edges { get; set; } = new List<Edge>();

        public Dijkstra()
        {
            MyGraph = new MyGraph();
            MyGraph.GraphFromFile(Global.PATH);
        }


        public List<Node> PrevNodes(Node node)
        {
            foreach (var edge in MyGraph.Edges)
            {
                if (edge.Begin == node)
                {
                    Nodes.Add(edge.Begin);
                    Edges.Add(edge);
                }
                if (edge.End == node)
                {
                    Nodes.Add(edge.End);
                    Edges.Add(edge);
                }
            }

            return Nodes;
        }

    }
}

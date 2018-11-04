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


        public void Algorithm(Node source)
        {
            int startID = 1;
            foreach (var node in MyGraph.Nodes)
            {
                node.Label = (double)int.MaxValue;
                node.PrevNodes = null;
                Nodes.Add(node);
            }

            source.Label = 0;
            Nodes.OrderBy(x => x.Label);

            while (Nodes != null)
            {
                var Start = MyGraph.Nodes.Find(x => x.ID == startID);

                Nodes.Remove(Start);
            }

        }
    }
}

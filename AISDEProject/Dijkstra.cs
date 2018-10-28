using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDEProject
{
    class Dijkstra
    {
        #region Public Default Properties

        public Node PreviousNode { get; set; }

        #endregion

        #region Constructors

        public Dijkstra()
        {
            PreviousNode = new Node();
        }

        public Dijkstra(Node previousNode)
        {
            PreviousNode = previousNode;
        }
        
        #endregion

        public void Algorithm()
        {
            Console.WriteLine("Chose Begin Node");

            int NodeId = 0;
            Console.Write(NodeId);
            var beginNode = new Node(NodeId);
            
            
             
            
        }
        
    }
}

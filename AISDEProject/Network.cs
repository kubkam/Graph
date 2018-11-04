using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Drawing;
using System.Drawing.Imaging;

namespace AISDEProject
{
    class Network
    {
        #region Public Properties

        public MyGraph MyGraph { get; set; }

        #endregion

        #region Constructor

        public Network()
        {
            MyGraph = new MyGraph();
        }

        public Network(MyGraph myGraph)
        {
            this.MyGraph = myGraph;
        }

        #endregion

    }
}

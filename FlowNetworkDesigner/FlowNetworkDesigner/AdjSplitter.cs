using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FlowNetworkDesigner
{
    class AdjSplitter : Splitter
    {

        internal int upValue;

        public AdjSplitter(Point location, string name, int up) : base(location, name) 
        {
            Image = Properties.Resources.AdjSplitter;
            upValue = up;
            listPipeOutputSplitter = new List<Pipe>();
        }


      
    }
}

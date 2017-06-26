using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FlowNetworkDesigner
{
    class Sink:Component
    {
        //variables
        private Pipe InnerPipe;

        //constructor
        public Sink(Point p, object sender) : base(p, sender)
        {
            //TODO Initalize pipe
            id += 1;
            pb.Name = "sink" + id;
            pb.BackColor = Color.Blue;
        }

        public override void AddInnerPipe(Pipe pipe, Form form)
        {
            InnerPipe = pipe;
        }
        //how do we change the pipe from null to something
    }
}

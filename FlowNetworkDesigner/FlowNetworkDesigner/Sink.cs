using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlowNetworkDesigner
{
    class Sink:Component
    {
        //variables
        private Pipe OuterPipe;


        //constructor
        public Sink(Point p, object sender) : base(p, sender)
        {
            //TODO Initalize pipe
            pb.BackColor = Color.Blue;
        }


        //how do we change the pipe from null to something
    }
}

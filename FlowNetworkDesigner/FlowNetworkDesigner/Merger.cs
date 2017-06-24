using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlowNetworkDesigner
{
    class Merger :Component
    {
        //variables
        private Pipe UpperInPipe;

        private Pipe LowerInPipe;

        private Pipe OuterPipe;



        //costructor
        public Merger(Point p, object sender) : base(p, sender)
        {
            // TODO Initiate Pipes
        }


        //methods
        public override void Draw(Point position, Form1 form)
        {
            
        }

        public override void UpdatePipe()
        {
            
        }
    }
}

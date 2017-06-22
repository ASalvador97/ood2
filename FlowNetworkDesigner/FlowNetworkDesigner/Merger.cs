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
        public Merger(Point p) : base(p)
        {
            // TODO Initiate Pipes
        }


        //methods
        public override void Draw()
        {
            
        }

        public override void UpdatePipe()
        {
            
        }
    }
}

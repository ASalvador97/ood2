using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlowNetworkDesigner
{
    class AdjSplitter : Component
    {
        //variables 
        private Pipe UpperOutPipe;

        private Pipe LowerOutPipe;

        private Pipe InnerPipe;


        //constructor
        public AdjSplitter(Point p, object sender) : base(p, sender)
        {
            //TODO initialize pipes
        }


        //methods 
        public override void Draw(Point position, Form1 form)
        {

        }

        public override Pipe UpdatePipe(Pipe pipe)
        {
            return null;

        }
        public override void AddPipe(Pipe pipe)
        {

        }
        public void SplitFlow(double flowsplit)
        {
            double inflow = InnerPipe.Flow;
            if(flowsplit <= inflow )
            {
                UpperOutPipe.Flow = flowsplit;
                LowerOutPipe.Flow = inflow - flowsplit;
            }
        }
    }
}

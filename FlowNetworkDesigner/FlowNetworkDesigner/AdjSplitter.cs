using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlowNetworkDesigner
{
    class AdjSplitter : Splitter
    {
        //variables 
        


        //constructor
        public AdjSplitter(Point p, object sender) : base(p, sender)
        {
            //TODO initialize pipes
            UpperOutPipe = null;
            LowerOutPipe = null;
            InnerPipe = null;
            id += 1;
            pb.Name = "merger" + id;
            pb.BackColor = Color.DarkOliveGreen;
        }


        //methods 
        public override void Draw(Point position, Form1 form)
        {

            base.Draw(position, form);
        }

        public override Pipe UpdatePipe(Pipe pipe)
        {
            return null;

        }
        public override void AddInnerPipe(Pipe pipe)
        {

        }
        public override void AddOuterPipe(Pipe pipe)
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

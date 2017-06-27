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
        internal int downValue;

        public AdjSplitter(Point location, string name, int up, int down) : base(location, name) //Constructor
        {
            Image = Properties.Resources.AdjSplitter;
            upValue = up;
            downValue = down;
            listPipeOutputSplitter = new List<Pipe>();
        }


        ////variables 



        ////constructor
        //public AdjSplitter(Point p, object sender) : base(p, sender)
        //{
        //    //TODO initialize pipes
        //    UpperOutPipe = null;
        //    LowerOutPipe = null;
        //    InnerPipe = null;
        //    id += 1;
        //    pb.Name = "adjsplitter" + id;
        //    pb.BackColor = Color.DarkOliveGreen;
        //}


        ////methods 
        //public override void Draw(Point position, Form1 form)
        //{

        //    base.Draw(position, form);
        //}

        //public override Pipe UpdatePipe(Pipe pipe, Form form)
        //{
        //    return null;

        //}
        //public override void AddInnerPipe(Pipe pipe, Form form)
        //{

        //}
        //public override void AddOuterPipe(Pipe pipe, Form form)
        //{

        //}

        //public void SplitFlow(double flowsplit)
        //{
        //    double inflow = InnerPipe.Flow;
        //    if(flowsplit <= inflow )
        //    {
        //        UpperOutPipe.Flow = flowsplit;
        //        LowerOutPipe.Flow = inflow - flowsplit;
        //    }
        //}
    }
}

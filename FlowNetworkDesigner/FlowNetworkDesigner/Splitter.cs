using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;

namespace FlowNetworkDesigner
{
    class Splitter :Component
    {
        //variables        
        protected Pipe UpperOutPipe;

        protected Pipe LowerOutPipe;

        protected Pipe InnerPipe;


        //constructor
        public Splitter(Point p, object sender) 
            :base(p, sender) 
        {
            // TODO initate pipes?
            UpperOutPipe = null;
            LowerOutPipe = null;
            InnerPipe = null;
            id += 1;
            pb.Name = "merger" + id;
            pb.BackColor = Color.Olive;
        }

        //methods 
        public override void Draw(Point position, Form1 form)
        {

            base.Draw(position, form);
        }
        public override void AddInnerPipe(Pipe pipe)
        {
            InnerPipe = pipe;
            base.AddInnerPipe(pipe);
        }
        public override void AddOuterPipe(Pipe pipe)
        {
            if(UpperOutPipe == null)
            {
                UpperOutPipe = pipe;
            }
            else
            {
                LowerOutPipe = pipe;
            }
            base.AddOuterPipe(pipe);
        }

        public override Pipe UpdatePipe(Pipe pipe)
        {
            if (UpperOutPipe != null)
            {
                UpperOutPipe.Flow = pipe.Flow / 2;
            }
            if(LowerOutPipe != null)
            {
              LowerOutPipe.Flow = pipe.Flow / 2;
            }
            return null;

        }

        public bool IsUpperOutPipeNull()
        {
            if (UpperOutPipe == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

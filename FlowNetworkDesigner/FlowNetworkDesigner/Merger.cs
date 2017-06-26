using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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
            UpperInPipe = null;
            LowerInPipe = null;
            OuterPipe = null;
            id += 1;
            OuterPipe = null;
            pb.Name = "merger" + id;
            pb.BackColor = Color.Orange;
        }


        //methods
        public override void Draw(Point position, Form1 form)
        {

            base.Draw(position, form);
        }

        public override void AddInnerPipe(Pipe pipe,Form form)
        {
            if (IsUpperInPipeNull())
            {
                UpperInPipe = pipe;
            }
            else
            {
                LowerInPipe = pipe;
            }
        }

        public override void AddOuterPipe(Pipe pipe, Form form)
        {
            base.AddOuterPipe(pipe,form);
        }

        public override Pipe UpdatePipe(Pipe pipe, Form form)
        {
            OuterPipe = pipe;
            if (!IsUpperInPipeNull())
            {
                if (LowerInPipe != null)
                {
                    OuterPipe.Flow = UpperInPipe.Flow + LowerInPipe.Flow;
                }
                else
                {
                    OuterPipe.Flow = UpperInPipe.Flow;
                }
            }
            else
            {
                OuterPipe.Flow = 0;
            }
            return OuterPipe;

        }

        private bool IsUpperInPipeNull()
        {
            if (UpperInPipe == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsLowerInPipeNull()
        {
            if (LowerInPipe == null)
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

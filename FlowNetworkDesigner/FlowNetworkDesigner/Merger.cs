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

        public override void AddInnerPipe(Pipe pipe)
        {

        }
        public override Pipe UpdatePipe(Pipe pipe)
        {
            return null;

        }

        public bool IsUpperInPipeNull()
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

    }
}

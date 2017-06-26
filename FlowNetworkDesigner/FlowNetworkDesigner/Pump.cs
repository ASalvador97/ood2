using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowNetworkDesigner 
{
     public class Pump:Component
     {
         //variables
         private Pipe OuterPipe;
         
         //constructor       
         public Pump(Point p, object sender,double currentFlow)
             :base(p,sender)
        {
            // TODO initiate pipe?
            //image
            id += 1;
            OuterPipe = null;
            pb.Name = "pipe" + id;
            pb.BackColor = Color.Red;
            Flow = currentFlow;
        }

         //methods
         public override void Draw(Point position, Form1 form)
         {

            
            base.Draw(position,form);
            
         }

        public override void AddOuterPipe(Pipe pipe, Form form)
        {
            base.AddOuterPipe(pipe, form);
        }

         public override Pipe UpdatePipe(Pipe pipe, Form form)
         {
            pipe.Flow = this.Flow;
            OuterPipe = pipe;
            return pipe;
         }
     }
}

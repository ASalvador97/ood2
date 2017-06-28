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
     

        public Pipe OutPipe
        {
            get; set;
        }

 

        //constructor       
        public Pump(Point p, string name,double currentFlow)
             :base(p,name)
        {
            // TODO initiate pipe?
            //image
            
            Image = Properties.Resources.Pump;
            //id += 1;
            //OuterPipe = null;
            //pb.Name = "pump" + id;
            //pb.BackColor = Color.Red;
            Flow = currentFlow;
        }

         //methods
         public override void Draw(Graphics graphic)
         {
            Size size = new Size(Image.Width, Image.Height);
            Rectangle rectangle = new Rectangle(Position, size);
            graphic.DrawImage(Image, rectangle);

            graphic.DrawString("Flow " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, Position.X - 6, Position.Y - 20);

        }
        public override void AddPipe(Pipe pipe)
        {
            OutPipe = pipe;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FlowNetworkDesigner
{
    class Sink:Component
    {

       

        public Pipe InPipe
        {
            get; set;
        }

        public Sink(Point location, string name, double capacity) : base(location, name) 
        {
            Image = Properties.Resources.Sink;
            Flow = capacity;
        }
        public override void Draw(Graphics graphic)
        {
            Size size = new Size(Image.Width, Image.Height);
            Rectangle rectangle = new Rectangle(Position, size);
            graphic.DrawImage(Image, rectangle);

            graphic.DrawString("Capacity " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Purple, Position.X - 6, Position.Y - 20);
        }

        public override void AddPipe(Pipe pipe)
        {
            InPipe = pipe;
        }

     
    }
}

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

        public double Flow { get; set; }
        public Sink(Point location, string name, double capacity) : base(location, name) //Constructor
        {
            Image = Properties.Resources.Sink;
            Flow = capacity;
        }
        public override void Draw(Graphics graphic)
        {
            Size size = new Size(Image.Width, Image.Height);
            Rectangle rectangle = new Rectangle(Location, size);
            graphic.DrawImage(Image, rectangle);

            graphic.DrawString("Capacity " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Blue, Location.X - 6, Location.Y - 20);
        }

        ////variables
        //private Pipe InnerPipe;

        ////constructor
        //public Sink(Point p, object sender) : base(p, sender)
        //{
        //    //TODO Initalize pipe
        //    id += 1;
        //    pb.Name = "sink" + id;
        //    pb.BackColor = Color.Blue;
        //}

        //public override void AddInnerPipe(Pipe pipe, Form form)
        //{
        //    InnerPipe = pipe;
        //}
        ////how do we change the pipe from null to something
    }
}

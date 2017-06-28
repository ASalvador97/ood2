using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace FlowNetworkDesigner
{
    class Splitter :Component
    {


        internal List<Pipe> listPipeOutputSplitter; 
        internal List<Pipe> listPipeInputSplitter;

        public Splitter(Point location, string name) : base(location, name) 
        {
            Image = Properties.Resources.Splitter;
            listPipeOutputSplitter = new List<Pipe>();
            listPipeInputSplitter = new List<Pipe>();
        }

        public Pipe Pipe  { get; set; }

        public override void Draw(Graphics graphic)
        {
            Size size = new Size(Image.Width, Image.Height);
            Rectangle rectangle = new Rectangle(Position, size);
            graphic.DrawImage(Image, rectangle);
        }

        public override void AddPipe(Pipe pipe)
        {
            listPipeInputSplitter.Add(pipe);
        }


       
    }
}

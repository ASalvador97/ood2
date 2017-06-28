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

     
        internal List<Pipe> listPipelineInputMerger;
        internal List<Pipe> listPipelineOutputMerger;

        public Merger(Point location, string name) : base(location, name) 
        {
            Image = Properties.Resources.Merger;
            listPipelineInputMerger = new List<Pipe>();
            listPipelineOutputMerger = new List<Pipe>();
        }

        public Pipe Pipe { get; set; }

        public override void Draw(Graphics graphic)
        {
            Size size = new Size(Image.Width, Image.Height);
            Rectangle rectangle = new Rectangle(Position, size);
            graphic.DrawImage(Image, rectangle);
        }


      

    }
}

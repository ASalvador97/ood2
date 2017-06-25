using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowNetworkDesigner
{
    public abstract class  Component
    {
        //properties

        //contains the position of the conponent on the canvas
        public Point Position { get; set; }
        public PictureBox pb;
        private PictureBox margin;
        protected static int id;

        public delegate Pipe UpdatePipeHandler(Pipe pipe);
        public event UpdatePipeHandler UpdatePipeEvent;
        // TODO image placeholder?
        // TODO put pipes here

        //constructor
        public Component(Point p,object sender)
        {
            
            Position = p;
            pb = new PictureBox();
            //margin = new PictureBox();
            pb.Size = ((Button)sender).Size;

            var size = new Control().Size;
            size.Height = pb.Size.Height * 3;
            size.Width = pb.Size.Width * 3;
            //margin.Size = size;

            pb.BackColor = ((Button)sender).BackColor;
            //margin.SendToBack();
            //margin.BackColor = Color.Transparent;
            
            pb.Visible = true;
            pb.Enabled = true;

            UpdatePipeEvent += new UpdatePipeHandler(UpdatePipe);
            
        }

        


        //methods
        public virtual void Draw(Point position, Form1 form) // TODO: needs a point as a parameter?
        {
          

            pb.Location = form.PointToClient(Cursor.Position);
            //margin.Location = pb.Location;
            //int lr, tb;
            //lr = margin.Location.X - pb.Size.Width;
            //tb = margin.Location.Y - pb.Size.Width;
             position = new Point();
            //position.X = lr;
            //position.Y = tb;
            //margin.Location = position;
            form.Controls.Add(pb);
            //form.Controls.Add(margin);
        }




        public virtual Pipe UpdatePipe(Pipe pipe) { return pipe; } // TODO: needs values as parameters?
       

        public virtual void AddPipe(Pipe pipe)
        {
            UpdatePipeEvent(pipe);
        }



    }
}

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

        public Point Position { get; set; }
        public Image Image { get; set; }
        public string Name { get; set; }
        public double Flow { get; set; }

        public Component(Point position, string name) 
        {
            Position = position;
            Name = name;
        }
        public bool Contains(Point p) // for method public Component CheckComponent(Point p) in class Network
        {
            if (p == Position || ((p.X > Position.X && p.X < Position.X + 63) && (p.Y > Position.Y && p.Y < Position.Y + 63)))
            {
                return true;
            }
            return false;
        }

        public virtual void AddPipe(Pipe pipe) { }

        public virtual void Draw(Graphics graphic) { }

        //public bool ContainsTogether(Point p) // only for adding elements (checking if this place has element or not)
        //{
        //    if (p == Position || ((p.X + 30 > Position.X && p.X < Position.X + 40) && (p.Y + 30 > Position.Y && p.Y < Position.Y + 40)))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        ////properties
        //public double Flow { get; set; }

        ////contains the position of the conponent on the canvas
        //public Point Position { get; set; }
        //public PictureBox pb;
        //private PictureBox margin;
        //protected static int id;

        //public delegate Pipe UpdatePipeHandler(Pipe pipe,Form form);
        //public event UpdatePipeHandler UpdatePipeEvent;
        //// TODO image placeholder?
        //// TODO put pipes here

        ////constructor
        //public Component(Point p,object sender)
        //{

        //    Position = p;
        //    pb = new PictureBox();
        //    //margin = new PictureBox();
        //    pb.Size = ((Button)sender).Size;

        //    var size = new Control().Size;
        //    size.Height = pb.Size.Height * 3;
        //    size.Width = pb.Size.Width * 3;
        //    //margin.Size = size;

        //    pb.BackColor = ((Button)sender).BackColor;
        //    //margin.SendToBack();
        //    //margin.BackColor = Color.Transparent;

        //    pb.Visible = true;
        //    pb.Enabled = true;

        //    UpdatePipeEvent += new UpdatePipeHandler(UpdatePipe);

        //}




        ////methods
        //public virtual void Draw(Point position, Form1 form) // TODO: needs a point as a parameter?
        //{


        //    pb.Location = form.PointToClient(Cursor.Position);
        //    //margin.Location = pb.Location;
        //    //int lr, tb;
        //    //lr = margin.Location.X - pb.Size.Width;
        //    //tb = margin.Location.Y - pb.Size.Width;
        //     position = new Point();
        //    //position.X = lr;
        //    //position.Y = tb;
        //    //margin.Location = position;
        //    form.Controls.Add(pb);
        //    //form.Controls.Add(margin);
        //}




        //public virtual Pipe UpdatePipe(Pipe pipe,Form form) { return pipe; } // TODO: needs values as parameters?


        //public virtual void AddInnerPipe(Pipe pipe,Form form)
        //{
        //    UpdatePipeEvent(pipe,form);
        //}

        //public virtual void AddOuterPipe(Pipe pipe,Form form)
        //{
        //    UpdatePipeEvent(pipe,form);
        //}


    }
}

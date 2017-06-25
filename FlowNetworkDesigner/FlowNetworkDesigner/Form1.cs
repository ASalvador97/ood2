using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowNetworkDesigner
{
    public partial class Form1 : Form
    {
        object ButtonClicked;
        Component component;
        Network network;
        double currentflow;
        List<object> point;
        Pipe pipe;
        bool IsReadyToPaint;

        public Form1()
        {
            InitializeComponent();
            network = new Network();
            IsReadyToPaint = false;
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            currentflow = Convert.ToInt32(numericUpDown1.Value);
             ButtonClicked = sender;
            component = new Pump(new Point(0, 0), sender,currentflow);
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (component != null)
            {
                component.Position = Cursor.Position;
                component.pb.Click += Pb_Click;
                network.AddComponent(component, this);
                ((Button)ButtonClicked).PerformClick();
                this.Refresh();
            }
            if (point != null)
            {
                if (point.Count > 0)
                {
                    //if (point[0] is Component)
                    //{

                    //    if (point[point.Count - 1] is Component)
                    //    {
                    //        pipe.Points = ConvertObjectToPoint();
                    //        IsReadyToPaint = true;
                    //        this.Refresh();
                    //        IsReadyToPaint = false;
                    //        //network.AddPipe(pipe, paint);

                    //    }
                    //    else
                    //    {
                            point.Add(Cursor.Position);

                    //    }

                    //}
                }
            }

        }
        private List<Point> ConvertObjectToPoint()
        {
            List<Point> positionPoint = new List<Point>();
            foreach (object c in point)
            {
                if (c is Component)
                {
                    positionPoint.Add(((Component)c).Position);
                }
                else
                {
                    positionPoint.Add((Point)c);
                }
            }
            return positionPoint;
        }
        private void Pb_Click(object sender, EventArgs e)
        {
            if (point != null)
            {
                if (point.Count == 0)
                {
                    point.Add(network.GetComponent(((PictureBox)sender).Name));
                }
                else if (point.Count > 0)
                {
                    point.Add(network.GetComponent(((PictureBox)sender).Name));
                    pipe.Points = ConvertObjectToPoint();
                    IsReadyToPaint = true;
                    this.Refresh();
                    IsReadyToPaint = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClicked = sender;
            component = new Sink(new Point(0, 0), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            component = null;
            point = new List<object>();
            pipe = new Pipe();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(IsReadyToPaint) network.AddPipe(pipe, e,this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/37908570/how-to-delete-a-drawn-line-on-a-form
            //Just create a pen with the same color of the background and redraw it in the same points, remove it from list after
        }
    }
}

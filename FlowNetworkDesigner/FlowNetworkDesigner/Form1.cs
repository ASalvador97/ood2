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
        //object ButtonClicked;
        //Component component;
        //Network network;
        //double currentflow;
        //List<object> point;
        //Pipe pipe;
        //bool IsReadyToPaint;

        static Network network = new Network();
        //public EditForm editForm = new EditForm(x); //connect with Dialog window (need for edit)
        private string component; //variable, wich save value "Pump", "Sink", and also for buttons "Delete" and "Edit"

        public Form1()
        {
            InitializeComponent();
           
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            // currentflow = Convert.ToInt32(numericUpDown1.Value);
            // ButtonClicked = sender;
            //component = new Pump(new Point(0, 0), sender,currentflow);
            component = "Pump";

            //numericUpDownFlow.Visible = false;
            //labelFlow.Visible = false;
            //numericUpDownCapacity.Visible = true;
            //labelCapacity.Visible = true;
            //numericUpDown1.Visible = false;
            //label9.Visible = false;
            //numericUpDown2.Visible = false;
            //label10.Visible = false;

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (component == "Pump") 
            {
                network.AddPump(new Point(e.X - 20, e.Y - 20), "Pump", Convert.ToInt32(numericUpDown1.Value));
            }
            if (component == "Sink") 
            {
                network.AddSink(new Point(e.X - 20, e.Y - 20), "Sink", Convert.ToInt32(numericUpDown1.Value));
            }
            if (component == "Merger") 
            {
                network.AddMerger(new Point(e.X - 20, e.Y - 20), "Merger");
            }
            if (component == "Splitter") 
            {
                network.AddSplitter(new Point(e.X - 20, e.Y - 20), "Splitter");
            }
            //if (component == "Adjustable Splitter") 
            //{
            //    if (Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) == 100) //checking, has Adjustable splitter 100% or not
            //    {
            //        network.AddAjustableSplitter(new Point(e.X - 20, e.Y - 20), "Adjustable Splitter", Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please write values for adjustable splitter that add up to 100");
            //    }
            //}
            if (component == "Pipeline") 
            {
                //there are around 46 pixels in diameter for merger and splitter picture
                network.AddPipe(new Point(e.X, e.Y), "Pipeline", Convert.ToInt32(numericUpDown1.Value));
            }
            if (component == "Delete") 
            {
                network.DeleteElementOrPipeline(new Point(e.X, e.Y));
            }
            this.Invalidate();

            //if (component != null)
            //{
            //    component.Position = Cursor.Position;
            //    component.pb.Click += Pb_Click;
            //    network.AddComponent(component, this);
            //    ((Button)ButtonClicked).PerformClick();
            //}
            //if (point != null)
            //{
            //    if (point.Count > 0)
            //    {

            //                point.Add(Cursor.Position);


            //    }
            //}

        }
        private List<Point> ConvertObjectToPoint()
        {
            List<Point> positionPoint = new List<Point>();
            //foreach (object c in point)
            //{
            //    if (c is Component)
            //    {
            //        positionPoint.Add(((Component)c).Position);
            //    }
            //    else
            //    {
            //        positionPoint.Add((Point)c);
            //    }
            //}
            return positionPoint;
        }
        private void Pb_Click(object sender, EventArgs e)
        {
            //if (point != null)
            //{
            //    if (point.Count == 0)
            //    {
            //        point.Add(network.GetComponent(((PictureBox)sender).Name));
            //    }
            //    else if (point.Count > 0)
            //    {
            //        point.Add(network.GetComponent(((PictureBox)sender).Name));
            //        pipe.Points = ConvertObjectToPoint();
            //        IsReadyToPaint = true;
            //        this.Refresh();
            //        IsReadyToPaint = false;
            //        button6.PerformClick();
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
         {
            component = "Sink";

            //numericUpDownFlow.Visible = false;
            //labelFlow.Visible = false;
            //numericUpDownCapacity.Visible = true;
            //labelCapacity.Visible = true;
            //numericUpDown1.Visible = false;
            //label9.Visible = false;
            //numericUpDown2.Visible = false;
            //label10.Visible = false;

            //ButtonClicked = sender;
            //component = new Sink(new Point(0, 0), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            component = "Pipeline";

            //numericUpDownFlow.Visible = true;
            //labelFlow.Visible = true;
            //numericUpDownCapacity.Visible = false;
            //labelCapacity.Visible = false;
            //numericUpDown1.Visible = false;
            //label9.Visible = false;
            //numericUpDown2.Visible = false;
            //label10.Visible = false;

            //component = null;
            //point = new List<object>();
            //pipe = new Pipe();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            network.Painting(e.Graphics);
            //if (IsReadyToPaint)
            //{

            //    network.AddPipe(pipe, e, this);
            //    if (network.Pipes.Count > 1)
            //    {
            //        foreach (Pipe c in network.Pipes)
            //        {
            //            network.DrawOnForm(c,e, this);
            //        }
            //    }
            //    IsReadyToPaint = false;
            //}
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/37908570/how-to-delete-a-drawn-line-on-a-form
            //Just create a pen with the same color of the background and redraw it in the same points, remove it from list after

            component = "Delete";

            //numericUpDownFlow.Visible = false;
            //labelFlow.Visible = false;
            //numericUpDownCapacity.Visible = false;
            //labelCapacity.Visible = false;
            //numericUpDown1.Visible = false;
            //label9.Visible = false;
            //numericUpDown2.Visible = false;
            //label10.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            component = "Merger";

            //numericUpDownFlow.Visible = false;
            //labelFlow.Visible = false;
            //numericUpDownCapacity.Visible = false;
            //labelCapacity.Visible = false;
            //numericUpDown1.Visible = false;
            //label9.Visible = false;
            //numericUpDown2.Visible = false;
            //label10.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            component = "Splitter";

            //numericUpDownFlow.Visible = false;
            //labelFlow.Visible = false;
            //numericUpDownCapacity.Visible = false;
            //labelCapacity.Visible = false;
            //numericUpDown1.Visible = false;
            //label9.Visible = false;
            //numericUpDown2.Visible = false;
            //label10.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            component = "Adjustable Splitter";

            //numericUpDown1.Visible = true;
            //label9.Visible = true;
            //numericUpDown2.Visible = true;
            //label10.Visible = true;
            //numericUpDownFlow.Visible = false;
            //labelFlow.Visible = false;
            //numericUpDownCapacity.Visible = false;
            //labelCapacity.Visible = false;
            //ButtonClicked = sender;

            //component = new AdjSplitter(new Point(0, 0), sender);
        }
    }
}

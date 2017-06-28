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
        private string component; 
        private bool Iscomponent;

        public Form1()
        {
            InitializeComponent();
           
        }

        internal Network Network
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // currentflow = Convert.ToInt32(numericUpDown1.Value);
            // ButtonClicked = sender;
            //component = new Pump(new Point(0, 0), sender,currentflow);
            component = "Pump";
            Iscomponent = true;
         

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Iscomponent)
            {
                network.AddComponent(null, Convert.ToInt32(numericUpDown3.Value), new Point(e.X, e.Y), component, Convert.ToDouble(numericUpDown1.Value),Convert.ToDouble(numericUpDown2.Value));
                Iscomponent = false;
                numericUpDown3.Visible = false;
            }

            else if (component == "Pipeline") 
            {
                network.AddPipe(new Point(e.X, e.Y), "Pipeline");
            }
            else if (component == "Delete") 
            {
                network.DeleteElementOrPipeline(new Point(e.X, e.Y));
            }
            this.Invalidate();


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
            Iscomponent = true;
            

            //ButtonClicked = sender;
            //component = new Sink(new Point(0, 0), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            component = "Pipeline";


            //component = null;
            //point = new List<object>();
            //pipe = new Pipe();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            network.Draw(e.Graphics);
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
            

            component = "Delete";

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            component = "Merger";
            Iscomponent = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            component = "Splitter";
            Iscomponent = true;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            component = "Adjustable Splitter";
            numericUpDown3.Visible = true;
            Iscomponent = true;
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            network.Save();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
               network.Load();
            }
            catch (Exception)
            {

            }
           
        }
    }
}

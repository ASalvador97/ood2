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
        int currentflow;

        public Form1()
        {
            InitializeComponent();
            network = new Network();
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            currentflow = Convert.ToInt32(numericUpDown1.Value);
             ButtonClicked = sender;
            component = new Pump(new Point(0, 0), sender,currentflow);

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            component.Position = Cursor.Position;
            network.AddComponent(component,this);
            ((Button)ButtonClicked).PerformClick();

            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClicked = sender;
            component = new Sink(new Point(0, 0), sender);
        }
    }
}

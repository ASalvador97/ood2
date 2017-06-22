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
        PictureBox pb;
        PictureBox margin;
        public Form1()
        {
            InitializeComponent();
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
              pb = new PictureBox();
            margin = new PictureBox();
            pb.Size = ((Button)sender).Size;
            
            var size = new Control().Size;
            size.Height = pb.Size.Height * 3;
            size.Width = pb.Size.Width * 3;
            margin.Size = size;
            
            pb.BackColor = ((Button)sender).BackColor;
            pb.Visible = true;
            pb.Enabled = true;

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            pb.Location = this.PointToClient(Cursor.Position);
            margin.Location = pb.Location;
            int lr ,tb;
            lr = margin.Location.X- pb.Size.Width;
            tb = margin.Location.Y- pb.Size.Width;
            var position = new Point();
            position.X = lr;
            position.Y = tb;
            margin.Location = position;
            this.Controls.Add(pb);
            this.Controls.Add(margin);
            this.Refresh();
        }
    }
}

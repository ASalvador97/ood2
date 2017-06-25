using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowNetworkDesigner
{
   public class Pipe
    {
        private double flow;
        public List<Point> Points { get; set; }

        //properties
        public double Flow
        {
            get
            {
                return this.flow;
            }
            set
            {
                flow = value;
            }
        }


        //constructor
        public Pipe() //needs a list of points as a parameter? 
        {
            Points = new List<Point>();
            Flow = 0;
        }
 
        //methods
        public void AddPoint(Point p)
        {
            Points.Add(p);
        }
        
    }
}

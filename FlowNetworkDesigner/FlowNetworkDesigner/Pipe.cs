using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowNetworkDesigner
{
    class Pipe:Component
    {
        private List<Point> points;

        //properties
        public double Flow { get; set; }


        //constructor
        public Pipe(double F, List<Point> p) //needs a list of points as a parameter? 
        {
            points = p;
            Flow = F;
        }
 
        //methods
        public void AddPoint(Point p)
        {
            points.Add(p);
        }
    }
}

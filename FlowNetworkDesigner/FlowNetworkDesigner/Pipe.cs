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
        public Point FirstPoint { get; set; }
        public Point SecondPoint { get; set; }
        public string Name { get; set; }
        public Double Flow { get; set; }

        public Pipe(Point firstPoint, Point secondPoint, string name) 
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            Name = name;
            Flow = 0;
        }
        public void Draw(Graphics graphic,double maxFlow)
        {
            if (Flow >= (maxFlow*0.7) && Flow < maxFlow) 
            {
                Pen pen = new Pen(Color.Yellow, 6);
                graphic.DrawLine(pen, FirstPoint, SecondPoint);
                graphic.DrawString("Flow " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.DarkOrange, FirstPoint.X + 20, FirstPoint.Y - 20);
            }
            else if (Flow >= maxFlow) 
            {
                Pen pen = new Pen(Color.Red, 6);
                graphic.DrawLine(pen, FirstPoint, SecondPoint);
                graphic.DrawString("Flow " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Red, FirstPoint.X + 20, FirstPoint.Y - 20);
            }
            else   
            {
                Pen pen = new Pen(Color.Green, 6);
                graphic.DrawLine(pen, FirstPoint, SecondPoint);
                graphic.DrawString("Flow " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Green, FirstPoint.X + 20, FirstPoint.Y - 20);
            }
        }
        public bool Contains(Point p)
        {
            int top = Math.Min(FirstPoint.Y, SecondPoint.Y);
            int left = Math.Min(FirstPoint.X, SecondPoint.X);

            int bottom = Math.Max(FirstPoint.Y, SecondPoint.Y);
            int right = Math.Max(FirstPoint.X, SecondPoint.X);
            //take points top, left, bottom, right and draw rechtangle
            Rectangle rectangle = Rectangle.FromLTRB(left, top, right, bottom);

            
            double x1 = FirstPoint.Y - SecondPoint.Y;
            double x2 = FirstPoint.X - SecondPoint.X;
            double xx = x1 / x2;
            double y1 = FirstPoint.X * xx;
            double yy = FirstPoint.Y - y1;

            if (p.Y + 30 > (xx * p.X + yy - 42) && p.Y - 40 < (xx * p.X + yy + 44))
            {
                return true;
            }
           
            return false;
        }



    }
}

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
        public int Flow { get; set; }

        public Pipe(Point firstPoint, Point secondPoint, string name, int flow) //Constructor
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            Name = name;
            Flow = flow;
        }
        public void Draw(Graphics graphic)
        {
            if (Flow >= 7 && Flow <= 9) //If flow is from 7 till 9, than color should be orange
            {
                Pen pen = new Pen(Color.Yellow, 6);
                graphic.DrawLine(pen, FirstPoint, SecondPoint);
                graphic.DrawString("Flow " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.DarkOrange, FirstPoint.X + 20, FirstPoint.Y - 20);
            }
            else if (Flow >= 10) //If flow is is 10 or bigger, than color should be red
            {
                Pen pen = new Pen(Color.Red, 6);
                graphic.DrawLine(pen, FirstPoint, SecondPoint);
                graphic.DrawString("Flow " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Red, FirstPoint.X + 20, FirstPoint.Y - 20);
            }
            else   //If flow is from 0 till 6, than color should be green
            {
                Pen pen = new Pen(Color.Green, 6);
                graphic.DrawLine(pen, FirstPoint, SecondPoint);
                graphic.DrawString("Flow " + Flow.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Green, FirstPoint.X + 20, FirstPoint.Y - 20);
            }
        }
        public bool Contains(Point p)
        {
            int verh = Math.Min(FirstPoint.Y, SecondPoint.Y);
            int levo = Math.Min(FirstPoint.X, SecondPoint.X);

            int niz = Math.Max(FirstPoint.Y, SecondPoint.Y);
            int pravo = Math.Max(FirstPoint.X, SecondPoint.X);
            //take points verh, levo, niz, pravo and drow rechtangle (из вышеобозначенных точек рисуем прямоугольник)
            Rectangle rectangle = Rectangle.FromLTRB(levo, verh, pravo, niz);

            //if (rectangle.Contains(p) == true) //Chech include rechtangle our point or not (проверяем входит ли наша точька в этот прямоугольник (эта команда находится в Element))
            //{
            double x1 = FirstPoint.Y - SecondPoint.Y;
            double x2 = FirstPoint.X - SecondPoint.X;
            double xx = x1 / x2;
            double y1 = FirstPoint.X * xx;
            double yy = FirstPoint.Y - y1;

            if (p.Y + 30 > (xx * p.X + yy - 42) && p.Y - 40 < (xx * p.X + yy + 44))
            {
                return true;
            }
            //}
            return false;
        }

        public bool ContainsForSink(Point p) 
        {
            int verh = Math.Min(FirstPoint.Y, SecondPoint.Y);
            int levo = Math.Min(FirstPoint.X, SecondPoint.X);

            int niz = Math.Max(FirstPoint.Y, SecondPoint.Y);
            int pravo = Math.Max(FirstPoint.X, SecondPoint.X);
            //take points verh, levo, niz, pravo and draw rechtangle
            Rectangle rectangle = Rectangle.FromLTRB(levo, verh, pravo, niz);

            //if (rectangle.Contains(p) == true) //Chech include rechtangle our point or not
            double x1 = FirstPoint.Y - SecondPoint.Y;
            double x2 = FirstPoint.X - SecondPoint.X;
            double xx = x1 / x2;
            double y1 = FirstPoint.X * xx;
            double yy = FirstPoint.Y - y1;

            if (p.Y > (xx * p.X + yy - 125) && p.Y < (xx * p.X + yy + 130))
            {
                return true;
            }
            //}
            return false;
        }







        //private double flow;
        //public List<Point> Points { get; set; }

        ////properties
        //public double Flow
        //{
        //    get
        //    {
        //        return this.flow;
        //    }
        //    set
        //    {
        //        flow = value;
        //    }
        //}


        ////constructor
        //public Pipe() //needs a list of points as a parameter? 
        //{
        //    Points = new List<Point>();
        //    Flow = 0;
        //}

        ////methods
        //public void AddPoint(Point p)
        //{
        //    Points.Add(p);
        //}

    }
}

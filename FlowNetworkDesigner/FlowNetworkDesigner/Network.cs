using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FlowNetworkDesigner
{
    class Network
    {
        private List<Component> Components;

        private List<Pipe> Pipes;

        private Component[] TempComponents;

        public Network()
        {
            Components = new List<Component>();
            Pipes = new List<Pipe>();
        }


        public void AddComponent(Component component, Form1 form)
        {
            if (CheckAddingComponents(component))
            {
                
                component.Draw(component.Position, form);
                               
                
                Components.Add(component);
            }
        }

        public void AddPipe(Pipe pipe, PaintEventArgs e, Form form)
        {
            ConnectPipe(pipe, e, form);
            Pipes.Add(pipe);
            
        }

        private void ConnectPipe(Pipe pipe, PaintEventArgs e, Form form)
        {
            TempComponents = new Component[2];
            foreach (Component c in Components)
            {
                if(pipe.Points[0] == c.Position)
                {
                    if(!(c is Sink))
                    {
                        TempComponents[0] = c;
                        c.AddOuterPipe(pipe);
                        pipe = c.UpdatePipe(pipe);
                    }
                    
                    //if(c is Pump)
                    //{
                        
                    //    ((Pump)c).AddPipe(pipe);
                    //    pipe = c.UpdatePipe(pipe);
                    //}
                    //else if(c is Merger)
                    //{

                    //}
                    //else if(c is Splitter)
                    //{

                    //}
                    //else if(c is AdjSplitter)
                    //{

                    //}
                }
                if(pipe.Points[pipe.Points.Count - 1] == c.Position)
                {
                    if(c is Splitter)
                    {
                        TempComponents[1] = c;
                    }
                    else if(!(c is Pump))
                    {
                        c.AddInnerPipe(pipe);
                        TempComponents[1] = c;
                    }
                   
                    //if (c is Sink)
                    //{
                    //    ((Sink)c).AddPipe(pipe);
                    //}
                    //else if (c is Merger)
                    //{

                    //}
                    //else if (c is Splitter)
                    //{

                    //}
                    //else if (c is AdjSplitter)
                    //{

                    //}
                }
            }
            // use the graphics class here to draw the pipes
            Graphics g = e.Graphics;
            Pen p = new Pen(Brushes.Green, 3);
            for (int i = 0; i<pipe.Points.Count; i++)
            {
                //this part is if the user clicked on the components only
                if(pipe.Points.Count == 2)
                {
                    if (TempComponents[0] is Splitter || TempComponents[0] is AdjSplitter)
                    {
                        if (((Splitter)TempComponents[0]).IsUpperOutPipeNull())
                        { 
                            //this is to align the first pipeline at the top of the picture box
                            int lr, td;
                            lr = pipe.Points[0].X + 45;
                            td = pipe.Points[0].Y + 5;
                            Point pos = new Point(lr, td);
                            // end of alignment

                            if (TempComponents[1] is Merger)
                            {
                                if (((Merger)TempComponents[1]).IsUpperInPipeNull())
                                {
                                    //this is for the positioning of the pipeline to the middle of the last component
                                    td = pipe.Points[pipe.Points.Count - 1].Y + 5;
                                    Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                                    //end of alignment
                                    g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                                    Label label = new Label();
                                    label.Text = "Flow " + pipe.Flow;
                                    label.BackColor = Color.Transparent;
                                    int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                                    int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                                    label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                                    form.Controls.Add(label);
                                    break;
                                }
                                else
                                {
                                    //this is for the positioning of the pipeline to the middle of the last component
                                    td = pipe.Points[pipe.Points.Count - 1].Y + (TempComponents[1].pb.Size.Height - 5);
                                    Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                                    //end of alignment
                                    g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                                    Label label = new Label();
                                    label.Text = "Flow " + pipe.Flow;
                                    label.BackColor = Color.Transparent;
                                    int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                                    int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                                    label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                                    form.Controls.Add(label);
                                    break;
                                }
                            }
                            else if (!(TempComponents[1] is Pump))
                            {
                                //this is for the positioning of the pipeline to the middle of the last component
                                td = pipe.Points[pipe.Points.Count - 1].Y + (50 / 2);
                                Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                                //end of alignment
                                g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                                Label label = new Label();
                                label.Text = "Flow " + pipe.Flow;
                                label.BackColor = Color.Transparent;
                                int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                                int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                                label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                                form.Controls.Add(label);
                                break;
                            }
                        }
                        else
                        {
                            //this is to align the second pipeline to the buttom of the picture box
                            int lr, td;
                            lr = pipe.Points[0].X + 45;
                            td = pipe.Points[0].Y + (TempComponents[0].pb.Size.Height-5);
                            Point pos = new Point(lr, td);
                            // end of alignment

                            if (TempComponents[1] is Merger)
                            {
                                if (((Merger)TempComponents[1]).IsUpperInPipeNull())
                                {
                                    //this is for the positioning of the pipeline to the middle of the last component
                                    td = pipe.Points[pipe.Points.Count - 1].Y + 5;
                                    Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                                    //end of alignment
                                    g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                                    Label label = new Label();
                                    label.Text = "Flow " + pipe.Flow;
                                    label.BackColor = Color.Transparent;
                                    int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                                    int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                                    label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                                    form.Controls.Add(label);
                                    break;
                                }
                                else
                                {
                                    //this is for the positioning of the pipeline to the middle of the last component
                                    td = pipe.Points[pipe.Points.Count - 1].Y + (TempComponents[1].pb.Size.Height - 5);
                                    Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                                    //end of alignment
                                    g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                                    Label label = new Label();
                                    label.Text = "Flow " + pipe.Flow;
                                    label.BackColor = Color.Transparent;
                                    int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                                    int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                                    label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                                    form.Controls.Add(label);
                                    break;
                                }
                            }
                            else if (!(TempComponents[1] is Pump))
                            {
                                //this is for the positioning of the pipeline to the middle of the last component
                                td = pipe.Points[pipe.Points.Count - 1].Y + (50 / 2);
                                Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                                //end of alignment
                                g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                                Label label = new Label();
                                label.Text = "Flow " + pipe.Flow;
                                label.BackColor = Color.Transparent;
                                int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                                int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                                label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                                form.Controls.Add(label);
                                break;
                            }
                        }
                    }
                    else if(!(TempComponents[0] is Sink))
                    { 
                        //this is to align the line in the middle of the picture box
                        int lr, td;
                        lr = pipe.Points[0].X + 45;
                        td = pipe.Points[0].Y + (50 / 2);
                        Point pos = new Point(lr, td);
                        // end of alignment


                        //Akinan pone if statement pa e lastu component den e list di tempcomponent (dus pa check e merger)
                        if (TempComponents[1] is Merger)
                        {
                            if (((Merger)TempComponents[1]).IsUpperInPipeNull())
                            {
                                //this is for the positioning of the pipeline to the middle of the last component
                                td = pipe.Points[pipe.Points.Count - 1].Y + 5;
                                Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                                //end of alignment
                                g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                                Label label = new Label();
                                label.Text = "Flow " + pipe.Flow;
                                label.BackColor = Color.Transparent;
                                int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                                int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                                label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                                form.Controls.Add(label);
                                break;
                            }
                            else
                            {
                                //this is for the positioning of the pipeline to the middle of the last component
                                td = pipe.Points[pipe.Points.Count - 1].Y + (TempComponents[1].pb.Size.Height-5);
                                Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                                //end of alignment
                                g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                                Label label = new Label();
                                label.Text = "Flow " + pipe.Flow;
                                label.BackColor = Color.Transparent;
                                int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                                int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                                label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                                form.Controls.Add(label);
                                break;
                            }
                        }
                        else if (!(TempComponents[1] is Pump))
                        {
                            //this is for the positioning of the pipeline to the middle of the last component
                            td = pipe.Points[pipe.Points.Count - 1].Y + (50 / 2);
                            Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                            //end of alignment
                            g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

                            Label label = new Label();
                            label.Text = "Flow " + pipe.Flow;
                            label.BackColor = Color.Transparent;
                            int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                            int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                            label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                            form.Controls.Add(label);
                            break;
                        }
                    }
                }
                //end of Checking for only clicking on the components in the form
               
                //this part is if the user clicked on multiple areas in the form
                if (i == 0)
                {
                    if (TempComponents[0] is Splitter || TempComponents[0] is AdjSplitter)
                    {
                        if (((Splitter)TempComponents[0]).IsUpperOutPipeNull())
                        {
                            //this is to align the first pipeline at the top of the picture box
                            int lr, td;
                            lr = pipe.Points[0].X + 45;
                            td = pipe.Points[0].Y + 5;
                            Point pos = new Point(lr, td);
                            // end of alignment
                            g.DrawLine(p, form.PointToClient(pos), form.PointToClient(pipe.Points[i+1]));
                        }
                        else
                        {
                            //this is to align the second pipeline to the buttom of the picture box
                            int lr, td;
                            lr = pipe.Points[0].X + 45;
                            td = pipe.Points[0].Y + (TempComponents[0].pb.Size.Height - 5);
                            Point pos = new Point(lr, td);
                            // end of alignment
                            g.DrawLine(p, form.PointToClient(pos), form.PointToClient(pipe.Points[i+1]));
                        }
                    }
                    else if (!(TempComponents[0] is Sink))
                    {
                        //this is to align the line in the middle of the picture box
                        int lr, td;
                        lr = pipe.Points[0].X + 45;
                        td = pipe.Points[0].Y + (50 / 2);
                        Point pos = new Point(lr, td);
                        // end of alignment
                        g.DrawLine(p, form.PointToClient(pos), form.PointToClient(pipe.Points[i+1]));
                    }
                    
                }
                else if(i < pipe.Points.Count-2)
                {
                    g.DrawLine(p, form.PointToClient(pipe.Points[i]), form.PointToClient(pipe.Points[i + 1]));
                }
                else
                {
                    if (TempComponents[1] is Merger)
                    {
                        if (((Merger)TempComponents[1]).IsUpperInPipeNull())
                        {
                            //this is for the positioning of the pipeline to the middle of the last component
                            int td = pipe.Points[pipe.Points.Count - 1].Y + 5;
                            Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                            //end of alignment
                            g.DrawLine(p, form.PointToClient(pipe.Points[i]), form.PointToClient(posf));

                            Label label = new Label();
                            label.Text = "Flow " + pipe.Flow;
                            label.BackColor = Color.Transparent;
                            int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                            int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                            label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                            form.Controls.Add(label);
                            break;
                        }
                        else
                        {
                            //this is for the positioning of the pipeline to the middle of the last component
                            int td = pipe.Points[pipe.Points.Count - 1].Y + (TempComponents[1].pb.Size.Height - 5);
                            Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                            //end of alignment
                            g.DrawLine(p, form.PointToClient(pipe.Points[i]), form.PointToClient(posf));

                            Label label = new Label();
                            label.Text = "Flow " + pipe.Flow;
                            label.BackColor = Color.Transparent;
                            int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                            int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                            label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                            form.Controls.Add(label);
                            break;
                        }
                    }
                    else if (!(TempComponents[1] is Pump))
                    {
                        //this is for the positioning of the pipeline to the middle of the last component
                        int td = pipe.Points[pipe.Points.Count - 1].Y + (50 / 2);
                        Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
                        //end of alignment
                        g.DrawLine(p, form.PointToClient(pipe.Points[i]), form.PointToClient(posf));

                        Label label = new Label();
                        label.Text = "Flow " + pipe.Flow;
                        label.BackColor = Color.Transparent;
                        int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
                        int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

                        label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
                        form.Controls.Add(label);
                        break;
                    }
                    
                }
                //end if the user clicked on multiple areas in the form
            }


        }

        //Checks if the point you clicked is valid and not next to a component
        private bool CheckAddingComponents(Component comp)
        {
            if (Components.Count > 0)
            {
                foreach (Component c in Components)
                {
                    if(comp.Position.X >=(c.Position.X-63)&& comp.Position.Y >= (c.Position.Y - 63)&& comp.Position.X <= (c.Position.X + 63) && comp.Position.Y <= (c.Position.Y + 63))
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        public Component GetComponent(string type)
        {
            foreach(Component c in Components)
            {
                if (c.pb.Name == type)
                {
                    return c;
                }
            }
            return null;
        }


    }
}

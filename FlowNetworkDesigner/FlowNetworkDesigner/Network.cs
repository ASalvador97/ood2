using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FlowNetworkDesigner
{
    class Network
    {
        int i = 1; 
        int checkPump = 0; 
        internal List<Component> Components = new List<Component>(); 
        internal List<Pipe> Pipes = new List<Pipe>(); 
        internal List<Splitter> Splitters = new List<Splitter>(); 
        internal List<Merger> Mergers = new List<Merger>();
        internal List<AdjSplitter> AdjSplitters = new List<AdjSplitter>(); 
        Splitter splitter;
        Merger merger;
        AdjSplitter adjsplit;

        List<Component> componentlist = new List<Component>();//only contains two components, this is for when clicking a component to add pipe

        Point tempPoint = new Point(); //for AddPipeline method
        bool position = false; //for AddPipeline method
        public Network() { }

        public Pipe Pipe
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Component Component
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void AddComponent(Component component, int percentage,Point position,string type,double currentFlow,double maxFlow)
        {
            if (type == "Pump")
            {
                component = new Pump(position, type, currentFlow);
            }
            else if (type == "Sink")
            {
                component = new Sink(position, type, maxFlow);
            }
            else if (type == "Merger")
            {
                component = new Merger(position, type);
                Mergers.Add(((Merger)component));
            }
            else if (type == "Splitter")
            {
                component = new Splitter(position, type);
                Splitters.Add(((Splitter)component));
            }
            else if (type == "AdjSplitter")
            {
                component = new AdjSplitter(position, type,percentage);
                AdjSplitters.Add(((AdjSplitter)component));
            }


            if (CheckAddingComponents(component))
            {

                Components.Add(component);
                // component.Draw(component.Position, form);
            }
        }

        public void AddPipe(Point p, string name) 
        {
           
            int xx = 0;
            Component comp = CheckComponent(p);
            componentlist.Add(comp);
            if (comp != null) //If comp is not null means user clicked on a component in the form else it returns null
            {
                if (position == false) //if click first time (first point of pipeline)
                {
                    position = true;
                    tempPoint = p;

                    if (comp is Pump) 
                    {
                        checkPump++;
                        
                    }             

                   else if (comp is Sink) 
                    {
                        int x = CheckPipelineForSink(p,comp).Count();
                        
                        if (x > 1)
                        {
                            MessageBox.Show("The sink can not have more then 2 input ");
                            position = false;
                        }
                    }            
                }
                else //if click second time (second point of pipeline)
                {
                    if (comp is Pump && checkPump == 1) 
                    {
                        MessageBox.Show("Pump can not be your next component");
                        checkPump--;
                        position = false;
                    }
                    else
                    {
                       
                        if (!(comp is Pump))
                        {
                            checkPump = 0;
                        }                           
                        if (comp is Sink)            
                        {
                            int x = CheckPipelineForSink(p,comp).Count();
                           
                            if (x > 1)
                            {
                                MessageBox.Show("The sink can not have more then 2 input ");
                                x = 0;
                                xx = 1;
                                position = false;
                            }
                        }                        

                        if (xx == 0)
                        {
                            Pipe plf = new Pipe(tempPoint, p, name);
                            foreach (Component c in Components)
                            {
                                if (c == componentlist[0] && c is Pump|| c == componentlist[1] && c is Pump)
                                {
                                    plf.Flow = c.Flow;
                                    c.AddPipe(plf);
                                }
                                if(c == componentlist[0] && c is Sink || c == componentlist[1] && c is Sink)
                                {
                                    c.AddPipe(plf);
                                }
                                if (c == componentlist[0] && c is Splitter || c == componentlist[1] && c is Splitter)
                                {
                                    if (((Splitter)c).listPipeInputSplitter.Count == 0)
                                    {
                                        c.AddPipe(plf);
                                    }
                                }
                            }
                            Pipes.Add(plf);
                          
                            foreach (Component e in Components.ToList()) 
                            {
                                if ((tempPoint.X + 63 >= e.Position.X && (tempPoint.Y + 63 >= e.Position.Y) && (tempPoint.X - 63 <= e.Position.X) && (tempPoint.Y - 63 <= e.Position.Y) && (e is Splitter) && !(e is AdjSplitter)))
                                {
                                    CheckSplitter(p, name, e, plf);
                                }
                                else if (tempPoint.X + 63 >= e.Position.X && (tempPoint.Y + 63 >= e.Position.Y) && (tempPoint.X - 63 <= e.Position.X) && (tempPoint.Y - 63 <= e.Position.Y) && (e is AdjSplitter))
                                {
                                    CheckAdjustableSplitter(p, name, e, plf);
                                }
                                else if ((p.X + 63 >= e.Position.X && (p.Y + 63 >= e.Position.Y) && (p.X - 63 <= e.Position.X) && (p.Y - 63 <= e.Position.Y) && (e is Splitter) && !(e is AdjSplitter)))
                                {
                                    splitter = new Splitter(e.Position,e.Name);
                                    splitter.listPipeInputSplitter.Add(plf);
                                    CheckSplitter1(p, name, e, plf);
                                }
                                else if (p.X + 63 >= e.Position.X && (p.Y + 63 >= e.Position.Y) && (p.X - 63 <= e.Position.X) && (p.Y - 63 <= e.Position.Y) && (e is AdjSplitter))
                                {
                                    adjsplit = new AdjSplitter(e.Position, e.Name,100);
                                    adjsplit.listPipeInputSplitter.Add(plf);
                                    CheckAdjustableSplitter1(p, name, e, plf);
                                }
                                if (((p.X + 63 >= e.Position.X && (p.Y + 63 >= e.Position.Y) && (p.Y - 63 <= e.Position.Y) && (e is Merger))))
                                {
                                    CheckMerger(p, name, e, plf);
                                }
                                else if (((tempPoint.X + 63 >= e.Position.X && (tempPoint.Y + 63 >= e.Position.Y) && (tempPoint.Y - 63 <= e.Position.Y) && (e is Merger))))
                                {
                                    try
                                    {
                                        merger.listPipelineOutputMerger.Add(plf);
                                    }
                                    catch (Exception)
                                    {

                                    }
                                    CheckMerger1(p, name, e, plf);
                                }
                                position = false;
                            }

                            if (componentlist.Count == 2)
                            {
                                componentlist = new List<Component>();
                            }
                        }
                    }
                }
            }
        }

        internal List<Pipe> CheckPipelineForSink(Point p, Component comp)
        {
            List<Pipe> temp;
            temp = new List<Pipe>();
            foreach (Pipe pl in Pipes)
            {
                if (ContainsForSink(pl.FirstPoint,comp) == true || ContainsForSink(pl.SecondPoint, comp) == true)
                {
                    temp.Add(pl);
                }
                
            }
            return temp;
        }
        private bool ContainsForSink(Point p , Component comp)
        {
            if (p.X  >= comp.Position.X && p.X <=(comp.Position.X+63) && p.Y >= comp.Position.Y && p.Y  <= (comp.Position.Y+63))
            {
                return true;
            }
            return false;
        }

       
        internal void CheckSplitter(Point p, string name, Component e, Pipe plf) 
        {
            foreach (Splitter spl in Splitters)
            {
                if (spl.Position == e.Position)
                {
                    splitter = spl;
                }
            }

            splitter.listPipeOutputSplitter.Add(plf);

            foreach (Pipe pl in Pipes)
            {
                foreach (Pipe pl2 in splitter.listPipeOutputSplitter)
                {
                    if (pl.SecondPoint == pl2.SecondPoint)
                    {
                        pl.Flow = pl2.Flow;
                        pl.FirstPoint = pl2.FirstPoint;
                    }
                }
            }

            if (splitter.listPipeOutputSplitter.Count == 1)
            {
                try
                {
                    splitter.listPipeOutputSplitter[0].Flow = splitter.listPipeInputSplitter[0].Flow;
                }
                catch (Exception)
                {

                }
                foreach (Pipe pl in splitter.listPipeOutputSplitter)
                {
                    if ((pl.FirstPoint.X) + 63 >= e.Position.X && ((pl.FirstPoint.Y) + 63 >= e.Position.Y && (pl.FirstPoint.X) - 63 <= e.Position.X) && ((pl.FirstPoint.X) + 63 >= e.Position.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Position;
                    }
                }

                foreach (Pipe pl in Pipes)
                {
                    if ((pl.FirstPoint.X) + 63 >= e.Position.X && ((pl.FirstPoint.Y) + 63 >= e.Position.Y && (pl.FirstPoint.X) - 63 <= e.Position.X) && ((pl.FirstPoint.X) + 63 >= e.Position.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Position;
                    }
                }
            }
            else if (splitter.listPipeOutputSplitter.Count == 2)
            {
                splitter.listPipeOutputSplitter[0].Flow = splitter.listPipeInputSplitter[0].Flow / 2;
                splitter.listPipeOutputSplitter[1].Flow = splitter.listPipeInputSplitter[0].Flow / 2;
            }
            else if (splitter.listPipeOutputSplitter.Count > 2)
            {
                MessageBox.Show("You cant have more than 2 output pumps.");
                splitter.listPipeOutputSplitter.Remove(splitter.listPipeOutputSplitter[2]);

                Component el = CheckComponent(p);
                Pipe pl = CheckPipeline(p);

                if (el != null)
                {
                    foreach (Pipe pl12 in Pipes.ToList())
                    {
                        if (pl12.SecondPoint == pl.SecondPoint)
                        {
                            Pipes.Remove(pl12);
                        }
                    }
                }
                if (pl != null)
                {
                    Pipes.Remove(pl);
                }
            }
        }

       
        internal void CheckSplitter1(Point p, string name, Component e, Pipe plf)
        {
            foreach (Splitter spl in Splitters)
            {
                if (spl.Position == e.Position)
                {
                    splitter = spl;
                }
            }


            foreach (Pipe pl in Pipes)
            {
                foreach (Pipe pl2 in splitter.listPipeOutputSplitter)
                {
                    if (pl.SecondPoint == pl2.SecondPoint)
                    {
                        pl.Flow = pl2.Flow;
                        pl.FirstPoint = pl2.FirstPoint;
                    }
                }
            }

            if (splitter.listPipeInputSplitter.Count > 1)
            {
                MessageBox.Show("It is not possible for pump to have more than 1 input.");
                Pipe pl = CheckPipeline(p);

                foreach (Pipe plg in splitter.listPipeInputSplitter.ToList())
                {
                    if (splitter.listPipeInputSplitter.Count > 1)
                    {
                        if (splitter.listPipeInputSplitter.Count > 2)
                        {
                            Pipes.Remove(splitter.listPipeInputSplitter[i]);
                            splitter.listPipeInputSplitter.Remove(splitter.listPipeInputSplitter[i]);
                        }
                        else if (splitter.listPipeInputSplitter.Count == 2)
                        {
                            Pipes.Remove(splitter.listPipeInputSplitter[1]);
                            splitter.listPipeInputSplitter.Remove(splitter.listPipeInputSplitter[1]);
                        }

                        i++;
                    }
                }
            }

            if (splitter.listPipeOutputSplitter.Count == 1)
            {
                splitter.listPipeOutputSplitter[0].Flow = splitter.listPipeInputSplitter[0].Flow;
                foreach (Pipe pl in splitter.listPipeOutputSplitter)
                {
                    if ((pl.FirstPoint.X) + 63 >= e.Position.X && ((pl.FirstPoint.Y) + 63 >= e.Position.Y && (pl.FirstPoint.X) - 63 <= e.Position.X) && ((pl.FirstPoint.X) + 63 >= e.Position.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Position;
                    }
                }

                foreach (Pipe pl in Pipes)
                {
                    if ((pl.FirstPoint.X) + 63 >= e.Position.X && ((pl.FirstPoint.Y) + 63 >= e.Position.Y && (pl.FirstPoint.X) - 63 <= e.Position.X) && ((pl.FirstPoint.X) + 63 >= e.Position.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Position;
                    }
                }
            }
            else if (splitter.listPipeOutputSplitter.Count == 2)
            {
                splitter.listPipeOutputSplitter[0].Flow = splitter.listPipeInputSplitter[0].Flow / 2;
                splitter.listPipeOutputSplitter[1].Flow = splitter.listPipeInputSplitter[0].Flow / 2;
            }
        }



        
        internal void CheckAdjustableSplitter(Point p, string name, Component e, Pipe plf)
        {
            foreach (AdjSplitter spl in AdjSplitters)
            {
                if (spl.Position == e.Position)
                {
                    adjsplit = spl;
                }
            }

            adjsplit.listPipeOutputSplitter.Add(plf);

            foreach (Pipe pl in Pipes)
            {
                foreach (Pipe pl2 in adjsplit.listPipeOutputSplitter)
                {
                    if (pl.SecondPoint == pl2.SecondPoint)
                    {
                        pl.Flow = pl2.Flow;
                        pl.FirstPoint = pl2.FirstPoint;
                    }
                }
            }


            if (adjsplit.listPipeOutputSplitter.Count == 1)
            {
                adjsplit.listPipeOutputSplitter[0].Flow = adjsplit.listPipeInputSplitter[0].Flow;
                foreach (Pipe pl in adjsplit.listPipeOutputSplitter)
                {
                    if ((pl.FirstPoint.X) + 63 >= e.Position.X && ((pl.FirstPoint.Y) + 63 >= e.Position.Y && (pl.FirstPoint.X) - 63 <= e.Position.X) && ((pl.FirstPoint.X) + 63 >= e.Position.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Position;
                    }
                }

                foreach (Pipe pl in Pipes)
                {
                    if ((pl.FirstPoint.X) + 63 >= e.Position.X && ((pl.FirstPoint.Y) + 63 >= e.Position.Y && (pl.FirstPoint.X) - 63 <= e.Position.X) && ((pl.FirstPoint.X) + 63 >= e.Position.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Position;
                    }
                }
            }
            else if (adjsplit.listPipeOutputSplitter.Count == 2)
            {
                adjsplit.listPipeOutputSplitter[0].Flow = Convert.ToDouble(adjsplit.listPipeInputSplitter[0].Flow * adjsplit.upValue / 100);
                adjsplit.listPipeOutputSplitter[1].Flow = Convert.ToDouble(adjsplit.listPipeInputSplitter[0].Flow - (adjsplit.listPipeInputSplitter[0].Flow * adjsplit.upValue / 100));
            }
            else if (adjsplit.listPipeOutputSplitter.Count > 2)
            {
                MessageBox.Show("It is not possible to have more than 2 output pumps.");
                adjsplit.listPipeOutputSplitter.Remove(adjsplit.listPipeOutputSplitter[1]);

                Component el = CheckComponent(p);
                Pipe pl = CheckPipeline(p);

                if (el != null)
                {
                    foreach (Pipe pl12 in Pipes.ToList())
                    {
                        if (pl12.SecondPoint == pl.SecondPoint)
                        {
                            Pipes.Remove(pl12);
                        }
                    }
                }

                if (pl != null)
                {
                    Pipes.Remove(pl);
                }
            }
        }

        
        internal void CheckAdjustableSplitter1(Point p, string name, Component e, Pipe plf)
        {
            foreach (AdjSplitter spl in AdjSplitters)
            {
                if (spl.Position == e.Position)
                {
                    adjsplit = spl;
                }
            }

            foreach (Pipe pl in Pipes)
            {
                foreach (Pipe pl2 in adjsplit.listPipeOutputSplitter)
                {
                    if (pl.SecondPoint == pl2.SecondPoint)
                    {
                        pl.Flow = pl2.Flow;
                        pl.FirstPoint = pl2.FirstPoint;
                    }
                }
            }

            if (adjsplit.listPipeInputSplitter.Count > 1)
            {
                MessageBox.Show("It is not possible for pump to have more than 1 input.");
                Pipe pl = CheckPipeline(p);

                foreach (Pipe plg in adjsplit.listPipeInputSplitter.ToList())
                {
                    if (adjsplit.listPipeInputSplitter.Count > 1)
                    {
                        if (adjsplit.listPipeInputSplitter.Count > 2)
                        {
                            Pipes.Remove(adjsplit.listPipeInputSplitter[i]);
                            adjsplit.listPipeInputSplitter.Remove(adjsplit.listPipeInputSplitter[i]);
                        }
                        else if (adjsplit.listPipeInputSplitter.Count == 2)
                        {
                            Pipes.Remove(adjsplit.listPipeInputSplitter[1]);
                            adjsplit.listPipeInputSplitter.Remove(adjsplit.listPipeInputSplitter[1]);
                        }

                        i++;
                    }
                }
            }

            if (adjsplit.listPipeOutputSplitter.Count == 1)
            {
                adjsplit.listPipeOutputSplitter[0].Flow = adjsplit.listPipeInputSplitter[0].Flow;
                foreach (Pipe pl in adjsplit.listPipeOutputSplitter)
                {
                    if ((pl.FirstPoint.X) + 63 >= e.Position.X && ((pl.FirstPoint.Y) + 63 >= e.Position.Y && (pl.FirstPoint.X) - 63 <= e.Position.X) && ((pl.FirstPoint.X) + 63 >= e.Position.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Position;
                    }
                }

                foreach (Pipe pl in Pipes)
                {
                    if ((pl.FirstPoint.X) + 63 >= e.Position.X && ((pl.FirstPoint.Y) + 63 >= e.Position.Y && (pl.FirstPoint.X) - 63 <= e.Position.X) && ((pl.FirstPoint.X) + 63 >= e.Position.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Position;
                    }
                }
            }
            else if (adjsplit.listPipeOutputSplitter.Count == 2)
            {
                adjsplit.listPipeOutputSplitter[0].Flow = Convert.ToInt32(adjsplit.listPipeInputSplitter[0].Flow * adjsplit.upValue / 100);
                adjsplit.listPipeOutputSplitter[1].Flow = Convert.ToInt32(adjsplit.listPipeInputSplitter[0].Flow - (adjsplit.listPipeInputSplitter[0].Flow * adjsplit.upValue / 100));
            }
        }

        
        internal void CheckMerger(Point p, string name, Component e, Pipe plf)  
        {
            foreach (Merger merg in Mergers)
            {
                if (merg.Position == e.Position)
                {
                    merger = merg;
                }
            }

            if ((tempPoint.X + 63 >= e.Position.X && (tempPoint.Y + 63 >= e.Position.Y) && ((tempPoint.X - 63 <= e.Position.X) && (tempPoint.Y - 63 <= e.Position.Y)) && (e is Merger)))
            {
                merger.listPipelineOutputMerger.Add(plf);
            }
            if ((p.X + 63 >= e.Position.X && (p.Y + 63 >= e.Position.Y) && ((p.X - 63 <= e.Position.X) && (p.Y - 63 <= e.Position.Y)) && (e is Merger)))
            {
                merger.listPipelineInputMerger.Add(plf);

                foreach (Pipe pl in Pipes)
                {
                    foreach (Pipe pl2 in merger.listPipelineInputMerger)
                    {
                        if (pl.SecondPoint == pl2.SecondPoint)
                        {
                            pl.Flow = pl2.Flow;
                            pl.FirstPoint = pl2.FirstPoint;
                        }
                    }
                }
            }


            if (merger.listPipelineInputMerger.Count > 2)
            {
                MessageBox.Show("It is not possible to have more than 2 input pumps.");

                Pipe pl = CheckPipeline(p);

                Pipes.Remove(merger.listPipelineInputMerger[2]);
                merger.listPipelineInputMerger.Remove(merger.listPipelineInputMerger[2]);
            }




            if (merger.listPipelineInputMerger.Count == 1)
            {
                merger.listPipelineInputMerger[0].Flow = plf.Flow;

                foreach (Pipe pl in merger.listPipelineInputMerger)
                {
                    if ((pl.SecondPoint.X) + 63 >= e.Position.X && ((pl.SecondPoint.Y) + 63 >= e.Position.Y && (pl.SecondPoint.X) - 63 <= e.Position.X) && ((pl.SecondPoint.X) + 63 >= e.Position.X) && (e is Merger))
                    {
                        pl.SecondPoint = new Point(e.Position.X + 18, e.Position.Y + 18);
                    }
                }
                foreach (Pipe pl in Pipes)
                {
                    if ((pl.SecondPoint.X) + 63 >= e.Position.X && ((pl.SecondPoint.Y) + 63 >= e.Position.Y && (pl.SecondPoint.X) - 63 <= e.Position.X) && ((pl.SecondPoint.X) + 63 >= e.Position.X) && (e is Merger))
                    {
                        pl.SecondPoint = new Point(e.Position.X + 18, e.Position.Y + 18);
                    }
                }
            }
            else if (merger.listPipelineInputMerger.Count == 2 && merger.listPipelineOutputMerger.Count == 1)
            {
                double final = 0;
                foreach (Pipe pl in merger.listPipelineInputMerger)
                {
                    final += pl.Flow;
                }
                merger.listPipelineOutputMerger[0].Flow = final;
            }
        }

       
        internal void CheckMerger1(Point p, string name, Component e, Pipe plf)
        {
            if (merger.listPipelineInputMerger.Count == 2 && merger.listPipelineOutputMerger.Count == 1)
            {
                double final = 0;
                foreach (Pipe pl in merger.listPipelineInputMerger)
                {
                    final += pl.Flow;
                }
                merger.listPipelineOutputMerger[0].Flow = final;
            }

            if (merger.listPipelineOutputMerger.Count > 1 && merger.listPipelineOutputMerger.Count == 2)
            {
                MessageBox.Show("It is not possible for pump to have more than 1 output.");
                merger.listPipelineOutputMerger.Remove(merger.listPipelineOutputMerger[0]);
                Pipe pl = CheckPipeline(p);
                if (pl != null)
                {
                    Pipes.Remove(pl);
                }
            }
        }


        //Checks if the point you clicked is valid and not next to a component
        private bool CheckAddingComponents(Component comp)
        {
            if (Components.Count > 0)
            {
                foreach (Component c in Components)
                {
                    if (comp.Position.X >= (c.Position.X - 63) && comp.Position.Y >= (c.Position.Y - 63) && comp.Position.X <= (c.Position.X + 63) && comp.Position.Y <= (c.Position.Y + 63))
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        internal Component CheckComponent(Point p) // for pipeline (checking, does picture include point p) 
        {
            foreach (Component e in Components)
            {
                if (e.Contains(p) == true)
                {
                    return e;
                }
            }
            return null;
        }
        

        internal Pipe CheckPipeline(Point p) //checking, does pipeline include point p 
        {
            foreach (Pipe pl in Pipes)
            {
                if (pl.Contains(p) == true)
                {
                    return pl;
                }
            }
            return null;
        }
        public void Draw(Graphics graphic) 
        {
            double maxflow=0;
            foreach (Component e in Components)
            {
                e.Draw(graphic);
                if(e is Sink)
                {
                    maxflow = ((Sink)e).Flow;
                }
            }
            foreach (Pipe p in Pipes)
            {
                p.Draw(graphic,maxflow);
            }
         
        }
        public void DeleteElementOrPipeline(Point p) //Delete component or pipe that was chosen
        {
            Component el = CheckComponent(p);
            Pipe pl = CheckPipeline(p);

            if (el != null)
            {
                if (pl != null)
                {
                    MessageBox.Show("First delete Pipeline");
                }
                else
                {
                    Components.Remove(el);
                }
            }
            else if (pl != null)
            {
                Pipes.Remove(pl);
            }
        }
       
       
        public void Save() 
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                SaveFileDialog info = new SaveFileDialog();
                info.Title = "Saving Pipeline Network";
                info.Filter = "Text Document |*.txt";
                info.ShowDialog();

                fs = new FileStream(info.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                sw = new StreamWriter(fs);

                foreach (Component x in Components)
                {
                    if (x.Name == "Pump")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Position.X);
                        sw.WriteLine(((Component)x).Position.Y);

                        Pump temp = (Pump)x;
                        sw.WriteLine(temp.Flow);
                    }
                    if (x.Name == "Sink")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Position.X);
                        sw.WriteLine(((Component)x).Position.Y);

                        Sink temp = (Sink)x;
                        sw.WriteLine(temp.Flow);
                    }
                    if (x.Name == "Merger")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Position.X);
                        sw.WriteLine(((Component)x).Position.Y);
                    }
                    if (x.Name == "Splitter")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Position.X);
                        sw.WriteLine(((Component)x).Position.Y);
                    }
                    if (x.Name == "Adjustable Splitter")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Position.X);
                        sw.WriteLine(((Component)x).Position.Y);

                        AdjSplitter temp = (AdjSplitter)x;
                        sw.WriteLine(temp.upValue);
                    }
                }
                foreach (Pipe p in Pipes)
                {
                    sw.WriteLine(p.Name);
                    sw.WriteLine(p.FirstPoint.X); //Write coordinates of first point
                    sw.WriteLine(p.FirstPoint.Y);

                    sw.WriteLine(p.SecondPoint.X); //Write coordinates of second point
                    sw.WriteLine(p.SecondPoint.Y);
                    sw.WriteLine(p.Flow);
                }
                sw.WriteLine("*"); //* - means end
                MessageBox.Show("Saving is done.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }
        public void Load() 
        {
            Components.Clear();
            Pipes.Clear();

            FileStream fs = null;
            StreamReader sr = null;
            OpenFileDialog info = new OpenFileDialog();
            info.Title = "Openning Pipeline Network";
            info.Filter = "Text Document |*.txt";
            info.ShowDialog();

            try
            {
                fs = new FileStream(info.FileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);

                string name = sr.ReadLine();
                Point location, location1, location2;
                int capacity = 0;
                int flow = 0;
                int percup = 0;
                

                while (name != "*")
                {
                    if (name == "Pump")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        capacity = Convert.ToInt32(sr.ReadLine());
                        AddComponent(null, 0, location, null, capacity, 0);

                    }
                    if (name == "Sink")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        capacity = Convert.ToInt32(sr.ReadLine());
                        AddComponent(null, 0, location, null, 0, capacity);
                    }
                    if (name == "Merger")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        AddComponent(null, 0, location, null, capacity, 0);

                    }
                    if (name == "Splitter")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        AddComponent(null, 0, location, null, capacity, 0);

                    }
                    if (name == "Adjustable Splitter")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        percup = Convert.ToInt32(sr.ReadLine());
                        AddComponent(null, percup, location, null, capacity, 0);

                    }
                    if (name == "Pipeline")
                    {
                        location1 = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        location2 = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        flow = Convert.ToInt32(sr.ReadLine());
                        Pipes.Add(new Pipe(location1, location2, name));
                    }
                    name = sr.ReadLine();
                }

                MessageBox.Show("Loading is done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
            }
        }
        


    }
}

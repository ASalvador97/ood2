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
        int i = 1; //for the splitter1 class -> "index"
        int checkPump = 0; // for Pump in addPupmpline
        internal List<Component> Components = new List<Component>(); //Create list with all elements
        internal List<Pipe> Pipes = new List<Pipe>(); //Create list with all pipelines
        internal List<Splitter> Splitters = new List<Splitter>(); //Create list with all splitters
        internal List<Merger> Mergers = new List<Merger>(); //Create list with all mergers
        internal List<AdjSplitter> AdjSplitters = new List<AdjSplitter>(); //Create list with all adjustable splitters
        Splitter splitter;
        Merger merger;
        AdjSplitter adjsplit;

        Point tempPoint = new Point(); //for AddPipeline
        bool position = false; //for AddPipeline
        public Network() { }
        public void AddPump(Point p, string name, int capacity) //Add pump in list 
        {
            Component comp = CheckElementTogether(p);
            {
                if (comp != null)
                {
                    MessageBox.Show("Select empty space.");
                }
                else
                {
                    Components.Add(new Pump(p, name, capacity));
                }
            }
        }
        public void AddSink(Point p, string name, int capacity) //Add sink in list 
        {
            Component comp = CheckElementTogether(p);
            {
                if (comp != null)
                {
                    MessageBox.Show("Select empty space.");
                }
                else
                {
                    Components.Add(new Sink(p, name, capacity));
                }
            }
        }
        public void AddMerger(Point p, string name) //Add merger in lists 
        {
            Component comp = CheckElementTogether(p);
            {
                if (comp != null)
                {
                    MessageBox.Show("Select empty space.");
                }
                else
                {
                    merger = new Merger(p, name);
                    Components.Add(merger);
                    Mergers.Add(merger);
                }
            }
        }
        public void AddSplitter(Point p, string name) //Add splitter in lists 
        {
            Component comp = CheckElementTogether(p);
            {
                if (comp != null)
                {
                    MessageBox.Show("Select empty space.");
                }
                else
                {
                    splitter = new Splitter(p, name);
                    Splitters.Add(splitter);
                    Components.Add(splitter);
                }
            }
        }
        public void AddAdjSplitter(Point p, string name, int percup, int percdown) //Add adjustable splitter in lists
        {
            Component comp = CheckElementTogether(p);
            {
                if (comp != null)
                {
                    MessageBox.Show("Select empty space.");
                }
                else
                {
                    adjsplit = new AdjSplitter(p, name, percup, percdown);
                    Components.Add(adjsplit);
                    AdjSplitters.Add(adjsplit);
                }
            }
        }

        public void AddPipe(Point p, string name, int flow) 
        {
            int xx = 0;
            Component comp = CheckComponent(p);
            List<Component> componentlist = new List<Component>();
            componentlist.Add(comp);

            if (comp != null) //here we check is "p" empty space or element
            {
                if (position == false) //if click first time (first point of pipeline)
                {
                    position = true;
                    tempPoint = p;

                    if (comp is Pump) 
                    {
                        checkPump++;
                        foreach (Pump pp in componentlist)
                        {
                            if (pp.Flow < flow)
                            {
                                MessageBox.Show("Flow cannot be more than capacity");
                                checkPump = 0;
                                position = false;
                            }
                        }
                    }             

                    if (comp is Sink) 
                    {
                        int x = CheckPipelineForSink(p).Count();
                        foreach (Sink pp in componentlist)
                        {
                            if (pp.Flow < flow)
                            {
                                MessageBox.Show("Flow cannot be more than capacity");
                                position = false;
                            }
                        }
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
                        MessageBox.Show("Connection cannot be between two Pumps. Please select another element");
                        checkPump--;
                        position = false;
                    }
                    else
                    {
                        if (comp is Pump && checkPump == 0)
                        {
                            foreach (Pump pp in componentlist)
                            {
                                if (pp.Flow < flow)
                                {
                                    MessageBox.Show("Flow cannot be more than capacity");
                                    xx = 1;
                                    position = false;
                                }
                            }
                        }
                        if (!(comp is Pump))
                        {
                            checkPump = 0;
                        }                           
                        if (comp is Sink)            
                        {
                            int x = CheckPipelineForSink(p).Count();
                            foreach (Sink pp in componentlist)
                            {
                                if (pp.Flow < flow)
                                {
                                    MessageBox.Show("Flow cannot be more than capacity");
                                    xx = 1;
                                    position = false;
                                }
                            }
                            if (x > 1)
                            {
                                MessageBox.Show("The sink can not have more then 2 input ");
                                x = 0;
                                xx = 1;
                                position = false;
                            }
                        }                        

                        if (!(xx == 1))
                        {
                            Pipe plf = new Pipe(tempPoint, p, name, flow);
                            Pipes.Add(plf);

                            foreach (Component e in Components.ToList()) 
                            {
                                if ((tempPoint.X + 46 >= e.Location.X && (tempPoint.Y + 46 >= e.Location.Y) && (tempPoint.X - 46 <= e.Location.X) && (tempPoint.Y - 46 <= e.Location.Y) && (e is Splitter) && !(e is AdjSplitter)))
                                {
                                    CheckSplitter(p, name, flow, e, plf);
                                }
                                else if (tempPoint.X + 46 >= e.Location.X && (tempPoint.Y + 46 >= e.Location.Y) && (tempPoint.X - 46 <= e.Location.X) && (tempPoint.Y - 46 <= e.Location.Y) && (e is AdjSplitter))
                                {
                                    CheckAdjustableSplitter(p, name, flow, e, plf);
                                }
                                else if ((p.X + 46 >= e.Location.X && (p.Y + 46 >= e.Location.Y) && (p.X - 46 <= e.Location.X) && (p.Y - 46 <= e.Location.Y) && (e is Splitter) && !(e is AdjSplitter)))
                                {
                                    splitter.listPipeInputSplitter.Add(plf);
                                    CheckSplitter1(p, name, flow, e, plf);
                                }
                                else if (p.X + 46 >= e.Location.X && (p.Y + 46 >= e.Location.Y) && (p.X - 46 <= e.Location.X) && (p.Y - 46 <= e.Location.Y) && (e is AdjSplitter))
                                {
                                    adjsplit.listPipeInputSplitter.Add(plf);
                                    CheckAdjustableSplitter1(p, name, flow, e, plf);
                                }
                                if (((p.X + 46 >= e.Location.X && (p.Y + 46 >= e.Location.Y) && (p.Y - 46 <= e.Location.Y) && (e is Merger))))
                                {
                                    CheckMerger(p, name, flow, e, plf);
                                }
                                else if (((tempPoint.X + 46 >= e.Location.X && (tempPoint.Y + 46 >= e.Location.Y) && (tempPoint.Y - 46 <= e.Location.Y) && (e is Merger))))
                                {
                                    merger.listPipelineOutputMerger.Add(plf);
                                    CheckMerger1(p, name, flow, e, plf);
                                }
                                position = false;
                            }
                        }
                    }
                }
            }
        }

        internal List<Pipe> CheckPipelineForSink(Point p)
        {
            List<Pipe> temp;
            temp = new List<Pipe>();
            foreach (Pipe pl in Pipes)
            {
                if (pl.ContainsForSink(p) == true)
                {
                    temp.Add(pl);
                }
            }
            return temp;
        }

        //check splitter 
        internal void CheckSplitter(Point p, string name, int flow, Component e, Pipe plf) //calculation for splitter
        {
            foreach (Splitter spl in Splitters)
            {
                if (spl.Location == e.Location)
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
                splitter.listPipeOutputSplitter[0].Flow = flow;
                foreach (Pipe pl in splitter.listPipeOutputSplitter)
                {
                    if ((pl.FirstPoint.X) + 46 >= e.Location.X && ((pl.FirstPoint.Y) + 46 >= e.Location.Y && (pl.FirstPoint.X) - 46 <= e.Location.X) && ((pl.FirstPoint.X) + 46 >= e.Location.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Location;
                    }
                }

                foreach (Pipe pl in Pipes)
                {
                    if ((pl.FirstPoint.X) + 46 >= e.Location.X && ((pl.FirstPoint.Y) + 46 >= e.Location.Y && (pl.FirstPoint.X) - 46 <= e.Location.X) && ((pl.FirstPoint.X) + 46 >= e.Location.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Location;
                    }
                }
            }
            else if (splitter.listPipeOutputSplitter.Count == 2)
            {
                splitter.listPipeOutputSplitter[0].Flow = flow / 2;
                splitter.listPipeOutputSplitter[1].Flow = flow / 2;
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

        //check splitter1 
        internal void CheckSplitter1(Point p, string name, int flow, Component e, Pipe plf)
        {
            foreach (Splitter spl in Splitters)
            {
                if (spl.Location == e.Location)
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
                splitter.listPipeOutputSplitter[0].Flow = flow;
                foreach (Pipe pl in splitter.listPipeOutputSplitter)
                {
                    if ((pl.FirstPoint.X) + 46 >= e.Location.X && ((pl.FirstPoint.Y) + 46 >= e.Location.Y && (pl.FirstPoint.X) - 46 <= e.Location.X) && ((pl.FirstPoint.X) + 46 >= e.Location.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Location;
                    }
                }

                foreach (Pipe pl in Pipes)
                {
                    if ((pl.FirstPoint.X) + 46 >= e.Location.X && ((pl.FirstPoint.Y) + 46 >= e.Location.Y && (pl.FirstPoint.X) - 46 <= e.Location.X) && ((pl.FirstPoint.X) + 46 >= e.Location.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Location;
                    }
                }
            }
            else if (splitter.listPipeOutputSplitter.Count == 2)
            {
                splitter.listPipeOutputSplitter[0].Flow = flow / 2;
                splitter.listPipeOutputSplitter[1].Flow = flow / 2;
            }
        }



        //check adjustable splitter 
        internal void CheckAdjustableSplitter(Point p, string name, int flow, Component e, Pipe plf) //calculation for adjustable splitter
        {
            foreach (AdjSplitter spl in AdjSplitters)
            {
                if (spl.Location == e.Location)
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
                adjsplit.listPipeOutputSplitter[0].Flow = flow;
                foreach (Pipe pl in adjsplit.listPipeOutputSplitter)
                {
                    if ((pl.FirstPoint.X) + 46 >= e.Location.X && ((pl.FirstPoint.Y) + 46 >= e.Location.Y && (pl.FirstPoint.X) - 46 <= e.Location.X) && ((pl.FirstPoint.X) + 46 >= e.Location.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Location;
                    }
                }

                foreach (Pipe pl in Pipes)
                {
                    if ((pl.FirstPoint.X) + 46 >= e.Location.X && ((pl.FirstPoint.Y) + 46 >= e.Location.Y && (pl.FirstPoint.X) - 46 <= e.Location.X) && ((pl.FirstPoint.X) + 46 >= e.Location.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Location;
                    }
                }
            }
            else if (adjsplit.listPipeOutputSplitter.Count == 2)
            {
                adjsplit.listPipeOutputSplitter[0].Flow = Convert.ToInt32(flow * adjsplit.upValue / 100);
                adjsplit.listPipeOutputSplitter[1].Flow = Convert.ToInt32(flow * adjsplit.downValue / 100);
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

        //check adjsplitter1 
        internal void CheckAdjustableSplitter1(Point p, string name, int flow, Component e, Pipe plf)
        {
            foreach (AdjSplitter spl in AdjSplitters)
            {
                if (spl.Location == e.Location)
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
                splitter.listPipeOutputSplitter[0].Flow = flow;
                foreach (Pipe pl in adjsplit.listPipeOutputSplitter)
                {
                    if ((pl.FirstPoint.X) + 46 >= e.Location.X && ((pl.FirstPoint.Y) + 46 >= e.Location.Y && (pl.FirstPoint.X) - 46 <= e.Location.X) && ((pl.FirstPoint.X) + 46 >= e.Location.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Location;
                    }
                }

                foreach (Pipe pl in Pipes)
                {
                    if ((pl.FirstPoint.X) + 46 >= e.Location.X && ((pl.FirstPoint.Y) + 46 >= e.Location.Y && (pl.FirstPoint.X) - 46 <= e.Location.X) && ((pl.FirstPoint.X) + 46 >= e.Location.X) && (e is Splitter))
                    {
                        pl.FirstPoint = e.Location;
                    }
                }
            }
            else if (adjsplit.listPipeOutputSplitter.Count == 2)
            {
                adjsplit.listPipeOutputSplitter[0].Flow = Convert.ToInt32(flow * adjsplit.upValue / 100);
                adjsplit.listPipeOutputSplitter[1].Flow = Convert.ToInt32(flow * adjsplit.downValue / 100);
            }
        }

        
        internal void CheckMerger(Point p, string name, int flow, Component e, Pipe plf)  //calculation for merger
        {
            foreach (Merger merg in Mergers)
            {
                if (merg.Location == e.Location)
                {
                    merger = merg;
                }
            }

            if ((tempPoint.X + 46 >= e.Location.X && (tempPoint.Y + 46 >= e.Location.Y) && ((tempPoint.X - 46 <= e.Location.X) && (tempPoint.Y - 46 <= e.Location.Y)) && (e is Merger)))
            {
                merger.listPipelineOutputMerger.Add(plf);
            }
            if ((p.X + 46 >= e.Location.X && (p.Y + 46 >= e.Location.Y) && ((p.X - 46 <= e.Location.X) && (p.Y - 46 <= e.Location.Y)) && (e is Merger)))
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
                merger.listPipelineInputMerger[0].Flow = flow;

                foreach (Pipe pl in merger.listPipelineInputMerger)
                {
                    if ((pl.SecondPoint.X) + 46 >= e.Location.X && ((pl.SecondPoint.Y) + 46 >= e.Location.Y && (pl.SecondPoint.X) - 46 <= e.Location.X) && ((pl.SecondPoint.X) + 46 >= e.Location.X) && (e is Merger))
                    {
                        pl.SecondPoint = new Point(e.Location.X + 18, e.Location.Y + 18);
                    }
                }
                foreach (Pipe pl in Pipes)
                {
                    if ((pl.SecondPoint.X) + 46 >= e.Location.X && ((pl.SecondPoint.Y) + 46 >= e.Location.Y && (pl.SecondPoint.X) - 46 <= e.Location.X) && ((pl.SecondPoint.X) + 46 >= e.Location.X) && (e is Merger))
                    {
                        pl.SecondPoint = new Point(e.Location.X + 18, e.Location.Y + 18);
                    }
                }
            }
            else if (merger.listPipelineInputMerger.Count == 2 && merger.listPipelineOutputMerger.Count == 1)
            {
                int final = 0;
                foreach (Pipe pl in merger.listPipelineInputMerger)
                {
                    final += pl.Flow;
                }
                merger.listPipelineOutputMerger[0].Flow = final;
            }
        }

       
        internal void CheckMerger1(Point p, string name, int flow, Component e, Pipe plf)
        {
            if (merger.listPipelineInputMerger.Count == 2 && merger.listPipelineOutputMerger.Count == 1)
            {
                int final = 0;
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

        internal Component CheckElementTogether(Point p) // only for adding elements (checking if this place has element or not) 
        {
            foreach (Component e in Components)
            {
                if (e.ContainsTogether(p) == true)
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
        public void Painting(Graphics graphic) //Draw all elements and pipelines 
        {
            foreach (Pipe p in Pipes)
            {
                p.Draw(graphic);
            }
            foreach (Component e in Components)
            {
                e.Draw(graphic);
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
        public void EditElement(Point p, int cap, int newUpper, int newLower) //Changing capacity of Pump or Sink and also changing precantage of the AdjSplitter 
        {
            Component comp = CheckComponent(p);
            Pipe pl = CheckPipeline(p);

            if (comp != null)
            {
                if (comp is Pump)
                {
                    var obj = (Pump)comp;
                    obj.Flow = cap;
                }
                if (comp is Sink)
                {
                    var obj = (Sink)comp;
                    obj.Flow = cap;
                }
                if (comp is AdjSplitter)
                {
                    var obj = (AdjSplitter)comp;
                    obj.upValue = newUpper;
                    obj.downValue = newLower;
              
                }
            }
        }
        public void newFile() 
        {
            Components.Clear(); //Clear list with all elements
            Pipes.Clear(); //Clear list with all pipelines
        }
        public void saveFile() 
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
                        sw.WriteLine(((Component)x).Location.X);
                        sw.WriteLine(((Component)x).Location.Y);

                        Pump temp = (Pump)x;
                        sw.WriteLine(temp.Flow);
                    }
                    if (x.Name == "Sink")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Location.X);
                        sw.WriteLine(((Component)x).Location.Y);

                        Sink temp = (Sink)x;
                        sw.WriteLine(temp.Flow);
                    }
                    if (x.Name == "Merger")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Location.X);
                        sw.WriteLine(((Component)x).Location.Y);
                    }
                    if (x.Name == "Splitter")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Location.X);
                        sw.WriteLine(((Component)x).Location.Y);
                    }
                    if (x.Name == "Adjustable Splitter")
                    {
                        sw.WriteLine(x.Name);
                        sw.WriteLine(((Component)x).Location.X);
                        sw.WriteLine(((Component)x).Location.Y);

                        AdjSplitter temp = (AdjSplitter)x;
                        sw.WriteLine(temp.upValue);
                        sw.WriteLine(temp.downValue);
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
        public void loadFile() //Load project from a file that was selected
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
                int percdown = 0;

                while (name != "*")
                {
                    if (name == "Pump")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        capacity = Convert.ToInt32(sr.ReadLine());
                        AddPump(location, name, capacity);
                    }
                    if (name == "Sink")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        capacity = Convert.ToInt32(sr.ReadLine());
                        AddSink(location, name, capacity);
                    }
                    if (name == "Merger")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        AddMerger(location, name);
                    }
                    if (name == "Splitter")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        AddSplitter(location, name);
                    }
                    if (name == "Adjustable Splitter")
                    {
                        location = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        percup = Convert.ToInt32(sr.ReadLine());
                        percdown = Convert.ToInt32(sr.ReadLine());
                        AddAdjSplitter(location, name, percup, percdown);
                    }
                    if (name == "Pipeline")
                    {
                        location1 = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        location2 = new Point(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                        flow = Convert.ToInt32(sr.ReadLine());
                        Pipes.Add(new Pipe(location1, location2, name, flow));
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

        //public List<Component> Components { get; set; }

        //public List<Pipe> Pipes { get; set; }

        //private Component[] TempComponents;

        //public Network()
        //{
        //    Components = new List<Component>();
        //    Pipes = new List<Pipe>();
        //}


        //public void AddComponent(Component component, Form1 form)
        //{
        //    if (CheckAddingComponents(component))
        //    {

        //        component.Draw(component.Position, form);


        //        Components.Add(component);
        //    }
        //}

        //public void AddPipe(Pipe pipe, PaintEventArgs e, Form form)
        //{
        //    ConnectPipe(pipe, e, form);
        //    Pipes.Add(pipe);

        //}

        //private void ConnectPipe(Pipe pipe, PaintEventArgs e, Form form)
        //{
        //    TempComponents = new Component[2];
        //    foreach (Component c in Components)
        //    {
        //        if(pipe.Points[0] == c.Position)
        //        {
        //            if(!(c is Sink))
        //            {
        //                c.AddOuterPipe(pipe,form);
        //                pipe = c.UpdatePipe(pipe,form);

        //                TempComponents[0] = c;
        //            }

        //            //if(c is Pump)
        //            //{

        //            //    ((Pump)c).AddPipe(pipe);
        //            //    pipe = c.UpdatePipe(pipe);
        //            //}
        //            //else if(c is Merger)
        //            //{

        //            //}
        //            //else if(c is Splitter)
        //            //{

        //            //}
        //            //else if(c is AdjSplitter)
        //            //{

        //            //}
        //        }
        //        if (pipe.Points.Count > 0)
        //        {
        //            if (pipe.Points[pipe.Points.Count - 1] == c.Position)
        //            {
        //                if (c is Splitter)
        //                {
        //                    TempComponents[1] = c;
        //                }
        //                else if (!(c is Pump))
        //                {
        //                    c.AddInnerPipe(pipe,form);
        //                    TempComponents[1] = c;
        //                }

        //                //if (c is Sink)
        //                //{
        //                //    ((Sink)c).AddPipe(pipe);
        //                //}
        //                //else if (c is Merger)
        //                //{

        //                //}
        //                //else if (c is Splitter)
        //                //{

        //                //}
        //                //else if (c is AdjSplitter)
        //                //{

        //                //}
        //            }
        //        }
        //    }

        //    this.DrawOnForm(pipe, e, form);
        //    foreach (object c in form.Controls)
        //    {
        //        if(c is Label)
        //        ((Label)c).Text="Flow "+GetComponent(((Label)c).Name).Flow;
        //    }
        //}


        //public void DrawOnForm(Pipe pipe, PaintEventArgs e, Form form)
        //{
        //    // use the graphics class here to draw the pipes
        //    Graphics g = e.Graphics;
        //    Pen p = new Pen(Brushes.Green, 3);
        //    for (int i = 0; i < pipe.Points.Count; i++)
        //    {
        //        //this part is if the user clicked on the components only
        //        if (pipe.Points.Count == 2)
        //        {
        //            if (TempComponents[0] is Splitter || TempComponents[0] is AdjSplitter)
        //            {
        //                if (((Splitter)TempComponents[0]).IsUpperOutPipeNull())
        //                {
        //                    //this is to align the first pipeline at the top of the picture box
        //                    int lr, td;
        //                    lr = pipe.Points[0].X + 45;
        //                    td = pipe.Points[0].Y + 5;
        //                    Point pos = new Point(lr, td);
        //                    // end of alignment

        //                    if (TempComponents[1] is Merger)
        //                    {
        //                        if (((Merger)TempComponents[1]).IsLowerInPipeNull())
        //                        {
        //                            //this is for the positioning of the pipeline to the middle of the last component
        //                            td = pipe.Points[pipe.Points.Count - 1].Y + 5;
        //                            Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                            //end of alignment
        //                            g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                            Label label = new Label();
        //                            label.Name = TempComponents[0].pb.Name;
        //                            label.Text = "Flow " + pipe.Flow;
        //                            label.BackColor = Color.Transparent;
        //                            int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                            int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                            label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                            form.Controls.Add(label);
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            //this is for the positioning of the pipeline to the middle of the last component
        //                            td = pipe.Points[pipe.Points.Count - 1].Y + (TempComponents[1].pb.Size.Height - 5);
        //                            Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                            //end of alignment
        //                            g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                            Label label = new Label();
        //                            label.Text = "Flow " + pipe.Flow;
        //                            label.Name = TempComponents[0].pb.Name;
        //                            label.BackColor = Color.Transparent;
        //                            int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                            int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                            label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                            form.Controls.Add(label);
        //                            break;
        //                        }
        //                    }
        //                    else if (!(TempComponents[1] is Pump))
        //                    {
        //                        //this is for the positioning of the pipeline to the middle of the last component
        //                        td = pipe.Points[pipe.Points.Count - 1].Y + (50 / 2);
        //                        Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                        //end of alignment
        //                        g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                        Label label = new Label();
        //                        label.Text = "Flow " + pipe.Flow;
        //                        label.Name = TempComponents[0].pb.Name;
        //                        label.BackColor = Color.Transparent;
        //                        int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                        int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                        label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                        form.Controls.Add(label);
        //                        break;
        //                    }
        //                }
        //                else if(!(TempComponents[0] is Sink))
        //                {
        //                    //this is to align the second pipeline to the buttom of the picture box
        //                    int lr, td;
        //                    lr = pipe.Points[0].X + 45;
        //                    td = pipe.Points[0].Y + (TempComponents[0].pb.Size.Height - 5);
        //                    Point pos = new Point(lr, td);
        //                    // end of alignment

        //                    if (TempComponents[1] is Merger)
        //                    {
        //                        if (((Merger)TempComponents[1]).IsLowerInPipeNull())
        //                        {
        //                            //this is for the positioning of the pipeline to the middle of the last component
        //                            td = pipe.Points[pipe.Points.Count - 1].Y + 5;
        //                            Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                            //end of alignment
        //                            g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                            Label label = new Label();
        //                            label.Text = "Flow " + pipe.Flow;
        //                            label.Name = TempComponents[0].pb.Name;
        //                            label.BackColor = Color.Transparent;
        //                            int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                            int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                            label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                            form.Controls.Add(label);
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            //this is for the positioning of the pipeline to the middle of the last component
        //                            td = pipe.Points[pipe.Points.Count - 1].Y + (TempComponents[1].pb.Size.Height - 5);
        //                            Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                            //end of alignment
        //                            g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                            Label label = new Label();
        //                            label.Text = "Flow " + pipe.Flow;
        //                            label.Name = TempComponents[0].pb.Name;
        //                            label.BackColor = Color.Transparent;
        //                            int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                            int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                            label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                            form.Controls.Add(label);
        //                            break;
        //                        }
        //                    }
        //                    else if (!(TempComponents[1] is Pump))
        //                    {
        //                        //this is for the positioning of the pipeline to the middle of the last component
        //                        td = pipe.Points[pipe.Points.Count - 1].Y + (50 / 2);
        //                        Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                        //end of alignment
        //                        g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                        Label label = new Label();
        //                        label.Text = "Flow " + pipe.Flow;
        //                        label.Name = TempComponents[0].pb.Name;
        //                        label.BackColor = Color.Transparent;
        //                        int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                        int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                        label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                        form.Controls.Add(label);
        //                        break;
        //                    }
        //                }
        //            }
        //            else if (!(TempComponents[0] is Sink))
        //            {
        //                //this is to align the line in the middle of the picture box
        //                int lr, td;
        //                lr = pipe.Points[0].X + 45;
        //                td = pipe.Points[0].Y + (50 / 2);
        //                Point pos = new Point(lr, td);
        //                // end of alignment


        //                //Akinan pone if statement pa e lastu component den e list di tempcomponent (dus pa check e merger)
        //                if (TempComponents[1] is Merger)
        //                {
        //                    if (((Merger)TempComponents[1]).IsLowerInPipeNull())
        //                    {
        //                        //this is for the positioning of the pipeline to the middle of the last component
        //                        td = pipe.Points[pipe.Points.Count - 1].Y + 5;
        //                        Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                        //end of alignment
        //                        g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                        Label label = new Label();
        //                        label.Text = "Flow " + pipe.Flow;
        //                        label.Name = TempComponents[0].pb.Name;
        //                        label.BackColor = Color.Transparent;
        //                        int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                        int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                        label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                        form.Controls.Add(label);
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        //this is for the positioning of the pipeline to the middle of the last component
        //                        td = pipe.Points[pipe.Points.Count - 1].Y + (TempComponents[1].pb.Size.Height - 5);
        //                        Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                        //end of alignment
        //                        g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                        Label label = new Label();
        //                        label.Text = "Flow " + pipe.Flow;
        //                        label.Name = TempComponents[0].pb.Name;
        //                        label.BackColor = Color.Transparent;
        //                        int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                        int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                        label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                        form.Controls.Add(label);
        //                        break;
        //                    }
        //                }
        //                else if (!(TempComponents[1] is Pump))
        //                {
        //                    //this is for the positioning of the pipeline to the middle of the last component
        //                    td = pipe.Points[pipe.Points.Count - 1].Y + (50 / 2);
        //                    Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                    //end of alignment
        //                    g.DrawLine(p, form.PointToClient(pos), form.PointToClient(posf));

        //                    Label label = new Label();
        //                    label.Text = "Flow " + pipe.Flow;
        //                    label.Name = TempComponents[0].pb.Name;
        //                    label.BackColor = Color.Transparent;
        //                    int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                    int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                    label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                    form.Controls.Add(label);
        //                    break;
        //                }
        //            }
        //        }
        //        //end of Checking for only clicking on the components in the form

        //        //this part is if the user clicked on multiple areas in the form
        //        if (i == 0)
        //        {
        //            if (TempComponents[0] is Splitter || TempComponents[0] is AdjSplitter)
        //            {
        //                if (((Splitter)TempComponents[0]).IsUpperOutPipeNull())
        //                {
        //                    //this is to align the first pipeline at the top of the picture box
        //                    int lr, td;
        //                    lr = pipe.Points[0].X + 45;
        //                    td = pipe.Points[0].Y + 5;
        //                    Point pos = new Point(lr, td);
        //                    // end of alignment
        //                    g.DrawLine(p, form.PointToClient(pos), form.PointToClient(pipe.Points[i + 1]));
        //                }
        //                else
        //                {
        //                    //this is to align the second pipeline to the buttom of the picture box
        //                    int lr, td;
        //                    lr = pipe.Points[0].X + 45;
        //                    td = pipe.Points[0].Y + (TempComponents[0].pb.Size.Height - 5);
        //                    Point pos = new Point(lr, td);
        //                    // end of alignment
        //                    g.DrawLine(p, form.PointToClient(pos), form.PointToClient(pipe.Points[i + 1]));
        //                }
        //            }
        //            else if (!(TempComponents[0] is Sink))
        //            {
        //                //this is to align the line in the middle of the picture box
        //                int lr, td;
        //                lr = pipe.Points[0].X + 45;
        //                td = pipe.Points[0].Y + (50 / 2);
        //                Point pos = new Point(lr, td);
        //                // end of alignment
        //                g.DrawLine(p, form.PointToClient(pos), form.PointToClient(pipe.Points[i + 1]));
        //            }

        //        }
        //        else if (i < pipe.Points.Count - 2)
        //        {
        //            g.DrawLine(p, form.PointToClient(pipe.Points[i]), form.PointToClient(pipe.Points[i + 1]));
        //        }
        //        else
        //        {
        //            if (TempComponents[1] is Merger)
        //            {
        //                if (((Merger)TempComponents[1]).IsLowerInPipeNull())
        //                {
        //                    //this is for the positioning of the pipeline to the middle of the last component
        //                    int td = pipe.Points[pipe.Points.Count - 1].Y + 5;
        //                    Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                    //end of alignment
        //                    g.DrawLine(p, form.PointToClient(pipe.Points[i]), form.PointToClient(posf));

        //                    Label label = new Label();
        //                    label.Text = "Flow " + pipe.Flow;
        //                    label.Name = TempComponents[0].pb.Name;
        //                    label.BackColor = Color.Transparent;
        //                    int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                    int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                    label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                    form.Controls.Add(label);
        //                    break;
        //                }
        //                else
        //                {
        //                    //this is for the positioning of the pipeline to the middle of the last component
        //                    int td = pipe.Points[pipe.Points.Count - 1].Y + (TempComponents[1].pb.Size.Height - 5);
        //                    Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                    //end of alignment
        //                    g.DrawLine(p, form.PointToClient(pipe.Points[i]), form.PointToClient(posf));

        //                    Label label = new Label();
        //                    label.Text = "Flow " + pipe.Flow;
        //                    label.Name = TempComponents[0].pb.Name;
        //                    label.BackColor = Color.Transparent;
        //                    int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                    int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                    label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                    form.Controls.Add(label);
        //                    break;
        //                }
        //            }
        //            else if (!(TempComponents[1] is Pump))
        //            {
        //                //this is for the positioning of the pipeline to the middle of the last component
        //                int td = pipe.Points[pipe.Points.Count - 1].Y + (50 / 2);
        //                Point posf = new Point(pipe.Points[pipe.Points.Count - 1].X, td);
        //                //end of alignment
        //                g.DrawLine(p, form.PointToClient(pipe.Points[i]), form.PointToClient(posf));

        //                Label label = new Label();
        //                label.Text = "Flow " + pipe.Flow;
        //                label.Name = TempComponents[0].pb.Name;
        //                label.BackColor = Color.Transparent;
        //                int x = (pipe.Points[1].X - pipe.Points[0].X) / 2;
        //                int y = (pipe.Points[1].Y - pipe.Points[0].Y) / 2;

        //                label.Location = form.PointToClient(new Point(pipe.Points[0].X + x, pipe.Points[0].Y + y));
        //                form.Controls.Add(label);
        //                break;
        //            }

        //        }
        //        //end if the user clicked on multiple areas in the form
        //    }
        //}


        ////Checks if the point you clicked is valid and not next to a component
        //private bool CheckAddingComponents(Component comp)
        //{
        //    if (Components.Count > 0)
        //    {
        //        foreach (Component c in Components)
        //        {
        //            if(comp.Position.X >=(c.Position.X-63)&& comp.Position.Y >= (c.Position.Y - 63)&& comp.Position.X <= (c.Position.X + 63) && comp.Position.Y <= (c.Position.Y + 63))
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    return true;

        //}

        //public Component GetComponent(string type)
        //{
        //    foreach(Component c in Components)
        //    {
        //        if (c.pb.Name == type)
        //        {
        //            return c;
        //        }
        //    }
        //    return null;
        //}


    }
}

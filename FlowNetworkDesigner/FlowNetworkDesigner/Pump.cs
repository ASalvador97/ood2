﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowNetworkDesigner 
{
     public class Pump:Component
     {
         //variables
         private Pipe OuterPipe;
        private int CurrentFlow;
         
         
         //constructor       
         public Pump(Point p, object sender,int currentflow)
             :base(p,sender)
        {
            // TODO initiate pipe?
            //image
            id += 1;
            OuterPipe = null;
            CurrentFlow = currentflow;
            pb.Name = "pipe" + id;
            pb.BackColor = Color.Red;
        }

         //methods
         public override void Draw(Point position, Form1 form)
         {

            
            base.Draw(position,form);
            
         }

        public override void AddPipe(Pipe pipe)
        {
            OuterPipe = pipe;
        }

         public override void UpdatePipe()
         {
             
         }
     }
}

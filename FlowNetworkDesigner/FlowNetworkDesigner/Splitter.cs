﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;

namespace FlowNetworkDesigner
{
    class Splitter :Component
    {
        //variables        
        private Pipe UpperOutPipe;

        private Pipe LowerOutPipe;

        private Pipe InnerPipe;


        //constructor
        public Splitter(Point p) 
            :base(p) 
        {
            // TODO initate pipes?
        }

        //methods 
        public override void Draw()
        {
           
        }

        public override void UpdatePipe()
        {
           
        }

        public void SplitFlow()
        {
            double inflow = InnerPipe.Flow;
            UpperOutPipe.Flow = inflow / 2;
            LowerOutPipe.Flow = inflow / 2;
        }
    }
}
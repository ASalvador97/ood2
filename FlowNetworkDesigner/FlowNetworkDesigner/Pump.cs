using System;
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
         
         
         
         //constructor       
         public Pump(Point p)
             :base(p)
        {
           // TODO initiate pipe?
           //image

            
        }

         //methods
         public override void Draw()
         {
            
         }

         public override void UpdatePipe()
         {
             
         }
     }
}

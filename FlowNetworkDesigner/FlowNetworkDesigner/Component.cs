using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowNetworkDesigner
{
    public abstract class  Component
    {
        //properties

        //contains the position of the conponent on the canvas
        public Point Position { get; set; }

        // TODO image placeholder?
        // TODO put pipes here

        //constructor
        public Component(Point p)
        {
            Position = p;
        }
        
           
        


        //methods
        public virtual void Draw() // TODO: needs a point as a parameter?
        {
            
        } 




        public virtual void  UpdatePipe() // TODO: needs values as parameters?
        {
            
        } 

        public virtual void AddPipe()



    }
}

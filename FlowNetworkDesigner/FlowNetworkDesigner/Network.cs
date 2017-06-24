using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowNetworkDesigner
{
    class Network
    {
        private List<Component> Components;

        private List<Pipe> Pipes;


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

        public bool CheckAddingComponents(Component comp)
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
    }
}

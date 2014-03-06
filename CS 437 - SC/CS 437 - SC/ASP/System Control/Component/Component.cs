using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASP
{
    namespace General
    {
        public class Component
        {
            private int component_id;
            private bool functional;
            private List<Component> componentDependencies = new List<Component>();

            public int Component_id
            {
                get { return component_id; }
            }
            public bool Functional
            {
                get {
                    if (functional && componentDependencies == null)
                    {
                        return true;
                    }
                    else if (functional && functionDependencies())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                set { functional = value; }
            }

            private bool functionDependencies()
            {
                foreach(Component comp in componentDependencies)
                    if (!comp.functional) 
                        return false;
                return true;

            
            }

            public bool AddDependency(Component component)
            {
                if(component.componentDependencies!=null)
                    foreach(Component comp in component.componentDependencies)
                        if (comp==this || component == this)
                            return false;
                componentDependencies.Add(component);
                return true;


            }

            public List<Component> ComponentDependencies
            {
                get { return componentDependencies; }
            }

            public Component(int component_id, bool functional = true, List<Component> children = null)
            {
                this.component_id = component_id;
                this.Functional = functional;
                if(children!=null)
                    this.componentDependencies = children;

            }

            public void Status()
            {
                Console.WriteLine("COMPONENT " + component_id + " " + (Functional ? "is functioning." : "is broken."));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASP
{
    namespace SystemControl
    {
        public class Component
        {
            private int component_id;
            private bool functional;
            private List<Component> componentDependencies;

            public int Component_id
            {
                get { return component_id; }
            }
            public bool Functional
            {
                get {
                    if (functional && componentDependencies == null)
                        return true;
                    else if(functional && functionDependencies())
                        return true;
                    else 
                        return false;
                }
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
                foreach(Component comp in component.componentDependencies)
                    if (comp==this)
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
                this.functional = functional;
                this.componentDependencies = children;
            }
        }
    }
}
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
            int component_id;
            bool functional;
            List<Component> componentDependencies;

            public Component(int component_id, bool functional = true, List<Component> children = null)
            {
                this.component_id = component_id;
                this.functional = functional;
                this.componentDependencies = children;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;


namespace ASP
{
    namespace General
    {
        public class ComponentManager
        {
            private List<Component> components = new List<Component>();

            public void AddComponent(Component component)
            {

                components.Add(component);
            }

            public void AddComponent()
            {
                AddComponent(new Component(components.Count));
            }
            public void AddComponent(bool working)
            {
                AddComponent(new Component(components.Count, working));
            }

            public void AddDependency(int id, int dependency_id)
            {
                components[id].AddDependency(components[dependency_id]);
            }

            public List<Component> ListComponent() // Should go to report
            {
                return components;
            }

            public void RemoveComponent(int id)
            {
                
                components.RemoveAt(id);
            }



        }
    }
}
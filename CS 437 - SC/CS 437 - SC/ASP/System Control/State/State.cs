using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP
{
    namespace SystemControl
    {
        namespace States
        {
            public abstract class State
            {
                public abstract void Cycle();
                public abstract void Start();
                public abstract void Stop();
                public abstract bool Ready();

            }
        }
    }
}
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
            public class Obstruction : State
            {
                override public void Cycle()
                {
                    //
                }

                override public void Start()
                {

                }

                override public void Stop()
                {

                }

                override public bool Ready()
                {
                    return false;
                }

        }
        }
    }
}
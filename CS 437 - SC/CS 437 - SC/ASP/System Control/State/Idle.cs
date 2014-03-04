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
            public class Idle : State
            {
                
                int waitTime = 5;

                DateTime startPoint;
                bool locked;
                int timeWaited = 0;
                override public void Cycle()
                {

                    TimeSpan temp = Driver.locationTime.Time - startPoint;
                    timeWaited = temp.Seconds;
                    if (locked)
                    {

                    }
                    else
                        if (timeWaited > waitTime)
                        {
                            Console.WriteLine("DRIVER:IDLE -> Idle cycle complete. ");
                            Stop();
                        }
                }

                override public void Start()
                {

                    Console.WriteLine("DRIVER:IDLE -> MNM  Idle");
                    startPoint = Driver.locationTime.Time;
                    timeWaited = 0;
                }
                public void Start(int time)
                {

                    waitTime = time;
                    Console.WriteLine("DRIVER:IDLE -> MNM  Idle");
                    startPoint = Driver.locationTime.Time;
                    timeWaited = 0;
                }

                public void Start(bool val)
                {
                    if (val)
                    {
                        Console.WriteLine("DRIVER:IDLE-> MNM  Idle");
                        locked = true;
                    }
                    else
                        Start();
                   
                }

                override public void Stop()
                {
                    locked = false;
                    Driver.CheckState();
                }

                override public bool Ready()
                {
                    if (timeWaited > waitTime)
                    {
                        return false;
                    }
                    return true;
                }

        }
        }
    }
}
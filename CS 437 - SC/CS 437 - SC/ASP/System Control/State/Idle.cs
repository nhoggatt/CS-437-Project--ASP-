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
                bool ready = true;
                int timeWaited = 0;
                override public void Cycle()
                {

                    TimeSpan temp = Driver.locationTime.Time - startPoint;
                    timeWaited = temp.Seconds;

                    if (timeWaited > waitTime)
                    {
                        Console.WriteLine("DRIVER:IDLE -> Idle cycle complete. ");
                        Driver.storedObstructon.DoneExploring();
                        Stop();
                    }
                }

                override public void Start()
                {


                    Start(5);
                }
                public void Start(int time)
                {

                    waitTime = time;
                    Console.WriteLine("DRIVER:IDLE -> MNM  Idle for "+time+" seconds.");
                    startPoint = Driver.locationTime.Time;
                    ready = false;
                    timeWaited = 0;
                }

                override public void Stop()
                {
                    Driver.RemoveForcedState();
                    ready = true;
                }

                override public bool Ready()
                {
                    if (!ready)
                    {
                        return false;
                    }
                    return true;
                }

        }
        }
    }
}
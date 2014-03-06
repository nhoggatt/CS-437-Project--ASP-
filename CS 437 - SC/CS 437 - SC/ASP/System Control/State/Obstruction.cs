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
                private bool foundObstruction;
                private bool canExplore;
                
                override public void Cycle()
                {
                    if (new Random().Next(500) == 1) // To simulate the time it may take to analyze an obstruction.
                    {
                        Console.WriteLine("DRIVER:OBSTRUCTION Examination complete. Sending report...");
                        Driver.report.reports["obstruction"]();
                        Stop();
                    }


                }

                override public void Start()
                {
                    Console.WriteLine("DRIVER:OBSTRUCTION -> MNM Path around obstruction.");
                    Console.WriteLine("DRIVER:OBSTRUCTION -> ER Examine Obstruction.");

                }

                override public void Stop(){
                
                    foundObstruction = false;
                    canExplore = false;
                    Driver.RemoveForcedState();

                }

                public void FoundObstruction()
                {
                    Console.WriteLine("DRIVER:OBSTRUCTION Obstruction detected. Idling...");
                    foundObstruction = true;
                    Driver.ForceIdleState();
                }
                public void StartExploring()
                {
                    canExplore = true;
                }
                // Neil obstruction is based off of parser alone. Finish that, and then mapping.
                public void DoneExploring()
                {
                   foundObstruction = false;

                }

                override public bool Ready()
                {
                    return canExplore||foundObstruction;
                }

        }
        }
    }
}
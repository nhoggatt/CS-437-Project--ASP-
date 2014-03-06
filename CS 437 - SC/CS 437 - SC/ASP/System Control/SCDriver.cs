using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;
using ASP.Report;
using ASP.SystemControl.States;


namespace ASP
{
    namespace SystemControl
    {
        class Driver
        {
            //Faking MNM + other hardware
            public static ASP.CurrentLocationTime locationTime = new ASP.CurrentLocationTime(0, 0, 0);
            public static Scheduler scheduler;
            public static ReportManager report;

            private enum StateType { Idle, Standard, Obstruction, Detection, None };
            private static StateType currentStateType;
            private static StateType nextStateType;
            private static StateType forcedState = StateType.None;

            public static Standard storedStandard;
            public static Detection storedDetection;
            public static Obstruction storedObstructon;
            public static Idle storedIdle;

            public static State currentState;

            private static Queue<string> unexecutedCommands = new Queue<string>();


            public static WaypointManager storedWaypoints = new WaypointManager();
            public static ComponentManager storedComponents = new ComponentManager();


            public Driver()
            {

                //TestData
                storedComponents.AddComponent();
                storedComponents.AddComponent();
                storedComponents.AddComponent();
                storedComponents.AddComponent(false);
                storedComponents.AddComponent();
                storedComponents.AddComponent();
                storedComponents.AddDependency(5, 0);
                storedComponents.AddDependency(4, 3);
                
                //End TestData

                nextStateType = StateType.Standard;
                storedStandard = new Standard();
                storedObstructon = new Obstruction();
                storedDetection = new Detection();
                storedIdle = new Idle();
                scheduler = new Scheduler();


                report = new ReportManager();


                Thread behaviorLoop = new Thread(new ThreadStart(BehaviorLoop));
                behaviorLoop.Start();
                
                
                parser(null);
            }

            static void parser(string[] args)
            {
                //Test Data
                Driver.QueueCommand("nav waypoint add 0 0 0 1");
                Driver.QueueCommand("nav waypoint add 3 0 4 2");
                Driver.QueueCommand("nav waypoint add 2 1 1 1");
                //End Test Data

                while (true)
                {
                    int i = 0;
                    while (unexecutedCommands.Count() > 0)
                    {
                        try
                        {
                            Console.WriteLine();
                            string command = unexecutedCommands.Dequeue();
                            Parser.InterpretCommand(command);
                        }
                        catch (ParserException e)
                        {
                            //Do stuff with exception
                            Console.Error.WriteLine("Error " + (++i) + "! An exception has occurred in the Parser.");
                            Console.Error.WriteLine("Command Name: " + e.CommandName);
                            Console.Error.WriteLine("Error Description: " + e.Description);
                            Console.Error.WriteLine();
                        };
                    }

                    string temp = Console.ReadLine();
                    QueueCommand(temp);



                }
            }

            public static void QueueCommand(string command)
            {
                unexecutedCommands.Enqueue(command);
            }


            public static void BehaviorLoop() //something related behavior
            {
                while (true)
                {
                    if (forcedState != StateType.None && forcedState != currentStateType)
                        CheckState();

                    if (currentStateType != nextStateType)
                        switchState(nextStateType);
                    else
                    {
                        if (currentState != null)
                            currentState.Cycle();

                    }
                }

            }

            private static State GetStateFromStateType(StateType type)
            {
                if (type == StateType.Detection)
                {
                    return storedDetection;
                }
                else if (type == StateType.Obstruction)
                {
                    return storedObstructon;
                }
                else if (type == StateType.Standard)
                {
                    return storedStandard;
                }
                else
                {
                    return storedIdle;
                }

            }

            public static void CheckState()
            {
                bool forced = false;
                if (forcedState != StateType.None)
                {

                    if (GetStateFromStateType(forcedState).Ready())
                    {
                        Console.WriteLine("DRIVER Forced State");
                        nextStateType = forcedState;
                        forced = true;
                    }
                    else
                    {
                        Console.WriteLine("DRIVER Forced State failed.");
                        forcedState = StateType.None;
                    }
                }

               if(!forced)
                {

                    if (storedDetection.Ready() && !storedObstructon.Ready())
                    {
                        Console.WriteLine("DRIVER Detection Mode Ready");
                        nextStateType = StateType.Detection;
                    }
                    else if (storedObstructon.Ready() && !storedDetection.Ready())
                    {
                        Console.WriteLine("DRIVER Obstruction Mode Ready");
                        nextStateType = StateType.Obstruction;
                    }
                    else if (storedObstructon.Ready() && storedDetection.Ready())
                    {
                        Console.WriteLine("DRIVER -> IC Detection/Obstruction Conflict.");
                        ForceIdleState();
                    }
                    else
                    {
                        Console.WriteLine("DRIVER Default to Standard Mode");
                        nextStateType = StateType.Standard;
                    }
                }

            }



            public static void ForceObstructionState()
            {
                forcedState = StateType.Obstruction;
            }

            public static void ForceIdleState()
            {
                forcedState = StateType.Idle;
            }


            public static void RemoveForcedState()
            {
                forcedState = StateType.None;
                CheckState();
            }

            private static void switchState(StateType nextStateType)
            {
                if (nextStateType != currentStateType)
                {
                    if (currentState != null)
                        currentState.Stop();

                    if (nextStateType == StateType.Detection)
                    {
                        currentState = storedDetection;
                        currentState.Start();
                        currentStateType = StateType.Detection;
                    }
                    else if (nextStateType == StateType.Obstruction)
                    {
                        currentState = storedObstructon;
                        currentState.Start();
                        currentStateType = StateType.Obstruction;
                    }
                    else if (nextStateType == StateType.Standard)
                    {
                        currentState = storedStandard;
                        currentState.Start();
                        currentStateType = StateType.Standard;
                    }
                    else
                    {
                        currentState = storedIdle;
                        if (forcedState == StateType.Idle)
                            storedIdle.Start(10);
                        else
                            storedIdle.Start();

                        currentStateType = StateType.Idle;
                    }


                    Console.WriteLine("DRIVER New StateType: " + System.Enum.GetName(typeof(StateType), currentStateType));

                }

            }

        }
    }
}


public class MainClass
{

    public static void Main(string[] args)
    {

        ASP.SystemControl.Driver driver = new ASP.SystemControl.Driver();

    }
}

/* Define behavior loop here
 * 
 * [Standard Mode]
 * 1) Recieves Data from other modules
 *  a) Module information will be raw
 *  b) IC information will follow a format
 * 2) Interpret module information
 *  a) use MNM module information first for navigation
 *  b) If error/malfunction occurs that obviously shows that something is broken, prepare to send error report
 * 3) [Short Report] send locational data + time to IC to transmit 
 *      float float float long
 *     OR
 *    [Long Report]
 *      i) Recieve information from environment module
 *      ii) create a report using the information gathered
 *      iii) geographical data + time to IC to transmit  
 *      iv) send to IC for transmission
 *     [Maintenance Report] 
 *     i) Compile status of modules (if functioning)
 *     ii) Power levels
 *     iii) [Long Report]
 *     iv) Send to IC for transmission 
 */
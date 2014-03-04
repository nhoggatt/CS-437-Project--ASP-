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

            private enum StateType {Idle, Standard, Obstruction, Detection, None};
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

            public static Dictionary<int,Component> components = new Dictionary<int,Component>()
            {
            {0, new Component(0)}, //ER
            {1, new Component(1)}, //IC
            {2, new Component(2, false)}, //MNM
            {3, new Component(3, false)}, // Temp Sensor
            {4, new Component(4)} // Motor 
            };

            public Driver()
            {
                
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
                QueueCommand("nav waypoint add 0 0 0 1");
                QueueCommand("nav waypoint add 3 0 4 2");
                QueueCommand("nav waypoint add 2 1 1 1");

                while (true)
                {
                    string temp = Console.ReadLine();
                    QueueCommand(temp);


                    int i = 0;


                    while (unexecutedCommands.Count() > 0)
                    {
                        try
                        {
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
                    
                }
            }

            public static void QueueCommand(string command)
            {
                unexecutedCommands.Enqueue(command);
            }



            public static void ContaminantUpdate(Material contaminant)
            {
                storedDetection.ContaminantUpdate(contaminant);

            }

            public static void DiagnosticRoutine()
            {

            }

            public static Waypoint CurrentWaypoint()
            {
                return storedStandard.pathfinding.Goal;
            }
          
            public static void BehaviorLoop() //something related behavior
            {
                /*
                     * Standard Mode 
                     * 
                     * Upon receiving waypoints, and started up, SC will plot the best route between all waypoints. The path does not take
                     * into account obstructions found along the way, but it does take into account obstructions listed by the user.
                     * The SC DOES NOT CONTROL THE MOVEMENT, IT JUST SETS THE DESTINATION. IT MAKES A DECISION AND SAYS "MNM, DO STUFF" 
                     * AND IT HAPPENS. It says "Going left is safe" or "Halt", mnm uses that information to perform correctly.
                     * MNM is not a pseudo module and extension of SC. No, MNM handles all short term (immediate) movement options. 
                     * SC handles plotting the current direction and goal destination. 
                     * 
                     * 1) If not in proximity of goal waypoint, continue on toward goal waypoint. 
                     * 2) If in proximity of goal waypoint, traverse around point. 
                     * 3) If done exploring the point, move on to next point.
                     * 
                     * ---
                     * Detection Mode
                     * 
                     * 1) Switches to this mode when ER finds a threat. It makes that threat its current "target". 
                     * 2) Will move closer to areas that have higher concentration.
                     * 3) Upon finding area with higher concentration, will path around it.
                     * 4) If it finds area with higher concentration, it will return to step 3.
                     * 
                     * ---
                     * Obstruction Mode
                     * 
                     * 1) Upon detecting an "obstruction". Will send request to user to determine what to do next.
                     *      a) If procede, then the probe will 
                     *          a) navigate around the obstruction, and using scanners to gather info.
                     *      b) If Ignore, the probe will ignore the obstruction.
                     *      c) Not responding is same as ignoring.
                     * 2) After gathering Info, the probe will continue on as normal.
                     * 
                     * ---
                     * Special Case
                     * 
                     * Obstruction + Detection 
                     * 
                     * 1) User gets choice, as if obstruction.
                     * 2) If use does not respond, defaults to detection.
                     */

                while (true)
                {

                    if ( currentStateType != nextStateType)
                        switchState(nextStateType);
                    else
                    {
                        if(currentState!=null)
                            currentState.Cycle();
                        else
                            Console.WriteLine("DRIVER -> MNM Idle ");
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

                if (forcedState != StateType.None)
                {
                    
                    if (GetStateFromStateType(forcedState).Ready())
                    {
                        Console.WriteLine("DRIVER Forced State");
                        nextStateType = forcedState;
                    }
                    else
                    {
                        Console.WriteLine("DRIVER Forced State failed.");
                        forcedState = StateType.None;
                    }
                }
                
                else
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
                        nextStateType = StateType.Idle;
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
                CheckState();
            }

            public static void Stop()
            {
                forcedState = StateType.Idle;
                CheckState();
            }

            public static void Move()
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
                        if(forcedState == StateType.Idle)
                            storedIdle.Start(true);
                        else
                            storedIdle.Start(false);

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
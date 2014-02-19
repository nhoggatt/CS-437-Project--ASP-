using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASP
{
    namespace SystemControl
    {
        class Driver
        {
            private static Queue<string> unexecutedCommands = new Queue<string>();
            public static WaypointManager storedWaypoints = new WaypointManager();

            static void Main(string[] args)
            {
                QueueCommand("nav waypoint list");
                

                int i = 0;

                while (true)
                {
                    if (unexecutedCommands.Count() > 0)
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
            }

            public static void QueueCommand(string command)
            {
                unexecutedCommands.Enqueue(command);
            }
        }
    }
}
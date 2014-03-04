using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.General;
using ASP.Report;


namespace ASP
{
    namespace SystemControl
    {
        public class Parser
        {
            //change return type to ParsingError that contains an error number and a description of the issue. If it returns null then there was no error.
            delegate void Command(string args);
            private static Dictionary<string, Command> commandDict = new Dictionary<string, Command>(){

          { "kill", new Command(Kill)},
          { "print", new Command(Print)},
          { "nav", new Command(Nav)},
          { "envdata", new Command(Envdata)},
          { "report", new Command(Report)}

       };

            public static void InterpretCommand(string line)
            {
                string[] lineSplit = line.Split(new char[] { ' ' }, 2);
                string command = lineSplit[0];
                string args = lineSplit.Length > 1 ? lineSplit[1] : "";

                if (commandDict.ContainsKey(command))
                {
                    commandDict[command](args);
                }
            }

            private static void Kill(string args)
            {
                Environment.Exit(0);
            }

            private static void Print(string args)
            {
                
                if (args == "")
                    throw new ParserException("Print", "Attempted to print nothing.");
                System.Console.WriteLine(args);
                System.Console.WriteLine();
            }

            private static void Nav(string args)
            {
                string[] argsToArray = args.Split(' ');
                if (argsToArray.Length == 1 && argsToArray[0] == "status")
                {
                    Console.WriteLine("Current Location: "+Driver.locationTime.TimeLocation.Location);
                    Console.WriteLine("Current Destination: " + Driver.storedStandard.pathfinding.Goal);
                }
                else if (argsToArray.Length > 1 && argsToArray[0] == "control")
                {
                    if (argsToArray.Length == 2 && argsToArray[1] == "move")
                    {
                        Driver.Move();
                    }
                    else if (argsToArray.Length == 2 && argsToArray[1] == "stop")
                    {
                        Driver.Stop();
                    }
                }
                else if (argsToArray.Length > 1 && argsToArray[0] == "waypoint")
                {
                    if (argsToArray.Length == 2 && argsToArray[1] == "list")
                    {
                        
                    for (int i = 0; i < Driver.storedWaypoints.ListWaypoints().Count; i++)
                        {
                            Waypoint way = Driver.storedWaypoints.ListWaypoints()[i];
                            Console.WriteLine(i+" "+way.getX() + " " + way.getY() + " " + way.getZ() + " " + way.getRadius());
                        }
                    }
                    else if ((argsToArray.Length - 2) % 4 == 0 && argsToArray[1] == "add")
                    {
                        for (int i = 2; i < (argsToArray.Length); i += 4)
                            Driver.storedWaypoints.AddWaypoint(Double.Parse(argsToArray[i]), Double.Parse(argsToArray[i + 1]),
                                Double.Parse(argsToArray[i + 2]), Double.Parse(argsToArray[i + 3]));
                    }
                    else if (argsToArray.Length > 2 && argsToArray[1] == "remove")
                    {
                        for (int i = 2; i < (argsToArray.Length); i++)
                            Driver.storedWaypoints.RemoveWaypoint((Int32.Parse(argsToArray[i])));
                    }
                    else if ((argsToArray.Length - 2) % 3 == 0 && argsToArray[1] == "update")
                    {
                        for (int i = 2; i < (argsToArray.Length ); i += 5)
                            Driver.storedWaypoints.UpdateWaypoint(Int32.Parse(argsToArray[i]), Double.Parse(argsToArray[i + 1]),
                                Double.Parse(argsToArray[i + 2]), Double.Parse(argsToArray[i + 3]), Double.Parse(argsToArray[i + 4]));
                    }

                }



            }
            private static void Report(string args)
            {
                string[] argsToArray = args.Split(' ');
                if (argsToArray.Length == 3 && argsToArray[0] == "frequency")
                {
                    Driver.ContaminantUpdate(new Material(Int32.Parse(argsToArray[1]),
                         argsToArray[2] == "true",
                         Int32.Parse(argsToArray[3]),
                         Double.Parse(argsToArray[4])));
                }
                else if (argsToArray.Length == 2 && argsToArray[0] == "send")
                {
                    Driver.scheduler.EnvironmentalDataLifetime = (Int64.Parse(argsToArray[1]));
                }
            }
            private static void Envdata(string args)
            {
                
                string[] argsToArray = args.Split(' ');
                if (argsToArray.Length == 5 && argsToArray[0] == "contaminant")
                {
                   Driver.ContaminantUpdate( new Material(Int32.Parse(argsToArray[1]),
                        argsToArray[2] == "true",
                        Int32.Parse(argsToArray[3]),
                        Double.Parse(argsToArray[4])));
                }
                else if (argsToArray.Length == 2 && argsToArray[0] == "lifetime")
                {
                   Driver.scheduler.EnvironmentalDataLifetime = (Int64.Parse(argsToArray[1]));
                }
              



            
            }
            private static void Shutdown(string args)
            {
                Environment.Exit(0);
            }
            private static void Log(string args)
            {
                Environment.Exit(0);
            }
        }
    }
}
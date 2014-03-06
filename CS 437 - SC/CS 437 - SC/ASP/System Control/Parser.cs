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
          { "report", new Command(Report)},
          { "test", new Command(Test)},
          {"obstruction", new Command(Obstruction)}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               

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
                    if (argsToArray.Length == 2 && argsToArray[1] == "start")
                    {
                        Driver.RemoveForcedState();
                    }
                    else if (argsToArray.Length == 2 && argsToArray[1] == "stop")
                    {
                        Driver.ForceIdleState();
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
                    else if ((argsToArray.Length - 2) % 5 == 0 && argsToArray[1] == "update")
                    {
                        for (int i = 2; i < (argsToArray.Length ); i += 5)
                            Driver.storedWaypoints.UpdateWaypoint(Int32.Parse(argsToArray[i]), Double.Parse(argsToArray[i + 1]),
                                Double.Parse(argsToArray[i + 2]), Double.Parse(argsToArray[i + 3]), Double.Parse(argsToArray[i + 4]));
                    }

                }



            }
            private static void Report(string args)
            {
                //report frequency status 10
                //report frequency environmental 5
                //report maintenance 1
                string[] argsToArray = args.Split(' ');
                if (argsToArray.Length == 3 && argsToArray[0] == "frequency")
                {
                    if (argsToArray[1] == "maintenance")
                    {
                        Driver.scheduler.Maintenance = Int64.Parse(argsToArray[2]);
                    }
                    else if (argsToArray[1] == "status")
                    {
                        Driver.scheduler.Status = Int64.Parse(argsToArray[2]);
                    }
                    else if (argsToArray[1] == "environmental")
                    {
                        Driver.scheduler.Environmental = Int64.Parse(argsToArray[2]);
                    }
                }
                else if (argsToArray.Length == 2 && argsToArray[0] == "send")
                {

                    // report send status
                    if (argsToArray[1] == "maintenance")
                    {
                        Driver.report.reports[argsToArray[1]]();
                    }
                    else if (argsToArray[1] == "status")
                    {
                        Driver.report.reports[argsToArray[1]]();
                    }
                    else if (argsToArray[1] == "environmental")
                    {
                        Driver.report.reports[argsToArray[1]]();
                    }
                    else if (argsToArray[1] == "obstruction")
                    {
                        Driver.report.reports[argsToArray[1]]();
                    }
                }
            }
            private static void Envdata(string args)
            {
                
                string[] argsToArray = args.Split(' ');
                if (argsToArray.Length == 2 && argsToArray[0] == "lifetime")
                {
                   Driver.scheduler.EnvironmentalDataLifetime = (Int64.Parse(argsToArray[1]));
                }
              
            }

            private static void Obstruction(string args)
            {
                // obstruction true/false
                string[] argsToArray = args.Split(' ');
                
                if (argsToArray.Length ==  1)
                {
                    bool val = argsToArray[0] == "true" ? true : false;
                    if (Driver.storedObstructon.Ready())
                    {
                        Driver.storedObstructon.StartExploring();

                        Driver.ForceObstructionState();
                    }

                }

            }

            private static void Test(string args)
            {
                //test report environmental [bool]
                //test report obstruction [int]
                Random ran = new Random();
                string[] argsToArray = args.Split(' ');
                
                if (argsToArray.Length == 2 && argsToArray[0] == "component")
                {
                    if (argsToArray[1] == "list")
                    {
                        var temp = Driver.storedComponents.ListComponent();
                        Console.WriteLine(temp.Count);
                        foreach (Component comp in temp)
                        {
                            comp.Status();

                            for (int i = 0; i < comp.ComponentDependencies.Count; i++)
                            {
                                Console.Write(" ");
                                comp.ComponentDependencies[i].Status();
                            }
                              
                        }
                    }
                    else
                    {
                        Driver.storedComponents.ListComponent()[Int32.Parse(argsToArray[1])].Functional =
                            Driver.storedComponents.ListComponent()[Int32.Parse(argsToArray[1])].Functional == true ? false : true;
                    }

                }
                else if (argsToArray.Length == 4 && argsToArray[0] == "location")
                {
                    Driver.locationTime.UpdatePosition(Double.Parse(argsToArray[1]), Double.Parse(argsToArray[2]), Double.Parse(argsToArray[3]));

                }
                else if (argsToArray.Length == 2 && argsToArray[0] == "waypoint")
                {
                    int temp = Int32.Parse(argsToArray[1]);
                    Driver.storedStandard.pathfinding.Goal = Driver.storedWaypoints.ListWaypoints()
                        [temp < Driver.storedWaypoints.ListWaypoints().Count ? temp : 0];
                }
                else if (argsToArray.Length == 5 && argsToArray[0] == "contaminant")
                {

                    Driver.storedDetection.ContaminantUpdate(new Material(Int32.Parse(argsToArray[1]),
                         argsToArray[2] == "true",
                         Int32.Parse(argsToArray[3]),
                         Double.Parse(argsToArray[4])));
                    Console.WriteLine("TEST Contaminant created.");
                }
                else if (argsToArray.Length > 1 && argsToArray[0] == "report")
                {
                    if (argsToArray[1] == "environmental" && argsToArray.Length == 3)
                    {
                        Console.WriteLine("TEST Environmental data created.");
                        int reports = Int32.Parse(argsToArray[2]);
                        for (int i = 0; i < reports; i++)
                        {
                            Driver.report.AddToEnvironmentalReportData(new EnvironmentalData(Driver.storedStandard.pathfinding.Goal));
                        }

                    }
                    else if (argsToArray[1] == "obstruction" && argsToArray.Length == 2)
                    {
                        Console.WriteLine("TEST Obstruction created.");
                        Driver.storedObstructon.FoundObstruction();
                    }
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
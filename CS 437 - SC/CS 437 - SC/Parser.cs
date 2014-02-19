using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
          { "nav", new Command(Nav)}

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

                }
                else if (argsToArray.Length > 1 && argsToArray[0] == "control")
                {
                    if (argsToArray.Length == 2 && argsToArray[0] == "move")
                    {

                    }
                    else if (argsToArray.Length == 2 && argsToArray[1] == "stop")
                    {

                    }
                }
                else if (argsToArray.Length > 1 && argsToArray[0] == "waypoint")
                {
                    if (argsToArray.Length == 2 && argsToArray[1] == "list")
                    {
                        foreach(Waypoint way in ASP.SystemControl.Driver.storedWaypoints.ListWaypoints())
                            Console.WriteLine(way.getX() + " " + way.getY() + " " + way.getZ());
                    }
                    else if ((argsToArray.Length - 2) % 2 == 0 && argsToArray[1] == "add")
                    {
                        for(int i = 0; )
                        ASP.SystemControl.Driver.storedWaypoints.AddWaypoint(double x, double y, double z, double radius);
                    }
                    else if (argsToArray.Length > 2 && argsToArray[1] == "remove")
                    {

                    }
                    else if ((argsToArray.Length - 2) % 3 == 0 && argsToArray[1] == "update")
                    {

                    }

                }



            }
            private static void Report(string args)
            {
                Environment.Exit(0);
            }
            private static void Envdata(string args)
            {
                Environment.Exit(0);
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
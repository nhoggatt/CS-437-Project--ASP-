using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_437
{
    public class Parser
    {
        //change return type to ParsingError that contains an error number and a description of the issue. If it returns null then there was no error.
        delegate void Command(string args);
        private static Dictionary<string, Command> commandDict = new Dictionary<string, Command>(){

          { "kill", new Command(Kill)},
          { "print", new Command(Print)}

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
            if(args == "")
                throw new ParserException("Print", "Attempted to print nothing.");
            System.Console.WriteLine(args);
            System.Console.WriteLine();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_437
{
    class ParserException : Exception
    {
        public string CommandName;
        public string Description;

        public ParserException(string commandName, string description)
        {
            CommandName = commandName;
            Description = description;
        }
    }
}

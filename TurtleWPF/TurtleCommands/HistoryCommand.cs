using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWPF.CommandInterfaces;
using TurtleWPF.DataBase;

namespace TurtleWPF.TurtleCommands
{
    public class HistoryCommand: ICommandWithoutArgs
    {
        private IDBAppReader dbAppReader;
        public HistoryCommand(IDBAppReader reader)
        {
            dbAppReader = reader;
        }

        public void Execute(Turtle turtle)
        {
            // Console.WriteLine(dbReader.GetCommands());
        }
    }
}

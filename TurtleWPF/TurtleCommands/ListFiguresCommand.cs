using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWPF.CommandInterfaces;
using TurtleWPF.DataBase;

namespace TurtleWPF.TurtleCommands
{
    public class ListFiguresCommand: ICommandWithoutArgs
    {
        private IDBAppReader dbAppReader;
        public ListFiguresCommand(IDBAppReader reader)
        {
            dbAppReader = reader;
        }

        public void Execute(Turtle turtle)
        {
            // Console.WriteLine(dbReader.GetFigures());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWPF.CommandInterfaces;

namespace TurtleWPF.TurtleCommands
{
    public class WidthCommand: ICommandWithArgs
    {
        public void Execute(Turtle turtle, string arg)
        {
            turtle.SetWidth(double.Parse(arg));

        }
    }
}

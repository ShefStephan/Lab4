using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWPF.CommandInterfaces;

namespace TurtleWPF.TurtleCommands
{
    public class PenUpCommand: ICommandWithoutArgs
    {
        public void Execute(Turtle turtle)
        {
            turtle.SetPenCondition(false);

        }
    }
}

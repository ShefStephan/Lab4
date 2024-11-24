using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWPF.CommandInterfaces;

namespace TurtleWPF.TurtleCommands
{
    public class AngleCommand: ICommandWithArgs
    {
        public void Execute(Turtle turtle, string str)
        {

            turtle.SetAngle(int.Parse(str));

        }
    }
}

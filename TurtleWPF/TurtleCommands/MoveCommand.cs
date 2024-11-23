using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWPF.CommandInterfaces;

namespace TurtleWPF.TurtleCommands
{
    public class MoveCommand: ICommandWithArgs
    {
        public void Execute(Turtle turtle, string str)
        {

            turtle.SetCoordx(double.Parse(str)
                * Math.Sin(turtle.GetAngle() * (Math.PI / 180)));

            turtle.SetCoordY(double.Parse(str)
                * Math.Cos(turtle.GetAngle() * (Math.PI / 180)));

        }
    }
}

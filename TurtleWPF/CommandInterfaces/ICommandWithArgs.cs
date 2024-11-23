using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWPF.Model;

namespace TurtleWPF.CommandInterfaces
{
    public interface ICommandWithArgs: ICommands
    {
        void Execute(Turtle turtle, string arg = null);
    }
}

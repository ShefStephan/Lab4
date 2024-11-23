using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWPF.CommandInterfaces
{
    public interface ICommandWithoutArgs: ICommands
    {
        public void Execute(Turtle turtle);
    }
}

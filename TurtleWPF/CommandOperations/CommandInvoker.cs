using TurtleWPF.CommandInterfaces;
using TurtleWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWPF.CommandOperations
{
    public class CommandInvoker
    {

        private Turtle turtle;

        public CommandInvoker(Turtle turtle)
        {
            this.turtle = turtle;
        }

        public void Invoke(ICommandWithoutArgs comm)
        {
            comm.Execute(turtle);
        }

        public void Invoke(ICommandWithArgs comm, string arg)
        {
            comm.Execute(turtle, arg);
        }
    }
}

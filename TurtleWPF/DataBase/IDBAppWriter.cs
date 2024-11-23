using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWPF.DataBase
{
    public interface IDBAppWriter
    {
        public Task SaveCommand(string commandText);

        public Task SaveFigure(string figureType, string parameters);
        public Task SaveTurtleCoords(Turtle turtle);

        public Task SaveTurtleStatus(Turtle turtle);
    }
}

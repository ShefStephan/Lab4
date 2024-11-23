using TurtleWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWPF.DataBase
{
    public interface IDBAppReader
    {
        public Task<TurtleStatus?> GetTurtleStatus();

        public Task<TurtleCoords?> GetTurtleCoords();

        public Task<List<CommandHistory>> GetCommands();

        public Task<List<Figure>> GetFigures();
    }
}

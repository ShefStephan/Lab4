using TurtleWPF.DataBase;
using TurtleWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWPF.DataBase
{
    public class DBAppReader: IDBAppReader
    {
        public async Task<TurtleStatus?> GetTurtleStatus()
        {
            using (var context = new TurtleAppContext())
            {
                return context.TurtleStatus.OrderByDescending(t => t.Id).FirstOrDefault();

            }
        }

        public async Task<TurtleCoords?> GetTurtleCoords()
        {
            using (var context = new TurtleAppContext())
            {
                return context.TurtleCoords.OrderByDescending(t => t.Id).FirstOrDefault();
            }
        }
        public async Task<List<CommandHistory>> GetCommands()
        {
            using (var context = new TurtleAppContext())  // using гарантирует освобождение
            {
                return context.CommandHistory.ToList();
            }
        }

        public async Task<List<Figure>> GetFigures()
        {
            using (var context = new TurtleAppContext())
            {
                return context.Figure.ToList();
            }
        }

    }
}

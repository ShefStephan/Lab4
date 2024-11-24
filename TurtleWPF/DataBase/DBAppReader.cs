using TurtleWPF.DataBase;
using TurtleWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TurtleWPF.DataBase
{
    public class DBAppReader: IDBAppReader
    {
        public async Task<TurtleStatus?> GetTurtleStatus()
        {
            using (var context = new TurtleAppContext())
            {
                return await context.TurtleStatus.OrderByDescending(t => t.Id).FirstOrDefaultAsync();

            }
        }

        public async Task<TurtleCoords?> GetTurtleCoords()
        {
            using (var context = new TurtleAppContext())
            {
                return await context.TurtleCoords.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            }
        }
        public async Task<List<CommandHistory>> GetCommands()
        {
            using (var context = new TurtleAppContext())  // using гарантирует освобождение
            {
                return await context.CommandHistory.ToListAsync();
            }
        }

        public async Task<List<Figure>> GetFigures()
        {
            using (var context = new TurtleAppContext())
            {
                return await context.Figure.ToListAsync();
            }
        }

    }
}

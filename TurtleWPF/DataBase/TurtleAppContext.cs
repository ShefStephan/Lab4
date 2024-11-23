using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWPF.Model;
using Microsoft.EntityFrameworkCore;


namespace TurtleWPF.DataBase
{
    public class TurtleAppContext: DbContext
    {
        public DbSet<CommandList> CommandLists { get; set; } = null!; // не факт что нужно
        public DbSet<TurtleStatus> TurtleStatus { get; set; } = null!;
        public DbSet<TurtleCoords> TurtleCoords { get; set; } = null!;
        public DbSet<CommandHistory> CommandHistory { get; set; } = null!;
        public DbSet<Figure> Figure { get; set; } = null!;

        public TurtleAppContext(DbContextOptions<TurtleAppContext> options) : base(options) { }
        public TurtleAppContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            object value = optionsBuilder.UseSqlite("Data Source=lab4.db"); 
        }


        public void InitializeDatabase()
        {
            using (var context = new TurtleAppContext())
            {
                // Проверка есть ли записи в таблице TurtleStatus
                if (!context.TurtleStatus.Any())
                {
                    var initialStatus = new TurtleStatus
                    {
                        Xcoors = 0,           // начальная координата X
                        Ycoors = 0,           // начальная координата Y
                        PenCondition = "down",// начальное состояние пера
                        Angle = 0,            // начальный угол поворота
                        Color = "black",      // начальный цвет
                        Width = 1             // начальная ширина пера
                    };

                    context.TurtleStatus.Add(initialStatus);
                }

                // Проверка есть ли записи в таблице TurtleCoords
                if (!context.TurtleCoords.Any())
                {
                    var initialCoords = new TurtleCoords
                    {
                        xCoord = 0,           // начальная координата X
                        yCoord = 0            // начальная координата Y
                    };

                    context.TurtleCoords.Add(initialCoords);
                }

                context.SaveChanges();
            }
        }
    }
}

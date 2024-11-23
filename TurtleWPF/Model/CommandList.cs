using TurtleWPF.CommandInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWPF.Model
{
    public class CommandList
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [NotMapped]
        public ICommands Command { get; set; }
    }
}

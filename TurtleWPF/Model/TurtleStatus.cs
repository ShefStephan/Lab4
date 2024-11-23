using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWPF.Model
{
    public class TurtleStatus
    {
        public int Id { get; set; }
        public double Xcoors { get; set; }
        public double Ycoors { get; set; }
        public double Angle { get; set; }
        public string PenCondition { get; set; }
        public string Color { get; set; }
        public double Width { get; set; }
    }
}

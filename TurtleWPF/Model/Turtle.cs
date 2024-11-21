using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TurtleWPF.Model
{
    public class Turtle: INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string xCoord;
        private string yCoord;
        private string angle;

        public Turtle(string xCoord, string yCoord, string angle) {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            this.angle = angle;

        }

        public string XCoord
        {
            get { return xCoord; }
            set
            {
                xCoord = value;
                OnPropertyChanged("XCoord");
            }
        }

        public string YCoord
        {
            get { return yCoord; }
            set
            {
                yCoord = value;
                OnPropertyChanged("YCoord");
            }
        }

        public string Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                OnPropertyChanged("Angle");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

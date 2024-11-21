using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using TurtleWPF.Model;
using System.Windows.Input;
namespace TurtleWPF.AppViewModel
{
    public class ViewModel: INotifyPropertyChanged
    {
        private Turtle turtle;

        public ObservableCollection<Turtle> Turtles { get; set; }

        RelayCommand? addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Turtle turtle = new Turtle(" ", " ", "0");
                      Turtles.Insert(0, turtle);
                      SelectedTurtle = turtle;
                  }));
            
            }
        }

        // команда удаления
        RelayCommand? removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Turtle turtle = obj as Turtle;
                      if (turtle != null)
                      {
                          Turtles.Remove(turtle);
                      }
                  },
                 (obj) => Turtles.Count > 0));
            }
        }

        RelayCommand? doubleCommand;
        public RelayCommand DoubleCommand
        {
            get
            {
                return doubleCommand ??
                    (doubleCommand = new RelayCommand(obj =>
                    {
                        Turtle? turtle = obj as Turtle;
                        if (turtle != null)
                        {
                            Turtle phoneCopy = new Turtle(turtle.XCoord, turtle.YCoord, turtle.Angle);
                            Turtles.Insert(0, phoneCopy);
                        }
                    }));
            }
        }

        public Turtle SelectedTurtle
        {
            get { return turtle; }
            set
            {
                turtle = value;
                OnPropertyChanged("SelectedTurtle");
            }
        }

        public ViewModel()
        {
            Turtles = new ObservableCollection<Turtle>
            {
                new Turtle ("100", "50", "90" ),
                new Turtle ("100", "50", "90" ),
                new Turtle ("100", "50", "90" ),
                new Turtle ("100", "50", "90" )
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

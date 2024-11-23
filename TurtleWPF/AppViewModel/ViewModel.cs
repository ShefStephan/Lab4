using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TurtleWPF.CommandOperations;
using TurtleWPF.TurtleCommands;
using TurtleWPF.DataBase;

namespace TurtleWPF.AppViewModel
{
    public class ViewModel: INotifyPropertyChanged
    {
        private Turtle turtle;
        private string _resultState;
        private CommandInvoker invoker;
        private IDBAppReader dataBaseAppReader;
        private IDBAppWriter dataBaseAppWriter;

        public ViewModel(DBAppReader reader, DBAppWriter writer, CommandInvoker invoker)
        {
            turtle = new Turtle(); // Инстанцирование бизнес-логики
            ExecuteCommand = new RelayCommand(SaveCommand, CanExecuteCommand);
            dataBaseAppReader = reader;
            dataBaseAppWriter = writer;
            this.invoker = invoker;

        }

        public bool IsMoveChecked { get; set; }
        public bool IsAngleChecked { get; set; }
        public bool IsColorChecked { get; set; }
        public bool IsPenDownChecked { get; set; }
        public bool IsPenUpChecked { get; set; }

        public string MoveParameter { get; set; }
        public string AngleParameter { get; set; }
        public string ColorParameter { get; set; }

        public string ResultState
        {
            get => _resultState;
            set
            {
                _resultState = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExecuteCommand { get; }

        private bool CanExecuteCommand(object parameter)
        {
            return IsMoveChecked;
        }

        public void SaveCommand(object parameter)
        {
            if (invoker == null || dataBaseAppWriter == null)
            {
                ResultState = "Ошибка: invoker или dataBaseAppWriter не инициализированы!";
                return;
            }

            if (IsMoveChecked && int.TryParse(MoveParameter, out int moveValue))
            {
                invoker.Invoke(new MoveCommand(), MoveParameter);
                ResultState = $"coords: ({turtle.GetCoordX};{turtle.GetCoordY})\n" +
                              $"angle: {turtle.GetAngle}\n" +
                              $"color: {turtle.GetColor}\n" +
                              $"penCondition: {turtle.GetPenCondition}\n" +
                              $"width: {turtle.GetWidth}";
                dataBaseAppWriter.SaveCommand("move" + " " + MoveParameter);
                dataBaseAppWriter.SaveTurtleStatus(turtle);
            }
            else
            {
                ResultState = "Ошибка: Некорректный параметр команды!";
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    

}


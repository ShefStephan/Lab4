using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TurtleWPF.CommandOperations;
using TurtleWPF.TurtleCommands;
using TurtleWPF.DataBase;
using TurtleWPF.Model;

namespace TurtleWPF.AppViewModel
{
    // Класс для хранения состояния черепашки
    

    public class ViewModel : INotifyPropertyChanged
    {
        private Turtle turtle;
        private TurtleState turtleState;
        private string resultState;
        private string resultFigureState;
        private CommandInvoker invoker;
        private IDBAppReader dataBaseAppReader;
        private IDBAppWriter dataBaseAppWriter;
        private NewFigureChecker checker;

        public ViewModel(DBAppReader reader, DBAppWriter writer, CommandInvoker invoker)
        {
            this.turtle = invoker.turtle; 
            this.invoker = invoker;
            dataBaseAppWriter = writer;
            dataBaseAppReader = reader;
            ExecuteCommand = new AsyncRelayCommand(SaveCommandAsync, CanExecuteCommand);
            checker = new NewFigureChecker(turtle, writer, reader);
        }


        public bool IsMoveChecked { get; set; }
        public bool IsAngleChecked { get; set; }
        public bool IsColorChecked { get; set; }
        public bool IsWidthChecked { get; set; }
        public bool IsPenDownChecked { get; set; }
        public bool IsPenUpChecked { get; set; }
        public bool IsHistoryChecked { get; set; }
        public bool IsListFiguresChecked { get; set; }

        public string MoveParameter { get; set; }
        public string AngleParameter { get; set; }
        public string ColorParameter { get; set; }
        public string WidthParameter { get; set; }

        public TurtleState TurtleState
        {
            get => turtleState;
            set
            {
                turtleState = value;
                OnPropertyChanged();
            }
        }

        public string ResultState
        {
            get => resultState;
            set
            {
                resultState = value;
                OnPropertyChanged();
            }
        }

        public string ResultFigureState
        {
            get => resultFigureState;
            set
            {
                resultFigureState = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExecuteCommand { get; }

        private bool CanExecuteCommand(object parameter)
        {
            return IsMoveChecked ||
                IsAngleChecked ||
                IsColorChecked ||
                IsWidthChecked ||
                IsPenUpChecked ||
                IsPenDownChecked ||
                IsHistoryChecked ||
                IsListFiguresChecked;
        }

        private void UpdateTurtleState()
        {
            TurtleState = new TurtleState
            {
                Coordinates = $"Coords: ({turtle.GetCoordX()}; {turtle.GetCoordY()})",
                Angle = $"Angle: {turtle.GetAngle()}",
                Color = $"Color: {turtle.GetColor()}",
                PenCondition = $"PenCondition: {turtle.GetPenCondition()}",
                Width = $"Width: {turtle.GetWidth()}"
            };
        }

        public async Task SaveCommandAsync(object parametr)
        {
            try
            {
                if (IsHistoryChecked)
                {
                    var history = await dataBaseAppReader.GetCommands();
                    TurtleState = null;
                    ResultFigureState = null;
                    string historyText = string.Join("\n", history.Select(comm => comm.CommandText));
                    ResultState = historyText;
                }
                else if (IsListFiguresChecked)
                {

                    var figures = await dataBaseAppReader.GetFigures();
                    TurtleState = null;
                    ResultState = null;
                    string figuresText = string.Join("\n", figures.Select(figure => $"{figure.FigureType} {figure.Parameters}"));
                    ResultFigureState = figuresText;
                }
                else
                {
                    if (IsMoveChecked)
                    {
                        invoker.Invoke(new MoveCommand(), MoveParameter);
                        await dataBaseAppWriter.SaveCommand("move " + MoveParameter);
                    }
                    else if (IsAngleChecked)
                    {
                        invoker.Invoke(new AngleCommand(), AngleParameter);
                        await dataBaseAppWriter.SaveCommand("angle " + AngleParameter);
                    }
                    else if (IsColorChecked)
                    {
                        invoker.Invoke(new ColorCommand(), ColorParameter);
                        await dataBaseAppWriter.SaveCommand("color " + ColorParameter);
                    }
                    else if (IsWidthChecked)
                    {
                        invoker.Invoke(new WidthCommand(), WidthParameter);
                        await dataBaseAppWriter.SaveCommand("width " + WidthParameter);
                    }
                    else if (IsPenDownChecked)
                    {
                        invoker.Invoke(new PenDownCommand());
                        await dataBaseAppWriter.SaveCommand("penDown");
                    }
                    else if (IsPenUpChecked)
                    {
                        invoker.Invoke(new PenUpCommand());
                        await dataBaseAppWriter.SaveCommand("penUp");
                    }

                    await dataBaseAppWriter.SaveTurtleStatus(turtle);
                    await checker.Check();
                    UpdateTurtleState();
                }
            }
            catch (Exception ex)
            {
                ResultState = "Error: " + ex.Message; 
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}






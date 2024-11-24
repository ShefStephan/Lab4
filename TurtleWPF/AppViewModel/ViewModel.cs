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
using System.Reflection.PortableExecutable;
using System.DirectoryServices.ActiveDirectory;

namespace TurtleWPF.AppViewModel
{
    public class ViewModel: INotifyPropertyChanged
    {
        private Turtle turtle;
        private string _resultState;
        private CommandInvoker invoker;
        private IDBAppReader dataBaseAppReader;
        private IDBAppWriter dataBaseAppWriter;
        private NewFigureChecker checker;

        public ViewModel(DBAppReader reader, DBAppWriter writer, CommandInvoker invoker)
        {
            this.turtle = invoker.turtle; // Используем черепашку из инвокера
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
            return IsMoveChecked || 
                IsAngleChecked || 
                IsColorChecked || 
                IsWidthChecked || 
                IsPenUpChecked || 
                IsPenDownChecked ||
                IsHistoryChecked ||
                IsListFiguresChecked;
        }

        public async Task SaveCommandAsync(object parametr)
        {
            try
            {
                if (IsHistoryChecked)
                {
                    // Вывод истории команд
                    var history = await dataBaseAppReader.GetCommands();
                    ResultState = ""; // Очистить старые данные
                    foreach (var comm in history)
                    {
                        ResultState += comm.CommandText + "\n"; // Добавить разделитель
                    }
                }
                else if (IsListFiguresChecked)
                {
                    // Вывод списка фигур
                    var figures = await dataBaseAppReader.GetFigures();
                    ResultState = ""; // Очистить старые данные
                    foreach (var figure in figures)
                    {
                        ResultState += figure.FigureType + " " + figure.Parameters + "\n"; // Добавить разделитель
                    }
                }
                else
                {
                    // Выполнение остальных команд
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

                    // Сохранение состояния черепашки
                    await dataBaseAppWriter.SaveTurtleStatus(turtle);
                    await checker.Check();

                    // Обновление состояния черепашки
                    ResultState = $"coords: ({turtle.GetCoordX()};{turtle.GetCoordY()})\n" +
                                  $"angle: {turtle.GetAngle()}\n" +
                                  $"color: {turtle.GetColor()}\n" +
                                  $"penCondition: {turtle.GetPenCondition()}\n" +
                                  $"width: {turtle.GetWidth()}";
                }
            }

            catch (Exception ex)
            {
                ResultState = "Error: " + ex.Message; // Выводить сообщение об ошибке
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    

}


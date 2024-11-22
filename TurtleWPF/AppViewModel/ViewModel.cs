using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace TurtleWPF.AppViewModel
{
    public class ViewModel : INotifyPropertyChanged
{
    private Turtle turtle;
    private string _resultState;

    public ViewModel()
    {
        turtle = new Turtle(); // Инстанцирование бизнес-логики
        ExecuteCommand = new RelayCommand(SaveCommand, CanExecuteCommand);
    }

    public bool IsMoveChecked { get; set; }
    public bool IsColorChecked { get; set; }
    public bool IsPenDownChecked { get; set; }

    public string MoveParameter { get; set; }
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
        return IsMoveChecked || IsColorChecked || IsPenDownChecked;
    }

        private void SaveCommand(object parameter)
        {
            if (IsMoveChecked && int.TryParse(MoveParameter, out int moveValue))
            {
                turtle.Move(moveValue);
            }
            else if (IsColorChecked && !string.IsNullOrWhiteSpace(ColorParameter))
            {
                turtle.ChangeColor(ColorParameter);
            }
            else if (IsPenDownChecked)
            {
                turtle.PenDown();
            }

            ResultState = turtle.GetState(); // Обновляем результат
        }





        public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

}

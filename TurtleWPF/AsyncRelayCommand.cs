using System;
using System.Threading.Tasks;
using System.Windows.Input;

public class AsyncRelayCommand : ICommand
{
    private readonly Func<object, Task> _executeAsync;
    private readonly Predicate<object> _canExecute;

    public AsyncRelayCommand(Func<object, Task> executeAsync, Predicate<object> canExecute = null)
    {
        _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    public async void Execute(object parameter)
    {
        await _executeAsync(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}

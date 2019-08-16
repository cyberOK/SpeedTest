using System;
using System.ComponentModel;
using System.Windows.Input;

namespace SpeedTest.ViewModel.Helpers
{
    public class SpeedTestCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public SpeedTestCommand(Action<object> execute) : this(execute, null)
        {
        }

        public SpeedTestCommand(Action<object> execute, Func<bool> canExecute)
        {
            this._execute = execute ?? throw new ArgumentException("execute");
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => this._canExecute == null ? true : this._canExecute();

        public void Execute(object parameter) => this._execute(parameter);

        public void RaiseCanExecuteChanged(string propertyName) => this.CanExecuteChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

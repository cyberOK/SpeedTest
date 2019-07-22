using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpeedTest.ViewModel.Helpers
{
    public class SpeedTestCommands : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public SpeedTestCommands(Action<object> execute) : this(execute, null)
        {
        }

        public SpeedTestCommands(Action<object> execute, Func<bool> canExecute)
        {
            this._execute = execute ?? throw new ArgumentException("execute");
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => this._canExecute == null ? true : this._canExecute();

        public void Execute(object parameter) => this._execute(parameter);

        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}

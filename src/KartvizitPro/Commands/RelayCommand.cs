using System;
using System.Windows.Input;

namespace KartvizitPro.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action DoWork;
        public RelayCommand(Action doWork)
        {
            DoWork = doWork;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            DoWork();
        }
    }
}

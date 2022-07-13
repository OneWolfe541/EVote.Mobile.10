using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace EVote.Utilities.Commands
{
    // Patterns - WPF Apps With The Model-View-ViewModel Design Pattern
    // https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    // Further reading
    // https://stackoverflow.com/questions/9673872/explanation-of-josh-smith-article?rq=1
    // https://stackoverflow.com/questions/1779465/mvvm-questions-on-josh-smiths-sample-application
    public class RelayCommand : ICommand
    {
        #region Fields 
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        #endregion // Fields

        #region Constructors 
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute; _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members 
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            // InvalidateRequerySuggested() added to force UI to refresh
            // https://stackoverflow.com/questions/14479303/icommand-canexecute-not-triggering-after-propertychanged
            CommandManager.InvalidateRequerySuggested();

            return _canExecute == null ? true : _canExecute(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter) { _execute(parameter); }
        #endregion // ICommand Members 
    }
}

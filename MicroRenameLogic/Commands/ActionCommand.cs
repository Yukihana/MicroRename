using System;
using System.Windows.Input;

namespace MicroRenameLogic.Commands
{
    public class ActionCommand : ICommand
    {
        #region Fields
        public event EventHandler CanExecuteChanged;
        public Action Action;
        public Func<object, bool> CanExecuteFunc;
        #endregion

        #region Ctor
        public ActionCommand(Action action, Func<object, bool> canExecute)
        {
            Action = action;
            CanExecuteFunc = canExecute;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter) => CanExecuteFunc(parameter);
        public void Execute(object parameter) => Action?.Invoke();
        public void Update(object sender = null) => CanExecuteChanged?.Invoke(sender, new EventArgs());
        #endregion
    }
}
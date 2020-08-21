using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LayotsMvvm.ViewModel.Base.OpenDialog
{
    abstract class MainOpenDialogCommand : ICommand
    {
        protected MainViewModel _mainWindowVeiwModel;

        public MainOpenDialogCommand(MainViewModel mainWindowVeiwModel)
        {
            _mainWindowVeiwModel = mainWindowVeiwModel;
        }

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}

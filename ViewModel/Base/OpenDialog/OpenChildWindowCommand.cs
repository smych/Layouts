using LayotsMvvm.ViewModel.OpenDialogViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayotsMvvm.ViewModel.Base.OpenDialog
{
    class OpenChildWindowCommand : MainOpenDialogCommand
    {
        public OpenChildWindowCommand(MainViewModel mainWindowVeiwModel) : base(mainWindowVeiwModel)
        {
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override async void Execute(object parameter)
        {
            DisplayRootRegistry displayRootRegistry = (Application.Current as App).displayRootRegistry;

            ChildWindowViewModel otherWindowViewModel = new ChildWindowViewModel();
            await displayRootRegistry.ShowModalPresentation(otherWindowViewModel);
        }
    }
}

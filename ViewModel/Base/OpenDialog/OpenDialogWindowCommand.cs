using LayotsMvvm.ViewModel.OpenDialogViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayotsMvvm.ViewModel.Base.OpenDialog
{
    class OpenDialogWindowCommand : MainOpenDialogCommand
    {
        public OpenDialogWindowCommand(MainViewModel mainWindowVeiwModel) : base(mainWindowVeiwModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override async void Execute(object parameter)
        {
            DisplayRootRegistry displayRootRegistry = (Application.Current as App).displayRootRegistry;

            DialogImageEditViewModel dialogWindowViewModel = new DialogImageEditViewModel();

            await displayRootRegistry.ShowModalPresentation(dialogWindowViewModel);
        }
    }
}

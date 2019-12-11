using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LayotsMvvm.View;
using LayotsMvvm.ViewModel;
using LayotsMvvm.ViewModel.Base;

namespace LayotsMvvm
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DisplayRootRegistry displayRootRegistry = new DisplayRootRegistry();
        //#1
        //MainWindowViewModel mainWindowViewModel;

        public App()
        {
            //#1
            //displayRootRegistry.RegisterWindowType<MainWindowViewModel, MainWindowView>();
            displayRootRegistry.RegisterWindowType<ChildWindowViewModel, ChildWindow>();
            displayRootRegistry.RegisterWindowType<DialogImageEditViewModel, DialogImageEditWindow>();
        }

        // #1
        //protected override async void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    mainWindowViewModel = new MainWindowViewModel();

        //    await displayRootRegistry.ShowModalPresentation(mainWindowViewModel);

        //    Shutdown();
        //}
    }
}

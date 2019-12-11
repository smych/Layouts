using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LayotsMvvm.Model;
using LayotsMvvm.ViewModel.Base;

namespace LayotsMvvm.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region fields
        readonly string rootPath = @"\\fs1.rc.loc\temp\prog\Image\Root";
        private ObservableCollection<ItemTreeViewModel> _treeViewItemsCollection = null;
        private ItemTreeViewModel _selectedItemTreeViewItem;
        private ICommand _selectItemChangedCommand = null;
        private FolderViewModelBase _currentFolderViewModel = null;
        ProgramListFoldersAndFiles CreatCollectionModel;

        #endregion fields

        #region constructors

        public MainViewModel()
        {
            // генерируем список моделей
            CreatCollectionModel = new ProgramListFoldersAndFiles(rootPath);

            // Инициализируем коллекцию Представления папок
            _treeViewItemsCollection = new ObservableCollection<ItemTreeViewModel>();

            // Сопостовляем с возвращенными колекцией представления "ObservableCollection<ItemViewModel>"
            _treeViewItemsCollection = CreatCollectionModel.RecursiveCreateCollectionViewModelAll();

            #region Отображение Root директории на экране

            CurrentFolderViewModel = ReturnFolderViewModel(CreatCollectionModel.rootItemViewModel);

            #endregion
        }

        #endregion

        #region Propeties
        /// <summary>
        /// Gets the current documwnt viewmodel to determine the current document view
        /// and content being shown in the document view area.
        /// 
        /// Получает текущую модель представления документа, чтобы определить текущее представление документа и содержимое, 
        /// отображаемое в области просмотра документа.
        /// </summary>

        public FolderViewModelBase CurrentFolderViewModel
        {
            get
            {
                return _currentFolderViewModel;
            }

            set
            {
                if (_currentFolderViewModel != value)
                {
                    _currentFolderViewModel = value;
                    RaisePropertyChanged(() => CurrentFolderViewModel);
                }
            }
        }

        /// <summary>
        /// Gets a command that changes the currently selected item in the tree of items.
        /// The command expects a <seealso cref="ItemTreeViewModel"/> object as parameter.
        /// </summary>
        public ICommand SelectItemChangedCommand
        {
            get
            {
                if (_selectItemChangedCommand == null)
                {
                    _selectItemChangedCommand = new RelayCommand<object>((p) =>
                    {
                        ItemTreeViewModel param = p as ItemTreeViewModel;

                        if (param != null)
                        {
                            this.SelectedItemTreeViewItem = param;
                            this.CurrentFolderViewModel = ReturnFolderViewModel(SelectedItemTreeViewItem);

                            //if (this.CurrentFolder != null)
                            //    this.DirtyFlagChangedEvent -= CurrentDocument_DirtyFlagChangedEvent;

                            //if (param.NameFolder == "Root")
                            //{
                            //    this.CurrentDocument = new RootViewModel();
                            //}
                            //else


                            //this.CurrentFolderViewModel = new FolderViewModel();

                            //// Это отображение имени текущей папки
                            //this.CurrentFolderViewModel.FolderTitle = param.NameFolder;

                            //// Это коллекция файлов текущей папки
                            //this.CurrentFolderViewModel.ChildrenFileViewModelsCollection = param.ChildrenFiles;
                            // this.CancelTreeVieSelection = this.CurrentDocument.IsDirty;
                            // this.CurrentDocument.DirtyFlagChangedEvent += CurrentDocument_DirtyFlagChangedEvent;
                        }
                    });
                }

                return _selectItemChangedCommand;
            }
        }

        /// <summary>
        /// Gets the currently selected item from the bound WPF treeview.
        /// </summary>
        public ItemTreeViewModel SelectedItemTreeViewItem
        {
            get
            {
                return _selectedItemTreeViewItem;
            }

            set
            {
                if (_selectedItemTreeViewItem != value)
                {
                    _selectedItemTreeViewItem = value;
                    RaisePropertyChanged(() => SelectedItemTreeViewItem);
                }
            }
        }

        // Преабразуем из ItemTreeViewModel в FolderViewModel
        private FolderViewModel ReturnFolderViewModel(ItemTreeViewModel _item)
        {
            if (_item == null)
            {
                System.Windows.MessageBox.Show("Вы находитесь в корневой директории");
                _item = CreatCollectionModel.rootItemViewModel;
            }

            FolderViewModel result = new FolderViewModel
            {
                FolderTitle = _item.NameFolder,
                FolderPath = _item.FolderPath,
                UpItemTreeViewFolder = _item.UpFolder,
                ChildrenFileViewModelsCollection = _item.ChildrenFiles
            };

            return result;
        }

        /// <summary>
        /// Gets a collection of <seealso cref="ItemTreeViewModel"/> objects that can
        /// be displayed in a bound treeview WPf control.
        /// </summary>
        public ObservableCollection<ItemTreeViewModel> TreeViewItems
        {
            get
            {
                return _treeViewItemsCollection;
            }
        }

        // Команда возвращаемся в корень директории
        private ICommand _rootCommand=null;
        public ICommand RootCommand
        {
            get
            {
                // если rootCommand не null
                if(_rootCommand == null)
                {
                    _rootCommand = new RelayCommand(
                        () => CurrentFolderViewModel = ReturnFolderViewModel(CreatCollectionModel.rootItemViewModel)
                    ) ; 
                }

                return _rootCommand;
            }
        }

        // На одну папку выше
        private ICommand _upFolderCommand = null;
        public ICommand UpFolderCommand
        {
            get
            {
                return _upFolderCommand ??
                    (
                        _upFolderCommand = new RelayCommand(
                            () => CurrentFolderViewModel = ReturnFolderViewModel(CurrentFolderViewModel.UpItemTreeViewFolder)));
                //() => CurrentFolderViewModel != null));
            }
        }

        #region Command OpenDialog
        //OpenDialog
        private ICommand _openChildWindow;

        private ICommand _openDialogWindow;


        // Свойства доступные только для чтения для обращения к командам и их инициализации
        public ICommand OpenChildWindow
        {
            get
            {
                if (_openChildWindow == null)
                {
                    _openChildWindow = new OpenChildWindowCommand(this);
                }
                return _openChildWindow;
            }
        }
        public ICommand OpenDialogWindow
        {
            get
            {
                if (_openDialogWindow == null)
                {

                    _openDialogWindow = new OpenDialogWindowCommand(this);
                }
                return _openDialogWindow;
            }
        }
        #endregion Command OpenDialog

        #endregion Propeties
    }

    #region Реализация интерфейса ICommand для открытия дополнительных окон в MVVM
    abstract class MyCommand : ICommand
    {
        protected MainViewModel _mainWindowVeiwModel;

        public MyCommand(MainViewModel mainWindowVeiwModel)
        {
            _mainWindowVeiwModel = mainWindowVeiwModel;
        }

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }

    class OpenChildWindowCommand : MyCommand
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

    class OpenDialogWindowCommand : MyCommand
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

    #endregion Реализация интерфейса ICommand для открытия дополнительных окон в MVVM

}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using LayotsMvvm.Model;
using LayotsMvvm.ViewModel.Base;
using LayotsMvvm.ViewModel.Base.Dialog.OpenDialog;

namespace LayotsMvvm.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        // folder root
        string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);


        //Получает текущий рабочий каталог приложения - Debug
        private string rootD = Directory.GetCurrentDirectory();

        readonly string rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\RootFolder";
        private ObservableCollection<ItemTreeViewModel> _treeViewItemsCollection = null;
        private ItemTreeViewModel _selectedItemTreeViewItem;
        private ICommand _selectItemChangedCommand = null;
        private FolderViewModelBase _currentFolderViewModel = null;
        private ProgramListFoldersAndFiles CreatCollectionModel;

        #endregion fields

        #region Constructors

        public MainViewModel()
        {
            // Необходимо для работы с диалоговыми окнами
            ReturnMainViewModel.GetMainViewModel = this;

            // генерируем список моделей
            CreatCollectionModel = new ProgramListFoldersAndFiles(rootPath);

            // Инициализируем коллекцию Представления папок
            _treeViewItemsCollection = new ObservableCollection<ItemTreeViewModel>();

            // Сопостовляем с возвращенными колекцией представления "ObservableCollection<ItemViewModel>"
            _treeViewItemsCollection = CreatCollectionModel.RecursiveCreateCollectionViewModelAll();

            #region Отображение Root директории на экране
            CurrentFolderViewModel = Auxiliary.ReturnFolderViewModel(CreatCollectionModel.rootItemViewModel);

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
                    GetFolderUpCollection = Auxiliary.ReturnFolderList(CurrentFolderViewModel as FolderViewModel);
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
                            FolderViewModel FolderTemp = Auxiliary.ReturnFolderViewModel(SelectedItemTreeViewItem);
                            this.CurrentFolderViewModel = FolderTemp;
                            // this.GetFolderUpCollection = Auxiliary.ReturnFolderList(FolderTemp);
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

            set
            {
                if (_treeViewItemsCollection != value)
                {
                    _treeViewItemsCollection = value;
                    RaisePropertyChanged(() => TreeViewItems);
                }
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
                        () => CurrentFolderViewModel = Auxiliary.ReturnFolderViewModel(CreatCollectionModel.rootItemViewModel)
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
                            () => CurrentFolderViewModel = Auxiliary.ReturnFolderViewModel(CurrentFolderViewModel.UpItemTreeViewFolder)));
                //() => CurrentFolderViewModel != null));
            }
        }

        // На одну папку выше
        private ICommand _uppFolderCommand = null;
        public ICommand UppFolderCommand
        {
            get
            {
                return _uppFolderCommand ??
                    (
                        _uppFolderCommand = new RelayCommand(
                            () => CurrentFolderViewModel = Auxiliary.ReturnFolderViewModel(CurrentFolderViewModel.UpItemTreeViewFolder)));
                //() => CurrentFolderViewModel != null));
            }
        }

        #region
        private List<FolderViewModel> _getFolderUpCollection = null;
        public List<FolderViewModel> GetFolderUpCollection
        {
            get => _getFolderUpCollection;
            set
            {
                if (_getFolderUpCollection != value)
                {
                    _getFolderUpCollection = value;
                    RaisePropertyChanged(() => GetFolderUpCollection);
                }
            }
        }
        #endregion

        #region region Реализация интерфейса ICommand для открытия дополнительных окон в MVVM

        private ICommand _openDialogWindow;


        // Свойства доступные только для чтения для обращения к командам и их инициализации
        //public ICommand OpenChildWindow
        //{
        //    get
        //    {
        //        if (_openChildWindow == null)
        //        {
        //            _openChildWindow = new OpenChildWindowCommand(this);
        //        }
        //        return _openChildWindow;
        //    }
        //}
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
        #endregion Реализация интерфейса ICommand для открытия дополнительных окон в MVVM


        #endregion
    }
}

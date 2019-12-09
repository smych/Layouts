   using System.Collections.ObjectModel;
using System.Windows.Input;
using ver01_TreeView.Model;
using ver01_TreeView.ViewModel.Base;

namespace ver01_TreeView.ViewModel
{
    public class MainViewModel : Base.ViewModelBase
    {
        #region fields
        //private readonly ItemTreeViewModel RootTreeViewModel;
        readonly string rootPath = @"\\fs1.rc.loc\temp\prog\Image\Root";
        private ObservableCollection<ItemTreeViewModel> _treeViewItemsCollection = null;
        private ItemTreeViewModel _selectedItemTreeViewItem;
        private ICommand _selectItemChangedCommand = null;
        private FolderViewModelBase _currentFolderViewModel = null;
        ProgramListFoldersAndFiles CreatCollectionModel;
        //private FileViewModel _selectedFile;
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
                    );
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
                if (_upFolderCommand == null)
                {
                // Если null значет вы в корневой директории  
                
                    _upFolderCommand = new RelayCommand(() =>
                        CurrentFolderViewModel = ReturnFolderViewModel(CurrentFolderViewModel.UpItemTreeViewFolder)
                    );
                
                }
               
                return _upFolderCommand;
            }
        }

        #endregion Propeties
    }
}

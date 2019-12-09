using System.Collections.ObjectModel;

namespace ver01_TreeView.ViewModel
{

    // <summary>
    /// Implements a viewmodel object that binds to an element in a collection that can be bound to treeview.
    /// Реализует объект viewmodel, который привязывается к элементу в коллекции, который может быть привязан к древовидной структуре.
    /// </summary>

    public class ItemTreeViewModel : Base.ViewModelBase
    {
        private ItemTreeViewModel _upFolder = null;
        private ObservableCollection<ItemTreeViewModel> _childrenFolders = null;
        private ObservableCollection<FileViewModel> _childrenFiles = null;
        private string _nameFolder;
        private string _folderPatch;
        private bool _isSelected;
        private bool _isExpanded;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="name"></param>
        /// 
        public ItemTreeViewModel()
        { 
        
        }

        public ItemTreeViewModel(string _nameFolder, string _fPach)
        {
            _childrenFolders = new ObservableCollection<ItemTreeViewModel>();
            _childrenFiles = new ObservableCollection<FileViewModel>();
            this._nameFolder = _nameFolder;
            this._folderPatch = _fPach;
        }

        public ItemTreeViewModel(ItemTreeViewModel _upFolder, string _nameFolder, string _fPach)
        {
            _childrenFolders = new ObservableCollection<ItemTreeViewModel>();
            _childrenFiles = new ObservableCollection<FileViewModel>();
            this._upFolder = _upFolder;
            this._nameFolder = _nameFolder;
            this._folderPatch = _fPach;
        }

        /// <summary>
        /// Gets a propety that holds all the children of this trre view item.
        /// </summary>
        public ObservableCollection<ItemTreeViewModel> ChildrenFolders
        {
            get
            {
                return _childrenFolders;
            }
        }

        /// <summary>
        /// Gets a propety that holds all the childrenFiles of this trre view item.
        /// </summary>

        public ObservableCollection<FileViewModel> ChildrenFiles
        {
            get
            {
                return _childrenFiles;
            }
        }

        /// <summary>
        /// Gets a name to display a human readable name for this item.
        /// </summary>
        public string NameFolder
        {
            get
            {
                return _nameFolder;
            }
        }

        public string FolderPath
        {
            get { return _folderPatch; }
        }

        public ItemTreeViewModel UpFolder
        {
            get { return _upFolder; }
        }

        /// <summary>
        /// Gets/sets a proprrty to determine whether this item is currently selected or not.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
            }
        }

        /// <summary>
        /// Gets/sets a proprrty to determine whether this item is currently expanded or not.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    RaisePropertyChanged(() => IsExpanded);
                }
            }
        }


    }
}

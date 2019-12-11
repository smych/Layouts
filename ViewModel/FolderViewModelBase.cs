using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayotsMvvm.ViewModel
{
    /// <summary>
    /// Class implements document base functions which are properties and methods that all documents support.
    /// Класс реализует базовые функции Folder, которые являются свойствами и методами, которые поддерживаются всеми документами.
    /// </summary>

    public class FolderViewModelBase : Base.ViewModelBase
    {
        #region fields

        private string _folderTitle;
        private string _folderPath;
        private ItemTreeViewModel _upItemTreeViewFolder;
        private ObservableCollection<FileViewModel> _childrenFiles;

        #endregion fields

        #region cosntructors

        /// <summary>
        /// Class constructor
        /// </summary>
        public FolderViewModelBase()
        {
        
        }

        #endregion cosntructors

        #region properties
        /// <summary>
        /// Gets the title of a document to display the information in the UI.
        /// </summary>
        public string FolderTitle
        {
            get
            {
                return _folderTitle;
            }

            set
            {
                if (_folderTitle != value)
                {
                    _folderTitle = value;
                    RaisePropertyChanged(() => FolderTitle);
                }
            }
        }

        public ObservableCollection<FileViewModel> ChildrenFileViewModelsCollection
        {
            get
            {
                return _childrenFiles;
            }

            set
            {
                if (_childrenFiles != value)
                {
                    _childrenFiles = value;
                    RaisePropertyChanged(() => ChildrenFileViewModelsCollection);
                }
            }
        }

        // Выбор файла изображения
        private FileViewModel _selectedFile;
        public FileViewModel SelectedFile
        {
            get
            {
                return _selectedFile;
            }

            set
            {
                if (_selectedFile != value)
                {
                    _selectedFile = value;
                    RaisePropertyChanged(() => SelectedFile);
                }
            }
        }

        public string FolderPath
        {
            get
            {
                return _folderPath;
            }

            set
            {
                if (_folderPath != value)
                {
                    _folderPath = value;
                    RaisePropertyChanged(() => FolderPath);
                }
            }
        }

        public ItemTreeViewModel UpItemTreeViewFolder
        {
            get
            {
                return _upItemTreeViewFolder;
            }

            set
            {
                if (_upItemTreeViewFolder != value)
                {
                    _upItemTreeViewFolder = value;
                    RaisePropertyChanged(() => UpItemTreeViewFolder);
                }
            }
        }

        #endregion

    }
}

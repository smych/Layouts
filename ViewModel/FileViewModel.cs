using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver01_TreeView.ViewModel
{
    public class FileViewModel : Base.ViewModelBase
    {
        #region fields

        private string _fileTitle;
        private string _filePath;
        //private bool _isSelected;

        #endregion fields

        #region cosntructors

        /// <summary>
        /// Class constructor
        /// </summary>
        public FileViewModel()
        {

        }

        #endregion cosntructors

        #region properties
        /// <summary>
        /// Gets the title of a document to display the information in the UI.
        /// </summary>
        public string FileTitle
        {
            get
            {
                return _fileTitle;
            }

            set
            {
                if (_fileTitle != value)
                {
                    _fileTitle = value;
                    RaisePropertyChanged(() => FileTitle);
                }
            }
        }

        /// <summary>
        /// Нахождение файла изображения
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    RaisePropertyChanged(() => FilePath);
                }
            }
        }

        //public bool IsSelected
        //{
        //    get
        //    {
        //        return _isSelected;
        //    }

        //    set
        //    {
        //        if (_isSelected != value)
        //        {
        //            _isSelected = value;
        //            RaisePropertyChanged(() => IsSelected);
        //        }
        //    }
        //}

        #endregion

    }
}

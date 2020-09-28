using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayotsMvvm.ViewModel
{
    public class FileViewModel : DefaultFileViewModel
    {
        #region fields

        private string _getFileTitle;
        private string _getFilePath;
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
        public override string GetFileTitle
        {
            get => _getFileTitle;
            set
            {
                if (_getFileTitle != value)
                {
                    _getFileTitle = value;
                    RaisePropertyChanged(() => GetFileTitle);
                }
            }
        }

        /// <summary>
        /// Нахождение файла изображения
        /// </summary>
        public override string GetFilePath
        {
            get => _getFilePath;

            set
            {
                if (_getFilePath != value)
                {
                    _getFilePath = value;
                    RaisePropertyChanged(() => GetFilePath);
                }
            }
        }

        #endregion

        #region MainViewModel
        // Для диалогового окна
        public static MainViewModel GetMainViewModel { get; set; }

        #endregion
    }
}

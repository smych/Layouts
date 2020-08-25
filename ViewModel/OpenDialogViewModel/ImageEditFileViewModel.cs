using LayotsMvvm.ViewModel.Base.ImageConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayotsMvvm.ViewModel.OpenDialogViewModel
{
    public class ImageEditFileViewModel : FileViewModel
    {
        #region Path SaveFile

        private string _getSaveFilePath = null;
        public string GetSaveFilePath
        {
            get => _getSaveFilePath;
            set
            {
                if (_getSaveFilePath != value)
                {
                    _getSaveFilePath = value;
                    RaisePropertyChanged(() => GetSaveFilePath);
                }
            }
        }

        #endregion

        #region GetFilePath

        private string _getFPath = null;

        public override string GetFilePath 
        { 
            get => _getFPath; 
            set
            {
                if (_getFPath != value)
                {
                    _getFPath = value;
                    RaisePropertyChanged(() => GetFilePath);
                    GetInfoFile = new FInfo(GetFilePath);
                    if (GetFileViewModel.GetFilePath != this.GetFilePath)
                    {
                        GetIsEqualDefault = true;
                    }
                }
            } 
        }

        #endregion

        #region Path ReplaseFile

        //private string _getReplaseFilePath = null;
        //public string GetReplaseFilePath
        //{
        //    get => _getReplaseFilePath;
        //    set
        //    {
        //        if (_getReplaseFilePath != value)
        //        {
        //            _getReplaseFilePath = value;
        //            RaisePropertyChanged(() => GetReplaseFilePath);
        //        }
        //    }
        //}

        #endregion

        #region Path MainFile

        //private string _getMainFilePath = null;
        //public string GetMainFilePath
        //{
        //    get => _getMainFilePath;
        //    set
        //    {
        //        if (_getMainFilePath != value)
        //        {
        //            _getMainFilePath = value;
        //            RaisePropertyChanged(() => GetMainFilePath);
        //            GetInfoFile = new FInfo(_getMainFilePath);
        //        }
        //    }
        //}

        #endregion

        #region FileInfo

        private FInfo _getInfoFile = null;
        public FInfo GetInfoFile
        {
            get => _getInfoFile;
            set
            {
                if (_getInfoFile != value)
                {
                    _getInfoFile = value;
                    RaisePropertyChanged(() => GetInfoFile);
                }
            }
        }

        public FileViewModel GetFileViewModel { get; set; }
        
        #region bool - Если путь к файлу изменился относительно "по умолчанию"

        private bool _getIsEqual = false;
        public bool GetIsEqualDefault
        {
            get => _getIsEqual;
            set
            {
                if (_getIsEqual != value)
                {
                    _getIsEqual = value;
                    RaisePropertyChanged(() => GetIsEqualDefault);
                }
            }
        }

        #endregion bool

        #endregion
    }
}

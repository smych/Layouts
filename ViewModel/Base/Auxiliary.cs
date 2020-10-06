using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayotsMvvm.ViewModel.Base
{
    static class Auxiliary
    {
        #region  Преабразуем из ItemTreeViewModel в FolderViewModel
        public static FolderViewModel ReturnFolderViewModel(ItemTreeViewModel _item)
        {
            if (_item == null)
            {
                //System.Windows.MessageBox.Show("Вы находитесь в корневой директории");
                // _item = CreatCollectionModel.rootItemViewModel;
                return null;
            }

            FolderViewModel result = new FolderViewModel
            {
                FolderTitle = _item.NameFolder,
                FolderPath = _item.FolderPath,
                UpItemTreeViewFolder = _item.UpFolder,
                ChildrenFileViewModelsCollection = _item.ChildrenFiles
            };


            // Необходим для окрытия диалоговых окон
            //if (FolderViewModel.GetMainViewModel == null)
            //{
            //    FolderViewModel.GetMainViewModel = this;
            //}

            return result;
        }
        #endregion

        #region
        public static List<FolderViewModel> ListFolderReturn(FolderViewModel _folder)
        {
            if (_folder == null)
            {
                return null;
            }

            // List<int> a = new List<int>();
            List<FolderViewModel> _temp = new List<FolderViewModel>();

            do
            {

                _temp.Add(_folder);
                _folder = ReturnFolderViewModel(_folder.UpItemTreeViewFolder);

            }
            while (_folder != null);

            _temp.Reverse();

            return _temp;
        }

        #endregion
    }
}

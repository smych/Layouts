using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver01_TreeView.ViewModel;

namespace ver01_TreeView.Model
{
    public class ProgramListFoldersAndFiles
    {
        #region fields

        private readonly string[] File_Exclusion = new string[] { "Thumbs.db" };
        private readonly string[] AllowedFile = new string[] { "png", "jpg", "wmf" };

        //private ObservableCollection<FolderViewModel> mFoldersTreeViewItems = null;

        #endregion

        #region Get
        private FolderViewModel GetCurentItem { get; set; } 
        private Folder rootFolderObj { get; set; }
        private Folder GetCurentFolderObj { get ; set ; }
        private File GetFileObj { get; set; }

        #endregion

        #region Main Constructor
        public ProgramListFoldersAndFiles(string _patch)
        {
            if (rootFolderObj == null)
            {
                // Создаем начальный объект
                rootFolderObj = new Folder() { FolderName = NoDirPatch(_patch), FullPatch = _patch };

                // делаем начальный объект текущим
                GetCurentFolderObj = rootFolderObj;

                // Запускаем процесс генерации Полной коллекции каталогов и файлов
                LookFullCollectionAllItems(GetCurentFolderObj);
            }
        }
        #endregion

        #region Прохождение по всем папкам (полный цикл от начало до конца)
        void LookFullCollectionAllItems(Folder _nFolder)
        {
            if (_nFolder.ChildFoldersList.Count == 0)
            {
                LookAndCreatingCollection(_nFolder);
            }

            foreach (var ItemChildFolder in _nFolder.ChildFoldersList)
            {
                if (ItemChildFolder is Folder)
                {
                    LookFullCollectionAllItems(ItemChildFolder);
                }
            }
        }

        #endregion

        #region Метод Просаматра и генерации коллекции папок и файлов в текущем конкретном каталоге

        /// Метод прохождения по переданному _patch директории
        /// и создания на основе существующих в ней папок коллекции

        private void LookAndCreatingCollection(Folder _nFolder)
        {
            // выесняем существует ли каталог
            if (System.IO.Directory.Exists(_nFolder.FullPatch))
            {
                string[] dirs = System.IO.Directory.GetDirectories(_nFolder.FullPatch);
                foreach (string curDir in dirs)
                {
                    Folder ChildFolder = new Folder() { FolderName = NoDirPatch(curDir), FullPatch = curDir, OldLinkObj = _nFolder };
                    _nFolder.ChildFoldersList.Add(ChildFolder);
                }

                // выесняем существует ли файл
                //if (System.IO.File.Exists(_nFolder.FullPatch))
                //{

                string[] files = System.IO.Directory.GetFiles(_nFolder.FullPatch);

                    foreach (string curFile in files)
                    {
                        foreach (var s in File_Exclusion)
                        {
                            if (s != NoDirPatch(curFile))
                            {
                                File ChildFile = new File() { FileName = NoDirPatch(curFile), FilePath = curFile };
                                _nFolder.ChildFilesList.Add(ChildFile);
                            }
                        }
                    }
                //}
            }
        }

        #endregion

        // обрезка до имен(файлов, папки)
        private string NoDirPatch(string _fullPatchFile)
        {
            // ищем до символа
            return _fullPatchFile.Substring(_fullPatchFile.LastIndexOf("\\") + 1);
        }

        #region Преобразуем из "Модели" в "Модель представления"

        private ObservableCollection<ItemTreeViewModel> mItemTreeViewItems = null;

        public ItemTreeViewModel rootItemViewModel;
        //FolderViewModel GetCurentItemViewModel { get; set; }

        // Возвращаем полную коллекцию FolderViewModel
        public ObservableCollection<ItemTreeViewModel> RecursiveCreateCollectionViewModelAll()
        {
            // Инициализируем экземпляр
            rootItemViewModel = new ItemTreeViewModel(rootFolderObj.FolderName, rootFolderObj.FullPatch);

            // Сопостовляем с root
            //rootItemViewModel = GetCurentItemViewModel;

            // Инициализируем коллекцию
            mItemTreeViewItems = new ObservableCollection<ItemTreeViewModel>();

            // запускаем рекурсию создании полной коллекции 
            RecursiveCollectionViewFolder(rootItemViewModel, rootFolderObj);

            // Добовляем в коллекцию Item from Root

            foreach (var _n in rootItemViewModel.ChildrenFolders)
            {
                mItemTreeViewItems.Add(_n);
            }
            
            // Возвращаем коллекцию
            return mItemTreeViewItems;
        }

        void RecursiveCollectionViewFolder(ItemTreeViewModel _nItemViewModel, Folder _nFolder)
        {
            // Добавляем в коллекцию "ItemTreeViewModel ChildrenFolders" наши папки
            foreach (Folder ChildFolder in _nFolder.ChildFoldersList)
            {
                // Есть ли есть папка, значит создаем предстовление этой папки, инициализируем его
                ItemTreeViewModel tempItemFolderViewModel = new ItemTreeViewModel(_nItemViewModel, ChildFolder.FolderName, ChildFolder.FullPatch);


                // Заносим в корневой экземпляр текущий tempItemViewModel
                _nItemViewModel.ChildrenFolders.Add(tempItemFolderViewModel);

                // Запускаем рекурсию
                RecursiveCollectionViewFolder(tempItemFolderViewModel, ChildFolder);
            }

            //добавить коллекцию файлов
            foreach (File itemFile in _nFolder.ChildFilesList)
            {
                _nItemViewModel.ChildrenFiles.Add(new FileViewModel() { FileTitle = itemFile.FileName, FilePath = itemFile.FilePath} );
            }
        }


        #endregion
    }
}

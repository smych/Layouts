using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ver01_TreeView.Model
{
    public class Folder
    {
        public string FolderName { get; set; }
        public string FullPatch { get; set; }

        // ссылка на уровень выше
        public Folder OldLinkObj { get; set; }

        #region Child Folders List
        List<Folder> _childFolderList;
        public List<Folder> ChildFoldersList
        {
            get
            {
                if (this._childFolderList == default)
                {
                    this._childFolderList = new List<Folder>();
                }

                return _childFolderList;
            }
        }
        #endregion

        #region Child Files List
        List<File> _childFilesList;
        public List<File> ChildFilesList
        {
            get
            {
                if (this._childFilesList == default)
                {
                    this._childFilesList = new List<File>();
                }

                return this._childFilesList;
            }
        }
        #endregion Child Files List
    }
}

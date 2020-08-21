using LayotsMvvm.ViewModel.Base;
using LayotsMvvm.ViewModel.Base.Dialog.ImageDialog;
using LayotsMvvm.ViewModel.Base.ImageConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LayotsMvvm.ViewModel.OpenDialogViewModel
{
    class DialogImageEditViewModel : ViewModelBase
    {
        #region Fields

        FileViewModel _getFileImage = null;
        public FileViewModel GetFileImage 
        {
            get => _getFileImage;
            set 
            {
                if (_getFileImage != value)
                {
                    _getFileImage = value;
                    RaisePropertyChanged(() => GetFileImage);
                }
            } 
        }

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

        #region FileInfo

        private FInfo _getFile = null;
        public FInfo GetFile
        {
            get
            {
                return _getFile;
            }
            set
            {
                if (_getFile != value)
                {
                    _getFile = value;
                    RaisePropertyChanged(() => GetFile);
                }
            }
        }

        #endregion

        #endregion Fields

        public DialogImageEditViewModel()
        {

        }

        public DialogImageEditViewModel(FileViewModel _image)
        {
            GetFileImage = _image;
        }

        #region DragAndDrop
        public ICommand DandCommand { get; set; }

        private void ExecuteDandCommand(object obj)
        {
            if (obj != null)
            {
                IDataObject ido = obj as IDataObject;
                if (ido != null)
                {
                    var fileDrop = ido.GetData(DataFormats.FileDrop, true);
                    var filesOrDirectories = fileDrop as String[];
                    if (filesOrDirectories != null && filesOrDirectories.Length > 0)
                    {
                        foreach (string fullPath in filesOrDirectories)
                        {
                            this.GetFileImage.FilePath = fullPath;
                            //if (Directory.Exists(fullPath))
                            //{
                            //    Console.WriteLine(@"{0} is a directory", fullPath);
                            //}
                            //else if (File.Exists(fullPath))
                            //{
                            //    this.GetCurentFilePath = fullPath;
                            //}
                            //else
                            //{
                            //    MessageBox.Show($"Файл не найден: '{fullPath}' \n ! Исправте путь к файлу !");
                            //}
                        }
                    }
                }
            }
        }
        private bool CanExecuteDandCommand(object obj)
        {
            return true;
        }

        #endregion DragAndDrob

        //Интерфейс диалог сервис
        private IImageDialogService dialogService;

        // команда Сохранить как
        private RelayCommand<object> saveCommand = null;
        public RelayCommand<object> SaveCommand => saveCommand ?? (saveCommand = new RelayCommand<object>(obj =>
        {
            try
            {
                BitmapImage TempBitmapImage = obj as BitmapImage;
                ConvertFsStream.SaveFromBitmapImageToFileFormatPNG(TempBitmapImage, GetFileImage.FilePath);
                GetSaveFilePath = GetFileImage.FilePath;
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }));

        // команда Сохранить как
        private RelayCommand<object> saveAsCommand = null;
        public RelayCommand<object> SaveAsCommand => saveAsCommand ?? (saveAsCommand = new RelayCommand<object>(obj =>
        {
            try
            {
                if (dialogService.SaveAsFileDialog(obj as BitmapImage) == true)
                {
                    GetSaveFilePath = dialogService.FilePath;
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }));

        // команда открытия файла
        private RelayCommand<object> openCommand;
        public RelayCommand<object> OpenCommand => openCommand ?? (openCommand = new RelayCommand<object>(obj =>
        {
            try
            {
                if (dialogService.OpenFileDialog() == true)
                {
                    GetFileImage.FilePath = dialogService.FilePath;
                    //dialogService.ShowMessage($"Файл открыт: '{dialogService.FilePath}'");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }));
    }
}

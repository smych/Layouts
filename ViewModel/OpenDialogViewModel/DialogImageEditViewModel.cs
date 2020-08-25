using LayotsMvvm.ViewModel.Base;
using LayotsMvvm.ViewModel.Base.Dialog.ImageDialog;
using LayotsMvvm.ViewModel.Base.ImageConverter;
using System;
using System.Collections.Generic;
using System.IO;
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

        private FileViewModel _getCurentObjectFileImage = null;
        public FileViewModel GetCurentObjectFileImage 
        {
            get => _getCurentObjectFileImage;
            set 
            {
                if (_getCurentObjectFileImage != value)
                {
                    _getCurentObjectFileImage = value;
                    //GetInfoFile = new FInfo(_getCurentObjectFileImage.GetFilePath);
                    RaisePropertyChanged(() => GetCurentObjectFileImage);
                }
            } 
        }

        private ImageEditFileViewModel _getCurentEditFileImage = null;
        public ImageEditFileViewModel GetCurentEditFileImage
        {
            get => _getCurentEditFileImage;
            set
            {
                if (_getCurentEditFileImage != value)
                {
                    _getCurentEditFileImage = value;
                    RaisePropertyChanged(() => GetCurentEditFileImage);
                }
            }
        }

        #endregion Fields

        public DialogImageEditViewModel()
        {
            //DragAndDrop
            this.DandCommand = new RelayCommand<object>(ExecuteDandCommand, CanExecuteDandCommand);

            // DialogService
            this.dialogService = new ImageDefaultDialogService();

            GetCurentEditFileImage = new ImageEditFileViewModel();
        }

        public DialogImageEditViewModel(FileViewModel _image) : this()
        {
            // Назначаем текущий рисунок
            GetCurentObjectFileImage = _image;
            GetCurentEditFileImage = RestoreDefaultFile(_image);
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
                            //this.GetCurentObjectFileImage.GetFilePath = fullPath;

                            if (File.Exists(fullPath))
                            {
                                GetCurentEditFileImage.GetFilePath = fullPath;
                            }
                            else
                            {
                                MessageBox.Show($"Файл не найден: '{fullPath}' \n ! Исправте путь к файлу !");
                            }

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

        // команда Сохранить
        private RelayCommand<object> saveCommand = null;
        public RelayCommand<object> SaveCommand => saveCommand ?? (saveCommand = new RelayCommand<object>(obj =>
        {
            try
            {
                BitmapImage TempBitmapImage = obj as BitmapImage;
                ConvertFsStream.SaveFromBitmapImageToFileFormatPNG(TempBitmapImage, GetCurentObjectFileImage.GetFilePath);
                GetCurentEditFileImage.GetSaveFilePath = GetCurentObjectFileImage.GetFilePath;
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
                    GetCurentEditFileImage.GetSaveFilePath = dialogService.FilePath;
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
                    GetCurentObjectFileImage.GetFilePath = dialogService.FilePath;
                    //dialogService.ShowMessage($"Файл открыт: '{dialogService.FilePath}'");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }));

        // команда Вернуть по умолчанию
        private RelayCommand<object> _returnDefaultFile = null;
        public RelayCommand<object> ReturnDefaultFile => _returnDefaultFile ?? (_returnDefaultFile = new RelayCommand<object>(obj =>
        {
            try
            {
                GetCurentEditFileImage = RestoreDefaultFile(GetCurentObjectFileImage);
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }));

        // Вернуть начальное отображение FileViewModel
        ImageEditFileViewModel RestoreDefaultFile(FileViewModel _image)
        {
            var temp = new ImageEditFileViewModel();
            temp.GetFileViewModel = _image;
            temp.GetFilePath = _image.GetFilePath;
            temp.GetFileTitle = _image.GetFileTitle;
            temp.GetIsEqualDefault = false;
            return temp;            
        }
    }
}

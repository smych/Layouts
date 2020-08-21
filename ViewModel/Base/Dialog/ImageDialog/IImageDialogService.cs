using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LayotsMvvm.ViewModel.Base.Dialog.ImageDialog
{
    public interface IImageDialogService
    {
        BitmapImage SourceImage { get; set; } // текущий (активный) Image
        string FilePath { get; set; }   // путь к выбранному файлу
       
        bool OpenFileDialog();  // открытие файла

        //void SaveFileDialog(string _path, Image _img);  // сохранение файла
        bool SaveAsFileDialog(BitmapImage _img);  // сохранить файл как

        void ShowMessage(string message);   // показ сообщения
    }
}

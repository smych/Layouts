using LayotsMvvm.ViewModel.Base.ImageConverter;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LayotsMvvm.ViewModel.Base.Dialog.ImageDialog
{
    public class ImageDefaultDialogService :  IImageDialogService
    {
        #region fields
        public string FilePath { get; set; }
        public BitmapImage SourceImage { get; set; }
        #endregion

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Загрузка изображения";
            openFileDialog.DefaultExt = "JPEG(*.jpg; *.jpeg)| *.jpg";
            openFileDialog.Filter = "Все поддерживаемые форматы | *.png; *.jpg; *.jpeg; *.bmp; *.gif";

            List<string> allowableFileTypes = new List<string>();
            allowableFileTypes.AddRange(new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" });

            if (openFileDialog.ShowDialog() == true)
            {
                if (!openFileDialog.Equals(string.Empty))
                {
                    FileInfo f = new FileInfo(openFileDialog.FileName);

                    if (allowableFileTypes.Contains(f.Extension.ToLower()))
                    {
                        // Место положение открытого файла
                        FilePath = openFileDialog.FileName;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Этот формат файла не поддерживается!");
                    }
                }
                else
                {
                    MessageBox.Show("Укажите файл для открытия!");
                }
            }

            return false;
        }

        // Сохранить файл
        //public void SaveFileDialog(string _path, Image _img)
        //{
        //    FilePath = string.Empty;

        //    SaveImage.SaveImageTofile(_path, _img);
        //    FilePath = _path;
        //}

        // Сахранить 

        public bool SaveAsFileDialog(BitmapImage _img)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Сохранение изображения";
            saveFileDialog.Filter = "Все поддерживаемые форматы|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
            saveFileDialog.DefaultExt = "JPEG(*.jpg; *.jpeg)| *.jpg";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;

                //Опредиляем расширение файла
                int iTemp = saveFileDialog.FileName.LastIndexOf(@".");
                string strTemp = saveFileDialog.FileName.Substring(++iTemp);

                if (strTemp == "png")
                {
                    ConvertFsStream.SaveFromBitmapImageToFileFormatPNG(_img, FilePath);
                }
                else if (strTemp == "jpg" || strTemp == "jpeg") // 
                {
                    var BitMapTemp = ConvertFsStream.FromBitmapImageToBitmap(_img);
                    ConvertFsStream.FromBitmapSaveJPGtoPath(BitMapTemp, FilePath);
                }
                //else if(saveFileDialog.SafeFileName)
                else
                {
                    FilePath = "Выберите формат файла png либо jpeg";
                    return false;
                }

                // Место положение сохраненного файла
                FilePath = saveFileDialog.FileName;

                return true;
            }

            FilePath = "Файл не сохранен!";
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

    }
}

using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace LayotsMvvm.ViewModel.Base.ImageConverter
{
    public static class ConvertFsStream
    {
        // MemoryStream to File
        public static void MemoryToFile(string _path)
        {
            MemoryStream ms = new MemoryStream();

            using (FileStream file = new FileStream(_path, FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[ms.Length];
                ms.Read(bytes, 0, (int)ms.Length);
                file.Write(bytes, 0, bytes.Length);
                ms.Close();
            }
        }

        // File to MemoryStream


        //file => FileStream => MemoryStream
        public static MemoryStream FileToMs(string _path)
        {
            MemoryStream ms = new MemoryStream();
            using (FileStream file = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                file.CopyTo(ms);
            }

            return ms;
        }

        #region FromImageToByte 
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        #endregion

        #region FromBitMapImageToByte ?
        public static Byte[] BitMapImageToByteArray(BitmapImage imageSource)
        {
            Stream stream = imageSource.StreamSource;
            Byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }

            return buffer;
        }

        #endregion

        #region FromByteToBitmap

        public static BitmapImage FromByteToBitmapImage(byte[] _byte)
        {
            var imageSource = new BitmapImage();

            using (MemoryStream memoryStream = new MemoryStream(_byte))
            {
                imageSource.BeginInit();
                imageSource.StreamSource = memoryStream;
                imageSource.EndInit();

                // Assign the Source property of your image
                //image.Source = imageSource;
            }

            return imageSource;
        }

        #endregion

        #region ConvertToJpg

        public static Bitmap FromPathToBitMap(string _path)
        {
            using (Stream BitmapStream = System.IO.File.Open(_path, System.IO.FileMode.Open))
            {
                Image img = Image.FromStream(BitmapStream);
                return new Bitmap(img);
            }
        }

        public static void FromBitmapSaveJPGtoPath(Bitmap bmp, string filename)
        {
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            bmp.Save(filename, GetEncoder(ImageFormat.Jpeg), encoderParameters);
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        public static Bitmap FromBitmapImageToBitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        #endregion

        #region SaveBitMap Encoder

        public static void SaveImageTofile(string _SaveToFile, Image _fromImage)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(FromImageToBitMapImage(_fromImage)));

            using (var fileStream = new System.IO.FileStream(_SaveToFile, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        #endregion

        #region Convert from Image to BitMapimage
        public static BitmapImage FromImageToBitMapImage(Image img)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        #endregion

        

        #region FromBitmapToByte
        public static byte[] FromBitmapToByte(BitmapImage _bm)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_bm));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
        #endregion

        #region from Web Stream bitmapImage, Convert to Stream, convert local bitmapImage

            #region ConvertFromHttpToBitmapImage
            //public static BitmapImage MemberImage(BitmapImage Member)
            //{
            //    get
            //    {
            //        var image = new BitmapImage();

            //        if (Member != null)
            //        {
            //            WebRequest request = WebRequest.Create(new Uri(Member. .ImageFilePath, UriKind.Absolute));
            //            request.Timeout = -1;
            //            WebResponse response = request.GetResponse();
            //            Stream responseStream = response.GetResponseStream();
            //            BinaryReader reader = new BinaryReader(responseStream);
            //            MemoryStream memoryStream = new MemoryStream();

            //            byte[] bytebuffer = new byte[BytesToRead];
            //            int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

            //            while (bytesRead > 0)
            //            {
            //                memoryStream.Write(bytebuffer, 0, bytesRead);
            //                bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
            //            }

            //            image.BeginInit();
            //            memoryStream.Seek(0, SeekOrigin.Begin);

            //            image.StreamSource = memoryStream;
            //            image.EndInit();
            //        }

            //        return image;
            //    }
            //}
            #endregion

        public static BitmapImage Image_http_Loaded(Uri _uri) // (string _str)
        {
            var BitmapImageTemp = new System.Windows.Media.Imaging.BitmapImage();
            int BytesToRead = 100;

            WebRequest request = WebRequest.Create(_uri); // (new Uri(_str, UriKind.Absolute))
            request.Timeout = -1;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            BinaryReader reader = new BinaryReader(responseStream);
            MemoryStream memoryStream = new MemoryStream();

            byte[] bytebuffer = new byte[BytesToRead];
            int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

            while (bytesRead > 0)
            {
                memoryStream.Write(bytebuffer, 0, bytesRead);
                bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
            }

            BitmapImageTemp.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);

            BitmapImageTemp.StreamSource = memoryStream;
            BitmapImageTemp.EndInit();

            return BitmapImageTemp;
        }
        #endregion

        #region Save BitmapImage obj to path

        public static void SaveFromBitmapImageToFileFormatPNG(this BitmapImage image, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        #endregion

        #region Resize Image
        // Resize image and if width and height are different ratio, keep it in center.

        public static Bitmap Resize(Image image, int width, int height)
        {

            int drawWidth = width;
            int drawHeight = height;
            if (width != height)
            {
                drawWidth = Math.Min(width, height);
                drawHeight = drawWidth;
            }

            var destRect = new Rectangle((width - drawWidth) / 2, (height - drawHeight) / 2, drawWidth, drawHeight);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        #endregion

    }

    public static class ConvertBitmapToBitmapImage
    {
        /// <summary>
        /// Takes a bitmap and converts it to an image that can be handled by WPF ImageBrush
        /// </summary>
        /// <param name="src">A bitmap image</param>
        /// <returns>The image as a BitmapImage for WPF</returns>
        public static BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }

    internal static class ResourceAccessor
    {
        public static Uri GetUri(string resourcePath)
        {
            var uri = string.Format(
                "pack://application:,,,/{0};component/{1}"
                , Assembly.GetExecutingAssembly().GetName().Name
                , resourcePath
            );

            return new Uri(uri);
        }
    }
}

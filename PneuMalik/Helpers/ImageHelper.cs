using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace PneuMalik.Helpers
{

    public class ImageHelper
    {

        public ImageHelper()
        {

            _myImageCodecInfo = GetEncoderInfo("image/jpeg");

            var myEncoderParameter = new EncoderParameter(Encoder.Quality, 80L);
            _myEncoderParameters.Param[0] = myEncoderParameter;

            _storePathRoot = ConfigurationManager.AppSettings["FileAppUrl"];
        }

        public void Save(string url, string id)
        {

            using (var client = new WebClient())
            {

                var file = $"{id}.jpg";
                var filePath = Path.Combine(_storePathRoot, "images", "orig", file);

                if (File.Exists(filePath))
                {
                    // Don't download already saved images.
                    return;
                }

                try
                {
                    client.DownloadFile(url.Replace("&amp;", "&"), filePath);
                }
                catch
                {

                    //throw new Exception($"Chyba stahování {url} {filePath}", e);
                    return;
                }

                if (File.Exists(filePath))
                {

                    using (var Resized1 = ResizeImageAdv(Image.FromFile(filePath), 50, 50, false))
                    using (var Resized2 = ResizeImageAdv(Image.FromFile(filePath), 90, 90, false))
                    using (var Resized3 = ResizeImageAdv(Image.FromFile(filePath), 200, 170, false))
                    { 
                        Resized1.Save(Path.Combine(_storePathRoot, "images", "ikona", file), _myImageCodecInfo, _myEncoderParameters);
                        Resized2.Save(Path.Combine(_storePathRoot, "images", "nahled", file), _myImageCodecInfo, _myEncoderParameters);
                        Resized3.Save(Path.Combine(_storePathRoot, "images", "detail", file), _myImageCodecInfo, _myEncoderParameters);
                    }
                }
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private static Image ResizeImageAdv(Image Source, int Width, int Height, bool blnGetFullCanvas)
        {
            //obrázek má požadované rozměry, nedělej nic
            if (Source.Width == Width && Source.Height == Height) return Source;

            int W; int H;

            double a = double.Parse(Source.Height.ToString());
            double b = double.Parse(Width.ToString());
            double c = double.Parse(Source.Width.ToString());
            double d = double.Parse(Height.ToString());
            double x = 0;
            double y = 0;

            if (c > b)
            {
                x = b;
                y = (a * (b / c));

                if (y > d)
                {
                    y = d;
                    x = (x * (d / (a * (b / c))));
                }
            }
            else if (a > d)
            {
                y = d;
                x = (c * (d / a));

                if (x > b)
                {
                    x = b;
                    y = (y * (b / (c * (d / a))));
                }
            }
            else
            {
                if (a > c)
                {
                    y = d;
                    x = (c * (d / a));
                }
                else
                {
                    x = b;
                    y = (a * (b / c));
                }

            }

            x = x + 0.000001;
            y = y + 0.000001;

            H = Int32.Parse(y.ToString().Substring(0, y.ToString().IndexOf(",")));
            W = Int32.Parse(x.ToString().Substring(0, x.ToString().IndexOf(",")));

            int[] intTargetSize = { (blnGetFullCanvas ? Width : W), (blnGetFullCanvas ? Height : H) };
            int[] intULCorner = { (blnGetFullCanvas ? (W < Width ? ((Width - W) / 2) : 0) : 0), (blnGetFullCanvas ? (H < Height ? ((Height - H) / 2) : 0) : 0) };

            //teď nakresli nový obrázek
            Bitmap Target = new Bitmap(intTargetSize[0], intTargetSize[1]);
            Graphics GPH = Graphics.FromImage(Target);
            GPH.FillRectangle(Brushes.White, 0, 0, intTargetSize[0], intTargetSize[1]);
            GPH.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            GPH.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            GPH.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            GPH.DrawImage(Source, intULCorner[0], intULCorner[1], W, H);

            Source.Dispose();
            GPH.Dispose();

            return Target;
        }

        private readonly string _storePathRoot;
        private readonly ImageCodecInfo _myImageCodecInfo;
        private EncoderParameters _myEncoderParameters = new EncoderParameters(1);
    }
}
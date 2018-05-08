using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Data;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace PEIS.Web.Core
{
    public class QrCodeHelper
    {
        // Methods
        public static Image GetQrcode(string code)
        {
            return GetQrcode(code, 200, null, 0, Brushes.White, Brushes.Black);
        }

        public static Image GetQrcode(string code, int size)
        {
            return GetQrcode(code, size, null, 0, Brushes.White, Brushes.Black);
        }

        public static Image GetQrcode(string code, int eSize, Image img, int iSize, Brush bColor, Brush fColor)
        {
            QrCode code2 = new QrEncoder(ErrorCorrectionLevel.H).Encode(code);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Four), fColor, bColor);
            using (Stream stream = new MemoryStream())
            {
                eSize = (eSize > 0) ? eSize : 200;
                renderer.WriteToStream(code2.Matrix, ImageFormat.Png, stream, new Point(10, 10));
                Bitmap sourceImage = new Bitmap(stream);
                sourceImage = ResizeImage(sourceImage, eSize, eSize);
                Graphics graphics = Graphics.FromImage(sourceImage);
                if (img != null)
                {
                    iSize = (iSize > 0) ? iSize : 50;
                    img = ResizeImage(img, iSize, iSize);
                    Bitmap image = new Bitmap(iSize + 10, iSize + 10);
                    Graphics graphics2 = Graphics.FromImage(image);
                    graphics2.Clear(Color.White);
                    graphics2.DrawImage(img, 5, 5, iSize, iSize);
                    graphics.DrawImage(image, (eSize - iSize) / 2, (eSize - iSize) / 2, iSize, iSize);
                }
                return sourceImage;
            }
        }

        public static Bitmap ResizeImage(Image sourceImage, int width, int height)
        {
            Graphics graphics = null;
            Bitmap bitmap2;
            try
            {
                Bitmap image = new Bitmap(width, height);
                graphics = Graphics.FromImage(image);
                graphics.CompositingQuality =CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
                bitmap2 = image;
            }
            catch
            {
                bitmap2 = null;
            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                }
            }
            return bitmap2;
        }
    }

    


}
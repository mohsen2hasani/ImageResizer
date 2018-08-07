using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ImageResizer.Methods
{
    public class ImageService
    {
        public void ResizeImage(Image waterMarkImage, string imageAddress, string destinationfileName, int width, bool setWaterMark)
        {
            using (Image image = Image.FromFile(imageAddress))
            using (Graphics imageGraphics = Graphics.FromImage(image))
            using (TextureBrush watermarkBrush = new TextureBrush(waterMarkImage))
            {
                //set watermark
                if (setWaterMark)
                {
                    int x = (image.Width / 2 - waterMarkImage.Width / 2);
                    int y = (image.Height / 2 - waterMarkImage.Height / 2);
                    watermarkBrush.TranslateTransform(x, y);
                    imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(waterMarkImage.Width + 1, waterMarkImage.Height)));
                }

                //fix image orientation
                FixImageOrientation(image);

                //remove all image roperties
                foreach (var i in image.PropertyIdList)
                    image.RemovePropertyItem(i);

                //resize and save image
                double ratio = (double)image.Width / width;
                if (ratio > 1)
                {
                    var newImage = new Bitmap(width, (int)(image.Height / ratio));
                    using (var graphics = Graphics.FromImage(newImage))
                        graphics.DrawImage(image, 0, 0, width, (int)(image.Height / ratio));

                    newImage.Save(destinationfileName, ImageFormat.Jpeg);
                }
                else
                {
                    image.Save(destinationfileName, ImageFormat.Jpeg);
                }
            }
        }

        static void FixImageOrientation(Image image)
        {
            const int ExifOrientationId = 0x112;
            // Read orientation tag
            if (!image.PropertyIdList.Contains(ExifOrientationId)) return;
            var prop = image.GetPropertyItem(ExifOrientationId);
            var orient = BitConverter.ToInt16(prop.Value, 0);
            // Force value to 1
            prop.Value = BitConverter.GetBytes((short)1);
            image.SetPropertyItem(prop);

            // Rotate/flip image according to <orient>
            switch (orient)
            {
                case 1:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
                case 2:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 4:
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 5:
                    image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 7:
                    image.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
        }
    }
}
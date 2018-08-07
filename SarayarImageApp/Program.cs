using ImageResizer.Methods;
using System;
using System.Drawing;
using System.Linq;
using ImageResizer.AppConfig;
using System.IO;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Any())
            {
                try
                {
                    string ImageAddress = args.Where(a => a.Contains("image:")).FirstOrDefault().Replace("image:", string.Empty);

                    string waterMark = args.Where(a => a.Contains("watermark:")).Any() ?
                        args.Where(a => a.Contains("watermark:")).FirstOrDefault().Replace("watermark:", string.Empty) :
                        Config.WaterMark;

                    string destinationDirectory = args.Where(a => a.Contains("destination:")).Any() ?
                        args.Where(a => a.Contains("destination:")).FirstOrDefault().Replace("destination:", string.Empty) :
                        Config.DestinationDirecoty;

                    //set '/' at the end of destination directory if it does not exist
                    destinationDirectory = destinationDirectory.Substring(destinationDirectory.Length - 1) == @"\" ? destinationDirectory : destinationDirectory + @"\";

                    //create destination directory if not exist
                    Directory.CreateDirectory(destinationDirectory);

                    destinationDirectory += ImageAddress.Replace(".png", ".jpg").Replace(".jpeg", ".jpg").Substring(ImageAddress.LastIndexOf(@"\") + 1);

                    Image waterMarkImage = Image.FromFile(waterMark);                   

                    ImageService imageService = new ImageService();
                    imageService.ResizeImage(waterMarkImage, ImageAddress, destinationDirectory.Replace(".jpg", "_original.jpg"), int.MaxValue, false);
                    imageService.ResizeImage(waterMarkImage, ImageAddress, destinationDirectory.Replace(".jpg", "_normal.jpg"), 1024, true);
                    imageService.ResizeImage(waterMarkImage, ImageAddress, destinationDirectory.Replace(".jpg", "_small.jpg"), 384, false);
                    imageService.ResizeImage(waterMarkImage, ImageAddress, destinationDirectory.Replace(".jpg", "_thumb.jpg"), 256, false);
                    imageService.ResizeImage(waterMarkImage, ImageAddress, destinationDirectory.Replace(".jpg", "_tiny.jpg"), 128, false);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message + Environment.NewLine);
                    Console.ReadKey();
                }
            }
        }
    }
}
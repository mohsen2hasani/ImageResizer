# ImageResizer
Image Resizer is a console application for resize and set watermark on pictures

You just need set destination folder for images and watermark picture (png) in "Config" class and build application.

I used this application for save 5 size of pictures and set watermark on 2 big sizes with this command, from external application :

```
List<string> args = new List<string> {
    @"image:D:\MyImageToResize.jpg",
    @"watermark:D:\CustomWatermarkImage.png",
    @"destination:D:\CustomDestinationFolder"
    };

Process.Start(@"C:\ImageResizerFolder\ImageResizer.exe",
    string.Join(" ", args));
```

Enjoy it !

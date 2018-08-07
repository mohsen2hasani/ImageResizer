using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer.AppConfig
{
    public static class Config
    {
        public static string DestinationDirecoty
        {
            get
            {
                return @"c:\Images\";
            }
        }
        public static string WaterMark
        {
            get
            {
                return @"c:\WatermarkPicture.png";
            }
        }
    }
}
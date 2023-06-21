using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Helpers
{
    public class WindowSizeHelper 
    {
        private static double defaultWidth;
        private static double defaultHeight;

        public static void SetWindowSize(Page page, int width, int height)
        {
            
            var window = Application.Current.MainPage.Window;
            if (window != null)
            {
                defaultWidth = window.Width;
                defaultHeight = window.Height;
                window.Width = width;
                window.Height = height;

                var displayInfo = DeviceDisplay.MainDisplayInfo;
                window.X = (displayInfo.Width / displayInfo.Density - window.Width) / 2;
                window.Y = (displayInfo.Height / displayInfo.Density - window.Height) / 2;
            }
        }
        public static void ResetWindowSize(Page page)
        {
            var window = Application.Current.MainPage.Window;
            if (window != null)
            {
                window.Width = defaultWidth;
                window.Height = defaultHeight;


                var displayInfo = DeviceDisplay.MainDisplayInfo;
                window.X = (displayInfo.Width / displayInfo.Density - window.Width) / 2;
                window.Y = (displayInfo.Height / displayInfo.Density - window.Height) / 2;
            }
        }
    }
}

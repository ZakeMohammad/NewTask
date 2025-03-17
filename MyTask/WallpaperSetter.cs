using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyTask
{
    public class WallpaperSetter
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        private const int SPI_SETDESKWALLPAPER = 0x0014;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        public static void SetWallpaper(string wallpaperPath)
        {
            if (IsLinux())
                SetWallpaperLinux(wallpaperPath);          
            else if (IsWindows())
                SetWallpaperWindows(wallpaperPath);
            else       
                Console.WriteLine("Unsupported OS");
        }

        private static void SetWallpaperWindows(string wallpaperPath) =>
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, wallpaperPath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);


        private static void SetWallpaperLinux(string wallpaperPath)
        {
            string gsettingsCommand = $"gsettings set org.gnome.desktop.background picture-uri file://{wallpaperPath}";
            Process.Start("bash", $"-c \"{gsettingsCommand}\"");
        }

        public static string GetWallpaperPath(string image)
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string wallpaperPath = Path.Combine(projectDirectory, "Images", image);
            return wallpaperPath;
        }

        private static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        private static bool IsLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    }
}

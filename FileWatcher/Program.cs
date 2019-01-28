using System;
using System.IO;
using System.Linq;

namespace FileSystemWatcherSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            //PrintSystemJobInfo

            #region 监控盘符
            var uDrive = DriveInfo.GetDrives().Where(s => s.DriveType == DriveType.Removable).FirstOrDefault();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"移动盘名称为：{uDrive.Name}---大小---{uDrive.TotalFreeSpace / 1024 / 1024 / 1024} G");


            var fileSystemWatcher = new FileSystemWatcher();
            var fileSystemWatcherC = new FileSystemWatcher();

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;

            fileSystemWatcherC.Created += FileSystemWatcher_Created;
            fileSystemWatcherC.Changed += FileSystemWatcher_Changed;
            fileSystemWatcherC.Deleted += FileSystemWatcher_Deleted;
            fileSystemWatcherC.Renamed += FileSystemWatcher_Renamed;

            //监控盘符
            fileSystemWatcher.Path = @"D:\";
            fileSystemWatcherC.Path = uDrive.RootDirectory.ToString();
            // 触发监控
            fileSystemWatcherC.EnableRaisingEvents = true;
            fileSystemWatcher.EnableRaisingEvents = true;

            Console.WriteLine("监听...");
            Console.WriteLine("(按下退出.)");
            #endregion

            Console.ReadLine();
        }

        private static void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"一个文件从 {e.OldName} 改名为： {e.Name}-----路径为：{e.FullPath} ");
        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"文件： {e.Name}  已删除----原路径为：{e.FullPath}");
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"- {e.Name}  文件被修改-----路径为：{e.FullPath}");
        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"新创建了一个文件 - {e.Name}-----路径为：{e.FullPath}");
        }
    }
}
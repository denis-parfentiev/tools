using RemoveTempFiles.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.IO;

namespace RemoveTempFiles.Infrastructure
{
    public class DeleteTempFilesCommand : ICommand
    {
        private readonly Dictionary<string, string> files = new Dictionary<string, string>() {
            { "TempFiles",$"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Temp" }
        };
        public object Execute()
        {
            foreach (var item in files)
            {
                var dInfo = new DirectoryInfo(item.Value);
                if (dInfo.Exists)
                {
                    foreach (var f in dInfo.GetFiles())
                        try
                        {
                            f.Delete();
                        }
                        catch { }

                    foreach (var d in dInfo.GetDirectories())
                        try
                        {
                            d.Delete(true);
                        }
                        catch { }
                }
            }
            return null;
        }
    }
}

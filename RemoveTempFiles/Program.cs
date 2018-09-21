using RemoveTempFiles.Infrastructure;
using RemoveTempFiles.Infrastructure.Abstract;
using System.Collections.Generic;

namespace RemoveTempFiles
{
    internal class Program
    {
        private static readonly List<ICommand> Commands = new List<ICommand>() {
            new DeleteTempFilesCommand()
        };
        private static void Main(string[] args)
        {
            foreach (var item in Commands)
            {
                if (item is ICommand value)
                    value.Execute();
            }
        }
    }
}

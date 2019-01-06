using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Command.Prompt.Helper;
using Command.Prompt.Interfaces;

// ReSharper disable UnusedMember.Global

namespace Command.Prompt
{
    public class CommandPrompt : ICommandPrompt
    {
        public string[] RunCommands(params string[] commands)
        {
            var result = new List<string>();

            var oSignalEvent = new ManualResetEvent(false);

            using (var p = new Process())
            {
                p.StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };
                p.ErrorDataReceived += (sender, e) =>
                {
                    if (CommandPromptHelper.IsExludeData(e.Data))
                        result.Add(DateTime.Now.Date.ToString("dd/MM/yyyy hh:mm:ss") + $" Error:[{e.Data }]");
                    oSignalEvent.Set();
                };
                p.OutputDataReceived += (sender, e) =>
                {
                    if (CommandPromptHelper.IsExludeData(e.Data))
                        result.Add(DateTime.Now.Date.ToString("dd/MM/yyyy HH:mm:ss") + $" Data:[{e.Data}]");
                    oSignalEvent.Set();
                };
                p.EnableRaisingEvents = true;
                p.Start();
                oSignalEvent.Reset();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                using (var sw = p.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        foreach (var item in commands)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                p.WaitForExit();
                WaitHandle.WaitAll(new WaitHandle[] { oSignalEvent }, 65);
            }

            return result.ToArray();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}

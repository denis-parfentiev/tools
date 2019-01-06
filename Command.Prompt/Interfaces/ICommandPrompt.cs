using System;

namespace Command.Prompt.Interfaces
{
    public interface ICommandPrompt : IDisposable
    {
        // ReSharper disable once UnusedMember.Global
        string[] RunCommands(params string[] commands);
    }
}

// ReSharper disable UnusedMember.Global
namespace Command.Prompt.Helper
{
    public class CommandPromptHelper
    {
        public static bool CheckError(string erro)
        {
            return erro.Contains("Error") || erro.Contains("No matching devices found") || erro.Contains("problem");
        }

        public static bool IsExludeData(string data)
        {
            return !string.IsNullOrEmpty(data) && !data.Contains("Microsoft");
        }
    }
}

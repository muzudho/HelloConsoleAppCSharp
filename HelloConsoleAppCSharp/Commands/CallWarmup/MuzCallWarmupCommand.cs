namespace HelloConsoleAppCSharp.Commands.CallWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzCallWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        string commandName)
    {
        return MuzRequestType.None;
    }
}

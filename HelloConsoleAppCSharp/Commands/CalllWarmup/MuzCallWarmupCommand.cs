namespace HelloConsoleAppCSharp.Commands.CallWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzCallWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        string commandName,
        DateTime startDateTime)
    {
        return await ProgramCommands.ExecuteAsync(commandName, startDateTime);
    }
}

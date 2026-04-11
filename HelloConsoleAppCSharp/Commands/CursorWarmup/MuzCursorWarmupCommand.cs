namespace HelloConsoleAppCSharp.Commands.CursorWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using static HelloConsoleAppCSharp.Infrastructure.REPL.MuzREPL;

internal static class MuzCursorWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        return MuzREPL.MuzRequestType.None;
    }
}

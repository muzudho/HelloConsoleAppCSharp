namespace HelloConsoleAppCSharp.Commands.TitlePageWarmup;

using HelloConsoleAppCSharp.Core.Infrastructure.REPL;

internal static class MuzTitlePageWarmupCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        MuzREPL.IsPromptVisible = false; // プロンプトは消す。

        string[] commandList = [
            "show-wall",
            //"show-message-box",
            "show-start-box",
            "show-title",
            "show-credit",
        ];
        foreach (var command in commandList)
        {
            await ProgramCommands.ExecuteAsync(services, command);
        }
        var result = await ProgramCommands.ExecuteAsync(services, "show-start-menu");

        return MuzREPLRequestType.None;
    }
}

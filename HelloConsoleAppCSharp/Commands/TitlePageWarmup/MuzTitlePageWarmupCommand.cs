namespace HelloConsoleAppCSharp.Commands.TitlePageWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzTitlePageWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        IServiceProvider services,
        ProgramContext pgContext)
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
            await ProgramCommands.ExecuteAsync(command, services, pgContext);
        }
        var result = await ProgramCommands.ExecuteAsync("show-start-menu", services, pgContext);

        return MuzRequestType.None;
    }
}

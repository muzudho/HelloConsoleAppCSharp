namespace HelloConsoleAppCSharp.Commands.ShowStartMenu;

using HelloConsoleAppCSharp.Controls;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzShowStartMenuCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        var result = await MuzStartMenuControl.ShowMenuAsync();
        Console.WriteLine($"メニューを抜けたぜ（＾～＾）！　選択された項目: {result}");

        return MuzRequestType.None;
    }
}

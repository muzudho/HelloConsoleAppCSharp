namespace HelloConsoleAppCSharp.Commands.ShowStartMenu;

using HelloConsoleAppCSharp.Controls;
using HelloConsoleAppCSharp.Core.Infrastructure;

internal static class MuzShowStartMenuCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        var result = await MuzStartMenuControl.ShowMenuAsync();
        Console.WriteLine($"メニューを抜けたぜ（＾～＾）！　選択された項目: {result}");

        return MuzREPLRequestType.None;
    }
}

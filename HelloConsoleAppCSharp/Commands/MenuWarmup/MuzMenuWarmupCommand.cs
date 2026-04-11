namespace HelloConsoleAppCSharp.Commands.MenuWarmup;

using HelloConsoleAppCSharp.Controls;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzMenuWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // ［開始メニュー］コントロール
        var startMenuControl = new MuzStartMenuControl(
            menuItems: new() { "開始", "設定", "終了" },
            stopYList: [16, 17, 18]);
        return await startMenuControl.Enter();
    }
}

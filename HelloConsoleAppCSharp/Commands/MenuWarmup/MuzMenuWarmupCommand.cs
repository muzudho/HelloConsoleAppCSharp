namespace HelloConsoleAppCSharp.Commands.MenuWarmup;

using HelloConsoleAppCSharp.Controls;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzMenuWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // ［開始メニュー］コントロール
        List<string> startMenuItems = new()
        {
            "開始",
            "設定",
            "終了",
        };
        var startMenuControl = new MuzStartMenuControl(
            menuItems: startMenuItems,
            stopYList: [16, 17, 18]);
        var requestType = await startMenuControl.Enter();

        if (startMenuControl.SelectedIndex == null)
        {
            Console.WriteLine("キャンセルしたぜ（＾～＾）");
        }
        else
        {
            Console.WriteLine($"{startMenuItems[startMenuControl.SelectedIndex!.Value]}を選択したぜ（＾▽＾）！");
        }

        return requestType;
    }
}

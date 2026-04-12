namespace HelloConsoleAppCSharp.Commands.MenuWarmup;

using HelloConsoleAppCSharp.Controls;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzMenuWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        ProgramContext pgContext)
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
            var label = startMenuItems[startMenuControl.SelectedIndex!.Value];
            Console.WriteLine($"{label}を選択したぜ（＾▽＾）！");

            switch (label)
            {
                case "終了":
                    return await ProgramCommands.ExecuteAsync("exit", pgContext);
            }
        }

        return requestType;
    }
}

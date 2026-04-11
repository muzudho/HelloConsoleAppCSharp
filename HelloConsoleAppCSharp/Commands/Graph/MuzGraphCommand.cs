namespace HelloConsoleAppCSharp.Commands.Graph;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Infrastructure.Scheduler;
using HelloConsoleAppCSharp.Views;
using System;

internal static class MuzGraphCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        DateTime startDateTime)
    {
        MuzREPL.IsPromptVisible = false; // プロンプトは消す。
        MuzPageLayouts.PrintTitlePage();

        // 📍 NOTE:
        //
        //      ここで、ボックスを表示してみようぜ（＾～＾）！
        //      方眼紙などに画面の想像図を描いてから、位置とサイズを測って、コーディングしろだぜ（＾～＾）！
        //
        MuzBoxViews.PrintDoubleBorderBox(
            left: 20,
            top: 15,
            width: 40,
            height: 5,
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan);

        // 📍 NOTE:
        //
        //     ここで、メニューを表示してみようぜ（＾～＾）！
        //
        MuzVMenus.PrintMenu(
            left: 38,
            top: 16,
            items: new[] { "開始", "設定", "終了" },
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan);

        // 📍 NOTE:
        //
        //      約1/60秒間隔のタイマーを起動するぜ（＾～＾）！
        //
        new MuzTimer(TimeSpan.FromMilliseconds(16)).Run(
            update: async () =>
            {


                // 📍 NOTE:
                //
                //      アプリケーション起動からの経過時刻を表示するぜ（＾～＾）！
                //
                MuzWidgets.PrintErapsedTime(
                    label: "Time ",
                    startDateTime: startDateTime,
                    left: 62,
                    top: 0,
                    fgColor: ConsoleColor.Black,
                    bgColor: ConsoleColor.Cyan);


                // 📍 NOTE:
                //
                //      一定間隔で点滅するカーソル（ブリンカー）を表示するぜ（＾～＾）！
                //
                MuzWidgets.PrintBlinkingText(
                    text: "▶",  // 右向きの三角形は、半角のようだ。
                    left: 36,
                    top: 16,
                    fgColor: ConsoleColor.Blue,
                    bgColor: ConsoleColor.Cyan,
                    isVisible: (DateTime.Now.Millisecond / 500) % 2 == 0); // 0.5秒ごとに点滅

                await Task.CompletedTask;
            });


        return MuzRequestType.None;
    }
}

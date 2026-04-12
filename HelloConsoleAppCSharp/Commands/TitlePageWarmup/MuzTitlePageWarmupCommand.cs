namespace HelloConsoleAppCSharp.Commands.TitlePageWarmup;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Infrastructure.Scheduler;
using HelloConsoleAppCSharp.Views;
using System;

internal static class MuzTitlePageWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        ProgramContext pgContext)
    {
        MuzREPL.IsPromptVisible = false; // プロンプトは消す。
        await MuzPageLayouts.PrintTitlePageAsync();

        // 📍 NOTE:
        //
        //      ここで、ボックスを表示してみようぜ（＾～＾）！
        //      方眼紙などに画面の想像図を描いてから、位置とサイズを測って、コーディングしろだぜ（＾～＾）！
        //      例えば、以下のような感じだぜ（＾～＾）！
        //
        //              4   8  12  16  20  24  28  32  36  40  44  48  52  56  60  64  68  72  76  80
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //        5 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       10 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                  20                                      60                   |
        //       15 +                15 ╔══════════════════════════════════════╗                    +
        //          |                   ║                                      ║                    |
        //          |                   ║                                      ║                    |
        //          |                   ║                                      ║                    |
        //          |                   ╚══════════════════════════════════════╝                    |
        //       20 +                20                                                             +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       25 |                                                                               |
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //
        //      👆　位置を決める（＾～＾）
        //

        // 色替え
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan,
            onColorChanged: async () =>
            {
                await MuzBoxViews.PrintDoubleBorderAsync(
                    left: 20,
                    top: 15,
                    width: 40,
                    height: 5);

                // 📍 NOTE:
                //
                //     ここで、メニューを表示してみようぜ（＾～＾）！
                //
                await MuzVerticalMenus.PrintMenuAsync(
                    left: 38,
                    top: 16,
                    items: new[] { "開始", "設定", "終了" });
            });

        // 📍 NOTE:
        //
        //      別スレッドで、約1/60秒間隔のタイマーを起動するぜ（＾～＾）！
        //
        new MuzTimer(TimeSpan.FromMilliseconds(16)).Run(
            update: async () =>
            {
                // 別スレッドでは、［色替え］はしないぜ（＾～＾）！　処理が混ざってしまうからな（＾～＾）！
                // 起動時の色のまま、描画するぜ（＾～＾）！　諦めろだぜ（＾～＾）！

                // 📍 NOTE:
                //
                //      - アプリケーション起動からの経過時刻を表示するぜ（＾～＾）！
                //      - XXX: バックグラウンドスレッドで画面を更新するのは、アンチパターンだった、これは悪い例だ（＾～＾）練習問題を変えたい（＾～＾）
                //
                await MuzWidgets.PrintErapsedTimeAsync(
                    label: "Time ",
                    launchDateTime: pgContext.LaunchDateTime,
                    left: 62,
                    top: 0);

                // 📍 NOTE:
                //
                //      - 一定間隔で点滅するカーソル（ブリンカー）を表示するぜ（＾～＾）！
                //      - XXX: バックグラウンドスレッドで画面を更新するのは、アンチパターンだった、これは悪い例だ（＾～＾）練習問題を変えたい（＾～＾）
                //
                await MuzWidgets.PrintBlinkingTextAsync(
                    text: "▶",  // 右向きの三角形は、半角のようだ。
                    left: 36,
                    top: 16,
                    isVisible: (DateTime.Now.Millisecond / 500) % 2 == 0); // 0.5秒ごとに点滅
            });

        return MuzRequestType.None;
    }
}

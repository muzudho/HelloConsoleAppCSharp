namespace HelloConsoleAppCSharp.Commands.ShowStartVerticalList;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;
using System;

internal class MuzShowStartVerticalList
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        ProgramContext pgContext)
    {
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
                // 📍 NOTE:
                //
                //     ここで、メニューを表示してみようぜ（＾～＾）！
                //
                await MuzVerticalListViews.PrintMenuAsync(
                    left: 38,
                    top: 16,
                    items: new[] { "開始", "設定", "終了" });
            });

        return MuzRequestType.None;
    }
}

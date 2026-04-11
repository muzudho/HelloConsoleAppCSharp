namespace HelloConsoleAppCSharp.Controls;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Text;

internal class MuzMessageBoxControl
{


    // ========================================
    // 生成／破棄
    // ========================================


    internal MuzMessageBoxControl(
        List<string> messageList)
    {
        this.MessageList = messageList;
    }


    // ========================================
    // 窓口データメンバー
    // ========================================


    /// <summary>
    /// メッセージの一覧
    /// </summary>
    internal List<string> MessageList { get; init; } = default!;


    // ========================================
    // 窓口メソッド
    // ========================================


    public async Task<MuzRequestType> Enter()
    {
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
        //          |                                                                               |
        //       15 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |╔═════════════════════════════════════════════════════════════════════════════╗|
        //       20 +║                                                                             ║+
        //          |║                                                                             ║|
        //          |║                                                                             ║|
        //          |║                                                                             ║|
        //          |║                                                                         ▼  ║|
        //       25 |╚═════════════════════════════════════════════════════════════════════════════╝|
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //
        //      👆　とりあえず、５行表示できるボックスを下の方いっぱいに配置してみるぜ（＾～＾）

        // ［ボックス］
        MuzBoxViews.PrintDoubleBorderBox(
            left: 0,
            top: 19,
            width: 80,
            height: 7,
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan);

        while (true)
        {
            // ［コンティニュープロンプト］
            MuzWidgets.PrintBlinkingText(
                text: "▼",  // エディターでは全角で表示されているが、コンソールに表示されるときは半角のようだ。
                left: 75,
                top: 24,
                fgColor: ConsoleColor.Blue,
                bgColor: ConsoleColor.Cyan,
                isVisible: (DateTime.Now.Millisecond / 500) % 2 == 0); // 0.5秒ごとに点滅

            // キー入力がない場合は、少し待ってからループの先頭に戻るぜ（＾～＾）！
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(16));    // およそ１／６０秒で画面更新（＾～＾）
                continue;
            }

            // 📍 NOTE:
            //
            //      キー入力を受け取ります。
            //      プログラムは、ユーザーがキーを押すまで、ここで待機します。  
            //      `intercept`:  true でエコー（表示）しない。
            //      戻り値は使いません。
            //
            _ = Console.ReadKey(
                intercept: true);
            break;  // キー入力があったら、ループを抜けるぜ（＾～＾）！
        }

        return MuzRequestType.None;
    }
}

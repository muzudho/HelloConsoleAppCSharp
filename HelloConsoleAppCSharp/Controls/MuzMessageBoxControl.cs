namespace HelloConsoleAppCSharp.Controls;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;
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
        //          |║                                                                             ║|
        //       25 |╚═════════════════════════════════════════════════════════════════════════════╝|
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //
        //      👆　とりあえず、５行表示できるボックスを下の方いっぱいに配置してみるぜ（＾～＾）

        // ボックス表示
        MuzBoxViews.PrintDoubleBorderBox(
            left: 0,
            top: 19,
            width: 80,
            height: 7,
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan);

        return MuzRequestType.None;
    }
}

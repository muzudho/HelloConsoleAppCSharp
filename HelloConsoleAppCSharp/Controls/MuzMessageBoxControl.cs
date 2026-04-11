namespace HelloConsoleAppCSharp.Controls;

using HelloConsoleAppCSharp.Commands.TypewriterEffectWarmup;
using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;
using System;
using System.Collections.Generic;

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

        // 色替え
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan,
            onColorChanged: async () =>
            {
                int messageBoxLeft = 0;
                int messageBoxTop = 19;

                // まずは、メッセージ毎に分割するぜ（＾～＾）
                foreach (var message in this.MessageList)
                {
                    // ［ボックス］
                    await MuzBoxViews.PrintDoubleBorderBoxAsync(
                        left: messageBoxLeft,
                        top: messageBoxTop,
                        width: 80,
                        height: 7);

                    // ［ブリンカー］（ホワイトスペースを表示）
                    await MuzWidgets.PrintBlinkingTextAsync(
                        text: string.Empty,
                        left: 75,
                        top: 24,
                        isVisible: false);  // 常にホワイトスペースを表示

                    // １行毎にタイプライター表示するぜ、戻り値は無視していいぜ（＾～＾）
                    _ = await MuzTypewriterEffectWarmupCommand.ExecuteAsync(
                        left: messageBoxLeft + 1,   // 枠線の太さを足す（＾～＾）
                        top: messageBoxTop + 1,
                        message: message);

                    // 何かキーを押下するまで、一定間隔で点滅するカーソル（ブリンカー）を表示するぜ（＾～＾）！
                    while (true)
                    {
                        // ［コンティニュープロンプト］
                        await MuzWidgets.PrintBlinkingTextAsync(
                            text: "▼",  // エディターでは全角で表示されているが、コンソールに表示されるときは半角のようだ。
                            left: 75,
                            top: 24,
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
                }
            });

        return MuzRequestType.None;
    }
}

namespace HelloConsoleAppCSharp.Controls;

using HelloConsoleAppCSharp.Commands.TypewriterEffectWarmup;
using HelloConsoleAppCSharp.Core.Infrastructure;
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


    public async Task<MuzREPLRequestType> Enter(
        IServiceProvider services)
    {
        //              4   8  12  16  20  24  28  32  36  40  44  48  52  56  60  64  68  72  76  80
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //          |                                                                               |
        //        2 +╔═════════════════════════════════════════════════════════════════════════════╗+
        //          |║                                                                             ║|
        //          |║                                                                             ║|
        //        5 +║                                                                             ║+
        //          |║                                                                             ║|
        //          |║                                                                         ▼  ║|
        //          |╚═════════════════════════════════════════════════════════════════════════════╝|
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
        //          |                                                                               |
        //       20 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       25 |                                                                               |
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
                int messageBoxTop = 1;

                // まずは、メッセージ毎に分割するぜ（＾～＾）
                foreach (var message in this.MessageList)
                {
                    // ［ボックス］
                    await MuzBoxViews.PrintDoubleBorderAsync(
                        left: messageBoxLeft,
                        top: messageBoxTop,
                        width: 80,
                        height: 7);

                    // ［コンティニュープロンプト］の消去
                    await MuzBoxViews.PrintAsync(
                        left: 75,
                        top: 6,
                        width: 1,
                        height: 1);

                    // １行毎にタイプライター表示するぜ、戻り値は無視していいぜ（＾～＾）
                    _ = await MuzTypewriterEffectWarmupCommand.ExecuteAsync(
                        services: services,
                        left: messageBoxLeft + 1,   // 枠線の太さを足す（＾～＾）
                        top: messageBoxTop + 1,
                        message: message);

                    // 何かキーを押下するまで、一定間隔で点滅するカーソル（ブリンカー）を表示するぜ（＾～＾）！
                    while (true)
                    {
                        // ［コンティニュープロンプト］の点滅
                        await MuzConsoleHelper.BlinkAsync(
                            fgColor1: ConsoleColor.Blue,
                            bgColor1: ConsoleColor.Cyan,
                            fgColor2: ConsoleColor.Cyan,   // 背景色と同じにする
                            bgColor2: ConsoleColor.Cyan,
                            isColor2: (DateTime.Now.Millisecond / 500) % 2 == 0, // 0.5秒ごとに色切替
                            onColorChanged: async () =>
                            {
                                await MuzLabelViews.PrintAsync(
                                    left: 75,
                                    top: 6,
                                    text: "▼");     // この三角形はエディターでは全角で表示されているが、コンソールに表示されるときは半角のようだ。
                            });

                        // キー入力がない場合は、少し待ってからループの先頭に戻るぜ（＾～＾）！
                        if (!Console.KeyAvailable)
                        {
                            Thread.Sleep(MuzREPL.KeyInputPollingIntervalMilliseconds);
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

        return MuzREPLRequestType.None;
    }
}

namespace HelloConsoleAppCSharp.Commands.ContinuePromptWarmup;

using HelloConsoleAppCSharp.Commands.TypewriterEffectWarmup;
using HelloConsoleAppCSharp.Core.Infrastructure;
using HelloConsoleAppCSharp.Views;

internal static class MuzContinuePromptWarmupCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services,
        List<string> messageList)
    {
        // 色変更
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan,
            onColorChanged: async () =>
            {
                // まずは、メッセージ毎に分割するぜ（＾～＾）
                foreach (var message in messageList)
                {
                    // 📍 NOTE:
                    //
                    //      ブリンカーの位置は、適当に調整してほしい（＾～＾）
                    //
                    await MuzWidgets.PrintBlinkingTextAsync(
                            text: string.Empty,
                            left: 36,
                            top: 16,
                            isVisible: false);  // 常にホワイトスペースを表示

                    // １行毎にタイプライター表示するぜ、戻り値は無視していいぜ（＾～＾）
                    _ = await MuzTypewriterEffectWarmupCommand.ExecuteAsync(
                            services: services,
                            left: Console.CursorLeft,
                            top: Console.CursorTop,
                            message: message);

                    // 📍 NOTE:
                    //
                    //      何かキーを押下するまで、一定間隔で点滅するカーソル（ブリンカー）を表示するぜ（＾～＾）！
                    //
                    while (true)
                    {
                        // 📍 NOTE:
                        //
                        //      ブリンカーの位置は、適当に調整してほしい（＾～＾）
                        //
                        await MuzWidgets.PrintBlinkingTextAsync(
                                text: "▼",  // エディターでは全角で表示されているが、コンソールに表示されるときは半角のようだ。
                                left: 36,
                                top: 16,
                                isVisible: (DateTime.Now.Millisecond / 500) % 2 == 0); // 0.5秒ごとに点滅

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

                    // メッセージ毎の処理が終わったら、次のメッセージに進むぜ（＾～＾）！
                }
            });

        return MuzREPLRequestType.None;
    }
}

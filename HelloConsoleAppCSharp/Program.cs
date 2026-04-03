// See https://aka.ms/new-console-template for more information

using HelloConsoleAppCSharp;
using HelloConsoleAppCSharp.Infrastructure.Configuration;
using HelloConsoleAppCSharp.Infrastructure.Logging;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

try
{
    Console.WriteLine("Hello, World!");

    // 文字色（前景色）を赤に変更
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("これは赤い文字だぜ！");

    // 背景色を青に変更
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.WriteLine("赤文字に青背景！");

    // 元の色に戻す（大事！）
    Console.ResetColor();


    // Console.Clear(); を呼ぶと、ウィンドウ全体の背景色も変わる（現在のBackgroundColorが適用される）。
    // ANSIエスケープシーケンス を使えば、真のRGBカラー（24bit）や下線・太字なども使えるようになる。
    // 1行の中で単語ごとに色を変えたい場合
    //  → 上の WriteColored を何度も呼ぶか、位置を指定して書き込む（Console.SetCursorPosition）必要がある。1回のWriteLineで複数色は標準では無理。

    /*
       📍 NOTE:

            全部で16色あるよ：

            Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray
            DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
    */

    Console.WriteLine("ここはデフォルトの色に戻ったよ");

    // ホストビルドするぜ（＾～＾）！
    // ［ホスト］ってのは［汎用ホスト］のことで、いろいろ［サービス］っていう便利機能を付け加えることができるフレームワークみたいなもんだぜ（＾～＾）
    // それを［ビルド］するぜ（＾▽＾）
    await MuzInfrastructureHelper.BuildHostAsync(
        commandLineArgs: args,
        onHostEnabled: async (host) =>
        {
            // ここからビルドされた［汎用ホスト］（host）が使えるぜ（＾▽＾）！
            Console.WriteLine("ここからビルドされた［汎用ホスト］（host）が使えるぜ（＾▽＾）！");

            // ［アプリケーション設定ファイル］を動作確認してみようぜ（＾～＾）
            var appSettings = host.Services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
            Console.WriteLine($"AppName: {appSettings.AppName}");

            // ［ロガー］を動作確認してみようぜ（＾～＾）
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("デフォルトのログを書き込むぜ～（＾～＾）！");

            // ［ロガー別のログ］を動作確認してみようぜ（＾～＾）
            var loggingSvc = host.Services.GetRequiredService<IMuzLoggingService>();
            loggingSvc.Others.LogInformation("その他のログだぜ（＾～＾）");
            loggingSvc.Verbose.LogInformation("大量のログだぜ（＾～＾）");

            await REPL.RunAsync(
                evalAsync: async (text) =>
                {
                    // ここに君のコマンド処理を書く。
                    // `exit` は REPL 内で処理されているから、ここでは処理されないぜ（＾～＾）！
                    switch (text)
                    {
                        case "hello":
                            Console.WriteLine("こんにちは！（＾～＾）");
                            break;

                        case "color":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("文字色を変えたよ！");
                            Console.ResetColor();
                            break;

                        default:
                            Console.WriteLine($"知らないコマンドだぜ: {text}");
                            break;
                    }
                });
        });
}
catch (Exception ex)
{
    Console.WriteLine($"アプリが死んだ... ログも取れない、むずでょ泣く。{ex}");
}
finally
{
    Console.WriteLine("アプリが終了するぜ（＾～＾）！");
    await MuzInfrastructureHelper.Cleanup();
}

// Program.cs を最後まで実行しても、必ずしもアプリケーションが終了するわけじゃないぜ（＾～＾）！
// ［汎用ホスト］が動いている限りは、アプリケーションは終了しないぜ（＾～＾）！

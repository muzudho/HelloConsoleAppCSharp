// See https://aka.ms/new-console-template for more information

using HelloConsoleAppCSharp;
using HelloConsoleAppCSharp.Infrastructure.Configuration;
using HelloConsoleAppCSharp.Infrastructure.Logging;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

try
{
    Console.WriteLine("Hello, World!");

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

            await MuzREPL.RunAsync(
                evalAsync: async (text) =>
                {
                    // ここに君のコマンド処理を書く。
                    // `exit` は REPL 内で処理されているから、ここでは処理されないぜ（＾～＾）！
                    switch (text)
                    {
                        case "exit":
                            Console.WriteLine("REPLを終了するぜ（＾～＾）");
                            return MuzREPL.MuzRequestType.Exit;

                        case "hello":
                            Console.WriteLine("こんにちは（＾～＾）！");
                            return MuzREPL.MuzRequestType.None;

                        case "color":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("文字色を変えたよ！");
                            Console.ResetColor();
                            return MuzREPL.MuzRequestType.None;

                        case "print-lesson":  // コンソール出力の勉強

                            // 文字色（前景色）を赤に変更
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("これは赤い文字だぜ！");

                            // 背景色を青に変更
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.WriteLine("赤文字に青背景！");

                            // 元の色に戻す（大事！）
                            Console.ResetColor();
                            Console.WriteLine("ここはデフォルトの色に戻ったよ");

                            Console.WriteLine("３秒待つ（＾～＾）");
                            await Task.Delay(TimeSpan.FromSeconds(3));

                            // 今のカーソル位置を記憶
                            int oldLeft = Console.CursorLeft;  // 横位置
                            int oldTop = Console.CursorTop;   // 縦位置

                            // 色を変更
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.SetCursorPosition(0, 0);   // コンソールの左上隅に移動
                            Console.WriteLine("コンソールの左上隅に移動（＾～＾）！");

                            Console.SetCursorPosition(10, 5);  // 11列目、6行目に移動（0始まりなので）
                            Console.Write("ここに文字を書くぜ！");

                            // 色を戻す（大事！）
                            Console.ResetColor();
                            // 元の位置に戻す
                            Console.SetCursorPosition(oldLeft, oldTop);

                            Console.WriteLine("これから、進捗バーの真似事をするぜ（＾～＾）");
                            oldLeft = Console.CursorLeft;  // 横位置
                            oldTop = Console.CursorTop;   // 縦位置

                            await Task.Delay(TimeSpan.FromSeconds(1));
                            Console.SetCursorPosition(0, oldTop);  // 元の行の先頭に戻る
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" ");
                            Console.ResetColor();
                            Console.Write("1");
                            Console.Write(" ".PadRight(Console.BufferWidth)); // 残りを空白で消す。カーソルは次の行の先頭へ行く。

                            await Task.Delay(TimeSpan.FromSeconds(1));
                            Console.SetCursorPosition(0, oldTop);
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("  ");
                            Console.ResetColor();
                            Console.Write("2");
                            Console.Write(" ".PadRight(Console.BufferWidth));

                            await Task.Delay(TimeSpan.FromSeconds(1));
                            Console.SetCursorPosition(0, oldTop);
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("   ");
                            Console.ResetColor();
                            Console.Write("3");
                            Console.Write(" ".PadRight(Console.BufferWidth));

                            Console.SetCursorPosition(0, Console.CursorTop);  // 現在の行の先頭に戻る


                            /*
                               📍 NOTE:

                                    全部で16色あるよ：

                                    Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray
                                    DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White

                                    Console.Clear(); を呼ぶと、ウィンドウ全体の背景色も変わる（現在のBackgroundColorが適用される）。
                                    ANSIエスケープシーケンス を使えば、真のRGBカラー（24bit）や下線・太字なども使えるようになる。
                            */
                            return MuzREPL.MuzRequestType.None;

                        default:
                            Console.WriteLine($"知らないコマンドだぜ: {text}");
                            return MuzREPL.MuzRequestType.None;
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

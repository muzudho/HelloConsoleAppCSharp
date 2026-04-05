// See https://aka.ms/new-console-template for more information

using HelloConsoleAppCSharp;
using HelloConsoleAppCSharp.Infrastructure.Configuration;
using HelloConsoleAppCSharp.Infrastructure.Logging;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Infrastructure.Scheduler;
using HelloConsoleAppCSharp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Media;
using System.Text;

try
{
    // 開始日時を記憶しておくぜ（＾～＾）！
    var startDateTime = DateTime.Now;

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


            // 📍 NOTE:
            //
            //      キーボードからのコマンド入力を待機するぜ（＾～＾）！
            //
            await MuzREPL.RunAsync(
                printPromptAsync: async () =>
                {
                    Console.Write("> ");  // プロンプトを表示
                    await Task.CompletedTask;  // ここは非同期関数なので、Taskを返す必要がある。今回は特に非同期処理はないので、完了済みのTaskを返す。
                },
                evalAsync: async (line) =>
                {
                    // 入力が空白だけだったら、無視するぜ（＾～＾）
                    if (string.IsNullOrWhiteSpace(line)) return MuzREPL.MuzRequestType.None;

                    // ここに君のコマンド処理を書く。
                    // `exit` は REPL 内で処理されているから、ここでは処理されないぜ（＾～＾）！
                    switch (line)
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

                        //case "sound":
                        //    Console.WriteLine("音を鳴らしてみるぜ（＾～＾）");
                        //    //Console.Beep(800, 300);   // Windows 限定。

                        //    //SystemSounds は無かった。
                        //    //SystemSounds.Beep.Play();      // 標準のビープ
                        //    //SystemSounds.Asterisk.Play();  // 情報音（！マークっぽい）
                        //    //SystemSounds.Exclamation.Play(); // 警告音
                        //    //SystemSounds.Hand.Play();      // エラー音（×マーク）
                        //    //SystemSounds.Question.Play();  // 質問音

                        //    //これも無かった。
                        //    //SoundPlayer player = new SoundPlayer(@"C:\Windows\Media\Alarm01.wav"); // WAVファイル指定
                        //    //player.Play();        // 非同期で鳴らす（PlaySync()で同期）
                        //    //                      // player.PlaySync(); // 音が終わるまで待つ

                        //    //NuGet を調べた方がいい？
                        //    //dotnet add package NetCoreAudio

                        //    return MuzREPL.MuzRequestType.None;

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
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;

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
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write(" ");
                            Console.ResetColor();
                            Console.Write("1");
                            Console.Write(" ".PadRight(Console.BufferWidth)); // 残りを空白で消す。カーソルは次の行の先頭へ行く。

                            await Task.Delay(TimeSpan.FromSeconds(1));
                            Console.SetCursorPosition(0, oldTop);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("  ");
                            Console.ResetColor();
                            Console.Write("2");
                            Console.Write(" ".PadRight(Console.BufferWidth));

                            await Task.Delay(TimeSpan.FromSeconds(1));
                            Console.SetCursorPosition(0, oldTop);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Blue;
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

                        case "graph":   // グラフィカルの意味。
                            MuzREPL.IsPromptVisible = false; // プロンプトは消す。
                            MuzPageLayouts.PrintTitlePage();


                            // 📍 NOTE:
                            //
                            //      ここで、ボーダーを表示してみましょう（＾～＾）！
                            //      方眼紙などに画面の想像図を描いてから、位置とサイズを測って、コーディングしましょう（＾～＾）！
                            //
                            MuzBorders.PrintDoubleBorder(
                                left: 20,
                                top: 15,
                                width: 40,
                                height: 5,
                                fgColor: ConsoleColor.Black,
                                bgColor: ConsoleColor.Cyan);


                            // 📍 NOTE:
                            //
                            //     ここで、メニューを表示してみましょう（＾～＾）！
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
                                    //      一定間隔で点滅するカーソルを表示するぜ（＾～＾）！
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


                            return MuzREPL.MuzRequestType.None;

                        default:
                            Console.WriteLine($"知らないコマンドだぜ: {line}");
                            return MuzREPL.MuzRequestType.None;
                    }
                });


            Console.WriteLine("次はキー入力待機モード（＾～＾）");

            // （エンターキーが押されるまでの）入力中で未確定な文字列。
            StringBuilder draftString = new StringBuilder();
            int cursorPos = 0;  // カーソル位置（簡易版）

            // 📍 NOTE:
            //
            //      キーボードからのキー入力を待機するぜ（＾～＾）！
            //
            await MuzREPL.RunAsync(
                pushKeyAsync: async (key) =>
                {
                    // 📍 NOTE:
                    //
                    //      ここにお前のキー入力処理を書く。
                    //

                    // 通常の文字
                    if (key.KeyChar != '\0')
                    {
                        draftString.Insert(cursorPos, key.KeyChar);
                        cursorPos++;

                        // 入力文字を表示
                        Console.Write(key.KeyChar);

                        return MuzREPL.MuzRequestType.None;
                    }

                    // 例えば、F1〜F12のファンクションキーを検知することができるぜ（＾～＾）！
                    if (key.Key >= ConsoleKey.F1 && key.Key <= ConsoleKey.F12)
                    {
                        Console.WriteLine($"{key.Key} が押されたぜ！（特殊処理）");

                        // 例: F1でヘルプ、F5でクリア など
                        if (key.Key == ConsoleKey.F1)
                        {
                            Console.WriteLine("ヘルプを表示します...");
                        }

                        return MuzREPL.MuzRequestType.None;
                    }

                    // ［エンターキー］が押されたら、そこまで入力された文字列を返します。
                    if (key.Key == ConsoleKey.Enter)
                    {
                        // 確定した文字列。
                        var actualString = draftString.ToString();
                        draftString.Clear();  // 入力中の文字列をクリア
                        cursorPos = 0;       // カーソル位置もリセット
                        Console.WriteLine($"{actualString} が入力されたぜ（＾～＾）！");

                        return MuzREPL.MuzRequestType.None;
                    }

                    // １つ前に入力した文字を取り消します。
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (draftString.Length > 0 && cursorPos > 0)
                        {
                            draftString.Remove(cursorPos - 1, 1);
                            cursorPos--;

                            // 表示を修正（Backspaceで消す）
                            Console.Write("\b \b");
                        }

                        return MuzREPL.MuzRequestType.None;
                    }

                    // 矢印キーなどもここで追加可能（LeftArrow, RightArrowなど）

                    // その他のキー入力は無視するぜ（＾～＾）！
                    return MuzREPL.MuzRequestType.None;
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

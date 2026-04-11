// See https://aka.ms/new-console-template for more information

using HelloConsoleAppCSharp;
using HelloConsoleAppCSharp.Commands.PrintLesson;
using HelloConsoleAppCSharp.Infrastructure.Configuration;
using HelloConsoleAppCSharp.Infrastructure.Logging;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Infrastructure.Scheduler;
using HelloConsoleAppCSharp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

                        // コンソール出力の勉強
                        case "print-lesson":    return await MuzPrintLessonCommand.ExecuteAsync();

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

                        case "key-input":


                            // 📍 NOTE:
                            //
                            //      日本語入力への対応や、バックスペースキーの自力実装が難しいので、ここでは半角英数字キー１つの押下だけを想定しているぜ（＾～＾）！
                            //


                            Console.WriteLine("日本語入力への対応や、バックスペースキーの自力実装が難しいので、ここでは半角英数字キー１つの押下だけを想定しているぜ（＾～＾）！");
                            Console.WriteLine("キー入力待機中。［エンターキー］押下でループを抜けるぜ（＾～＾）...");

                            // （エンターキーが押されるまでの）入力中で未確定な文字列。
                            StringBuilder draftString = new StringBuilder();

                            while (true)  // 無限ループ。
                            {
                                // 📍 NOTE:
                                //
                                //      キー入力を受け取ります。
                                //      プログラムは、ユーザーがキーを押すまで、ここで待機します。  
                                //      `intercept`:  true でエコー（表示）しない。
                                //
                                ConsoleKeyInfo key = Console.ReadKey(
                                    intercept: true);

                                // 📍 NOTE:
                                //
                                //      ここにお前のキー入力処理を書く。
                                //

                                // 例えば、F1〜F12のファンクションキーを検知することができるぜ（＾～＾）！
                                if (key.Key >= ConsoleKey.F1 && key.Key <= ConsoleKey.F12)
                                {
                                    Console.WriteLine($"{key.Key} が押されたぜ！（特殊処理）");

                                    // 例: F1でヘルプ、F5でクリア など
                                    if (key.Key == ConsoleKey.F1)
                                    {
                                        Console.WriteLine("ヘルプを表示します...");
                                    }

                                    continue;
                                }

                                // ［エンターキー］が押されたら、そこまで入力された文字列を返します。
                                if (key.Key == ConsoleKey.Enter || key.KeyChar == '\r' || key.KeyChar == '\n')
                                {
                                    Console.WriteLine($"［エンターキー］が入力されたぜ（＾～＾）！");
                                    break;  // ループを抜ける
                                }

                                // 表示可能な文字（改行も拾ってしまうので、最後に行うこと）
                                if (key.KeyChar != '\0' && !char.IsControl(key.KeyChar))
                                {
                                    // 入力文字を表示
                                    Console.WriteLine(key.KeyChar);
                                    continue;
                                }

                                if (key.Key == ConsoleKey.Backspace)
                                {
                                    Console.WriteLine($"バックスペースキーを押したぜ（＾～＾）");
                                    continue;
                                }

                                if (key.Key == ConsoleKey.LeftArrow)
                                {
                                    Console.WriteLine($"［←］キーを押したぜ（＾～＾）");
                                    continue;
                                }

                                if (key.Key == ConsoleKey.UpArrow)
                                {
                                    Console.WriteLine($"［↑］キーを押したぜ（＾～＾）");
                                    continue;
                                }

                                if (key.Key == ConsoleKey.RightArrow)
                                {
                                    Console.WriteLine($"［→］キーを押したぜ（＾～＾）");
                                    continue;
                                }

                                if (key.Key == ConsoleKey.DownArrow)
                                {
                                    Console.WriteLine($"［↓］キーを押したぜ（＾～＾）");
                                    continue;
                                }

                                // 他の制御キーも、欲しかったら実装してくれだぜ（＾～＾）
                                // その他のキー入力は無視するぜ（＾～＾）！
                            }
                            return MuzREPL.MuzRequestType.None;

                        default:
                            Console.WriteLine($"知らないコマンドだぜ: {line}");
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

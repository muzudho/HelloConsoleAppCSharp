// See https://aka.ms/new-console-template for more information

using HelloConsoleAppCSharp;
using HelloConsoleAppCSharp.Commands.Graph;
using HelloConsoleAppCSharp.Commands.KeyInputWarmup;
using HelloConsoleAppCSharp.Commands.PrintWarmup;
using HelloConsoleAppCSharp.Infrastructure.Configuration;
using HelloConsoleAppCSharp.Infrastructure.Logging;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

                        // ［コンソール出力］の動作確認
                        case "print-warmup":    return await MuzPrintWarmupCommand.ExecuteAsync();

                        // ［graph］はグラフィカルの意味。
                        case "graph":   return await MuzGraphCommand.ExecuteAsync(startDateTime);

                        // ［キー入力］の動作確認
                        case "key-input-warmup":   return await MuzKeyInputWarmupCommand.ExecuteAsync();

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

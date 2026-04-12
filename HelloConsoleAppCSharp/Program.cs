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
    // フォルダーに分類しても中身が１ファイルしかないような、分類する方がコストになるような細かいものを、ここにまとめておくぜ（＾～＾）！
    var pgContext = new ProgramContext(
        // 開始日時を記憶しておくぜ（＾～＾）！
        launchDateTime: DateTime.Now);

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
                evalAsync: async (command) => await ProgramCommands.ExecuteAsync(
                        command,
                        pgContext));


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

// See https://aka.ms/new-console-template for more information

using HelloConsoleAppCSharp;
using HelloConsoleAppCSharp.Core.Features.Messages;
using HelloConsoleAppCSharp.Core.Infrastructure;
using HelloConsoleAppCSharp.Core.Infrastructure.REPL;
using HelloConsoleAppCSharp.Application;
using HelloConsoleAppCSharp.Infrastructure.Configuration;
using HelloConsoleAppCSharp.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

try
{
    Console.WriteLine("Hello, World!");

    // ホストビルドするぜ（＾～＾）！
    // ［ホスト］ってのは［汎用ホスト］のことで、いろいろ［サービス］っていう便利機能を付け加えることができるフレームワークみたいなもんだぜ（＾～＾）
    // それを［ビルド］するぜ（＾▽＾）
    await MuzHostHelper.RunAsync(
        commandLineArgs: args,
        beforeHostBuild: async (builder, executeBeforeBuild) =>
        {
            // お前のアプリケーションに合わせて、［サービス］を追加していってくれだぜ（＾～＾）！
            Console.WriteLine("ホストビルドする前にやることがあればここでやるぜ（＾～＾）！例えば、［サービス］を追加したりとか、そういうのだぜ（＾～＾）！");

            //
            // ［アプリケーション設定ファイル］サービスの登録
            //
            MuzAppSettingsHelper.SetupBeforeHostBuild(builder);

            //
            // ［ロギング］サービスの登録
            //
            await MuzLogging.SetupBeforeHostBuildAsync(
                builder: builder,
                onBootstrapLoggingEnabled: async (bootstrapLogger) =>
                {
                    // ここから `bootstrapLogger` を使った［ロギング］できる（＾～＾）！
                    bootstrapLogger.LogInformation("ホストビルド前だが、ブートストラップ・ログは出せるぜ（＾～＾）！");


                    // 📍 NOTE:
                    //
                    //      （あとで）ここへサービスを追加していくぜ（＾～＾）
                    //
                    await executeBeforeBuild(builder.Services);


                });
        },
        executeBeforeBuild: async (services) =>
        {


            // 📍 NOTE:
            //
            //      ここで、あとで［サービスの登録］とかやるぜ（＾▽＾）！
            //


            //
            // ［プログラム］サービスの登録
            //
            services.AddScoped<ApplicationService>();


            //
            // ［メッセージ］サービスの登録
            //
            services.AddScoped<MuzMessagesService>(sp =>
            {
                var service = new MuzMessagesService();
                // アプリケーションのルートからの相対パスを設定
                service.FilePath = "Assets/Messages.json";
                return service;
            });


        },
        afterHostBuild: async (builder, services, executeAfterHostBuild) =>
        {
            // こっちはコメントアウト（＾～＾）
            //await executeAfterHostBuild(services);  // ホストは有効になっているぜ（＾▽＾）！

            await MuzLogging.SetupAfterHostBuildAsync(
                configurationMgr: builder.Configuration,
                onLoggingServiceEnabled: async () =>
                {
                    // ここから、以下のようにして、ロガー（ILogger）を使えるようになったぜ（＾▽＾）！
                    //var logger = host.Services.GetRequiredService<ILogger<Program>>();

                    await executeAfterHostBuild(
                        services);     // （ホストの様々な機能とか、このアプリケーションで使わないから）［サービスプロバイダー］だけ渡すぜ（＾～＾）
                });
        },
        executeAfterHostBuild: async (services) =>
        {
            // ここから［サービス・プロバイダー］（services）が使えるぜ（＾▽＾）！
            Console.WriteLine("ここから［サービス・プロバイダー］（services）が使えるぜ（＾▽＾）！");

            // ［アプリケーション設定ファイル］を動作確認してみようぜ（＾～＾）
            var appSettings = services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
            Console.WriteLine($"AppName: {appSettings.AppName}");

            // ［ロガー］を動作確認してみようぜ（＾～＾）
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("デフォルトのログを書き込むぜ～（＾～＾）！");

            // ［ロガー別のログ］を動作確認してみようぜ（＾～＾）
            var loggingSvc = services.GetRequiredService<IMuzLoggingService>();
            loggingSvc.Others.LogInformation("その他のログだぜ（＾～＾）");
            loggingSvc.Verbose.LogInformation("大量のログだぜ（＾～＾）");

            // フォルダーに分類するほどでもない雑多な変数は、［プログラム・サービス］にまとめておくぜ（＾～＾）！
            var pgSvc = services.GetRequiredService<ApplicationService>();
            // ［アプリケーションを開始した日時］を記憶しておくぜ（＾～＾）！
            pgSvc.LaunchDateTime = DateTime.Now;


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
                evalAsync: async (command) => await ProgramCommands.ExecuteAsync(services, command));


        });


}
catch (Exception ex)
{
    Console.WriteLine($"アプリが死んだ... ログも取れない、むずでょ泣く。{ex}");
}
finally
{
    Console.WriteLine("アプリが終了するぜ（＾～＾）！");

    // お前のアプリケーションに合わせて、［片付け］コードを追加していってくれだぜ（＾～＾）！
    MuzLogging.Cleanup(); // ロガーのクリーンアップ（＾～＾）
}

// Program.cs を最後まで実行しても、必ずしもアプリケーションが終了するわけじゃないぜ（＾～＾）！
// ［汎用ホスト］が動いている限りは、アプリケーションは終了しないぜ（＾～＾）！

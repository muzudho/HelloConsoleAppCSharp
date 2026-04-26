namespace HelloConsoleAppCSharp;

using HelloConsoleAppCSharp.Core.Features.Messages;
using HelloConsoleAppCSharp.Infrastructure.Configuration;
using HelloConsoleAppCSharp.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

/// <summary>
/// どんなコンソール・アプリを作るときでも、本題に入る前に似たようなコードを書くことになる……、そんな似たコード［ホストビルド］をまとめたヘルパークラスだぜ（＾～＾）！
/// </summary>
internal static class MuzInfrastructureHelper
{
    public static async Task BuildHostAsync(
        string[] commandLineArgs,
        Action<IServiceCollection> beforeBuild,
        Func<IServiceProvider, Task> onHostEnabled)
    {
        // ビルダー作成（＾～＾）
        //
        //      ここでは、［コンソールアプリケーション］用のビルダーを作るぜ（＾～＾）！
        //      もし、［ウェブアプリケーション］用のビルダーが必要なら、コメントアウトしてある行を使って、コードの対応個所の型を全部書き替えてくれだぜ（＾～＾）！
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(commandLineArgs);  // コンソールアプリケーション用（＾～＾）
        //WebApplicationBuilder builder = WebApplication.CreateBuilder(commandLineArgs);  // ウェブアプリケーション用（＾～＾）

        await SetupBeforeBuildAsync(    // ビルド前の処理（＾～＾）
            builder,
            beforeBuild: beforeBuild);
        var host = builder.Build(); // ホストビルド（＾～＾）

        //await onHostEnabled(host);  // ホストは有効になっているぜ（＾▽＾）！
        await MuzLogging.SetupAfterHostBuildAsync(
            configurationMgr: builder.Configuration,
            host: host,
            onLoggingServiceEnabled: async () =>
            {
                // ここから、以下のようにして、ロガー（ILogger）を使えるようになったぜ（＾▽＾）！
                //var logger = host.Services.GetRequiredService<ILogger<Program>>();

                await onHostEnabled(
                    host.Services);     // （ホストの様々な機能とか、このアプリケーションで使わないから）［サービスプロバイダー］だけ渡すぜ（＾～＾）
            });
    }


    /// <summary>
    /// ホストビルドする前にやることがあればここでやるぜ（＾～＾）！例えば、［サービス］を追加したりとか、そういうのだぜ（＾～＾）！
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static async Task SetupBeforeBuildAsync(
        IHostApplicationBuilder builder,
        Action<IServiceCollection> beforeBuild)
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
                beforeBuild(builder.Services);


                //
                // ［プログラム］サービスの登録
                //
                builder.Services.AddScoped<ProgramService>();

                //
                // ［メッセージ］サービスの登録
                //
                builder.Services.AddScoped<MuzMessagesService>(sp => 
                {
                    var service = new MuzMessagesService();
                    // アプリケーションのルートからの相対パスを設定
                    service.FilePath = "Assets/Messages.json";
                    return service;
                });


            });
    }


    /// <summary>
    /// アプリケーション終了時に片付けるぜ（＾▽＾）
    /// </summary>
    public static async Task Cleanup()
    {
        // お前のアプリケーションに合わせて、［片付け］コードを追加していってくれだぜ（＾～＾）！

        MuzLogging.Cleanup(); // ロガーのクリーンアップ（＾～＾）
    }
}

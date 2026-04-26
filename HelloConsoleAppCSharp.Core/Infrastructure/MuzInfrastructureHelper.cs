namespace HelloConsoleAppCSharp.Core.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class MuzInfrastructureHelper
{
    public static async Task RunAsync(
        string[] commandLineArgs,
        Action<IServiceCollection> beforeBuild,
        Func<Func<IServiceProvider, Task>, Task> onLoggingAsync,
        Func<IServiceProvider, Task> onHostEnabledAsync)
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

        await onLoggingAsync(onHostEnabledAsync);
        ////await onHostEnabled(host);  // ホストは有効になっているぜ（＾▽＾）！
        //await MuzLogging.SetupAfterHostBuildAsync(
        //    configurationMgr: builder.Configuration,
        //    host: host,
        //    onLoggingServiceEnabled: async () =>
        //    {
        //        // ここから、以下のようにして、ロガー（ILogger）を使えるようになったぜ（＾▽＾）！
        //        //var logger = host.Services.GetRequiredService<ILogger<Program>>();

        //        await onHostEnabled(
        //            host.Services);     // （ホストの様々な機能とか、このアプリケーションで使わないから）［サービスプロバイダー］だけ渡すぜ（＾～＾）
        //    });
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
        //MuzAppSettingsHelper.SetupBeforeHostBuild(builder);

        //
        // ［ロギング］サービスの登録
        //
        //await MuzLogging.SetupBeforeHostBuildAsync(
        //    builder: builder,
        //    onBootstrapLoggingEnabled: async (bootstrapLogger) =>
        //    {
        //        // ここから `bootstrapLogger` を使った［ロギング］できる（＾～＾）！
        //        bootstrapLogger.LogInformation("ホストビルド前だが、ブートストラップ・ログは出せるぜ（＾～＾）！");

        //        // 📍 NOTE:
        //        //
        //        //      （あとで）ここへサービスを追加していくぜ（＾～＾）
        //        //
        //        beforeBuild(builder.Services);


        //    });
    }


    /// <summary>
    /// アプリケーション終了時に片付けるぜ（＾▽＾）
    /// </summary>
    public static async Task Cleanup()
    {
        // お前のアプリケーションに合わせて、［片付け］コードを追加していってくれだぜ（＾～＾）！

        //MuzLogging.Cleanup(); // ロガーのクリーンアップ（＾～＾）
    }
}

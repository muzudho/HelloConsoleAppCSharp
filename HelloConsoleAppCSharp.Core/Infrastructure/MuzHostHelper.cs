namespace HelloConsoleAppCSharp.Core.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class MuzHostHelper
{
    public static async Task RunAsync(
        string[] commandLineArgs,
        Action<IServiceCollection> executeBeforeBuild,
        Func<IHostApplicationBuilder, IServiceProvider, Func<IServiceProvider, Task>, Task> afterHostBuild,
        Func<IServiceProvider, Task> executeAfterHostBuild)
    {
        // ビルダー作成（＾～＾）
        //
        //      ここでは、［コンソールアプリケーション］用のビルダーを作るぜ（＾～＾）！
        //      もし、［ウェブアプリケーション］用のビルダーが必要なら、コメントアウトしてある行を使って、コードの対応個所の型を全部書き替えてくれだぜ（＾～＾）！
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(commandLineArgs);  // コンソールアプリケーション用（＾～＾）
        //WebApplicationBuilder builder = WebApplication.CreateBuilder(commandLineArgs);  // ウェブアプリケーション用（＾～＾）

        // ホストビルド前の処理（＾～＾）
        await SetupBeforeBuildAsync(
            builder,
            executeBeforeBuild: executeBeforeBuild);

        // ホストビルド（＾～＾）
        var host = builder.Build();

        // ホストビルド後の処理（＾～＾）
        await afterHostBuild(builder, host.Services, executeAfterHostBuild);
    }


    /// <summary>
    /// ホストビルドする前にやることがあればここでやるぜ（＾～＾）！例えば、［サービス］を追加したりとか、そういうのだぜ（＾～＾）！
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static async Task SetupBeforeBuildAsync(
        IHostApplicationBuilder builder,
        Action<IServiceCollection> executeBeforeBuild)
    {
        // お前のアプリケーションに合わせて、［サービス］を追加していってくれだぜ（＾～＾）！
        Console.WriteLine("ホストビルドする前にやることがあればここでやるぜ（＾～＾）！例えば、［サービス］を追加したりとか、そういうのだぜ（＾～＾）！");

        ////
        //// ［アプリケーション設定ファイル］サービスの登録
        ////
        //MuzAppSettingsHelper.SetupBeforeHostBuild(builder);

        ////
        //// ［ロギング］サービスの登録
        ////
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
        //        executeBeforeBuild(builder.Services);


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

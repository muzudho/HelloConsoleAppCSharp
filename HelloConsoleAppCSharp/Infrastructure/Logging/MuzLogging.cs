namespace HelloConsoleAppCSharp.Infrastructure.Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Settings.Configuration;

/// <summary>
///     <pre>
/// ロギング。
/// 
///     - ロギングは自分でプログラムを書かず、Serilog に任せるぜ（＾～＾）！
///     </pre>
/// </summary>
internal static class MuzLogging
{


    // ========================================
    // 窓口メソッド
    // ========================================


    /// <summary>
    /// ［ホストビルド］の前に、諸々の設定をするぜ（＾～＾）
    /// </summary>
    /// <returns></returns>
    public static async Task SetupBeforeHostBuildAsync(
        IHostApplicationBuilder builder,
        Func<Microsoft.Extensions.Logging.ILogger, Task> onBootstrapLoggingEnabled)
    {
        // （ホストビルド前にログを出したいこともあるので、ホストビルド前に） Serilog のデフォルト状態をセットアップするぜ（＾▽＾）！
        // 設定ファイルを読込む前だから、設定はここでするぜ（＾～＾）
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                "Logs/Bootstrap-.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateBootstrapLogger();  // ホストビルド前のログ用

        // ［Serilog］パッケージをNuGetでダウンロードしてきているはず。そのロガーを、Microsoft の ILogger にブリッジ。これで ILogger<T> を使うと Serilog が使われるぜ（＾～＾）
        builder.Services.AddSerilog();

        // とりあえずブリッジだけはしたんで、さっき設定したロガー１つをここで作成（＾～＾）
        var bootstrapLogger = builder.Logging.Services.BuildServiceProvider()
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("BootstrapLogger");   // カテゴリ名は自由（Program とかでもOK）

        // ［複数のロガーを使い分けるサービス］を DI注入するぜ（＾～＾）
        builder.Services.AddSingleton<IMuzLoggingService, MuzLoggingService>();

        try
        {
            // ロギングはまだ DIサービスに登録できていないが、 bootstrapLogger だけは使えるようにしたぜ（＾～＾）
            await onBootstrapLoggingEnabled(bootstrapLogger);
        }
        catch (Exception ex)
        {
            // せっかくブートストラップ・ロガーがあるんで、ここで例外をログに出しておくぜ（＾～＾）！
            bootstrapLogger.LogCritical(ex, "アプリが死んだ... むずでょ泣く");
        }
    }


    /// <summary>
    /// ［ホストビルド］の後に、諸々の設定をするぜ（＾～＾）
    /// </summary>
    /// <returns></returns>
    public static async Task SetupAfterHostBuildAsync(
        IConfigurationManager configurationMgr,
        Func<Task> onLoggingServiceEnabled)
    {
        // ［ホストビルド］後だから、［アプリケーション設定ファイル］のサービス設定も終わって、ファイルを読めるようになってるだろ（＾▽＾）
        // ［設定ファイル］から［Serilog］の設定を読込むぜ（＾▽＾）！
        MuzLogging.SetupFrom(configurationMgr);

        // ここから、以下のようにして、ロガー（ILogger）を使えるようになったぜ（＾▽＾）！
        //var logger = host.Services.GetRequiredService<ILogger<Program>>();
        await onLoggingServiceEnabled();
    }


    /// <summary>
    /// アプリケーションの終了前に呼び出してくれだぜ（＾～＾）
    /// </summary>
    public static void Cleanup()
    {
        Log.CloseAndFlush();
    }


    // ========================================
    // 内部メソッド
    // ========================================


    /// <summary>
    /// ［設定ファイル］からロガーの設定を行う。
    /// </summary>
    private static void SetupFrom(IConfigurationManager configurationMgr)
    {
        var options = new ConfigurationReaderOptions
        {
            SectionName = "LoggingExtensions:Serilog"  // ← ここでセクションを指定！
        };
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configurationMgr, options)  // ← 設定ファイルから［Serilog］セクション全部読み込む！
            .Enrich.FromLogContext()  // 任意: 便利な enricher
            .CreateLogger();
    }
}

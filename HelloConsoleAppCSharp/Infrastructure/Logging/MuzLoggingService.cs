namespace HelloConsoleAppCSharp.Infrastructure.Logging;

using Microsoft.Extensions.Logging;

internal class MuzLoggingService : IMuzLoggingService
{


    // ========================================
    // 生成／破棄
    // ========================================


    /// <summary>
    /// 
    /// </summary>
    /// <param name="loggerFactory">DI から取る</param>
    public MuzLoggingService(ILoggerFactory loggerFactory)
    {
        // ［ロガー］を分ける動作確認してみようぜ（＾～＾） ［カテゴリー名］でロガー作成（ここがポイント！）
        this.Others = loggerFactory.CreateLogger("MuzOthersLogger");    // ［その他のログ用］（＾～＾）
        this.Verbose = loggerFactory.CreateLogger("MuzVerboseLogger");  // ［大量のログ用］（＾～＾）
    }


    // ========================================
    // 窓口プロパティ
    // ========================================


    public ILogger Others { get; init; }

    public ILogger Verbose { get; init; }
}

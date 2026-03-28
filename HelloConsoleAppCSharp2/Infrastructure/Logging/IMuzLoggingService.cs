namespace HelloConsoleAppCSharp.Infrastructure.Logging;

using Microsoft.Extensions.Logging;

/// <summary>
/// 複数のロガーをまとめたやつだぜ（＾▽＾）
/// </summary>
internal interface IMuzLoggingService
{


    // ========================================
    // 窓口プロパティ
    // ========================================


    /// <summary>
    /// その他のロガー。
    /// </summary>
    public ILogger Others { get; init; }

    /// <summary>
    /// 大量のロガー。
    /// </summary>
    public ILogger Verbose { get; init; }
}

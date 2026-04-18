namespace HelloConsoleAppCSharp.Experiment;

using HelloConsoleAppCSharp.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

/// <summary>
/// 実験用。
/// </summary>
internal class MuzExperiment
{


    // ========================================
    // 生成／破棄
    // ========================================


    public MuzExperiment(IMuzLoggingService loggingSvc)
    {
        loggingSvc.Others.LogInformation("MuzExperiment のコンストラクタが呼ばれたぜ（＾～＾）！");
    }
}

namespace HelloConsoleAppCSharp;

/// <summary>
///     <pre>
/// プログラム全体からアクセスできる変数。
/// 
///     - フォルダーに分類しても中身が１ファイルしかないような、分類する方がコストになるような細かいものを、ここにまとめておくぜ（＾～＾）！
///     </pre>
/// </summary>
internal class ProgramContext
{


    // ========================================
    // 生成／破棄
    // ========================================


    /// <summary>
    /// ここで初期化しておくぜ（＾～＾）！
    /// </summary>
    /// <param name="launchDateTime"></param>
    internal ProgramContext(
        DateTime launchDateTime)
    {
        this.LaunchDateTime = launchDateTime;
    }


    // ========================================
    // 窓口データメンバー
    // ========================================


    /// <summary>
    /// 起動日時を記憶しておくぜ（＾～＾）！
    /// </summary>
    public DateTime LaunchDateTime { get; init; }


}

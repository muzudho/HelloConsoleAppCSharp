namespace HelloConsoleAppCSharp.Features.Messages;

/// <summary>
/// メッセージを扱うサービスだぜ（＾～＾）！例えば、［メッセージのキャッシュ］を管理したりとか、そういうのをやるぜ（＾～＾）！
/// </summary>
internal class MuzMessagesService
{


    // ========================================
    // 構成
    // ========================================


    #region ［メッセージのキャッシュ］

    /// <summary>
    /// メッセージのキャッシュを格納しておくぜ（＾～＾）！
    /// </summary>
    internal Dictionary<string, string> MessageCache { get; set; } = new Dictionary<string, string>();

    #endregion


}

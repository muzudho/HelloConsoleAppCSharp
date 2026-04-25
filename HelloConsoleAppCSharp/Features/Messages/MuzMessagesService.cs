namespace HelloConsoleAppCSharp.Features.Messages;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// メッセージを扱うサービスだぜ（＾～＾）！例えば、［メッセージのキャッシュ］を管理したりとか、そういうのをやるぜ（＾～＾）！
/// </summary>
public class MuzMessagesService
{


    // ========================================
    // 構成
    // ========================================


    #region ［キャッシュ］

    /// <summary>
    /// メッセージのキャッシュを格納しておくぜ（＾～＾）！
    /// </summary>
    internal Dictionary<string, string> MessageCache { get; set; } = new Dictionary<string, string>();

    #endregion

    #region ［取得］

    /// <summary>
    /// メッセージの取得
    /// </summary>
    /// <param name="key">メッセージのキー</param>
    /// <param name="forceLoad">キャッシュを捨てて再読込したいとき真</param>
    /// <returns>メッセージ</returns>
    public string GetMessage(
        string key,
        bool forceLoad = false)
    {
        const string filePath = "Assets/Messages.json";     // ファイル名は埋込でいいかな（＾～＾）
        const string defaultMessage = "メッセージが見つからないぜ（＾～＾）！";    // 見つからないときのデフォルトメッセージも埋込でいいかな（＾～＾）

        // 強制読込（空のときも読込）
        if (!this.MessageCache.Any() || forceLoad) this.MessageCache = MuzMessagesHelper.GetMessagesAsDictionary(filePath);

        return this.MessageCache.GetValueOrDefault(key, defaultMessage);
    }

    #endregion


}

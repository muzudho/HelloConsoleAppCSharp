namespace HelloConsoleAppCSharp.Features.Messages;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// メッセージを扱うサービスだぜ（＾～＾）！例えば、［メッセージのキャッシュ］を管理したりとか、そういうのをやるぜ（＾～＾）！
/// </summary>
public class MuzMessagesService
{


    // ========================================
    // 窓口データメンバー
    // ========================================


    /// <summary>
    /// メッセージのキャッシュを格納しておくぜ（＾～＾）！
    /// </summary>
    internal Dictionary<string, string> MessageCache { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// メッセージ・ファイルへのパス
    /// </summary>
    public string FilePath { get; set; } = "Assets/Messages.json";

    /// <summary>
    /// 見つからないときのデフォルトメッセージ
    /// </summary>
    public string DefaultMessage { get; set; } = "メッセージが見つからないぜ（＾～＾）！";


    // ========================================
    // 窓口メソッド
    // ========================================


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
        // 強制読込（空のときも読込）
        if (!this.MessageCache.Any() || forceLoad) this.MessageCache = MuzMessagesHelper.GetMessagesAsDictionary(this.FilePath);

        return this.MessageCache.GetValueOrDefault(key, this.DefaultMessage);
    }


}

namespace HelloConsoleAppCSharp.Core.Features.Messages;

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
    public Dictionary<string, string> MessageCache { get; set; } = new Dictionary<string, string>();


    /// <summary>
    /// メッセージ・ファイルへのパス（実行ファイルのディレクトリからの相対パス）
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
        if (!this.MessageCache.Any() || forceLoad)
        {
            // 実行ファイルのディレクトリを基準にした絶対パスを生成
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var absolutePath = Path.Combine(baseDirectory, this.FilePath);
            this.MessageCache = MuzMessagesHelper.GetMessagesAsDictionary(absolutePath);
        }

        return this.MessageCache.GetValueOrDefault(key, this.DefaultMessage);
    }


}

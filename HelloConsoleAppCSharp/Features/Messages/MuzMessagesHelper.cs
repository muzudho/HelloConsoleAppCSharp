namespace HelloConsoleAppCSharp.Features.Messages;

using System.Text.Json;

/// <summary>
/// メッセージ・リソース読取の自前実装
/// </summary>
internal static class MuzMessagesHelper
{
    /// <summary>
    /// JSONファイルからメッセージを読み込み、Dictionary<string, string> で返す。
    /// 値が string[] の場合は "\n" で結合して1つの文字列にする。
    /// </summary>
    public static Dictionary<string, string> GetMessagesAsDictionary(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"メッセージファイルが見つかりません: {filePath}");
        }

        // ファイル全体をただの文字列として読み込む
        string jsonAsPlainText = File.ReadAllText(filePath);

        // Dictionary<string, object> で一度デシリアライズ（string も string[] も両方受け入れる）
        Dictionary<string, object?>? rawDict = JsonSerializer.Deserialize<Dictionary<string, object?>>(
            json: jsonAsPlainText,
            options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,   // キー名の大文字小文字を無視（念のため）
                ReadCommentHandling = JsonCommentHandling.Skip
            });

        var result = new Dictionary<string, string>(StringComparer.Ordinal); // キー比較は大文字小文字区別

        if (rawDict == null) return result;

        // ここで、JSONの構造を確認して、辞書の値が文字列の配列になっていれば、 "\n" で結合したい。
        foreach (var kvp in rawDict)
        {
            string key = kvp.Key;
            object? value = kvp.Value;

            string finalValue = value switch
            {
                // 普通の文字列の場合
                string str => str,

                // 配列の場合
                JsonElement element when element.ValueKind == JsonValueKind.Array =>
                    string.Join("\n", element.EnumerateArray().Select(e => e.GetString() ?? string.Empty)),

                // 文字列のJSON要素の場合
                JsonElement element when element.ValueKind == JsonValueKind.String =>
                    element.GetString() ?? string.Empty,

                // その他は念のためToString
                _ => value?.ToString() ?? string.Empty
            };

            result[key] = finalValue;
        }

        return result;
    }
}

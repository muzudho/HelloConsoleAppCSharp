namespace HelloConsoleAppCSharp.Application.Features.Messages;

using HelloConsoleAppCSharp.Core.Infrastructure.REPL;
using System.Text.Json;

internal static class MuzReadMessagesWarmupCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        try
        {
            // TODO: Messages.json ファイルを読込みます。
            string filePath = "Assets/MessagesWarmup.json";  // ← あなたのJSONファイルのパスに変更

            // ファイル全体を文字列として読み込む
            string jsonString = File.ReadAllText(filePath);

            // Dictionary<string, string> にデシリアライズ
            Dictionary<string, string>? items = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            if (items != null)
            {
                Console.WriteLine("読み込み成功！アイテム一覧：\n");

                foreach (var item in items)
                {
                    Console.WriteLine($"【{item.Key}】");
                    Console.WriteLine(item.Value);
                    Console.WriteLine("-------------------");
                }

                var target = "セメント";
                Console.WriteLine($"［{target}］の説明をもう１回見てみるぜ（＾～＾）");

                // 特定のアイテムを取り出す例
                if (items.TryGetValue(target, out string? desc))
                {
                    Console.WriteLine($"{target}の説明: {desc}");
                }
            }

            return MuzREPLRequestType.None;
        }
        catch (Exception ex)
        {
            // TODO: ログファイル出力したい。
            Console.WriteLine($"エラー: {ex.Message}");
            throw;
        }
    }
}


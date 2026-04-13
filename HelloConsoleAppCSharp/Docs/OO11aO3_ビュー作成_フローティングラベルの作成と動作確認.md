# ビュー作成　＞　フローティングラベルの作成と動作確認

コンソール画面上の指定の位置に、文字列を表示するフローティングラベルを作成してみましょう。  


## フォルダーとファイルの構成

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Views
|	+-- 📄 MuzLabelViews.cs
📄 ProgramCommands.cs
```


## ビューの作成

📄 `HelloConsoleAppCSharp/Views/MuzLabelViews.cs`:  

```csharp
namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// フローティングラベル
/// </summary>
internal static class MuzLabelViews
{
    public static async Task PrintAsync(
        string text,
        int left = 0,
        int top = 0)
    {
        // 処理の後、カーソルを元の位置に戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            int currentTop = top;

            // まず、行ごとに分割して、各行を順番に表示するぜ（＾～＾）
            string[] lines = text.Split('\n');

            foreach (string line in lines)
            {
                // 行頭
                Console.SetCursorPosition(left, currentTop);

                // 行を表示するぜ（＾～＾）
                Console.WriteLine(line);
                currentTop++;
            }
        });
    }
}
```


## コマンド実行部へ、コマンド追加

📄 `HelloConsoleAppCSharp/ProgramCommands.cs`:  

```csharp
～前後略～

/// <summary>
/// コマンド実行部
/// </summary>
internal static class ProgramCommands
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        string command,
        ProgramContext pgContext)
    {


        ～中略～


        switch (commandName)
        {


            // ～中略～
            
            
            // ［フローティングラベル］の動作確認
            case "floating-label-warmup":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.White,
                    bgColor: ConsoleColor.Green,
                    onColorChanged: async () =>
                    {
                        await MuzLabelViews.PrintAsync(
                            left: 20,
                            top: 5,
                            text: "フローティングラベルのウォームアップだぜ（＾～＾）！\n複数行にも対応だぜ（＾～＾）！");
                    });
                return MuzRequestType.None;
            
            
            // ～中略～


            default:
                Console.WriteLine($"知らないコマンドだぜ: {command}");
                return MuzRequestType.None;
        }
    }
}
```

これで、このコンソール・アプリケーションを起動し、 `floating-label-warmup` と入力すると、［壁面］が塗りつぶされます。  

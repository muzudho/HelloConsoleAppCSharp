# ビュー作成　＞　フローティングテキストの作成と動作確認

コンソール画面への Print を、ここではビュー（View）と呼ぶことにします。  
プログラミングでは View と呼ぶと、色んなものを指すので、混乱しないようにしてください。だいたい、目の前に見えているもの、ぐらいの意味です。  

コンソール画面上の指定の位置に、文字列を表示するフローティングテキストを作成してみましょう。  


## フォルダーとファイルの構成

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Views
|	+-- 📄 MuzFloatingTextViews.cs
📄 ProgramCommands.cs
```


## ビューの作成

📄 `HelloConsoleAppCSharp/Views/MuzFloatingTextViews.cs`:  

```csharp
namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// フローティングテキスト
/// </summary>
internal static class MuzFloatingTextViews
{
    public static async Task PrintWallAsync(
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
            
            
            // ［フローティングテキスト］の動作確認
            case "floating-text-warmup":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.White,
                    bgColor: ConsoleColor.Green,
                    onColorChanged: async () =>
                    {
                        await MuzFloatingTextViews.PrintAsync(
                            left: 20,
                            top: 5,
                            text: "フローティングテキストのウォームアップだぜ（＾～＾）！\n複数行にも対応だぜ（＾～＾）！");
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

これで、このコンソール・アプリケーションを起動し、 `floating-text-warmup` と入力すると、［壁面］が塗りつぶされます。  

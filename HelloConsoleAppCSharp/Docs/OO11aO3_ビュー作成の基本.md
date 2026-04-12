# ビュー作成の基本

コンソール画面への Print を、ここではビュー（View）と呼ぶことにします。  
プログラミングでは View と呼ぶと、色んなものを指すので、混乱しないようにしてください。だいたい、目の前に見えているもの、ぐらいの意味です。  


## フォルダーと空ファイルの作成

以下のフォルダーと空ファイルを作成してください。  

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Commands
|   +-- 📁 GraphWarmup
|       +-- 📄 MuzGraphWarnupCommand.cs        # ファイルを新規作成
+-- 📁 Views
|   +-- 📄 MuzPageLayouts.cs
+-- 📄 ProgramCommands.cs
```


## ページレイアウトの作成

📄 `HelloConsoleAppCSharp/Views/MuzPageLayouts.cs`:  

```csharp
namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// コンソール画面を、１画面に見立てて、枠組みを表示するためのものです。
/// </summary>
internal static class MuzPageLayouts
{
    public static async Task PrintTitlePageAsync()
    {
        int wallLeft = 0;
        int wallTop = 0;
        int wallWidth = 80;
        int wallHeight = 25;
        ConsoleColor wallColor = ConsoleColor.Cyan;

        // 壁面を塗りつぶす。
        await MuzWallViews.PrintWallAsync(
            wallLeft: wallLeft,
            wallTop: wallTop,
            wallWidth: wallWidth,
            wallHeight: wallHeight,
            wallColor: wallColor);

        // 処理の後、カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
            {
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Black,
                    bgColor: ConsoleColor.Cyan,
                    onColorChanged: async () =>
                    {
                        // 画面の真ん中辺りにタイトルを表示するとかっこいい。
                        var title = "Hello Console App C#";
                        var titleLeft = (wallWidth - title.Length) / 2;  // 漢字は横幅計算が難しいので、今回は半角英字だけのタイトルにします。
                        var titleTop = wallHeight / 2;
                        Console.SetCursorPosition(titleLeft, titleTop);
                        Console.Write(title);

                        // 画面の下辺辺りに、制作年、開発者を表示するとかっこいい。
                        var credit = "(C) 2026 by Muzudho ; MIT License";
                        var creditLeft = (wallWidth - credit.Length) / 2;
                        var creditTop = wallHeight - 1;
                        Console.SetCursorPosition(creditLeft, creditTop);
                        Console.Write(credit);
                    });
            });
    }
}
```


## コマンドの作成

📄 `HelloConsoleAppCSharp/Commands/GraphWarmup/MuzGraphWarmupCommand.cs`:  

```csharp
namespace HelloConsoleAppCSharp.Commands.GraphWarmup;

using HelloConsoleAppCSharp.Views;

internal static class MuzGraphWarnupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        ProgramContext pgContext)
    {
        await MuzPageLayouts.PrintTitlePageAsync();
        return MuzRequestType.None;
    }
}
```


## コマンド実行部へ、コマンドの追加

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
            
            
            // ［graph］はグラフィカルの意味。
            case "graph-warmup":   return await MuzGraphWarnupCommand.ExecuteAsync(pgContext);
            
            
            // ～中略～


            default:
                Console.WriteLine($"知らないコマンドだぜ: {command}");
                return MuzRequestType.None;
        }
    }
}
```

これで、このコンソール・アプリケーションを起動し、 `graph-warmup` と入力すると、タイトルページが表示されるようになります。  

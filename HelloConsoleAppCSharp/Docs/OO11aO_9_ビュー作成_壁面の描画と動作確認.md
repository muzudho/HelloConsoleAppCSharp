# ビュー作成_壁面の描画と動作確認

コンソール画面に長方形の領域を決め、そこを壁面と呼ぶことにして、そこを色で塗りつぶすんだぜ（＾▽＾）  
例えば、左上隅から横に半角８０文字分、縦２５文字分を壁面としてみようぜ（＾▽＾）  

以下のような方眼紙を用意すると便利だぜ（＾▽＾）！  

```csharp
        //              4   8  12  16  20  24  28  32  36  40  44  48  52  56  60  64  68  72  76  80
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //        5 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       10 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       15 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       20 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       25 |                                                                               |
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
```


## フォルダーとファイルの構成

以下のフォルダーとファイルを用意してください。  

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Commands
|   +-- 📁 GraphWarmup
|       +-- 📄 MuzGraphWarnupCommand.cs        # ファイルを新規作成
+-- 📁 Views
|    +-- 📄 MuzWallViews.cs
+-- 📄 ProgramCommands.cs
```


## ビューの作成

📄 `HelloConsoleAppCSharp/Views/MuzWallViews.cs`:  

```csharp
namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// 壁面
/// </summary>
internal static class MuzWallViews
{
    public static async Task PrintWallAsync(
        int wallWidth,
        int wallHeight,
        ConsoleColor wallColor,
        int wallLeft = 0,
        int wallTop = 0)
    {

        // 処理の後、カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // いったん、背景色を黒にして、画面全体を塗りつぶします。
            await MuzConsoleHelper.SetColorAsync(
                bgColor: ConsoleColor.Black,
                onColorChanged: async () =>
                {
                    Console.Clear();
                });


            // 次に、壁面の色で、使用する［固定サイズ］の免責を塗りつぶします。
            await MuzConsoleHelper.SetColorAsync(
                bgColor: wallColor,
                onColorChanged: async () =>
                {
                    for (int dy = 0; dy < wallHeight; dy++)
                    {
                        Console.SetCursorPosition(wallLeft, wallTop + dy);

                        for (int dx = 0; dx < wallWidth; dx++)
                        {
                            Console.Write(' '); // 全体を決め打ちでもいいが、とりあえず１文字ずつプリントする。
                        }
                        Console.WriteLine();    // 改行
                    }
                });


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
            
            
            // ［壁面の塗り潰し］の動作確認
            case "wall-warmup":
                int wallHeight = 25;
                await MuzWallViews.PrintWallAsync(
                    wallWidth: 80,
                    wallHeight: wallHeight,
                    wallColor: ConsoleColor.Cyan);
                Console.SetCursorPosition(0, wallHeight);   // 改行
                return MuzRequestType.None;
            
            
            // ～中略～


            default:
                Console.WriteLine($"知らないコマンドだぜ: {command}");
                return MuzRequestType.None;
        }
    }
}
```

これで、このコンソール・アプリケーションを起動し、 `wall-warmup` と入力すると、［壁面］が塗りつぶされます。  

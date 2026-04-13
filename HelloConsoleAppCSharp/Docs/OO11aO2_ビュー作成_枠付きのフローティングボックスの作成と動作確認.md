# ビュー作成　＞　枠付きのフローティングボックスの作成と動作確認

コンソール・アプリケーションでは、ウィンドウのようなものはありませんから、枠付きの欄（ボックス；Box）を引くことで、ウィンドウのようなものを表現することにします。  


## フォルダーとファイルの構成

以下のフォルダーとファイルを用意してください。  

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Views
|	+-- 📄 MuzBoxViews.cs
+-- 📄 ProgramCommands.cs
```


## ビューの作成

📄 `HelloConsoleAppCSharp/Views/MuzBoxViews.cs`:  

```csharp
namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// 矩形の塗り潰し領域を［ボックス］と呼ぶことにします。
/// </summary>
internal static class MuzBoxViews
{


～中略～


    /// <summary>
    ///     <pre>
    /// 下図のような感じの罫線を表示します。
    /// 
    ///     ╔══════╗
    ///     ║      ║
    ///     ║      ║
    ///     ╚══════╝
    ///     </pre>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="top"></param>
    /// <param name="width">2 以上としてください。</param>
    /// <param name="height">2 以上としてください。</param>
    public static async Task PrintDoubleBorderAsync(
        int left,
        int top,
        int width,
        int height)
    {
        // 処理の後、カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // ダブル・ボーダーを表示するための文字
            char topLeft = '╔';
            char topRight = '╗';
            char bottomLeft = '╚';
            char bottomRight = '╝';
            char horizontal = '═';
            char vertical = '║';

            // 左上隅
            Console.SetCursorPosition(left, top);
            Console.Write(topLeft);

            // 上辺
            for (int dx = 0; dx < width - 2; dx++)
            {
                Console.Write(horizontal);
            }

            // 右上隅
            Console.Write(topRight);
            Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行

            // 上辺下辺を除いた中段行
            for (int dy = 0; dy < height - 2; dy++)
            {
                // 左辺
                Console.Write(vertical);

                // 中身（空白）
                for (int dx = 0; dx < width - 2; dx++)
                {
                    Console.Write(' ');
                }

                // 右辺
                Console.Write(vertical);
                Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行
            }

            // 左下隅
            Console.Write(bottomLeft);

            // 下辺
            for (int dx = 0; dx < width - 2; dx++)
            {
                Console.Write(horizontal);
            }

            // 右下隅
            Console.Write(bottomRight);
            Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行
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
            
            
            // ［枠付きのフローティングボックス］の動作確認
            case "double-boarder-floating-box-warmup":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.White,
                    bgColor: ConsoleColor.Green,
                    onColorChanged: async () =>
                    {
                        await MuzBoxViews.PrintDoubleBorderAsync(
                            left: 20 - 1,
                            top: 5 - 1,
                            width: 60 + 2,
                            height: 2 + 2);
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

これで、このコンソール・アプリケーションを起動し、 `double-boarder-floating-box-warmup` と入力すると、［枠付きのフローティングボックス］が描画されます。  

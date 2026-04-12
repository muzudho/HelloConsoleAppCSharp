# ビュー作成　＞　点滅ラベルの作成と動作確認

点滅するラベルの作り方を説明しますが、工夫が必要です。  
例えば、［テキストの入力待ち中］や［キー入力待ち中］はブロックされているので、画面上の表示を点滅させることができません。  
つまり、テキストを点滅させることができるのは、［テキストの入力待ち中］でも［キー入力待ち中］でもないときに限られます。  


## フォルダーとファイルの構成

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Infrastructure
|	+-- 📁 ConsoleCustom
|		+-- 📄 MuzConsoleHelper.cs
📄 ProgramCommands.cs
```


## ヘルパー関数の作成

📄 `HelloConsoleAppCSharp/Infrastructure/ConsoleCustom/MuzConsoleHelper.cs`:  

```csharp
namespace HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// コンソールの操作でよく使う機能をまとめたクラス
/// </summary>
internal static class MuzConsoleHelper
{


	～中略～


    /// <summary>
    ///     <pre>
    /// テキストを点滅させます。
    /// 
    /// 仕組みとしては、２組みの［前景色、背景色］を用意して、一定間隔で切り替えることで点滅させる感じだぜ（＾～＾）！
    ///     </pre>
    /// </summary>
    /// <param name="onColorChanged">色を変更した後に実行する処理</param>
    /// <param name="fgColor">設定する前景色</param>
    /// <param name="bgColor">設定する背景色</param>
    public static async Task BlinkAsync(
        ConsoleColor fgColor1,
        ConsoleColor bgColor1,
        ConsoleColor fgColor2,
        ConsoleColor bgColor2,
        bool isColor2,
        Func<Task> onColorChanged)
    {
        // 現在の色を記憶
        var oldFgColor = Console.ForegroundColor;
        var oldBgColor = Console.BackgroundColor;

        // 色を設定
        if (isColor2)
        {
            Console.ForegroundColor = fgColor2;
            Console.BackgroundColor = bgColor2;
        }
        else
        {
            Console.ForegroundColor = fgColor1;
            Console.BackgroundColor = bgColor1;
        }

        // 色を変更した後の処理を実行
        await onColorChanged();

        // 色を戻す
        Console.ForegroundColor = oldFgColor;
        Console.BackgroundColor = oldBgColor;
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
            
            
            // ［点滅ラベル］の動作確認
            case "blink-label-warmup":
                await MuzConsoleHelper.BlinkAsync(
                    fgColor1: ConsoleColor.White,
                    bgColor1: ConsoleColor.Green,
                    fgColor2: ConsoleColor.Green,   // 色の反転
                    bgColor2: ConsoleColor.White,
                    isColor2: (DateTime.Now.Millisecond / 500) % 2 == 0, // 0.5秒ごとに色切替
                    onColorChanged: async () =>
                    {
                        await MuzLabelViews.PrintAsync(
                            left: 20,
                            top: 5,
                            text: "点滅ラベルのウォームアップだぜ（＾～＾）！\n複数行にも対応だぜ（＾～＾）！");
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

これで、このコンソール・アプリケーションを起動し、 `blink-label-warmup` と入力すると、［ラベルの点滅］の動作確認ができます。  

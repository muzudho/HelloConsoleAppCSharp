# アプリケーション起動からの経過時間を表示する

コンソール・アプリケーションの利用を開始してから、何秒経過したかを画面に表示してみましょう。  


## フォルダーとファイルの構成

以下のフォルダーと空ファイルを用意してください。  

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Commands
|   +-- 📁 LaunchTimerWarmup
|       +-- 📄 MuzLaunchTimerWarmupCommand.cs
+-- 📁 Views
    +-- 📄 MuzWidgets.cs
```


## 経過時間の表示ウィジェットの作成

ウィジェット（Widget）は元来、Web アプリケーション用のガジェットという意味ですが、ここでは、コンソール画面に表示する部品のこともウィジェットと呼ぶことにします。  

📄 MuzWidgets.cs:  

```csharp
namespace HelloConsoleAppCSharp.Views;

/// <summary>
/// 画面上の部品（ウィジェット）を表示するためのものです。
/// </summary>
internal static class MuzWidgets
{
    /// <summary>
    /// 開始日時からの経過時間を表示します。
    /// </summary>
    /// <param name="launchDateTime">開始日時</param>
    public static async Task PrintErapsedTimeAsync(
        string label,
        DateTime launchDateTime,
        int left,
        int top)
    {
        // カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // 位置設定
            Console.SetCursorPosition(left, top);

            var elapsed = DateTime.Now - launchDateTime;

            Console.WriteLine($"{label}{elapsed.Hours:D2}°{elapsed.Minutes:D2}'{elapsed.Seconds:D2}\"{elapsed.Milliseconds:D3}");
        });
    }
}
```


## コマンドの作成

📄 `Commands/LaunchTimerWarmup/MuzLaunchTimerWarmupCommand.cs`:  

```csharp
namespace HelloConsoleAppCSharp.Commands.LaunchTimer;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;
using System;

internal static class MuzLaunchTimerWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        ProgramContext pgContext)
    {
        // 📍 NOTE:
        //
        //      無限ループの抜け方を説明しておきましょう。
        //
        Console.WriteLine("［エスケープキー］押下で点滅を終了するぜ（＾～＾）...");
        while (true)  // 無限ループ。
        {
            // アプリケーション起動からの経過時刻を表示するぜ（＾～＾）！
            await MuzWidgets.PrintErapsedTimeAsync(
                label: "Time ",
                launchDateTime: pgContext.LaunchDateTime,
                left: 62,
                top: 0);

            // キー入力がない場合は、少し待ってからループの先頭に戻るぜ（＾～＾）！
            if (!Console.KeyAvailable)
            {
                // およそ１／６０秒後にループの先頭に戻るぜ（＾～＾）
                Thread.Sleep(TimeSpan.FromMilliseconds(16));
                continue;
            }

            // 📍 NOTE:
            //
            //      キー入力を受け取ります。
            //      プログラムは、ユーザーがキーを押すまで、ここで待機します。  
            //      `intercept`:  true でエコー（表示）しない。
            //
            ConsoleKeyInfo key = Console.ReadKey(
                intercept: true);

            // ［エスケープキー］が押されたら、ループを抜けます。
            if (key.Key == ConsoleKey.Escape)
            {
                break;  // ループを抜ける
            }
        }
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
            
            
            // ［アプリケーション起動からの経過時間を表示する］の練習
            case "launch-timer-warmup": return await MuzLaunchTimerWarmupCommand.ExecuteAsync(pgContext);
            
            
            // ～中略～


            default:
                Console.WriteLine($"知らないコマンドだぜ: {command}");
                return MuzRequestType.None;
        }
    }
}
```

これで、このコンソール・アプリケーションを起動し、 `launch-timer-warmup` と入力すると、［タイマー］の動作確認ができます。  

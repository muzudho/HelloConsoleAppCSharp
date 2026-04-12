# キー入力の作成と動作確認

REPL では、キーボードの［↑］キーを押したからカーソルを上に動かす、といったようなことはできませんでした。  
この記事［キー入力］では、キーボードで押下されたキーを受け取る方法を説明します。  

ただし、ゲームのような、キーの押下（Key down）と放す（Key up）ことを区別することは、このプロジェクト［コンソール・アプリケーション］ではサポートできません。それは［ゲーム・アプリケーション］の範囲になります。  


## フォルダーとファイルの構成

以下のようにフォルダーとファイルを用意してください。  

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Commands
|   +-- 📁 KeyInputWarmup
|       +-- 📄 MuzKeyInputWarmupCommand.cs
+-- 📄 ProgramCommands.cs
```


## コマンドの作成

📄 MuzKeyInputWarmupCommand.cs:  

```csharp
namespace HelloConsoleAppCSharp.Commands.KeyInputWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using System.Text;

internal static class MuzKeyInputWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {

        // 📍 NOTE:
        //
        //      日本語入力への対応や、バックスペースキーの自力実装が難しいので、ここでは半角英数字キー１つの押下だけを想定しているぜ（＾～＾）！
        //
        Console.WriteLine("日本語入力への対応や、バックスペースキーの自力実装が難しいので、ここでは半角英数字キー１つの押下だけを想定しているぜ（＾～＾）！");

        // 📍 NOTE:
        //
        //      無限ループの抜け方を説明しておきましょう。
        //
        Console.WriteLine("キー入力待機中。［エンターキー］か、［エスケープキー］押下でループを抜けるぜ（＾～＾）...");

        while (true)  // 無限ループ。
        {
            // 📍 NOTE:
            //
            //      キー入力を受け取ります。
            //      プログラムは、ユーザーがキーを押すまで、ここで待機します。  
            //      `intercept`:  true でエコー（表示）しない。
            //
            ConsoleKeyInfo key = Console.ReadKey(
                intercept: true);

            // 📍 NOTE:
            //
            //      ここにお前のキー入力処理を書く。
            //

            // 例えば、F1〜F12のファンクションキーを検知することができるぜ（＾～＾）！
            if (key.Key >= ConsoleKey.F1 && key.Key <= ConsoleKey.F12)
            {
                Console.WriteLine($"{key.Key} が押されたぜ！（特殊処理）");

                // 例: F1でヘルプ、F5でクリア など
                if (key.Key == ConsoleKey.F1)
                {
                    Console.WriteLine("ヘルプを表示します...");
                }

                continue;
            }

            // ［エンターキー］が押されたら、そこまで入力された文字列を返します。
            if (key.Key == ConsoleKey.Enter || key.KeyChar == '\r' || key.KeyChar == '\n')
            {
                Console.WriteLine($"［エンターキー］が入力されたぜ（＾～＾）！");
                break;  // ループを抜ける
            }

            // ［エスケープキー］が押されたら、ループを抜けます。
            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine($"［エスケープキー］が入力されたぜ（＾～＾）！");
                break;  // ループを抜ける
            }

            // 表示可能な文字（改行も拾ってしまうので、最後に行うこと）
            if (key.KeyChar != '\0' && !char.IsControl(key.KeyChar))
            {
                // 入力文字を表示
                Console.WriteLine(key.KeyChar);
                continue;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                Console.WriteLine($"バックスペースキーを押したぜ（＾～＾）");
                continue;
            }

            if (key.Key == ConsoleKey.LeftArrow)
            {
                Console.WriteLine($"［←］キーを押したぜ（＾～＾）");
                continue;
            }

            if (key.Key == ConsoleKey.UpArrow)
            {
                Console.WriteLine($"［↑］キーを押したぜ（＾～＾）");
                continue;
            }

            if (key.Key == ConsoleKey.RightArrow)
            {
                Console.WriteLine($"［→］キーを押したぜ（＾～＾）");
                continue;
            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                Console.WriteLine($"［↓］キーを押したぜ（＾～＾）");
                continue;
            }

            // 他の制御キーも、欲しかったら実装してくれだぜ（＾～＾）
            // その他のキー入力は無視するぜ（＾～＾）！
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
            
            
            // ［キー入力］の動作確認
            case "key-input-warmup":   return await MuzKeyInputWarmupCommand.ExecuteAsync();
            
            
            // ～中略～


            default:
                Console.WriteLine($"知らないコマンドだぜ: {command}");
                return MuzRequestType.None;
        }
    }
}
```

これで、このコンソール・アプリケーションを起動し、 `key-input-warmup` と入力すると、［キー入力］の動作確認ができます。  

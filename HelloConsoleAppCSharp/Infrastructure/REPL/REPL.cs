namespace HelloConsoleAppCSharp.Infrastructure.REPL;

using System;

internal class REPL
{
    internal static async Task RunAsync(
        Func<string, Task> evalAsync)
    {
        Console.WriteLine("コマンド入力待機中...（exit で終了）");

        while (true)  // ここが無限ループ（REPLのLoop部分）
        {
            Console.Write("> ");                    // プロンプト表示
            string? command = Console.ReadLine();    // Read

            if (string.IsNullOrWhiteSpace(command)) continue;

            if (command.Trim().ToLower() == "exit")
                break;

            // ここでコマンドを処理（Eval）
            await evalAsync(command);

            // 結果はProcessCommand内でPrintされる
        }

        Console.WriteLine("終了したぜ！");
    }

    /*
    static void ProcessCommand(string cmd)
    {
        // ここに君のコマンド処理を書く
        switch (cmd.ToLower())
        {
            case "hello":
                Console.WriteLine("こんにちは！（＾～＾）");
                break;
            case "color":
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("文字色を変えたよ！");
                Console.ResetColor();
                break;
            default:
                Console.WriteLine($"知らないコマンドだぜ: {cmd}");
                break;
        }
    }
    */
}
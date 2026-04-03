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

            if (command.Trim() == "exit") break;

            // ここでコマンドを処理（Eval）
            await evalAsync(command);

            // 結果はProcessCommand内でPrintされる
        }

        Console.WriteLine("終了したぜ！");
    }
}
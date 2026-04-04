namespace HelloConsoleAppCSharp.Infrastructure.REPL;

using System;

internal class MuzREPL
{
    internal enum MuzRequestType
    {
        None = 0,

        /// <summary>
        /// REPLのループの終了要求
        /// </summary>
        Exit,
    }


    internal static async Task RunAsync(
        Func<Task> printPromptAsync,
        Func<string, Task<MuzRequestType>> evalAsync)
    {
        Console.WriteLine("コマンド入力待機中...（exit で終了）");

        while (true)  // ここが無限ループ（REPLのLoop部分）
        {
            await printPromptAsync();  // プロンプト表示


            // 📍 NOTE:
            //
            //      Shift キーや、↑←↓→キー、ファンクション・キーが押されたかどうかを判別するのは、
            //      大がかりになるので、今回は文字列のみ取得するサンプル・プログラムです。
            //
            string? command = Console.ReadLine();    // Read。処理はブロック（ここで止まる）されます。


            // 入力が空白だけだったら、無視していいだろう多分……（＾～＾）
            if (string.IsNullOrWhiteSpace(command)) continue;

            // ここでコマンドを処理（Eval）
            var request = await evalAsync(command);

            if (request == MuzRequestType.Exit) break;
        }

        Console.WriteLine("終了したぜ！");
    }
}
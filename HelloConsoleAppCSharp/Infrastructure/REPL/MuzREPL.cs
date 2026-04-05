namespace HelloConsoleAppCSharp.Infrastructure.REPL;

using System;

internal static class MuzREPL
{
    internal enum MuzRequestType
    {
        None = 0,

        /// <summary>
        /// REPLのループの終了要求
        /// </summary>
        Exit,
    }


    /// <summary>
    /// プロンプトを表示するか。
    /// </summary>
    public static bool IsPromptVisible { get; set; } = true;


    /// <summary>
    ///     <pre>
    /// ［エンターキー］を押すまで、コマンド入力待機するREPLのループ。
    ///     </pre>
    /// </summary>
    /// <param name="printPromptAsync"></param>
    /// <param name="evalAsync"></param>
    /// <returns></returns>
    internal static async Task RunAsync(
        Func<Task> printPromptAsync,
        Func<string, Task<MuzRequestType>> evalAsync)
    {
        Console.WriteLine("コマンド入力待機中...（exit で終了）");

        while (true)  // ここが無限ループ（REPLのLoop部分）
        {
            // プロンプト表示
            if (IsPromptVisible)
            {
                await printPromptAsync();
            }

            // 📍 NOTE:
            //
            //      Shift キーや、↑←↓→キー、ファンクション・キーが押されたかどうかを判別するのは、
            //      大がかりになるので、今回は文字列のみ取得するサンプル・プログラムです。
            //
            string? line = Console.ReadLine();    // Read。処理はブロック（ここで止まる）されます。

            // ここでコマンドを処理（Eval）
            var request = await evalAsync(line);

            if (request == MuzRequestType.Exit) break;
        }

        //Console.WriteLine("終了したぜ！");
    }


    /// <summary>
    ///     <pre>
    /// キー入力を待機するREPLのループ。
    ///     </pre>
    /// </summary>
    /// <param name="printPromptAsync"></param>
    /// <param name="evalAsync"></param>
    /// <returns></returns>
    internal static async Task RunAsync(
        Func<Task> printPromptAsync,
        Func<ConsoleKeyInfo, Task<MuzRequestType>> evalAsync)
    {
        Console.WriteLine("キー入力待機中...");

        while (true)  // ここが無限ループ（REPLのLoop部分）
        {
            // プロンプト表示
            if (IsPromptVisible)
            {
                await printPromptAsync();
            }

            // `intercept`:  true でエコー（表示）しない。
            ConsoleKeyInfo key = Console.ReadKey(
                intercept: true);

            // ここでコマンドを処理（Eval）
            var request = await evalAsync(key);

            if (request == MuzRequestType.Exit) break;
        }

        //Console.WriteLine("終了したぜ！");
    }
}
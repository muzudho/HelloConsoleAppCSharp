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

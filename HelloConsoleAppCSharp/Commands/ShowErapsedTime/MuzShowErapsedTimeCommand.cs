namespace HelloConsoleAppCSharp.Commands.ShowErapsedTime;

using HelloConsoleAppCSharp.Application;
using HelloConsoleAppCSharp.Core.Infrastructure;
using HelloConsoleAppCSharp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

internal static class MuzShowErapsedTimeCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        var pgSvc = services.GetRequiredService<ApplicationService>();

        // 📍 NOTE:
        //
        //      無限ループの抜け方を説明しておきましょう。
        //
        Console.WriteLine("［エスケープキー］押下で時間経過表示を終了するぜ（＾～＾）...");
        while (true)  // 無限ループ。
        {
            // 色替え
            await MuzConsoleHelper.SetColorAsync(
                fgColor: ConsoleColor.Blue,
                bgColor: ConsoleColor.Cyan,
                onColorChanged: async () =>
                {
                    // アプリケーション起動からの経過時刻を表示するぜ（＾～＾）！
                    await MuzWidgets.PrintErapsedTimeAsync(
                        label: "Time ",
                        launchDateTime: pgSvc.LaunchDateTime,
                        left: 62,
                        top: 0);
                });

            // キー入力がない場合は、少し待ってからループの先頭に戻るぜ（＾～＾）！
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(MuzREPL.KeyInputPollingIntervalMilliseconds);
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
        return MuzREPLRequestType.None;
    }
}

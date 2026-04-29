namespace HelloConsoleAppCSharp.Application.Features.Prints;

using HelloConsoleAppCSharp.Core.Features.Messages;
using HelloConsoleAppCSharp.Core.Infrastructure;
using HelloConsoleAppCSharp.Core.Infrastructure.REPL;
using Microsoft.Extensions.DependencyInjection;
using System;

/// <summary>
/// 色を指定してメッセージ表示。
/// </summary>
internal class MuzPrintMessageWithColorCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services,
        string arguments,
        int argIndex = 1)
    {
        // 半角空白で引数を分割するぜ（＾～＾）
        //
        // 引数は必ず３つ入力されるものとし、
        // トークンの１つ目は、前景色、
        // トークンの２つ目は、背景色、
        // それ以降は、メッセージだぜ（＾～＾）
        var parts = arguments.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

        // 引数が３つ未満のとき
        if (parts.Length < 3)
        {
            // 使い方説明を表示して終了するぜ（＾～＾）
            var errorMessage = ToErrorMessage(services, argIndex);
            Console.WriteLine(errorMessage);
            return MuzREPLRequestType.None;
        }

        ConsoleColor? fgColor = MuzConsoleHelper.GetColorByName(parts[0]);
        ConsoleColor? bgColor = MuzConsoleHelper.GetColorByName(parts[1]);
        string message = parts[2];

        // 続けて、背景色を一時的に黄色に変更
        await MuzConsoleHelper.SetColorAsync(
            fgColor: fgColor ?? Console.ForegroundColor,
            bgColor: bgColor ?? Console.BackgroundColor,
            onColorChanged: async () =>
            {
                Console.WriteLine(message);
            });

        return MuzREPLRequestType.None;
    }


    /// <summary>
    /// エラーメッセージの生成
    /// </summary>
    /// <param name="services"></param>
    /// <param name="argIndex"></param>
    /// <returns></returns>
    internal static string ToErrorMessage(
        IServiceProvider services,
        int argIndex = 1)
    {
        var msgSvc = services.GetRequiredService<MuzMessagesService>();
        var rawMessage = msgSvc.GetMessage("ErrorMsg_1");
        return string.Format(rawMessage, argIndex, argIndex + 1, argIndex + 2);
    }
}

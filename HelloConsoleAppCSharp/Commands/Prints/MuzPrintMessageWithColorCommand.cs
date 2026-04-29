namespace HelloConsoleAppCSharp.Commands.Prints;

using HelloConsoleAppCSharp.Core.Features.Messages;
using HelloConsoleAppCSharp.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// コマンド引数の解析後
/// </summary>
internal record MuzPrintMessageWithColorCommandParameters
{
    public MuzPrintMessageWithColorCommandParameters(
        ConsoleColor? foregroundColor,
        ConsoleColor? backgroundColor,
        string message)
    {
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
        Message = message;
    }

    public ConsoleColor? ForegroundColor { get; init; }
    public ConsoleColor? BackgroundColor { get; init; }
    public string Message { get; init; }
}

/// <summary>
/// 色を指定してメッセージ表示。
/// </summary>
internal class MuzPrintMessageWithColorCommand
{
    /// <summary>
    /// コマンド引数の解析
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static bool TryParseParameters(
        string arguments,
        [NotNullWhen(true)] out MuzPrintMessageWithColorCommandParameters? parameters)
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
            // 終了
            parameters = null;
            return false;
        }

        string message = parts[2];

        parameters = new MuzPrintMessageWithColorCommandParameters(
            foregroundColor: MuzConsoleHelper.GetColorByName(parts[0]),
            backgroundColor: MuzConsoleHelper.GetColorByName(parts[1]),
            message: message);

        return true;
    }

    internal static async Task<MuzREPLRequestType> ExecuteByStringAsync(
        IServiceProvider services,
        string arguments,
        int argIndex = 1)
    {
        // コマンド引数の解析
        if(!TryParseParameters(arguments, out var parameters))
        {
            // 使い方説明を表示して終了するぜ（＾～＾）
            var errorMessage = ToErrorMessage(services, argIndex);
            Console.WriteLine(errorMessage);
            return MuzREPLRequestType.None;
        }

        // 続けて、色を一時的に変更
        await ExecuteAsync(
            foregroundColor: parameters.ForegroundColor,
            backgroundColor: parameters.BackgroundColor,
            message: parameters.Message);


        return MuzREPLRequestType.None;
    }


    internal static async Task ExecuteAsync(
        ConsoleColor? foregroundColor,
        ConsoleColor? backgroundColor,
        string message)
    {
        await MuzConsoleHelper.SetColorAsync(
            fgColor: foregroundColor ?? Console.ForegroundColor,
            bgColor: backgroundColor ?? Console.BackgroundColor,
            onColorChanged: async () =>
            {
                Console.WriteLine(message);
            });
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

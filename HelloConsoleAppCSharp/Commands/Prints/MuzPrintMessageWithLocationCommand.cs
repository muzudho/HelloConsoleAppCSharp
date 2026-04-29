namespace HelloConsoleAppCSharp.Commands.Prints;

using HelloConsoleAppCSharp.Core.Features.Messages;
using HelloConsoleAppCSharp.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// コマンド引数の解析後
/// </summary>
internal record MuzPrintMessageWithLocationCommandParameters
{
    public MuzPrintMessageWithLocationCommandParameters(
        int left,
        int top,
        string restCommand)
    {
        Left = left;
        Top = top;
        RestCommand = restCommand;
    }
    public int Left { get; init; }
    public int Top { get; init; }
    public string RestCommand { get; init; }
}

/// <summary>
/// 位置を指定してメッセージを表示するコマンド
/// </summary>
internal class MuzPrintMessageWithLocationCommand
{
    /// <summary>
    /// コマンド引数の解析
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static bool TryParseParameters(
        string arguments,
        [NotNullWhen(true)] out MuzPrintMessageWithLocationCommandParameters? parameters)
    {
        // 半角空白で引数を分割するぜ（＾～＾）
        //
        // 引数は必ず３つ以上入力されるものとし、
        // トークンの１つ目は、X位置、
        // トークンの２つ目は、Y位置、
        // それ以降は、次のコマンドに渡すぜ（＾～＾）
        var parts = arguments.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

        // 引数が３つ未満のとき
        if (parts.Length < 3) { parameters = null; return false; }

        int left = 0;   // 左端が 0。右に行くほど大きくなるぜ（＾～＾）
        if (parts[0] == "Default") { left = Console.CursorLeft; }
        else if (!int.TryParse(parts[0], out left)) { parameters = null; return false; }

        int top = 0;    // 上端が 0。下に行くほど大きくなるぜ（＾～＾）
        if (parts[1] == "Default") { top = Console.CursorTop; }
        else if (!int.TryParse(parts[1], out top)) { parameters = null; return false; }

        parameters = new MuzPrintMessageWithLocationCommandParameters(left, top, parts[2]);
        return true;
    }

    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services,
        string arguments,
        int argIndex = 1)
    {

        if (!MuzPrintMessageWithLocationCommand.TryParseParameters(arguments, out var parameters))
        {
            // 使い方説明を表示して終了するぜ（＾～＾）
            var errorMessage = string.Join(
                "\n",
                ToErrorMessage(services, argIndex),
                MuzPrintMessageWithColorCommand.ToErrorMessage(services, argIndex + 2));
            Console.WriteLine(errorMessage);
            return MuzREPLRequestType.None;
        }

        // 処理の後、カーソルを元の位置に戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            Console.SetCursorPosition(parameters.Left, parameters.Top);

            // トークンの３つ目以降を次のコマンドに渡すぜ（＾～＾）
            await MuzPrintMessageWithColorCommand.ExecuteByStringAsync(services, parameters.RestCommand, argIndex: argIndex + 2);
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
        var rawMessage = msgSvc.GetMessage("ErrorMsg_2");
        return string.Format(rawMessage, argIndex, argIndex + 1);
    }
}

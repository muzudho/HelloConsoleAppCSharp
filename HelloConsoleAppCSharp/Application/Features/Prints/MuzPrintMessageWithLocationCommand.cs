namespace HelloConsoleAppCSharp.Application.Features.Prints;

using HelloConsoleAppCSharp.Core.Features.Messages;
using HelloConsoleAppCSharp.Core.Infrastructure;
using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 位置を指定してメッセージを表示するコマンド
/// </summary>
internal class MuzPrintMessageWithLocationCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services,
        string arguments,
        int argIndex = 1)
    {
        // 半角空白で引数を分割するぜ（＾～＾）
        //
        // 引数は必ず３つ以上入力されるものとし、
        // トークンの１つ目は、X位置、
        // トークンの２つ目は、Y位置、
        // それ以降は、次のコマンドに渡すぜ（＾～＾）
        var parts = arguments.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

        bool isError = false;
        int left = 0;   // 左端が 0。右に行くほど大きくなるぜ（＾～＾）
        int top = 0;    // 上端が 0。下に行くほど大きくなるぜ（＾～＾）

        // 引数が３つ未満のとき
        if (parts.Length < 3) { isError = true; }
        else
        {
            if (parts[0] == "Default") { left = Console.CursorLeft; }
            else { isError |= !int.TryParse(parts[0], out left); }      // エラーのとき、エラーフラグを立てるぜ（＾～＾）

            if (parts[1] == "Default") { top = Console.CursorTop; }
            else { isError |= !int.TryParse(parts[1], out top); }
        }

        if (isError)
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
        await HelloConsoleAppCSharp.Infrastructure.ConsoleCustom.MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            Console.SetCursorPosition(left, top);

            // トークンの３つ目以降を次のコマンドに渡すぜ（＾～＾）
            await MuzPrintMessageWithColorCommand.ExecuteAsync(services, parts[2], argIndex: argIndex + 2);
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

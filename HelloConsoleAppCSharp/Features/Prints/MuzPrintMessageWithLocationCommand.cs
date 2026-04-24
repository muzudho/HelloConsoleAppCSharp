namespace HelloConsoleAppCSharp.Features.Prints;

using HelloConsoleAppCSharp.Features.Messages;
using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal class MuzPrintMessageWithLocationCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        IServiceProvider services,
        string arguments)
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
            // エラーのとき、エラーフラグを立てるぜ（＾～＾）
            isError |= !int.TryParse(parts[0], out left);
            isError |= !int.TryParse(parts[1], out top);
        }

        if (isError)
        {
            // 使い方説明を表示して終了するぜ（＾～＾）
            Console.WriteLine(MuzMessagesHelper.GetMessage(services, "ErrorMsg_2"));
            return MuzRequestType.None;
        }

        // 処理の後、カーソルを元の位置に戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            Console.SetCursorPosition(left, top);

            // トークンの３つ目以降を次のコマンドに渡すぜ（＾～＾）
            await MuzPrintMessageWithColorCommand.ExecuteAsync(services, parts[2]);
        });

        return MuzRequestType.None;
    }
}

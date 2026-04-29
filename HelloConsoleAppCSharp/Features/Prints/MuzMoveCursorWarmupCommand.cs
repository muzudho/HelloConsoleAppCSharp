namespace HelloConsoleAppCSharp.Features.Prints;

using HelloConsoleAppCSharp.Core.Infrastructure.REPL;
using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

internal class MuzMoveCursorWarmupCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        // 処理の後、カーソルを元の位置に戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // 色を一時的に変更
            await MuzConsoleHelper.SetColorAsync(
                fgColor: ConsoleColor.Black,
                bgColor: ConsoleColor.White,
                onColorChanged: async () =>
                {
                    Console.SetCursorPosition(5, 3);   // カーソルの移動。数字は 0 始まり。
                    Console.WriteLine("カーソルを (6, 4) の位置に移動したぜ（＾▽＾）！");  // 1 から始めた数にすると人間には読みやすいぜ（＾▽＾）
                });

            // 色を一時的に変更
            await MuzConsoleHelper.SetColorAsync(
                fgColor: ConsoleColor.Yellow,
                bgColor: ConsoleColor.Green,
                onColorChanged: async () =>
                {
                    Console.SetCursorPosition(10, 5);
                    Console.Write("カーソルを (11, 6) の位置に移動したぜ（＾▽＾）！");
                });


        });

        return MuzREPLRequestType.None;
    }
}

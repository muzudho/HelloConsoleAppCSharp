namespace HelloConsoleAppCSharp.Commands.Clear;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzClearCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // 処理の後、カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // いったん、背景色を黒にして、画面全体を塗りつぶします。
            await MuzConsoleHelper.SetColorAsync(
                bgColor: ConsoleColor.Black,
                onColorChanged: async () =>
                {
                    Console.Clear();
                });


        });

        return MuzRequestType.None;
    }
}


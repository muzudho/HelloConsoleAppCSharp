namespace HelloConsoleAppCSharp.Commands.Clear;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzClearCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        // いったん、背景色を黒にして、画面全体を塗りつぶします。
        await MuzConsoleHelper.SetColorAsync(
            bgColor: ConsoleColor.Black,
            onColorChanged: async () =>
            {
                Console.Clear();
            });
        // カーソルの位置は、先頭に戻ります。

        return MuzRequestType.None;
    }
}


namespace HelloConsoleAppCSharp.Commands.PrintWarmup;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzShowProgressBarWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        Console.WriteLine("これから、進捗バーの真似事をするぜ（＾～＾）");

        await Task.Delay(TimeSpan.FromSeconds(1));

        // 処理の後、カーソルを元の位置に戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // ［進捗バー］幅１
            await MuzConsoleHelper.SetColorAsync(
                fgColor: ConsoleColor.White,
                bgColor: ConsoleColor.Blue,
                onColorChanged: async () =>
                {
                    Console.Write(" ");
                });

            Console.Write("1");
            Console.Write(" ".PadRight(Console.BufferWidth)); // 残りを空白で消す。カーソルは次の行の先頭へ行く。
        });

        await Task.Delay(TimeSpan.FromSeconds(1));

        // 処理の後、カーソルを元の位置に戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // ［進捗バー］幅２
            await MuzConsoleHelper.SetColorAsync(
                fgColor: ConsoleColor.White,
                bgColor: ConsoleColor.Blue,
                onColorChanged: async () =>
                {
                    Console.Write("  ");
                });

            Console.Write("2");
            Console.Write(" ".PadRight(Console.BufferWidth));
        });

        await Task.Delay(TimeSpan.FromSeconds(1));

        // 処理の後、カーソルを元の位置に戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // ［進捗バー］幅３
            await MuzConsoleHelper.SetColorAsync(
                fgColor: ConsoleColor.White,
                bgColor: ConsoleColor.Blue,
                onColorChanged: async () =>
                {
                    Console.Write("   ");
                });

            Console.Write("3");
            Console.Write(" ".PadRight(Console.BufferWidth));
        });

        // まだ［進捗バー］の行の先頭にカーソルがあるので、改行
        Console.WriteLine();

        return MuzRequestType.None;
    }
}

namespace HelloConsoleAppCSharp.Commands.PrintWarmup;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzPrintWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // 文字色（前景色）を一時的に赤に変更
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Red,
            onColorChanged: async () =>
            {
                Console.WriteLine("これは赤い文字だぜ！");

                // 加えて、背景色を一時的に青に変更
                await MuzConsoleHelper.SetColorAsync(
                    bgColor: ConsoleColor.Blue,
                    onColorChanged: async () =>
                    {
                        Console.WriteLine("赤文字に青背景！");
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    });
            });

        Console.WriteLine("ここはデフォルトの色に戻ったよ");
        Console.WriteLine("３秒待つ（＾～＾）");
        await Task.Delay(TimeSpan.FromSeconds(3));

        // 色を一時的に変更
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.White,
            onColorChanged: async () =>
            {
                // 処理の後、カーソルを元の位置に戻す
                await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
                {
                    Console.SetCursorPosition(0, 0);   // コンソールの左上隅に移動
                    Console.WriteLine("コンソールの左上隅に移動（＾～＾）！");

                    Console.SetCursorPosition(10, 5);  // 11列目、6行目に移動（0始まりなので）
                    Console.Write("ここに文字を書くぜ！");
                });
            });

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


        /*
           📍 NOTE:

                全部で16色あるよ：

                Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray
                DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White

                Console.Clear(); を呼ぶと、ウィンドウ全体の背景色も変わる（現在のBackgroundColorが適用される）。
                ANSIエスケープシーケンス を使えば、真のRGBカラー（24bit）や下線・太字なども使えるようになる。
        */
    }
}

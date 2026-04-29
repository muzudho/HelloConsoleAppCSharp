namespace HelloConsoleAppCSharp.Commands.PrintWarmup;

using HelloConsoleAppCSharp.Core.Infrastructure.REPL;
using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using System.Text;

internal static class MuzShowProgressBarWarmupCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        Console.WriteLine("これから、進捗バーの真似事をするぜ（＾～＾）");

        // コンソール画面の横幅を取得（＾～＾）
        int consoleWidth = Console.WindowWidth;

        // "100%" の文字数（＾～＾） まあ、 4 文字だよね（＾～＾）
        int labelLength = "100%".Length;

        // プログレスバーの長さは、［コンソール画面の横幅］－［"100%" の文字数］とする（＾～＾）
        int progressBarLength = consoleWidth - labelLength;

        for (int i = 0; i < progressBarLength; i++)
        {
            // ちょっと待つ
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            // 処理の後、カーソルを元の位置に戻す
            await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
            {
                // ［進捗バー］幅１
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.White,
                    bgColor: ConsoleColor.Blue,
                    onColorChanged: async () =>
                    {
                        var sb = new StringBuilder();
                        for (int j = 0; j < i + 1; j++)
                        {
                            sb.Append(" ");
                        }
                        Console.Write(sb.ToString());
                    });

                Console.SetCursorPosition(progressBarLength, Console.CursorTop); // 画面の右端に "nnn%" を表示させるぜ（＾▽＾）
                Console.Write($"{(i + 1) * 100 / progressBarLength,3}%");
                //Console.Write(" ".PadRight(Console.BufferWidth)); // 残りを空白で消す。カーソルは次の行の先頭へ行く。
            });
        }

        // まだ［進捗バー］の行の先頭にカーソルがあるので、改行
        Console.WriteLine();

        Console.WriteLine("完了～（＾▽＾）");

        return MuzREPLRequestType.None;
    }
}

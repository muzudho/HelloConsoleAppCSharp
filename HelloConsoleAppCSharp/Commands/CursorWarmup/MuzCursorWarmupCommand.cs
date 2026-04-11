namespace HelloConsoleAppCSharp.Commands.CursorWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using static HelloConsoleAppCSharp.Infrastructure.REPL.MuzREPL;

internal static class MuzCursorWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // 📍 NOTE:
        //
        //      無限ループの抜け方を説明しておきましょう。
        //
        Console.WriteLine("キー入力待機中。［エンターキー］押下でループを抜けるぜ（＾～＾）...");

        while (true)  // 無限ループ。
        {
            // 📍 NOTE:
            //
            //      キー入力を受け取ります。
            //      プログラムは、ユーザーがキーを押すまで、ここで待機します。  
            //      `intercept`:  true でエコー（表示）しない。
            //
            ConsoleKeyInfo key = Console.ReadKey(
                intercept: true);

            // 📍 NOTE:
            //
            //      ここにお前のキー入力処理を書く。
            //
        }

        return MuzREPL.MuzRequestType.None;
    }
}

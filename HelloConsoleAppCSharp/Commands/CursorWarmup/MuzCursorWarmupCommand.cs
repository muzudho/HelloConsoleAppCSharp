namespace HelloConsoleAppCSharp.Commands.CursorWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;
using static HelloConsoleAppCSharp.Infrastructure.REPL.MuzREPL;

internal static class MuzCursorWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // 📍 NOTE:
        //
        //      無限ループの抜け方を説明しておきましょう。
        //
        Console.WriteLine("キー入力待機中。［エンターキー］か、［エスケープキー］押下でループを抜けるぜ（＾～＾）...");

        // カーソルの現在位置
        int cursorIndex = 0;
        // カーソルの停止Ｙ位置のリスト
        int[] stopYList = [16, 18, 20];

        while (true)  // 無限ループ。
        {
            // 📍 NOTE:
            //
            //      一定間隔で点滅するカーソル（ブリンカー）を表示するぜ（＾～＾）！
            //
            MuzWidgets.PrintBlinkingText(
                text: "▶",  // 右向きの三角形は、半角のようだ。
                left: 36,
                top: stopYList[cursorIndex],
                fgColor: ConsoleColor.Blue,
                bgColor: ConsoleColor.Cyan,
                isVisible: (DateTime.Now.Millisecond / 500) % 2 == 0); // 0.5秒ごとに点滅

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

            // ［エンターキー］が押されたら、そこまで入力された文字列を返します。
            if (key.Key == ConsoleKey.Enter || key.KeyChar == '\r' || key.KeyChar == '\n')
            {
                Console.WriteLine($"［エンターキー］が入力されたぜ（＾～＾）！");
                break;  // ループを抜ける
            }

            // ［エスケープキー］が押されたら、ループを抜けます。
            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine($"［エスケープキー］が入力されたぜ（＾～＾）！");
                break;  // ループを抜ける
            }

            if (key.Key == ConsoleKey.UpArrow)
            {
                Console.WriteLine($"［↑］キーを押したぜ（＾～＾）");
                cursorIndex--;
                if (cursorIndex < 0)
                {
                    cursorIndex = stopYList.Length - 1;
                }
                continue;
            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                Console.WriteLine($"［↓］キーを押したぜ（＾～＾）");
                cursorIndex++;
                if (cursorIndex >= stopYList.Length)
                {
                    cursorIndex = 0;
                }
                continue;
            }

            // その他のキー入力は無視するぜ（＾～＾）！
        }

        return MuzREPL.MuzRequestType.None;
    }
}

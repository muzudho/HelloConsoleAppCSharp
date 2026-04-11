namespace HelloConsoleAppCSharp.Commands.CursorWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;

internal static class MuzCursorWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // 📍 NOTE:
        //
        //      無限ループの抜け方を説明しておきましょう。
        //
        Console.WriteLine("キー入力待機中。［エンターキー］か、［エスケープキー］押下でループを抜けるぜ（＾～＾）...");

        int prevIndex = 0;   // カーソルの前回位置
        int currIndex = 0;    // カーソルの現在位置
        // カーソルの停止Ｙ位置のリスト
        int[] stopYList = [16, 18, 20];

        while (true)  // 無限ループ
        {
            // 📍 NOTE:
            //
            //      一定間隔で点滅するカーソル（ブリンカー）を表示するぜ（＾～＾）！
            //
            MuzWidgets.PrintBlinkingText(
                text: " ",  // ホワイトスペース
                left: 36,
                top: stopYList[prevIndex],
                fgColor: ConsoleColor.Blue,
                bgColor: ConsoleColor.Cyan,
                isVisible: false);  // 常にホワイトスペースを表示
            MuzWidgets.PrintBlinkingText(
                text: "▶",  // 右向きの三角形は、半角のようだ。
                left: 36,
                top: stopYList[currIndex],
                fgColor: ConsoleColor.Blue,
                bgColor: ConsoleColor.Cyan,
                isVisible: (DateTime.Now.Millisecond / 500) % 2 == 0); // 0.5秒ごとに点滅

            // キー入力がない場合は、少し待ってからループの先頭に戻るぜ（＾～＾）！
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(16));    // およそ１／６０秒で画面更新（＾～＾）
                continue;
            }

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
                prevIndex = currIndex;
                currIndex--;
                if (currIndex < 0)
                {
                    currIndex = stopYList.Length - 1;
                }
                continue;
            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                Console.WriteLine($"［↓］キーを押したぜ（＾～＾）");
                prevIndex = currIndex;
                currIndex++;
                if (currIndex >= stopYList.Length)
                {
                    currIndex = 0;
                }
                continue;
            }

            // その他のキー入力は無視するぜ（＾～＾）！
        }

        return MuzRequestType.None;
    }
}

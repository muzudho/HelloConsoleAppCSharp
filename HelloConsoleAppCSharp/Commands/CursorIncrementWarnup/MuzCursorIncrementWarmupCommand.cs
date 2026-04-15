namespace HelloConsoleAppCSharp.Commands.CursorIncrementWarmup;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzCursorIncrementWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // 色替え
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Blue,
            bgColor: ConsoleColor.Cyan,
            onColorChanged: async () =>
            {
                // 📍 NOTE:
                //
                //      無限ループの抜け方を説明しておきましょう。
                //
                Console.WriteLine("キー入力待機中。［エスケープキー］押下でループを抜けるぜ（＾～＾）...");

                int previousIndex = 0;   // カーソルの前回位置
                int selectedIndex = 0;    // カーソルの現在位置
                                          // カーソルの停止Ｙ位置のリスト
                int[] stopYList = [16, 18, 20];

                while (true)  // 無限ループ
                {
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

                    // ［エスケープキー］が押されたら、ループを抜けます。
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine($"［エスケープキー］が入力されたぜ（＾～＾）！");
                        break;  // ループを抜ける
                    }

                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        previousIndex = selectedIndex;
                        selectedIndex--;
                        if (selectedIndex < 0)
                        {
                            selectedIndex = stopYList.Length - 1;
                        }
                        Console.WriteLine($"［↑］キーを押したぜ（＾～＾）数は {selectedIndex} になった（＾～＾）");
                        continue;
                    }

                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        previousIndex = selectedIndex;
                        selectedIndex++;
                        if (selectedIndex >= stopYList.Length)
                        {
                            selectedIndex = 0;
                        }
                        Console.WriteLine($"［↓］キーを押したぜ（＾～＾）数は {selectedIndex} になった（＾～＾）");
                        continue;
                    }

                    // その他のキー入力は無視するぜ（＾～＾）！
                }
            });

        return MuzRequestType.None;
    }
}

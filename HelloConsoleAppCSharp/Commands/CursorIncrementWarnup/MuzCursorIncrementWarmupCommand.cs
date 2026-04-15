namespace HelloConsoleAppCSharp.Commands.CursorIncrementWarmup;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.Models;
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

                // カーソルの位置を管理するモデル
                var listCursor = new MuzListCursorModel(
                    size: 3);   // リストの項目数

                while (true)  // 無限ループ
                {
                    // キー入力がない場合は、少し待ってからループの先頭に戻るぜ（＾～＾）！
                    if (!Console.KeyAvailable)
                    {
                        Thread.Sleep(MuzREPL.KeyInputPollingIntervalMilliseconds);
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
                        listCursor.Add(-1);
                        Console.WriteLine($"［↑］キーを押したぜ（＾～＾）数は {listCursor.SelectedIndex} になった（＾～＾）");
                        continue;
                    }

                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        listCursor.Add(1);
                        Console.WriteLine($"［↓］キーを押したぜ（＾～＾）数は {listCursor.SelectedIndex} になった（＾～＾）");
                        continue;
                    }

                    // その他のキー入力は無視するぜ（＾～＾）！
                }
            });

        return MuzRequestType.None;
    }
}

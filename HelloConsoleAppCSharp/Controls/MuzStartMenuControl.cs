namespace HelloConsoleAppCSharp.Controls;

using HelloConsoleAppCSharp.Core.Infrastructure.REPL;
using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.Models;
using HelloConsoleAppCSharp.Views;

internal class MuzStartMenuControl
{
    internal static async Task<int> ShowMenuAsync()
    {
        // 📍 NOTE:
        //
        //      無限ループの抜け方を説明しておきましょう。
        //
        Console.WriteLine("キー入力待機中。［エンターキー］か、［エスケープキー］押下でループを抜けるぜ（＾～＾）...");

        // カーソルの位置を管理するモデル
        var listCursor = new MuzListCursorModel(
            size: 3);   // リストの項目数


        while (true)  // 無限ループ
        {
            // 📍 NOTE:
            //
            //      一定間隔で点滅（ブリンク）するラベルを表示するぜ（＾～＾）！
            //
            await MuzConsoleHelper.BlinkAsync(
                fgColor1: ConsoleColor.Black,
                bgColor1: ConsoleColor.Cyan,
                fgColor2: ConsoleColor.White,   // ２番目の色
                bgColor2: ConsoleColor.Blue,
                isColor2: listCursor.SelectedIndex == 0 && (DateTime.Now.Millisecond / 500) % 2 == 0, // 選択されていれば、0.5秒ごとに［２番目の色］に切替
                onColorChanged: async () =>
                {
                    await MuzLabelViews.PrintAsync(
                        left: 38,
                        top: 16,
                        text: "開始");
                });
            await MuzConsoleHelper.BlinkAsync(
                fgColor1: ConsoleColor.Black,
                bgColor1: ConsoleColor.Cyan,
                fgColor2: ConsoleColor.White,   // ２番目の色
                bgColor2: ConsoleColor.Blue,
                isColor2: listCursor.SelectedIndex == 1 && (DateTime.Now.Millisecond / 500) % 2 == 0,
                onColorChanged: async () =>
                {
                    await MuzLabelViews.PrintAsync(
                        left: 38,
                        top: 17,
                        text: "設定");
                });
            await MuzConsoleHelper.BlinkAsync(
                fgColor1: ConsoleColor.Black,
                bgColor1: ConsoleColor.Cyan,
                fgColor2: ConsoleColor.White,   // ２番目の色
                bgColor2: ConsoleColor.Blue,
                isColor2: listCursor.SelectedIndex == 2 && (DateTime.Now.Millisecond / 500) % 2 == 0,
                onColorChanged: async () =>
                {
                    await MuzLabelViews.PrintAsync(
                        left: 38,
                        top: 18,
                        text: "終了");
                });

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

            // ［エンターキー］が押されたら、メニューを抜けて、選択された項目のインデックスを返します。
            if (key.Key == ConsoleKey.Enter || key.KeyChar == '\r' || key.KeyChar == '\n') return listCursor.SelectedIndex;

            // ［エスケープキー］が押されたら、メニューを抜けて、 -1 を返します。
            if (key.Key == ConsoleKey.Escape) return -1;

            if (key.Key == ConsoleKey.UpArrow)
            {
                listCursor.Add(-1);  // カーソル位置を上に移動
                continue;
            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                listCursor.Add(1);   // カーソル位置を下に移動
                continue;
            }

            // その他のキー入力は無視するぜ（＾～＾）！
        }
    }
}

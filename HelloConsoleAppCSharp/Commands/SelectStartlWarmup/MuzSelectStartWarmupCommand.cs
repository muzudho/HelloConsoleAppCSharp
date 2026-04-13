namespace HelloConsoleAppCSharp.Commands.SelectStartlWarmup;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;

internal class MuzSelectStartWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // 📍 NOTE:
        //
        //      無限ループの抜け方を説明しておきましょう。
        //
        Console.WriteLine("［エスケープキー］押下で点滅を終了するぜ（＾～＾）...");
        while (true)  // 無限ループ。
        {
            // ラベルの表示
            await MuzConsoleHelper.BlinkAsync(
                fgColor1: ConsoleColor.Black,
                bgColor1: ConsoleColor.Cyan,
                fgColor2: ConsoleColor.White,   // ２番目の色
                bgColor2: ConsoleColor.Blue,
                isColor2: (DateTime.Now.Millisecond / 500) % 2 == 0, // 0.5秒ごとに色切替
                onColorChanged: async () =>
                {
                    await MuzLabelViews.PrintAsync(
                        left: 38,
                        top: 16,
                        text: "開始");
                });

            // キー入力がない場合は、少し待ってからループの先頭に戻るぜ（＾～＾）！
            if (!Console.KeyAvailable)
            {
                // およそ１／６０秒後にループの先頭に戻るぜ（＾～＾）
                Thread.Sleep(TimeSpan.FromMilliseconds(16));
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

            // ［エスケープキー］が押されたら、ループを抜けます。
            if (key.Key == ConsoleKey.Escape)
            {
                break;  // ループを抜ける
            }
        }
        return MuzRequestType.None;
    }
}

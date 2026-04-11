namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// コンソール画面を、１画面に見立てて、枠組みを表示するためのものです。
/// </summary>
internal static class MuzPageLayouts
{
    public static async Task PrintTitlePageAsync()
    {
        // カーソルの現在位置を記憶。
        var oldLeft = Console.CursorLeft;
        var oldTop = Console.CursorTop;

        // いったん、背景色を黒にして、画面全体を塗りつぶします。
        await MuzConsoleHelper.SetColorAsync(
            bgColor: ConsoleColor.Black,
            onColorChanged: async () =>
            {
                Console.Clear();
            });

        int pageWidth = 80;
        int pageHeight = 25;
        // 次に、シアン色の背景色で、使用する［固定サイズ］の免責を塗りつぶします。［可変］サイズは難しいので、ここでは扱いません。
        await MuzConsoleHelper.SetColorAsync(
            bgColor: ConsoleColor.Cyan,
            onColorChanged: async () =>
            {
                Console.SetCursorPosition(0, 0);
                for (int y = 0; y < pageHeight; y++)
                {
                    for (int x = 0; x < pageWidth; x++)
                    {
                        Console.Write(' '); // 全体を決め打ちでもいいが、とりあえず１文字ずつプリントする。
                    }
                    Console.WriteLine();    // 改行
                }
            });

        // 画面の真ん中辺りにタイトルを表示するとかっこいい。
        var title = "Hello Console App C#";
        var titleLeft = (pageWidth - title.Length) / 2;  // 漢字は横幅計算が難しいので、今回は半角英字だけのタイトルにします。
        var titleTop = pageHeight / 2;
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan,
            onColorChanged: async () =>
            {
                Console.SetCursorPosition(titleLeft, titleTop);
                Console.Write(title);
            });

        // 画面の下辺辺りに、制作年、開発者を表示するとかっこいい。
        var credit = "(C) 2026 by Muzudho ; MIT License";
        var creditLeft = (pageWidth - credit.Length) / 2;
        var creditTop = pageHeight - 1;
        Console.SetCursorPosition(creditLeft, creditTop);
        Console.Write(credit);

        // カーソルの位置を戻す（大事！）
        Console.SetCursorPosition(oldLeft, oldTop);
    }
}

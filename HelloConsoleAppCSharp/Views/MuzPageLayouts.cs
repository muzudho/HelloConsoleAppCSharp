namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// コンソール画面を、１画面に見立てて、枠組みを表示するためのものです。
/// </summary>
internal static class MuzPageLayouts
{
    public static async Task PrintTitlePageAsync()
    {
        int wallLeft = 0;
        int wallTop = 0;
        int wallWidth = 80;
        int wallHeight = 25;
        ConsoleColor wallColor = ConsoleColor.Cyan;

        // 壁面を塗りつぶす。
        await MuzWallViews.PrintAsync(
            wallLeft: wallLeft,
            wallTop: wallTop,
            wallWidth: wallWidth,
            wallHeight: wallHeight,
            wallColor: wallColor);

        // 処理の後、カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
            {
                // 色替え
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Black,
                    bgColor: ConsoleColor.Cyan,
                    onColorChanged: async () =>
                    {
                        // ［タイトル］
                        //      - 画面の真ん中辺りに表示するとかっこいい。
                        var title = "Hello Console App C#";
                        var titleLeft = (wallWidth - title.Length) / 2;  // 漢字は横幅計算が難しいので、今回は半角英字だけのタイトルにします。
                        var titleTop = wallHeight / 2;
                        Console.SetCursorPosition(titleLeft, titleTop);
                        Console.Write(title);

                        // ［クレジット］
                        //      - 画面の下辺辺りに、制作年、開発者を表示するとかっこいい。
                        var credit = "(C) 2026 by Muzudho ; MIT License";
                        var creditLeft = (wallWidth - credit.Length) / 2;
                        var creditTop = wallHeight - 1;
                        Console.SetCursorPosition(creditLeft, creditTop);
                        Console.Write(credit);
                    });
            });
    }
}

namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Core.Infrastructure;

/// <summary>
/// 壁面
/// </summary>
internal static class MuzWallViews
{
    public static async Task PrintAsync(
        int wallWidth,
        int wallHeight,
        ConsoleColor wallColor,
        int wallLeft = 0,
        int wallTop = 0)
    {
        // 処理の後、カーソルの位置を戻す
        await MuzConsole.PreserveCursorPositionAsync(async () =>
        {
            // いったん、背景色を黒にして、画面全体を塗りつぶします。
            await MuzConsole.RunWithColorAsync(
                bgColor: ConsoleColor.Black,
                onColorChanged: async () =>
                {
                    Console.Clear();
                });


            // 次に、壁面の色で、使用する［固定サイズ］の面積を塗りつぶします。
            await MuzConsole.RunWithColorAsync(
                bgColor: wallColor,
                onColorChanged: async () =>
                {
                    await MuzConsole.PrintBoxAsync(
                        left: wallLeft,
                        top: wallTop,
                        width: wallWidth,
                        height: wallHeight);
                });


        });


    }
}

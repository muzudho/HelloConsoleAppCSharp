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
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // いったん、背景色を黒にして、画面全体を塗りつぶします。
            await MuzConsoleHelper.SetColorAsync(
                bgColor: ConsoleColor.Black,
                onColorChanged: async () =>
                {
                    Console.Clear();
                });


            // 次に、壁面の色で、使用する［固定サイズ］の面積を塗りつぶします。
            await MuzConsoleHelper.SetColorAsync(
                bgColor: wallColor,
                onColorChanged: async () =>
                {
                    await MuzBoxViews.PrintAsync(
                        left: wallLeft,
                        top: wallTop,
                        width: wallWidth,
                        height: wallHeight);
                });


        });


    }
}

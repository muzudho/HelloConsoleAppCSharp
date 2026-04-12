namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// 壁面
/// </summary>
internal static class MuzWallViews
{
    public static async Task PrintWallAsync(
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


            // 次に、壁面の色で、使用する［固定サイズ］の免責を塗りつぶします。
            await MuzConsoleHelper.SetColorAsync(
                bgColor: wallColor,
                onColorChanged: async () =>
                {
                    for (int dy = 0; dy < wallHeight; dy++)
                    {
                        Console.SetCursorPosition(wallLeft, wallTop + dy);

                        for (int dx = 0; dx < wallWidth; dx++)
                        {
                            Console.Write(' '); // 全体を決め打ちでもいいが、とりあえず１文字ずつプリントする。
                        }
                        Console.WriteLine();    // 改行
                    }
                });


        });


    }
}

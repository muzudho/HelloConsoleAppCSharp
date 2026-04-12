namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

internal class MuzFloatingBoxViews
{
    public static async Task PrintAsync(
         int left,
         int top,
         int width,
         int height,
         ConsoleColor bgColor)
    {
        // 処理の後、カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // 次に、［固定サイズ］の面積を塗りつぶします。
            await MuzConsoleHelper.SetColorAsync(
                bgColor: bgColor,
                onColorChanged: async () =>
                {
                    for (int dy = 0; dy < height; dy++)
                    {
                        Console.SetCursorPosition(left, top + dy);

                        for (int dx = 0; dx < width; dx++)
                        {
                            Console.Write(' '); // ホワイトスペース
                        }
                        Console.WriteLine();    // 改行
                    }
                });


        });
    }
}

namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// ウィンドウの枠のようなものを表示するためのものです。
/// </summary>
internal static class MuzBoxViews
{
    /// <summary>
    ///     <pre>
    /// 下図のような感じの罫線を表示します。
    /// 
    ///     ╔══════╗
    ///     ║      ║
    ///     ║      ║
    ///     ╚══════╝
    ///     </pre>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="top"></param>
    /// <param name="width">2 以上としてください。</param>
    /// <param name="height">2 以上としてください。</param>
    public static async Task PrintDoubleBorderBoxAsync(
        int left,
        int top,
        int width,
        int height)
    {
        // 処理の後、カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // ダブル・ボーダーを表示するための文字
            char topLeft = '╔';
            char topRight = '╗';
            char bottomLeft = '╚';
            char bottomRight = '╝';
            char horizontal = '═';
            char vertical = '║';

            // 左上隅
            Console.SetCursorPosition(left, top);
            Console.Write(topLeft);

            // 上辺
            for (int dx = 0; dx < width - 2; dx++)
            {
                Console.Write(horizontal);
            }

            // 右上隅
            Console.Write(topRight);
            Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行

            // 上辺下辺を除いた中段行
            for (int dy = 0; dy < height - 2; dy++)
            {
                // 左辺
                Console.Write(vertical);

                // 中身（空白）
                for (int dx = 0; dx < width - 2; dx++)
                {
                    Console.Write(' ');
                }

                // 右辺
                Console.Write(vertical);
                Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行
            }

            // 左下隅
            Console.Write(bottomLeft);

            // 下辺
            for (int dx = 0; dx < width - 2; dx++)
            {
                Console.Write(horizontal);
            }

            // 右下隅
            Console.Write(bottomRight);
            Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行
        });
    }
}

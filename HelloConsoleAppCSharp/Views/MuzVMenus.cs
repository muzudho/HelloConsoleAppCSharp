namespace HelloConsoleAppCSharp.Views;

/// <summary>
/// 垂直方向の箇条書きを表示します。
/// </summary>
internal static class MuzVMenus
{
    public static void PrintMenu(
        int left,
        int top,
        string[] items,
        ConsoleColor fgColor,
        ConsoleColor bgColor)
    {
        // カーソルの現在位置を記憶。
        var oldLeft = Console.CursorLeft;
        var oldTop = Console.CursorTop;

        // 色を設定
        Console.ForegroundColor = fgColor;
        Console.BackgroundColor = bgColor;

        // メニュー項目を表示
        for (int dy = 0; dy < items.Length; dy++)
        {
            Console.SetCursorPosition(left, top + dy);
            Console.Write(items[dy]);
        }

        // 色とカーソルの位置を戻す（大事！）
        Console.ResetColor();
        Console.SetCursorPosition(oldLeft, oldTop);
    }
}
